using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
public class WallAutoLine : MonoBehaviour {
	static public float outDis = .3f;

	Transform mParent;
	// Use this for initialization
	void Start () {
		mParent = transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
		if (mParent == null)
			return;

		if (Application.isEditor && !Application.isPlaying) {
			if (mParent.lossyScale.x == 0 || mParent.lossyScale.y == 0)
				return;
			Vector2 worldDis = mParent.lossyScale;
			worldDis += Vector2.one * outDis;
			transform.localScale = new Vector2 (worldDis.x / mParent.lossyScale.x, worldDis.y / mParent.lossyScale.y);
			transform.localPosition = Vector2.zero;
		}
	}
}
