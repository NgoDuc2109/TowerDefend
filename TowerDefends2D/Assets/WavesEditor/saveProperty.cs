/*
 *This script was designed for Unity Editor only.
 *Save your custom level waves into a prefab
*/

#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;
using FThLib;



public class saveProperty : MonoBehaviour {
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
		if(this.gameObject.name=="saveLevel"){
			GameObject[] gos = GameObject.FindGameObjectsWithTag("Finish");
			GameObject[] customLevel = new GameObject[gos.Length];
			for(int i=0; i<gos.Length;i++){
				gos[i].GetComponent<SpriteRenderer>().enabled=false;
				gos[i].GetComponent<Collider>().enabled=false;
			}
			Object prefab = EditorUtility.CreateEmptyPrefab("Assets/Tower_Defense_Pack/CustomWaves/" + "customlevel" + ".prefab");
			EditorUtility.ReplacePrefab(GameObject.Find("wavesPrefab"), prefab);
			AssetDatabase.Refresh();
			UnityEditor.EditorApplication.isPlaying = false;
		}else{
			if(this.gameObject.name=="saveDelay"){
				//GameObject.Find("waveDelay").GetComponent<InputField>().text
				GameObject.Find("wave"+GameObject.Find("WaveIndex").GetComponent<InputField>().text).GetComponent<waves>().delay = int.Parse(GameObject.Find("waveDelay").GetComponent<InputField>().text);
			}else{
				GameObject MyWave = GameObject.Find("wave"+wavesIndex_.text);
				waves property =  MyWave.GetComponent<waves>();
				property.enemyList[int.Parse(index.text)] = name.text;
				property.speedList[int.Parse(index.text)] = float.Parse(speed.text);
				property.lifeList[int.Parse(index.text)] = int.Parse(life.text);
				property.enemyList[int.Parse(index.text)] = name.text;
				property.pathList[int.Parse(index.text)] = path.text;
			}
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
#endif
