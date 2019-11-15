using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieTrigger : MonoBehaviour {

	//其它元素介入
	protected virtual void OnTriggerEnter2D(Collider2D other) {
		MePlayer player = other.GetComponent<MePlayer> ();
		if (player != null) {
			player.Die ();
		}
	}

}
