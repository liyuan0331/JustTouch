using UnityEngine;
using System.Collections;

public class MoneySystem : YaSingleMono<MoneySystem> {//整个结构有待商榷
	//下面这几个数值的获取方式在不太增加混乱度的情况下可以改为用事件注册的方式来获得，可以减少每帧（或者隔几帧）遍历的资源消耗量
	//为了防止以后可能出现立即销毁或产生新的资金持有者，所以总金额采用每帧遍历（或者隔几帧遍历一次）的方法来获取
	public float allMoney;
	public float allLent;
	//获得所有的资金持有者，也同上，采用每帧遍历（或者隔几帧遍历一次）的方法来获取
	Player[] allplayers;

	void Start(){
		CheckAllPlayersAndMoney ();
	}

	void Update(){
		//每隔几帧来重新遍历获得一下player和money的新情况
		if (Time.frameCount % 5 == 0) {
			CheckAllPlayersAndMoney ();
		}


	}

	public void CheckAllPlayersAndMoney(){
		CheckAllPlayer ();
		float _allMoney = 0;
		float _allLent = 0;
		foreach (Player player in allplayers) {
			_allMoney += player.natureMoney;
			_allMoney += player.borrowMoney;
			_allLent += player.lentMoney;
		}
		allMoney = _allMoney;
		allLent = _allLent;
	}

	public void CheckAllPlayer(){
		allplayers = Object.FindObjectsOfType<Player> ();
	}

	float GetAllMoney(){
		float _allMoney = 0;
		foreach (Player player in allplayers) {
			_allMoney += player.natureMoney;
			_allMoney += player.borrowMoney;
		}
		return _allMoney;
	}

	//金钱的增加与扣除，一个player工作赚钱后其它player平均减钱，所以进行宏观调整
	//为了防止细小的增损，每次金钱交易之后宏观金钱总量微调一次
//	delegate void ChangeMoneyHandler(Player _player,float _dMoney);
//	public event ChangeMoneyHandler ChangeMoney;
	public void PlayerWorkEarnMoney(Player _player,float _dMoney){
		float realEarnMoney = 0;//所有从别的player扣除的资产存放在这里，最后转移到获取金钱的玩家
		float everyMoney = _dMoney / (allplayers.Length-1);
		foreach (Player player in allplayers) {
			if (player == _player)
				continue;
			realEarnMoney += player.ReduceMoney (everyMoney);
		}

		_player.IncreaseMoney (realEarnMoney);

		//误差微调(保证交易前后allMoney的值不变)
		float otherAllMoney = GetAllMoney () - (_player.natureMoney + _player.borrowMoney);
		float deviation = 0;
		while ((deviation = allMoney - otherAllMoney-(_player.natureMoney + _player.borrowMoney)) != 0) {
			_player.IncreaseMoney (deviation);
		}
	}
}
