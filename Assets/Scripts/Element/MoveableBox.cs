using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveableBox : MonoBehaviour {
	SpriteRenderer mySr;
	Rigidbody2D parentRig;
	// Use this for initialization
	void Start () {
		mySr = GetComponent<SpriteRenderer> ();
		parentRig = transform.parent.GetComponent<Rigidbody2D> ();
		SetMoveAble (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//其它元素介入
	void OnTriggerEnter2D(Collider2D other) {
		MePlayer player = other.GetComponent<MePlayer> ();
		if (player != null) {
			SetMoveAble (true);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		MePlayer player = other.GetComponent<MePlayer> ();
		if (player != null) {
			SetMoveAble (false);
		}
	}

	void SetMoveAble(bool b){
		mySr.enabled = b;
		if (b) {
			parentRig.bodyType = RigidbodyType2D.Dynamic;
		} else {
			parentRig.bodyType = RigidbodyType2D.Static;
		}
	}
}
