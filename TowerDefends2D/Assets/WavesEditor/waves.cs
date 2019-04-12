/*
 Here properties of wave
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using FThLib;

public class waves : MonoBehaviour {
	public int index=0;
	public List<string> enemyList;
	public List<float> speedList;
	public List<int> lifeList;
	public List<string> pathList;
	public int delay = 20;
	private GameObject enemyY;


	//public int Number_Enemies=0;

	void Start () {
		enemyY = GameObject.Find("enemyY");
	}

	void OnMouseDown (){
		master.ClickEffect(this.gameObject,this);
		Resetlist();
		GameObject.Find("waveDelay").GetComponent<InputField>().text=""+delay;
		GameObject.Find("WaveIndex").GetComponent<InputField>().text=""+index;
		show();
	}

	void Resetlist(){
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Respawn");
		for(int i=0; i<gos.Length;i++){Destroy(gos[i]);}
		enemyY.transform.position = new Vector3(enemyY.transform.position.x, 2.5f, enemyY.transform.position.z);
	}

	void show(){
		for(int i =0; i< enemyList.Count;i++){
			GameObject go = Instantiate(Resources.Load("WaveEditor/Enemy"), enemyY.transform.position, Quaternion.identity)as GameObject;
			go.name=""+i;
			enemyY.transform.position = new Vector3(enemyY.transform.position.x,enemyY.transform.position.y - 0.5f,enemyY.transform.position.z);
			go.GetComponent<enemyContent>().wavesindex=index;
		}
	}
}
