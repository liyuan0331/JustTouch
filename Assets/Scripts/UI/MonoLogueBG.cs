using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MonoLogueBG : MonoBehaviour {
	public RectTransform monologueMsg;
	RectTransform myRectTrans;
	// Use this for initialization
	void Awake () {
		myRectTrans = this.GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	void Update () {
		myRectTrans.localScale = monologueMsg.localScale;
		myRectTrans.sizeDelta = monologueMsg.sizeDelta+new Vector2(26,26);
		myRectTrans.anchoredPosition = monologueMsg.anchoredPosition;
		myRectTrans.localPosition = monologueMsg.localPosition+new Vector3(0,-13,0);

	}
}
