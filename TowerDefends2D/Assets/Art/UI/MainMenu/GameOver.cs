using UnityEngine;
using System.Collections;
using FThLib;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Jump")||Input.GetKeyDown(KeyCode.Return)){
			master.getChildFrom("GameOver0",this.gameObject).GetComponent<SpriteRenderer>().enabled=false;
			master.getChildFrom("GameOver1",this.gameObject).GetComponent<SpriteRenderer>().enabled=true;
		}
		if (Input.GetButtonUp("Jump")||Input.GetKeyUp(KeyCode.Return)){
			master.getChildFrom("GameOver0",this.gameObject).GetComponent<SpriteRenderer>().enabled=true;
			master.getChildFrom("GameOver1",this.gameObject).GetComponent<SpriteRenderer>().enabled=false;
			Application.LoadLevel("MainMenu");
		}
	}
}
