using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameObjectPool<U> :YaSingleMono<GameObjectPool<U>> where U:Component {

	[SerializeField]	private U m_prefab;
	[SerializeField]	private int m_initailSize = 5;

	private List<U> m_availableObjects = new List<U> ();

	// Use this for initialization
	override protected void Awake () {
		base.Awake ();
		for (int i = 0; i < m_initailSize; i++) {
			U go = Instantiate<U> (m_prefab, this.transform);
			m_availableObjects.Add (go);
			go.gameObject.SetActive (false);
		}
	}
	
	public U GetPlloedInstance(Vector3 pos,Quaternion rotation){
		int lastIndex = m_availableObjects.Count - 1;
		if (lastIndex >= 0) {
			U go = m_availableObjects [lastIndex];
			m_availableObjects.RemoveAt (lastIndex);
			go.transform.position = pos;
			go.transform.rotation = rotation;
			go.gameObject.SetActive (true);
			return go;
		} else {
			U go = Instantiate<U> (m_prefab, pos, rotation, this.transform);
			return go;
		}
	}

	public void BackToPool(U go){
		m_availableObjects.Add (go);
		go.gameObject.SetActive (false);
	}
}
