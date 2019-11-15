using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerExit : MonoBehaviour {

	Collider2D col;
	// Use this for initialization
	void Start () {
		col = this.GetComponent<Collider2D> ();
	}
//	
//	// Update is called once per frame
//	void Update () {
//		
//	}

//	bool FullyContains(Collider2D resident){
//		Collider2D zone = this.GetComponent<Collider2D> ();
//		return zone.bounds.Contains (resident.bounds.max) && zone.bounds.Contains (resident.bounds.min);
//	}
//
//	void OnTriggerStay2D(Collider2D other){
//		MePlayer mePlayer = other.GetComponent<MePlayer> ();
//		if (mePlayer == null)
//			return;
//
//		if (FullyContains (other)) {
//			print ("player is inside");
//			col.enabled = false;
//		}
//	}

	void OnTriggerEnter2D(Collider2D other){
		MePlayer mePlayer = other.GetComponent<MePlayer> ();
		if (mePlayer != null) {
			print ("player is exit");
			col.enabled = false;

			GameUI.Instance.GameWin ();
			mePlayer.enabled = false;
		}
	}
}
