using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementPlayer : YaElement {
	public enum TemperatureState{Normal,Hot,Cold}
	public TemperatureState temperatureState=TemperatureState.Normal;
	bool hurting = false;//因为元素造成伤害只会一次，退出伤害才能再次被伤害

	public SpriteRenderer hotMask;
	public ParticleSystem hotParticle;
	public SpriteRenderer coldMask;

	float hotThreshold=10;
	float coldThreshold=-10;

	float maxHotAlpha = 117 / 255f;
	float maxColdAlpha = 117 / 255f;

	IElementPlayer iElememtPlayer;
	// Use this for initialization
	void Start () {
		iElememtPlayer = transform.parent.GetComponent<IElementPlayer> ();
	}
	
	// Update is called once per frame
	protected override void Update ()
	{
		base.Update ();

		//现实变热遮罩
		if (temperatureState != TemperatureState.Hot) {
			if (elementValue > hotThreshold) {
				ShowHotMask ();
				temperatureState = TemperatureState.Hot;
			}
		}
		if (temperatureState != TemperatureState.Cold) {
			if (elementValue < coldThreshold) {
				ShowColdMask ();
				temperatureState = TemperatureState.Cold;
			}
		}
		if (temperatureState != TemperatureState.Normal) {
			if (elementValue > coldThreshold * .8f && elementValue < hotThreshold * .8f) {
				HideMask ();
				temperatureState = TemperatureState.Normal;
			}
		}

		//根据温度更改遮罩颜色
		float alpha;
		switch (temperatureState) {
		case TemperatureState.Hot:
			alpha = (elementValue - hotThreshold) / (100 - hotThreshold) * maxHotAlpha;
			ChangeAlpha (ref hotMask, alpha);
			if (elementValue > 95) {
				hotParticle.Play ();
				if (!hurting) {
					iElememtPlayer.GetHurt ();
					hurting = true;
				}
			}
			if (elementValue < 80) {
				hotParticle.Stop ();
				hurting = false;
			}
			break;
		case TemperatureState.Cold:
			alpha = (Mathf.Abs( elementValue) - Mathf.Abs( coldThreshold)) / (100 + coldThreshold) * maxColdAlpha;
			ChangeAlpha (ref coldMask, alpha);
			break;
		}
	}

	void ChangeAlpha(ref SpriteRenderer sr,float alpha){
		Color temp = sr.color;
		temp.a = alpha;
		sr.color = temp;
	}

	void ShowHotMask(){
		print ("enter hot");
		coldMask.gameObject.SetActive (false);
		hotMask.gameObject.SetActive (true);
		ChangeAlpha (ref hotMask, 0);
	}
	void ShowColdMask(){
		print ("enter cold");
		hotMask.gameObject.SetActive (false);
		coldMask.gameObject.SetActive (true);
		ChangeAlpha (ref coldMask, 0);
	}
	void HideMask(){
		print ("enter normal");
		hotMask.gameObject.SetActive (false);
		coldMask.gameObject.SetActive (false);
	}
}
