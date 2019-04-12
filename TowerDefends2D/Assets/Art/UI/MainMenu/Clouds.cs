using UnityEngine;
using System.Collections;

public class Clouds : MonoBehaviour {
	GameObject spawner;
	// Use this for initialization
	void Start () {
		spawner = GameObject.Find("spawner");
	}
	void OnTriggerEnter2D(Collider2D coll) {
		if(coll.name=="end"){
			GameObject cloud = Instantiate(Resources.Load("MainMenu/clouds"), new Vector3(spawner.transform.position.x,spawner.transform.position.y,spawner.transform.position.y), Quaternion.identity)as GameObject;
			Destroy (this.gameObject);
		}
	}
	// Update is called once per frame
	void Update () {
		transform.position = Vector2.MoveTowards (this.transform.position, GameObject.Find("end").transform.position, Time.deltaTime/3);
	}
}
