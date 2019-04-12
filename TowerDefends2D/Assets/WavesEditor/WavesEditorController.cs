using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WavesEditorController : MonoBehaviour {
	public InputField Number_Enemies;
	public InputField type;					//enemy type, example: "enemy1" , 2 or 3  (1 default, 2 sorcerer, 3 airunit)
	public InputField path;
	private GameObject wavesY;
	private GameObject latest=null;
	private List<GameObject> wavesList;
	private int index=0;
	// Use this for initialization
	void Start () {
		wavesY = GameObject.Find("wavesY");
		wavesList = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Add(){
		GameObject go = Instantiate(Resources.Load("WaveEditor/waves"), wavesY.transform.position, Quaternion.identity)as GameObject;
		go.name="wave" + index;
		latest=go;
		go.GetComponent<waves>().index=index;
		wavesY.transform.position = new Vector3(wavesY.transform.position.x,wavesY.transform.position.y - 0.5f,wavesY.transform.position.z);
		wavesList.Add(go);
		AddEnemiesDefault(go);
		index++;
		go.transform.parent = GameObject.Find("wavesPrefab").transform;
	}

	public void AddEnemiesDefault(GameObject go){
		int total = int.Parse(Number_Enemies.GetComponent<InputField>().text);
		/*Editing .... Wave properties*/
		List<string> enemyList = new List<string>();
		List<float> speedList = new List<float>();
		List<int> lifeList = new List<int>();
		List<string> pathList = new List<string>();

		for(int i =0;i<total;i++){		
			enemyList.Add(type.GetComponent<InputField>().text);	//by default the wave is created with enemy1
			speedList.Add(0.4f);		//by default enemy speed is 0.4f
			lifeList.Add(20);			//by default enemy life is 20
			pathList.Add(path.GetComponent<InputField>().text);
		}

		go.GetComponent<waves>().enemyList = enemyList;
		go.GetComponent<waves>().lifeList = lifeList;
		go.GetComponent<waves>().speedList = speedList;
		go.GetComponent<waves>().pathList = pathList;
		//===========================//
	}

	public void MoveCameraUp(){
		Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y+0.5f,Camera.main.transform.position.z);
	}

	public void MoveCameraDown(){
		Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y-0.5f,Camera.main.transform.position.z);
	}
}
