using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Screen_Interface : MonoBehaviour {
	Text life;
	int value=0;
	// Use this for initialization
	void Start () {
		life = GameObject.Find("Life").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		life.text = ""+value;
	}
}
