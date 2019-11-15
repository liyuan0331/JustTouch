using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YaElement : MonoBehaviour {
	static protected float globalElementValue = 0;
	static protected float globalElementValueSpeed = 8;

	public bool staticElement = false;

	//温度
	public float elementValueSpeed = .3f;
	public float elementValue;
	public float ElementValue{ 
		get{ 
			return elementValue;
		}
	}
	public void ElementValueAdd(float amount){
		if (!staticElement) {
			elementValue += amount;
			elementValue = Mathf.Clamp (ElementValue, -100, 100);
		}
	}

	//电
	public bool electrified;
	public bool Electrified{ 
		get{ 
			return electrified;
		}
	}

	protected List<YaElement> stayElement;
	protected List<ElementElectric> stayElectric;
	// Use this for initialization
	protected virtual void Awake () {
		stayElement = new List<YaElement> ();
		stayElectric = new List<ElementElectric> ();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		//与世界元素关系,逼近世界元素值大小
		if (!staticElement) {
			elementValue = Mathf.MoveTowards (ElementValue, globalElementValue, Time.deltaTime * globalElementValueSpeed);
		}
		foreach (YaElement element in stayElement) {
			float tempAmount =this.ElementValue - Mathf.MoveTowards (this.ElementValue, element.ElementValue, Time.deltaTime * elementValueSpeed);
			element.ElementValueAdd (tempAmount);
			this.ElementValueAdd (-tempAmount);
		}

		//电
		bool huoxian=false,lingxian=false;//火线零线
		foreach (ElementElectric electric in stayElectric) {
			if (electric.electrified) {
				huoxian = true;
			} else {
				lingxian = true;
			}
		}
		if (huoxian && lingxian) {
			electrified = true;
		} else {
			electrified = false;
		}

	}

	//其它元素介入
	void OnTriggerEnter2D(Collider2D other) {
		YaElement enterElement = other.GetComponent<YaElement> ();
		if (enterElement != null) {
			if (!stayElement.Contains (enterElement)&&enterElement!=this) {
				stayElement.Add (enterElement);
			}
		}
		//电
		ElementElectric electric=other.GetComponent<ElementElectric> ();
		if (electric != null) {
			if (!stayElectric.Contains (electric)&&electric!=this) {
				stayElectric.Add (electric);
			}
		}
	}

	//其它元素离开
	void OnTriggerExit2D(Collider2D other)
	{
		YaElement exitElement = other.GetComponent<YaElement> ();
		stayElement.Remove (exitElement);
		//电
		ElementElectric exitElectric=other.GetComponent<ElementElectric> ();
		stayElectric.Remove (exitElectric);
	}
}
