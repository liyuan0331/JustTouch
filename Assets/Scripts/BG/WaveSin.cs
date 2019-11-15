using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSin : MonoBehaviour {
	Vector3 startPos;
	Vector3 newPos;

	public float timeScaleX=.5f;
	// Use this for initialization
	void Start () {
		startPos = transform.position;
//		timeScale = Random.Range (-.8f, 1.5f);
	}
	
	// Update is called once per frame
	void Update () {
		newPos = startPos;
		newPos.x += Mathf.Sin (Time.time*timeScaleX)*.3f;
		transform.position = newPos;
	}
}
