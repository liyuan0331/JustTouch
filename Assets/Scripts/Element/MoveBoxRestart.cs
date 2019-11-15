using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoxRestart : MonoBehaviour {
	public Color trigColor;
	Dictionary<Transform,Vector3> origStates;
	List<Transform> children;
	SpriteRenderer mySr;
	// Use this for initialization
	void Start () {
		mySr = this.GetComponent<SpriteRenderer> ();
		children = new List<Transform> ();
		origStates = new Dictionary<Transform, Vector3> ();

		for (int i = 0; i < transform.childCount; i++) {
			children.Add (transform.GetChild (i));
			origStates.Add (children [i], children [i].position);
		}


	}
	
	// Update is called once per frame
	void Reset () {
		foreach (KeyValuePair<Transform,Vector3> state in origStates) {
			state.Key.position = state.Value;
		}
	}

	//玩家踩入，重置方块
	void OnTriggerEnter2D(Collider2D other) {
		MePlayer player = other.GetComponent<MePlayer> ();
		if (player != null) {
			Reset ();
			mySr.color = trigColor;
		}
	}

	//玩家离开
	void OnTriggerExit2D(Collider2D other)
	{
		MePlayer player = other.GetComponent<MePlayer> ();
		if (player != null) {
			mySr.color = Color.white;
		}
	}
}
