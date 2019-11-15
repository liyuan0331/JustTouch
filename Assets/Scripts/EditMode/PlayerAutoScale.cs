using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[ExecuteInEditMode]
public class PlayerAutoScale : MonoBehaviour {
	Player player;
	// Use this for initialization
	void Start () {
		player = this.GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.isEditor && !Application.isPlaying) {
			//根据金钱量来更新自己圆形的大小
			float myS = player.natureMoney + player.borrowMoney + player.lentMoney;
			float myD = YaMath.GetDfromS (myS);

			this.transform.localScale = Vector3.one * myD;
		}
	}
}
