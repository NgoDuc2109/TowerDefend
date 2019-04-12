/*
 	Enemy properties
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using FThLib;

public class enemyContent : MonoBehaviour {
	public int wavesindex=0;				//Waves index
	private InputField wavesIndex_;			//Input field
	private InputField index;
	private InputField name;
	private InputField speed;
	private InputField life;
	private InputField path;
	// Use this for initialization
	void Start () {
		wavesIndex_ = GameObject.Find("WaveIndex").GetComponent<InputField>();
		index = GameObject.Find("Index").GetComponent<InputField>();
		name = GameObject.Find("Name").GetComponent<InputField>();
		speed = GameObject.Find("Speed").GetComponent<InputField>();
		life = GameObject.Find("Life").GetComponent<InputField>();
		path = GameObject.Find("Path_").GetComponent<InputField>();

	}

	void OnMouseDown (){
		master.ClickEffect(this.gameObject,this);
		GameObject MyWave = GameObject.Find("wave"+wavesindex);
		wavesIndex_.text = ""+wavesindex;
		index.text = this.gameObject.name;
		name.text = MyWave.GetComponent<waves>().enemyList[int.Parse(this.gameObject.name)];
		speed.text =  ""+MyWave.GetComponent<waves>().speedList[int.Parse(this.gameObject.name)];
		life.text = ""+MyWave.GetComponent<waves>().lifeList[int.Parse(this.gameObject.name)];
		path.text = MyWave.GetComponent<waves>().pathList[int.Parse(this.gameObject.name)];
	}
	// Update is called once per frame
	void Update () {
	
	}

}
