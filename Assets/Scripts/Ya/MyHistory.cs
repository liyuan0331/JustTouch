using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyHistory : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (YaHistory ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator YaHistory(){
		for (int i = 1986; i < 2020; i++) {
			yield return new WaitForSeconds (3);
			print ("今年是 " + i + " 年");
		}
	}
}
