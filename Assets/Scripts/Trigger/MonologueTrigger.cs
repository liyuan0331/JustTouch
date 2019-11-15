using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonologueTrigger : MonoBehaviour {
	public int levelNum;
	public int index;
	Collider2D col;
	// Use this for initialization
	void Start () {
		col = this.GetComponent<Collider2D> ();
	}
	
//	// Update is called once per frame
//	void Update () {
//		
//	}

	void OnTriggerEnter2D(Collider2D other){
		MePlayer mePlayer = other.GetComponent<MePlayer> ();
		if (mePlayer != null) {
			print ("player is exit");
			col.enabled = false;

			MonoLogue.Instance.ShowThenHideMonlogue (YaLocalization.GetLevelMonoLogue (levelNum, index));
		}
	}
}
