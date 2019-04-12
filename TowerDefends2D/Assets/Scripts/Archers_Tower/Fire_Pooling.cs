/*
 	-This script is very important to get more memory into mobile plataform
 	-Fantastic tutorial here: "https://unity3d.com/es/learn/tutorials/modules/beginner/live-training-archive/object-pooling"
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fire_Pooling : MonoBehaviour {
	public int amount = 50;
	private GameObject fire;
	public List<GameObject> fireList;
	// Use this for initialization
	void Start () {
		fire = Instantiate(Resources.Load("Global/fire"), this.transform.position, Quaternion.identity)as GameObject;
		fireList = new List<GameObject>();
		for (int i=0; i<amount ; i++){
			GameObject obj = (GameObject)Instantiate(fire);
			obj.SetActive(false);
			fireList.Add(obj);
		}
	}
}