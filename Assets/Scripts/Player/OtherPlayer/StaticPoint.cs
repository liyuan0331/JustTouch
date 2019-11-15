using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticPoint : MonoBehaviour {
	public Vector3 pointScale = new Vector3 (.1f, .1f, .1f);
	// Use this for initialization
	void Start () {
//		pointScale = transform.lossyScale;
//		print (pointScale);
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = pointScale/transform.parent.localScale.x;
	}
}
