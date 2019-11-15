using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaloFade : MonoBehaviour {
	public SpriteRenderer halo1;
	public SpriteRenderer halo2;

	Vector3 startScale;
	Vector3 endScale;

	float startAlpha=88/255f;

	float fadeTime=2.8f;
	float waitTime=1f;

	float scaleSpeed;
	float alphaSpeed;
	// Use this for initialization
	IEnumerator Start () {
		startScale = Vector3.Min (halo1.transform.localScale, halo2.transform.localScale);
		endScale = Vector3.Max (halo1.transform.localScale, halo2.transform.localScale);

		scaleSpeed = Vector3.Distance(startScale,endScale) / fadeTime;
		alphaSpeed = startAlpha / fadeTime;

		StartCoroutine (FadeAnim (halo1));
		yield return new WaitForSeconds ((fadeTime + waitTime) * .5f);
		StartCoroutine (FadeAnim (halo2));
	}

	IEnumerator FadeAnim(SpriteRenderer sr){
		while (true) {
			sr.transform.localScale = startScale;
			SetAlpha (ref sr, startAlpha);
//		sr.color.a = startAlpha;
			while (sr.color.a != 0) {
				yield return new WaitForEndOfFrame ();
				sr.transform.localScale = Vector3.MoveTowards (sr.transform.localScale, endScale, Time.deltaTime * scaleSpeed);
				float curAlpha = Mathf.MoveTowards (sr.color.a, 0, Time.deltaTime * alphaSpeed);
				SetAlpha (ref sr, curAlpha);
//			sr.color.a = curAlpha;
			}
			yield return new WaitForSeconds (waitTime);
		}
	}

	void SetAlpha(ref SpriteRenderer sr,float alpha){
		sr.color = new Color (sr.color.r, sr.color.g, sr.color.b, alpha);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
