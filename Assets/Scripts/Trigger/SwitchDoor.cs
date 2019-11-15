using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDoor : MonoBehaviour {
	public enum DoorState{Open,CLose,}
	public DoorState doorState=DoorState.CLose;

	public SliderJoint2D sjDoor;

	Vector3 closePos=Vector2.zero;
	Vector3 openPos=new Vector2(0,-.29f);

	float motorSpeed=1f;
	// Use this for initialization
	void Start () {
		closePos += transform.position;
		openPos += transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		switch (doorState) {
		case DoorState.CLose:
			SetMotorSpeed (-motorSpeed);
			break;
		case DoorState.Open:
			SetMotorSpeed (motorSpeed);
			break;
		}
	}

	void SetMotorSpeed(float speed){
		JointMotor2D tmp = sjDoor.motor;
		tmp.motorSpeed = speed;
		sjDoor.motor = tmp;
	}

	public void SetOn(bool b){
		switch (b) {
		case true:
			doorState = DoorState.Open;
			break;
		case false:
			doorState = DoorState.CLose;
			break;
		}
	}
}
