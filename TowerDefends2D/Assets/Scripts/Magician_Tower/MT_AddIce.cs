using UnityEngine;
using System.Collections;

public class MT_AddIce: MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//transform.RotateAround(this.gameObject.transform.parent.transform.position, Vector3.forward, Time.deltaTime/100);
		CreateFire();
	}
	void CreateFire(){
		GameObject fire_ = Instantiate(Resources.Load("Global/Ice"), this.transform.position, Quaternion.identity)as GameObject;
		Vector3 scale = fire_.transform.localScale;
		scale.x = scale.x *1.5f;
		scale.y = scale.y *1.5f;
		fire_.transform.localScale = scale;
		
	}
}
