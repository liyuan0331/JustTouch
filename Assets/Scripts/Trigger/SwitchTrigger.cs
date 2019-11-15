using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTrigger : MonoBehaviour {
	public enum SwitchState{SwitchOn,SwitchOff,SwitchingOn,SwitchingOff}//,Switching}
	public SwitchState switchState=SwitchState.SwitchOff;

	public SwitchDoor target;

	public SliderJoint2D sjBtn;
	float motorSpeed=60;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		switch (switchState) {
		case SwitchState.SwitchOff:
			if (sjBtn.transform.localPosition.x > -.2f) {
				SetMotorSpeed (-motorSpeed);
				switchState = SwitchState.SwitchingOn;
			}
			break;
		case SwitchState.SwitchOn:
			if (sjBtn.transform.localPosition.x < .2f) {
				SetMotorSpeed (motorSpeed);
				switchState = SwitchState.SwitchingOff;
			}
			break;
//		case SwitchState.Switching:
//			if (sjBtn.transform.localPosition.x > 0.2f) {
//				SetMotorSpeed (-motorSpeed);
//				switchState = SwitchState.SwitchOn;
//			}else if (sjBtn.transform.localPosition.x < -.2f) {
//				SetMotorSpeed (motorSpeed);
//				switchState = SwitchState.SwitchOff;
//			}
//			break;
		case SwitchState.SwitchingOn:
			if (sjBtn.transform.localPosition.x > 0.2f) {
				SetMotorSpeed (-motorSpeed);
				switchState = SwitchState.SwitchOn;
				SetOn (true);
			}else if (sjBtn.transform.localPosition.x < -.2f) {
				SetMotorSpeed (motorSpeed);
				switchState = SwitchState.SwitchOff;
			}
			break;
		case SwitchState.SwitchingOff:
			if (sjBtn.transform.localPosition.x < -.2f) {
				SetMotorSpeed (motorSpeed);
				switchState = SwitchState.SwitchOff;
				SetOn (false);
			}else if (sjBtn.transform.localPosition.x > .2f) {
				SetMotorSpeed (-motorSpeed);
				switchState = SwitchState.SwitchOn;
			}
			break;
		}
	}

	void SetOn(bool b){
		target.SetOn (b);
	}

	void SetMotorSpeed(float speed){
		JointMotor2D tmp = sjBtn.motor;
		tmp.motorSpeed = speed;
		sjBtn.motor = tmp;
	}
}
