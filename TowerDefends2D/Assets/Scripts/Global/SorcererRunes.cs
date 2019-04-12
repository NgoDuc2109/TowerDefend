using UnityEngine;
using System.Collections;

public class SorcererRunes : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("Destroy_",GameObject.Find("Instance_Point").GetComponent<Master_Instance>().Sorcerer_Runes_Time);
	}
	
	// Update is called once per frame
	void Update () {
		if(this.gameObject.name=="Ring"){
			transform.Rotate(0, 0, Time.deltaTime*10);
		}
	}

	private void Destroy_(){
		Destroy(this.gameObject);
	}
}
