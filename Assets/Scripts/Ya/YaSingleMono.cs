using UnityEngine;
using System.Collections;
using System;

public class YaSingleMono<T>:MonoBehaviour where T:YaSingleMono<T>{
	public static T Instance; 

	virtual protected void Awake(){
		Instance = this as T;
	}
}
