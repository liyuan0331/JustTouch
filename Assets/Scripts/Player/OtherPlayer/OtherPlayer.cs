using UnityEngine;
using System.Collections;

public class OtherPlayer : Player {

    Color bigColor = new Color32(145, 119, 119, 255);
    Color smallColor = new Color32(70, 108, 138, 255);

    protected float hurtMoney;//受一次伤害的血量

    public float lentPercent = 1.5f;

    //float defeatPercent=.3f;//防御百分比
    //float hurtMoney {//受一次伤害的血量
    //    get {
    //        return (realPlayer.natureMoney + realPlayer.borrowMoney) * defeatPercent;
    //    }
    //}

    //public bool isEarnMoneyPerT=true;

    protected Rigidbody2D  parentRig2D{get;private set; }

	SpringJoint2D mySpring2D;

	//希望有在努力的感觉，所以赚取金钱不是匀速，而是分时间段
	float earnMoneyT=0;
	float earnedMoneyPerT=0;
	float dT=0;
	float fastT=1.5f;
	float slowT=2.5f;

    protected MePlayer realPlayer;

    public SpriteRenderer nature;

	// Use this for initialization
	protected override void Awake (){
        realPlayer = FindObjectOfType<MePlayer>();

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

        //if (isEarnMoneyPerT) {
        //	earnMoneyT = Random.Range (fastT, slowT);
        //	earnedMoneyPerT = workForMoneyRate * earnMoneyT;
        //	workForMoneyRate = 0;
        //	dT = Random.Range (0, slowT);
        //}

        hurtMoney = (natureMoney / hurtCount) * 1.2f;

    }

    public void GetHurt()
    {
        if (borrowMoney > 0) return;//不能累计伤害

        //自身减少部分
        natureMoney -= hurtMoney;
        borrowMoney += hurtMoney;
        if (natureMoney < 0)
        {
            borrowMoney += natureMoney;
            natureMoney = 0;
        }

        //玩家借出部分
        float getMoney = borrowMoney * lentPercent;
        float realGetMoney = realPlayer.ReduceMoney(getMoney);
        borrowMoney += realGetMoney;
    }


	protected override void Update(){
		base.Update ();

        if (natureMoney > realPlayer.natureMoney)
        {
            if (nature != null)
            {
                nature.color = bigColor;
            }
            else
            {
                natureMoneyCircle.GetComponent<SpriteRenderer>().color = bigColor;
            }
        }
        else
        {
            if (nature != null)
            {
                nature.color = smallColor;
            }
            else
            {
                natureMoneyCircle.GetComponent<SpriteRenderer>().color = smallColor;
            }
        }

        if (isDead)
			return;

        //还钱
        if (borrowMoney > 0)
        {
            float returnMoney = hurtMoney*borrowMoneyLossSpeed * Time.deltaTime;
            borrowMoney -= returnMoney;
            if (borrowMoney < 0)
            {
                returnMoney += borrowMoney;
                borrowMoney = 0;
            }
            returnMoney *= .8f;//玩家只吸收80%
            realPlayer.IncreaseMoney(returnMoney);
        }
		
		//if (isEarnMoneyPerT) {
		//	//努力赚钱
		//	dT += Time.deltaTime;
		//	if (dT >= earnMoneyT) {
		//		dT -= earnMoneyT;
		//		//if (!this.GetComponentInChildren<YaElement> ().Electrified) {//带电不能变大
		//			MoneySystem.Instance.PlayerWorkEarnMoney (this, earnedMoneyPerT);
		//		//}
		//	}
		//}

//		if (Vector2.Distance ( parentRig2D.position, myRig2D.position) > .7f) {
//			 parentRig2D.position = myRig2D.position;
//		}
	}
}
