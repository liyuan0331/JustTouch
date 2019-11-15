using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCamera : MonoBehaviour {
	Camera myCam;
	float widthOrtho;
	// Use this for initialization
	void Awake () {
		myCam = GetComponent<Camera> ();
		widthOrtho = myCam.orthographicSize * (4f / 3f);

		myCam.orthographicSize = widthOrtho / myCam.aspect;
	}
	
	// Update is called once per frame
	void Update () {
		myCam.orthographicSize = widthOrtho / myCam.aspect;
	}
}
