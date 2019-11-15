using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WallSnap : MonoBehaviour {
	static public float snapPos = .125f;
	static public float snapScale = .125f;
//	static public float snapRot=45*.5f;

//	// Use this for initialization
//	void Start () {
//		
//	}
	
	// Update is called once per frame
	void Update () {
		if (Application.isEditor && !Application.isPlaying) {
			//吸附位移
			transform.position = RoundTransform (transform.position, snapPos);

			//吸附缩放
			transform.localScale=RoundTransform(transform.localScale,snapScale);

		}
	}

	Vector3 RoundTransform(Vector3 v,float snapValue){
		return new Vector3 (
			snapValue * Mathf.Round (v.x / snapValue),
			snapValue * Mathf.Round (v.y / snapValue),
			v.z
		);
	}
}
