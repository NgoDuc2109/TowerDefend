using UnityEngine;
using System.Collections;

public class Blood_Destroy : MonoBehaviour {

	// Use this for initialization
	void Start () {

		Invoke("onDestroy",1);
	}
	void onDestroy(){
		Destroy(this.gameObject);
	}
	// Update is called once per frame
	//void Update () {
	//	this.gameObject.transform.parent = GameObject.Find("Environment").transform;
	//}
}
