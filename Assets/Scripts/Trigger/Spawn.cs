using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {
//	public GameObject bulletPrefab;
	public Transform spawnPos;//发射起始位置

	public float spawnT=2f;//发射间隔
	public float startPercent=0f;//第一次发射时间的百分数(0~1)
	//下面三个的和不能大于1
	public float changeColorPercent=.4f;//变色时间
	public float whiteColorPercent=.3f;//变白时间
	public float returnColorPercent = .2f;//恢复时间

	IEnumerator prepareShot = null;
	SpriteRenderer mySR;
	Color origColor;
	float startAlpha = .3f;
	// Use this for initialization
	void Start () {
		spawnPos.GetComponent<SpriteRenderer> ().enabled = false;
		mySR = this.GetComponent<SpriteRenderer> ();
		origColor = mySR.color;
		YaMath.SetAlpha (ref mySR, startAlpha);
		InvokeRepeating ("PrepareShot", spawnT+startPercent * spawnT - (changeColorPercent + whiteColorPercent + returnColorPercent*.8f), spawnT);
	}

	void PrepareShot(){
		if (prepareShot != null) {
			StopCoroutine (prepareShot);
			prepareShot = null;
		}
		prepareShot = PrepareShot2 ();
		StartCoroutine (prepareShot);
	}

	IEnumerator PrepareShot2(){
		YaMath.SetAlpha (ref mySR, startAlpha);

		while (mySR.color.a < 1) {
			yield return new WaitForEndOfFrame ();
			float tempAlpha = Mathf.MoveTowards (mySR.color.a, 1, (1 - startAlpha) / (changeColorPercent*spawnT) * Time.deltaTime);
			YaMath.SetAlpha (ref mySR, tempAlpha);
		}


		float progress=0;
		float showTime = whiteColorPercent * spawnT;
		while (progress < .8f) {
			mySR.color = Color.Lerp (origColor, Color.white, progress);
			progress += Time.deltaTime*(1/showTime);
			yield return new WaitForEndOfFrame ();
		}
		Shot ();
		while (progress < 1f) {
			mySR.color = Color.Lerp (origColor, Color.white, progress);
			progress += Time.deltaTime*(1/showTime);
			yield return new WaitForEndOfFrame ();
		}

		Color targetColor = origColor;
		targetColor.a = startAlpha;
		progress = 0;
		showTime = returnColorPercent * spawnT;
		while (progress < 1f) {
			mySR.color = Color.Lerp (Color.white, targetColor, progress);
			progress += Time.deltaTime*(1/showTime);
			yield return new WaitForEndOfFrame ();
		}
			
		mySR.color = origColor;
		YaMath.SetAlpha (ref mySR, startAlpha);

		yield break;
	}

	void Shot(){
		BulletPool.Instance.GetPlloedInstance (spawnPos.position,spawnPos.rotation);
//		GameObject.Instantiate (bulletPrefab,spawnPos.position,spawnPos.rotation);
	}
}
