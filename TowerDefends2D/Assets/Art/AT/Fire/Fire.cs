/*
 	About Fire life (used to object pooling) depending of device plataform
 */


using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {
	//Animator anim;
	void OnEnable(){
		if (Application.platform == RuntimePlatform.Android){
			Invoke("Destroy",0.3f);
		}else{
			Invoke("Destroy",1f);
		}
	}

	void Destroy(){
		this.gameObject.SetActive(false);
	}

}