using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ShowEdgeCollider : MonoBehaviour {

	EdgeCollider2D mCollider;
	Vector2[] points;
	Vector3 _t;

	// Use this for initialization
	void Start () {
		mCollider = this.GetComponent<EdgeCollider2D> ();
		points = mCollider.points;
		_t = mCollider.transform.position;
	}
	
	// Update is called once per frame
	void OnDrawGizmos () {
		if (mCollider == null || points == null)
			return;
		
		Gizmos.color = Color.red;
		for (int i = 0; i < points.Length - 1; i++) {
			Gizmos.DrawLine (new Vector3 (points [i].x + _t.x, points [i].y + _t.y), new Vector3 (points [i + 1].x + _t.x, points [i + 1].y + _t.y));
		}
	}
}
