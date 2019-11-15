using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
	public EdgeCollider2D edge;
	public Transform target;

	Camera myCam;

	float x0,x1,y0,y1;
	Vector3 followPos;

	float smooth=7;

	// Use this for initialization
	void Start () {
		myCam = GetComponent<Camera> ();
		float cameraHeight = myCam.orthographicSize;
		float cameraWidth = cameraHeight * myCam.aspect;
//		print ("h:" + cameraHeight + " w:" + cameraWidth);
	
		x0 = edge.points [0].x + edge.transform.position.x + cameraWidth;
		y0 = edge.points [0].y + edge.transform.position.y + cameraHeight;
		x1 = edge.points [2].x + edge.transform.position.x - cameraWidth;
		y1 = edge.points [2].y + edge.transform.position.y - cameraHeight;

		followPos.z = -10;
	}
	
	// Update is called once per frame
	void Update () {
		followPos.x = Mathf.Clamp (target.position.x, x0, x1);
		followPos.y = Mathf.Clamp (target.position.y, y0, y1);

		transform.position = Vector3.Lerp (transform.position, followPos, Time.deltaTime * smooth);
	}
}
