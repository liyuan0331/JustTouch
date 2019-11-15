using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelControl : YaSingleMono<LevelControl> {
	public Image fadeBg;
//	public Canvas curtainCanvas;
	string[] levels=new string[]{"_levelControl","level1__prologue","level1_main"};
	int nextLevelCount=0;

	IEnumerator loadNextLevel = null;

	float fadeSpeed=.5f;
	float fadeWaitTime=.5f;
	// Use this for initialization
	void Start () {
		if (SceneManager.sceneCount==1) {
//			yield return new WaitForSeconds (.5f);
			LoadNextLevel ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadNextLevel(){
		string lastSceneName = "";//SceneManager.GetSceneAt (SceneManager.sceneCount-1).name;//SceneManager.GetActiveScene ().name;
		for (int i = 0; i < SceneManager.sceneCount; i++) {
			lastSceneName = SceneManager.GetSceneAt (i).name;
			if (lastSceneName != levels [0]) {
				break;
			}
		}

		nextLevelCount = Array.IndexOf (levels, lastSceneName)+1;
		if (nextLevelCount < 1) {
			return;
		}

		if (loadNextLevel != null) {
			StopCoroutine (loadNextLevel);
			loadNextLevel = null;
		}
		loadNextLevel = LoadNextLevel2 ();
		StartCoroutine (loadNextLevel);
	}

	IEnumerator LoadNextLevel2(){
		fadeBg.gameObject.SetActive (true);
		if (nextLevelCount > 1) {
			while (fadeBg.color.a < 1) {
				yield return new WaitForEndOfFrame ();
				float alpha = Mathf.MoveTowards (fadeBg.color.a, 1, Time.deltaTime * fadeSpeed);
				YaMath.SetAlpha (ref fadeBg, alpha);
			}

			SceneManager.UnloadSceneAsync (levels [nextLevelCount - 1]);
		} else {
			YaMath.SetAlpha (ref fadeBg, 1);
		}

		yield return new WaitForSeconds (fadeWaitTime);
		SceneManager.LoadScene (levels [nextLevelCount], LoadSceneMode.Additive);
		nextLevelCount++;

		while (fadeBg.color.a > 0) {
			yield return new WaitForEndOfFrame ();
			float alpha = Mathf.MoveTowards (fadeBg.color.a, 0, Time.deltaTime * fadeSpeed);
			YaMath.SetAlpha (ref fadeBg, alpha);
		}
		fadeBg.gameObject.SetActive (false);
	}
}
