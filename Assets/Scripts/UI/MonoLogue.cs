using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//动态现实或隐藏独白
public class MonoLogue : YaSingleMono<MonoLogue> {
	CanvasRenderer canvasRender;
	Text monologueText;

	IEnumerator changeMyAlpha = null;

	float changeSpeed=1f;
//	float showTime=4f;
	float showTimeByWord=.12f;
	float addShowTime=.5f;
	// Use this for initialization
	void Start () {
		transform.GetChild (0).gameObject.SetActive (true);
		monologueText = GetComponentInChildren<Text> ();
		canvasRender = GetComponentInChildren<CanvasRenderer> ();
		YaMath.SetCanvasRenderAlphaWithChildren (ref canvasRender, 0);
	}
	
//	// Update is called once per frame
//	void Update () {
//		
//	}

	public void ShowThenHideMonlogue(string _msg){
		CleanCoroutine ();

		changeMyAlpha = ShowThenHideMonlogue2 (_msg);
		StartCoroutine (changeMyAlpha);
	}
//	public void HideMonlogue(){
//		CleanCoroutine ();
//
//		changeMyAlpha = ChangeMyAlpha (1, 0, false);
//		StartCoroutine (changeMyAlpha);
//	}

	void CleanCoroutine(){
		if (changeMyAlpha != null) {
			StopCoroutine (changeMyAlpha);
			changeMyAlpha = null;
		}
	}

	IEnumerator ShowThenHideMonlogue2(string _msg){
		float startAlpha=0,endAlpha=1;
		//隐藏之前的信息(如果有)
		while (canvasRender.GetAlpha () != startAlpha) {
			yield return new WaitForEndOfFrame ();
			float tempAlpha = Mathf.MoveTowards (canvasRender.GetAlpha (), startAlpha, Time.deltaTime * changeSpeed*2f);
			YaMath.SetCanvasRenderAlphaWithChildren (this.transform, tempAlpha);
		}

		//显示现在的信息
		monologueText.text = _msg;
		while (canvasRender.GetAlpha () != endAlpha) {
			yield return new WaitForEndOfFrame ();
			float tempAlpha = Mathf.MoveTowards (canvasRender.GetAlpha (), endAlpha, Time.deltaTime * changeSpeed);
			YaMath.SetCanvasRenderAlphaWithChildren (this.transform, tempAlpha);
		}
		//屏幕暂留
		yield return new WaitForSeconds (showTimeByWord*_msg.Length);
		yield return new WaitForSeconds (addShowTime);
		//隐藏现在的信息
		while (canvasRender.GetAlpha () != startAlpha) {
			yield return new WaitForEndOfFrame ();
			float tempAlpha = Mathf.MoveTowards (canvasRender.GetAlpha (), startAlpha, Time.deltaTime * changeSpeed);
			YaMath.SetCanvasRenderAlphaWithChildren (this.transform, tempAlpha);
		}
	}

//	IEnumerator ChangeMyAlpha(float startAlpha,float endAlpha,bool waitAndpinpong){
//		while (canvasRender.GetAlpha () != startAlpha) {
//			yield return new WaitForEndOfFrame ();
//			float tempAlpha = Mathf.MoveTowards (canvasRender.GetAlpha (), startAlpha, Time.deltaTime * changeSpeed);
//			YaMath.SetCanvasRenderAlphaWithChildren (this.transform, tempAlpha);
//		}
////		YaMath.SetCanvasRenderAlphaWithChildren (this.transform, startAlpha);
//		while (canvasRender.GetAlpha () != endAlpha) {
//			yield return new WaitForEndOfFrame ();
//			float tempAlpha = Mathf.MoveTowards (canvasRender.GetAlpha (), endAlpha, Time.deltaTime * changeSpeed);
//			YaMath.SetCanvasRenderAlphaWithChildren (this.transform, tempAlpha);
//		}
////		print ("a:"+canvasRender.GetAlpha ());
//		if (waitAndpinpong) {
//			yield return new WaitForSeconds (showTime);
//			while (canvasRender.GetAlpha () != startAlpha) {
//				yield return new WaitForEndOfFrame ();
//				float tempAlpha = Mathf.MoveTowards (canvasRender.GetAlpha (), startAlpha, Time.deltaTime * changeSpeed);
//				YaMath.SetCanvasRenderAlphaWithChildren (this.transform, tempAlpha);
//			}
////			print ("b:"+canvasRender.GetAlpha ());
//		}
//	}
}
