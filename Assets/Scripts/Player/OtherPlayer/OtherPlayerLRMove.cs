using UnityEngine;
using System.Collections;

public class OtherPlayerLRMove : OtherPlayer {
	enum State{right,left}
	State myState=State.right;

	protected override void Awake ()
	{
		base.Awake ();
		maxSpeed = 1.5f;
//		acc = .3f;
	}

	// Update is called once per frame
	protected override void Update(){
		base.Update ();

		switch (myState) {
		case State.right:
			if (!PlayerMoveTowards (Vector3.right)) {
				myState = State.left;
			}

			break;
		case State.left:
			if (!PlayerMoveTowards (Vector3.left)) {
				myState = State.right;
			}
			break;
		}
	}
}
