using UnityEngine;
using System.Collections;
using UnityEditor;

[SelectionBase]
public class Player : MonoBehaviour {
    //	Color natureMoneyColor = new Color (119, 145, 140) / 255f;
    //	Color borrowMoneyColor = new Color (173, 210, 203) / 255f;
    //	Color lentMoneyColor = new Color (220, 173, 83) / 255f;

    public float borrowPercent = 1;
    public float borrowMoneyLossSpeed = 1f;//还款速度
    public int hurtCount = 1;//受伤几次死亡

    public float natureMoney = .785f;
	public float borrowMoney = 0;	//从别人借入的money
	public float lentMoney = 0;     //借出给别人的money
    public Rigidbody2D myRig2D;
    public Rigidbody2D moveRig2D;
	protected float maxSpeed=4f;

	protected Transform natureMoneyCircle;
    protected Transform borrowMoneyCircle;

	/*这里去掉，改为下面将生活所需的工作与物品等价交换与金钱的工作抽离
	//为了生活和其它消费话费金钱的速率 unit/s
	public float spendMoneyRate = .1f;
	*/
	//将工作和生活品等价交换抽离。
	//不工作，每个人的金钱量不变，有人工作，则从其它人身上平均抽取金额
	//赚取金钱的速率 unit/s
	public float workForMoneyRate = 0;

	//是否死亡
	protected bool isDead=false;

	// Use this for initialization
	protected virtual void Awake () {
		myRig2D = this.GetComponent<Rigidbody2D> ();
		moveRig2D = myRig2D;

		natureMoneyCircle = this.transform.Find ("natureMoneyCircle");
		borrowMoneyCircle = this.transform.Find ("borrowMoneyCircle");

//		Color a = new Color(119 / 255f, 145 / 255f, 140 / 255f);
//		a *= 256;
//		ColorHSV b = ColorConverter.RGB2HSV (a);
//		b.S *= 1.5f;
//		b.S = Mathf.Clamp01 (b.S);
//		Color test = new Color (a.b, a.g, a.r);
	}

	protected virtual void Update(){
		if (isDead)
			return;


		////工作赚钱
		//if (workForMoneyRate > 0) {
		//	//if (!this.GetComponentInChildren<YaElement> ().Electrified) {//带电不能变大
		//		MoneySystem.Instance.PlayerWorkEarnMoney (this, workForMoneyRate * Time.deltaTime);
		//	//}
		//}

		//根据金钱量来更新自己圆形的大小
		float natureMoneyS = natureMoney;
		float borrowMoneyS = natureMoney + borrowMoney;
		float lentMoneyS = natureMoney + borrowMoney + lentMoney;

		float lentD = YaMath.GetDfromS (lentMoneyS);
		float borrowD = YaMath.GetDfromS (borrowMoneyS);
		float natureD = YaMath.GetDfromS (natureMoneyS);

		this.transform.localScale = Vector3.Lerp (this.transform.localScale, Vector3.one * lentD, 3f * Time.deltaTime);

		if (Mathf.Abs( lentD) > .001f) {
			borrowMoneyCircle.localScale = Vector3.one * borrowD / lentD;
			natureMoneyCircle.localScale = Vector3.one * natureD / lentD;
		} 
//		else {//判断破产
//			GoBankrupt();
//		}
		if (Mathf.Abs (transform.localScale.magnitude) < .05f) {
			GoBankrupt ();
		}
	}

	protected virtual void FixedUpdate(){

		if(myRig2D.bodyType!=RigidbodyType2D.Static){
			myRig2D.velocity = Vector3.zero;//不需要惯性
//			myRig2D.velocity=Vector3.Lerp(myRig2D.velocity,Vector3.zero,Time.deltaTime*15f);
		}
	}

	//Player向一个方向移动
	protected virtual void PlayerMoveDir(Vector2 dir){
		if (isDead)
			return;
		moveRig2D.MovePosition(myRig2D.position+dir*maxSpeed*Time.deltaTime);
	}

	//Player向一个目标点移动,移动中返回true，如果到达目的地返回false
	protected virtual bool PlayerMoveTowards(Vector2 goal){
        if (!GameUI.Instance.userControl) return false;
		if (isDead)
			return true;

		if (moveRig2D.position == goal) {
			return false;
		}

		Vector2 curPos = moveRig2D.position;
		Vector2 targetPos = Vector2.MoveTowards (curPos, goal, maxSpeed * Time.deltaTime);
		moveRig2D.MovePosition (targetPos);

		return true;
	}



	//金额增多
	public void IncreaseMoney(float dMoney){
		natureMoney += dMoney;
	}

	//金额减少，预计扣除dMoey金额，返回的是真正扣除的金额
	public float ReduceMoney(float dMoney){
		natureMoney -= dMoney;
		if (natureMoney < 0) {
			borrowMoney += natureMoney;
			natureMoney = 0;
			if (borrowMoney < 0) {//该player破产
				float result=dMoney + borrowMoney;
				borrowMoney = 0;
//				GoBankrupt();
				return result;
			}
		}
		return dMoney;
	}

	//破产
	protected virtual void GoBankrupt(){
		print (this.name + "破产");
        if (this as MePlayer)
        {
            this.gameObject.SetActive (false);
        }
        else
        {
            Destroy(this.transform.root.gameObject);
        }
		MoneySystem.Instance.CheckAllPlayer ();
	}

	//死亡
	public virtual void Die(){
		isDead = true;
		Transform dieMask = transform.Find ("DieMask");
		if (dieMask != null) {
			dieMask.GetComponent<SpriteRenderer> ().enabled = true;
		}

	}
}
