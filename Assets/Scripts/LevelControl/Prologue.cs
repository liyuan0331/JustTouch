using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Prologue : MonoBehaviour {
	public int actIndex = 1;
	public Text actMsg;
	// Use this for initialization
	IEnumerator Start () {
		actMsg.text = YaLocalization.GetActMsg (actIndex);

		if (LevelControl.Instance != null) {
			yield return new WaitForSeconds (5f);
			LevelControl.Instance.LoadNextLevel ();
		}
	}

	void Update(){
		actMsg.text = YaLocalization.GetActMsg (actIndex);
	}

//	public void NextLevel(){
//		if (LevelControl.Instance != null) {
//			LevelControl.Instance.LoadNextLevel ();
//		}
//	}
}
