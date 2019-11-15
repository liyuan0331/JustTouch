using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightShake : MonoBehaviour {
	public Transform big;
	public Transform small;

	// Use this for initialization
	void Start () {
		StartCoroutine (scaleRandom (small));
		StartCoroutine (scaleRandom (big));
	}

	IEnumerator scaleRandom(Transform target){
		Vector3 normalScale = target.localScale;
		bool big = true;
		while (true) {
			float scaleTime = Random.Range (.02f, .06f);
			if (big) {
				Vector3 targetScale = normalScale * Random.Range (1f, 1.03f);
				float speed = Vector3.Distance (normalScale, targetScale) / scaleTime;
				while (target.localScale != targetScale) {
					yield return new WaitForEndOfFrame ();
					target.localScale = Vector3.MoveTowards (target.localScale, targetScale, Time.deltaTime * speed);
				}
			} else {
				Vector3 targetScale = normalScale * Random.Range (.97f, 1);
				float speed = Vector3.Distance (normalScale, targetScale) / scaleTime;
				while (target.localScale != targetScale) {
					yield return new WaitForEndOfFrame ();
					target.localScale = Vector3.MoveTowards (target.localScale, targetScale, Time.deltaTime * speed);
				}
			}
			big = !big;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
