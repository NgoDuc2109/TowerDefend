/*
    Waves creator
 	Here action when finish the scene, by default enable "UI_Exit" canvas.
*/


using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using FThLib;

public class Waves_Creator_Controller : MonoBehaviour {
	public GameObject customWavePrefab;
	private Master_Instance masterPoint;
	private int wavesIndex = 0; // Number of waves
	//public int[] enemiesInWaves    = new int[] {5,  4};//The last object in array is the first to appear
	private int[] delayBetweenWaves;// = new int[] {20, 0};//The last object in array is the first to appear
	//public GameObject[][] enemiesInWaves_;
	private bool playing=false;
	private bool auxplaying=false;
	public bool end=false;
	private bool sw=false;
	private Text waves;
	// Use this for initialization
	void Start () {
		wavesIndex=customWavePrefab.GetComponentsInChildren<Transform>().Length - 1;
		delayBetweenWaves = new int[customWavePrefab.GetComponentsInChildren<Transform>().Length - 1];
		Transform[] aux = customWavePrefab.GetComponentsInChildren<Transform>();
		for(int i = 0; i< delayBetweenWaves.Length;i++){
			delayBetweenWaves[i] = aux[i+1].GetComponent<waves>().delay;
		}
		masterPoint = GameObject.Find("Instance_Point").GetComponent<Master_Instance>();
		waves = GameObject.Find("Waves").GetComponent<Text>();
		waves.text = wavesIndex + "/" + (customWavePrefab.GetComponentsInChildren<Transform>().Length - 1);
		wavesIndex--;

	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<Master_Instance>().Finish==false){
			if (Input.GetButtonDown("Jump")){
				StartPlaying();
			}
			if(playing==true&&wavesIndex>=0){
				if(auxplaying==false){
					auxplaying=true;
					if(delayBetweenWaves[wavesIndex]>0){
						master.Instantiate_Progressbar(delayBetweenWaves[wavesIndex],this.gameObject);
						master.getChildFrom("ProgressBar",this.gameObject).transform.localScale=new Vector3(2,2,1);
					}
					Invoke("Wave_Creator",delayBetweenWaves[wavesIndex]);
				}
			}
			if(wavesIndex<0){playing=false;}
		}
		if(wavesIndex<0&&playing==false&&end==false&&GameObject.FindGameObjectsWithTag("Respawn").Length==0){end=true;}	//Finish the game, you win
		if(end==true&&sw==false){								
			sw=true;
			Invoke("FinishScene",3f);									//Load finish event
		}
	}
		
	public void StartPlaying(){
		Destroy (GameObject.Find("pressSpace"));
		playing=true;
	}

	public void Exit(){Invoke("CrossfadeDelayed",0.5f);}

	private void CrossfadeDelayed(){
		Debug.Log("clicked");
		GameObject.Find("Crossfade").GetComponent<Animator>().SetBool("out",true);
		Invoke("ExitDelayed",2);
	}

	private void ExitDelayed(){Application.Quit(); }

	/*Creating the wave, calling "CreateWave" on "Master_Instance" ========================================================*/

	private void Wave_Creator(){
		auxplaying=false;

		/*Creating wave with Number of enemies / Enemy type*/
		/*Creating wave by Gameobjects[][] ?????????????????????    masterPoing.createWave(enemyWave[wavesIndex],path)*/
		//masterPoing.createWave(enemyWave[wavesIndex],path,)
		/*Create Editor Gameobjects[][] and save into a folder, then before playing assing it here .... must include "speed", "life" and "path"... Scale???? */


		//masterPoint.createWave(enemiesInWaves[wavesIndex],1);//This pack only contains enemy type 1
		//List<GameObject> children = customWavePrefab.transform.GetChildren();
		if(wavesIndex>=0){
			masterPoint.createWave(customWavePrefab.GetComponentsInChildren<Transform>(),wavesIndex+1);
			waves.text = wavesIndex + "/" + (customWavePrefab.GetComponentsInChildren<Transform>().Length - 1);
			wavesIndex--;
		}
	}
	//=====================================================================================================================

	//Here action when finish =============================================================================================

	private void FinishScene(){
		if(GameObject.FindGameObjectsWithTag("Respawn").Length==0||GameObject.Find("Life").GetComponent<Text>().text=="0"){
			GetComponent<Master_Instance>().wavePlaying=false;
			GetComponent<Master_Instance>().waveCount=0;
			GetComponent<Master_Instance>().Finish=true;
			GameObject.Find("UI_Exit").GetComponent<Canvas>().enabled=true;
			GameObject[] enemies_ = GameObject.FindGameObjectsWithTag("Respawn");
			if(enemies_.Length>0){															//Destroy all enemies
				for(int i=0;i<enemies_.Length;i++){
					Destroy(enemies_[i]);
				}
			}
		}else{
			sw=false;
		}
	}
}
