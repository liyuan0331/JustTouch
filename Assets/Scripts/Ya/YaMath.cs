using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

static public class YaMath {
	static public float GetDfromS(float _s){
		_s = Mathf.Clamp (_s, 0, _s);
		return 2 * Mathf.Sqrt (_s / Mathf.PI);
	}
	static public void SetAlpha(ref SpriteRenderer _sr,float _a){
		Color temp = _sr.color;
		temp.a = _a;
		_sr.color = temp;
	}
	static public void SetAlpha(ref Image _image,float _a){
		Color temp = _image.color;
		temp.a = _a;
		_image.color = temp;
	}
	static public void SetCanvasRenderAlphaWithChildren(ref CanvasRenderer _canvasRender,float _a){
//		List<CanvasRenderer> canvasRenders = new List<CanvasRenderer> ();
//		_canvasRender.GetComponentsInChildren<CanvasRenderer> (canvasRenders);
		CanvasRenderer[] canvasRenders = _canvasRender.GetComponentsInChildren<CanvasRenderer> ();
		foreach (CanvasRenderer c in canvasRenders) {
			c.SetAlpha (_a);
		}
	}
	static public void SetCanvasRenderAlphaWithChildren(Transform _canvasRender,float _a){
		//		List<CanvasRenderer> canvasRenders = new List<CanvasRenderer> ();
		//		_canvasRender.GetComponentsInChildren<CanvasRenderer> (canvasRenders);
		CanvasRenderer[] canvasRenders = _canvasRender.GetComponentsInChildren<CanvasRenderer> ();
		foreach (CanvasRenderer c in canvasRenders) {
			c.SetAlpha (_a);
		}
	}
}
