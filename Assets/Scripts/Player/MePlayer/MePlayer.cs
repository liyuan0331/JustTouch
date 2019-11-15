using UnityEngine;
using System.Collections;

public class MePlayer : Player,IElementPlayer {

	protected MoveAcc maX;
	protected MoveAcc maY;

	void IElementPlayer.GetHurt(){
//		print("hurt!");
		//每次受伤使自己资产减少0.9
		float outMoney = Mathf.Max (natureMoney + borrowMoney, .9f);//考虑一下减少资产是否可以使自己变为负数(目前设置的不能为负数)
		MoneySystem.Instance.PlayerWorkEarnMoney (this, -outMoney);
	}

	protected override void Awake ()
	{
		base.Awake ();
		maxSpeed = 1.9f;
		maX = new MoveAcc (maxSpeed);
		maY=new MoveAcc (maxSpeed);
	}

	//玩家死亡(GameOver)
	public override void Die ()
	{
		base.Die ();
		Invoke ("ShowGameOver", .6f);
	}

	//玩家破产(GameOver)
	protected override void GoBankrupt ()
	{
		base.GoBankrupt ();
		Invoke ("ShowGameOver", .6f);
//		ShowGameOver();
	}

	void ShowGameOver(){
		GameUI.Instance.GameOver ();
	}

	protected override void FixedUpdate ()
	{
		float h= Input.GetAxisRaw ("Horizontal");
		float v=Input.GetAxisRaw ("Vertical");
		Vector2 movement = new Vector2 (h, v);
		movement.Normalize ();

		Vector2 moveDir=new Vector2();
		moveDir.x = maX.UpdateMovement2Dis (movement.x, Time.fixedDeltaTime);//Time.deltaTime);
		moveDir.y = maY.UpdateMovement2Dis (movement.y, Time.fixedDeltaTime);//Time.deltaTime);

		//		Vector2 dir = playerAcc.UpdateMovement2Dir (movement, Time.deltaTime);

		PlayerMoveDir (moveDir);

		base.FixedUpdate ();
	}

	// Update is called once per frame
	protected override void Update ()
	{
		if (isDead)
			return;

		//玩家按键工作赚钱
		if (Input.GetKeyDown (KeyCode.K)) {
			workForMoneyRate = .2f;
		} else if (Input.GetKeyUp (KeyCode.K)) {
			workForMoneyRate = 0;
		} else if (Input.GetKey (KeyCode.K)) {

		}
		else if (Input.GetKeyDown (KeyCode.J)) {
			//if (!this.GetComponentInChildren<YaElement> ().Electrified) {//带电不能变大
				MoneySystem.Instance.PlayerWorkEarnMoney (this, .06f);
			//}
//			MonoLogue.Instance.ShowThenHideMonlogue ("独白测试");
		}

//		if (workForMoneyRate > 0) {
//			MoneySystem.Instance.PlayerWorkEarnMoney (this, workForMoneyRate * Time.deltaTime);
//		}

		base.Update ();
	}

	//惯性系统
	protected class MoveAcc{
		enum State{NoAcc,Accing,}

		State myState;
		float moveAcc;
		float StopAcc;
		float maxSpeed;

		float curSpeed;
		float no0move;

		public MoveAcc(float _maxSpeed=2f,float _moveAcc=10f,float _stopAcc=7f){
			this.maxSpeed=_maxSpeed;
			this.moveAcc=_moveAcc;
			this.StopAcc=_stopAcc;
			myState=State.NoAcc;
		}

		public float UpdateMovement2Dis(float _move,float _deltaTime){
			if (_move != 0) {
				no0move = _move;
			}
			if (no0move == 0) {
				return 0;
			}

			float tempAcc=0;
//			float dSpeed;

			switch (myState) {
			case State.NoAcc:
				if (_move != 0) {
					myState = State.Accing;
				}
				tempAcc = no0move > 0 ? -StopAcc : StopAcc;

				break;
			case State.Accing:
				if (_move == 0) {
					myState = State.NoAcc;
				}
				tempAcc = no0move > 0 ? moveAcc : -moveAcc;

				break;
			}

			float dSpeed = tempAcc * _deltaTime;
			curSpeed += dSpeed;
			if (curSpeed * no0move < 0) {
				curSpeed = 0;
			}

			if (curSpeed > 0) {
				curSpeed = Mathf.Clamp (curSpeed, 0, maxSpeed);
			} else if(curSpeed<0) {
				curSpeed = Mathf.Clamp (curSpeed, -maxSpeed, 0);
			}

			float resultDis = curSpeed;

			return resultDis;

//			_movement.Normalize ();
//			if (_movement != Vector2.zero) {
//				this.movement = _movement.normalized;
//			}
//
//			Vector2 dV = Vector2.zero;
//
//			switch (myState) {
//			case State.NoAcc:
//				if (_movement != Vector2.zero) {
//					myState = State.Accing;
//				}
//					
//				dV.x = -Mathf.Abs (StopAcc * _deltaTime * movement.x);
//				dV.x = curV.x < 0 ? -dV.x : dV.x;
//					
//				dV.y = -Mathf.Abs (StopAcc * _deltaTime * movement.y);
//				dV.y = curV.y < 0 ? -dV.y : dV.y;
//
////				dV = StopAcc * _deltaTime*movement;
//
//				break;
//
//			case State.Accing:
//				if (_movement == Vector2.zero) {
//					myState = State.NoAcc;
//				}
//
//				dV.x = Mathf.Abs (moveAcc * _deltaTime * movement.x);
//				dV.x = curV.x < 0 ? -dV.x : dV.x;
//
//				dV.y = Mathf.Abs (moveAcc * _deltaTime * movement.y);
//				dV.y = curV.y < 0 ? -dV.y : dV.y;
//
//				break;
//			}
//
//			print (dV);
//
//			Vector2 tempCurV = curV;
//
//			curV.x += dV.x;
//			if (tempCurV.x * curV.x < 0) {
//				curV.x = 0;
//			}
//			curV.y += dV.y;
//			if (tempCurV.y * curV.y < 0) {
//				curV.y = 0;
//			}
//
//
////			curV += dV;
//
////			curSpeed = Mathf.Clamp (curSpeed, 0, maxSpeed);
//
////			curV = Vector2.ClampMagnitude (curV, maxSpeed);
//
//
//			Vector2 resultDir = new Vector2 (_deltaTime * curV.x, _deltaTime * curV.y);
//			return resultDir;
		}
	}
}

