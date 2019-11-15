using UnityEngine;
using System.Collections;

public class OtherPlayer : Player {
	public bool isEarnMoneyPerT=true;

	protected Rigidbody2D  parentRig2D{get;private set; }

	SpringJoint2D mySpring2D;

	//希望有在努力的感觉，所以赚取金钱不是匀速，而是分时间段
	float earnMoneyT=0;
	float earnedMoneyPerT=0;
	float dT=0;
	float fastT=1.5f;
	float slowT=2.5f;

	// Use this for initialization
	protected override void Awake (){
		base.Awake ();

//		money = .3f;

		GameObject tempGO = new GameObject (this.transform.name, typeof( Rigidbody2D));
		tempGO.transform.position = this.transform.position;
		this.transform.parent = tempGO.transform;
		 parentRig2D = tempGO.GetComponent<Rigidbody2D> ();
		 parentRig2D.isKinematic = true;

		mySpring2D = this.GetComponent<SpringJoint2D> ();
		mySpring2D.connectedBody =  parentRig2D;

		moveRig2D =  parentRig2D;

		if (isEarnMoneyPerT) {
			earnMoneyT = Random.Range (fastT, slowT);
			earnedMoneyPerT = workForMoneyRate * earnMoneyT;
			workForMoneyRate = 0;
			dT = Random.Range (0, slowT);
		}
	}

	protected override void Update(){
		base.Update ();

		if (isDead)
			return;
		
		if (isEarnMoneyPerT) {
			//努力赚钱
			dT += Time.deltaTime;
			if (dT >= earnMoneyT) {
				dT -= earnMoneyT;
				//if (!this.GetComponentInChildren<YaElement> ().Electrified) {//带电不能变大
					MoneySystem.Instance.PlayerWorkEarnMoney (this, earnedMoneyPerT);
				//}
			}
		}

//		if (Vector2.Distance ( parentRig2D.position, myRig2D.position) > .7f) {
//			 parentRig2D.position = myRig2D.position;
//		}
	}
}
