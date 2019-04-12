using UnityEngine;
using System.Collections;

public class MainMenu_Controller : MonoBehaviour {
	public Sprite StartClicked;
	private Sprite auxClicked;
	private GameObject title;
	private GameObject pressStart;
	private GameObject start;
	private bool auxpress=false;
	private bool move=false;
	private bool auxmove=false;
	private Vector3 firstpos;
	// Use this for initialization


	void Start () {
		title=GameObject.Find("Title");
		pressStart=GameObject.Find("pressStart");
		//start=GameObject.Find("Start");
		//auxClicked = start.GetComponent<SpriteRenderer>().sprite;
		firstpos = title.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		/*if (Input.GetButtonDown("Jump")||Input.GetKeyDown(KeyCode.Return)){
			start.GetComponent<SpriteRenderer>().sprite = StartClicked;
		}
		if (Input.GetButtonUp("Jump")||Input.GetKeyUp(KeyCode.Return)){
			start.GetComponent<SpriteRenderer>().sprite = auxClicked;
			Application.LoadLevel("Example_Scene");
		}*/

		if(move==false){
			move=true;
			auxmove=!auxmove;
			Invoke ("MoveTitle",3f);
		}
		if(auxpress==false){
			auxpress=true;
			Invoke("setPress",1);
		}
	}

	private void MoveTitle(){
		move=false;
		if(auxmove==true){
			title.transform.position = new Vector3(title.transform.position.x,title.transform.position.y-0.05f,title.transform.position.z);
		}else{
			title.transform.position = firstpos;
		}
	}

	private void setPress(){
		auxpress=false;
		pressStart.GetComponent<SpriteRenderer>().enabled=!pressStart.GetComponent<SpriteRenderer>().enabled;
	}
}
