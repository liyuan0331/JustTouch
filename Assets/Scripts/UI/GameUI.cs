using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : YaSingleMono<GameUI> {
	public Text textAllMoney;
	public Text textPlayerMoney;

	MePlayer mePlayer;

	public GameObject gameOver;
	public GameObject gameWin;

	// Use this for initialization
	void Start () {
		mePlayer = Object.FindObjectOfType<MePlayer> ();
	}
	
	// Update is called once per frame
	void Update () {
		textAllMoney.text = "总金额: " + MoneySystem.Instance.allMoney.ToString("f2");
		if (mePlayer == null || mePlayer.isActiveAndEnabled == false) {
			textPlayerMoney.text = "玩家额度: 无";
		} else {
			textPlayerMoney.text = "玩家额度: " + (mePlayer.lentMoney + mePlayer.borrowMoney + mePlayer.natureMoney).ToString("f2");
		}
	}

	public void GameOver(){


		gameWin.SetActive (false);
		gameOver.SetActive (true);
	}

	public void GameWin(){
		
		gameOver.SetActive (false);
		gameWin.SetActive (true);
	}

	public void Restart(){
//		Application.LoadLevel (Application.loadedLevel);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
