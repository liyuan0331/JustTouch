using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : DieTrigger {
	Rigidbody2D myRig;
	float speed = 3;
	// Use this for initialization
	void Start () {
		myRig = this.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		myRig.MovePosition(myRig.position+(Vector2)transform.right*speed*Time.fixedDeltaTime);
	}

	protected override void OnTriggerEnter2D (Collider2D other)
	{
		base.OnTriggerEnter2D (other);
		BulletPool.Instance.BackToPool (this);
	}
}
