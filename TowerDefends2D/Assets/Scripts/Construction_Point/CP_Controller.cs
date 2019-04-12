using UnityEngine;
using System.Collections;
using FThLib;

public class CP_Controller : MonoBehaviour {
	private bool mouseover=false;
	public bool clicked =false;


	void OnMouseOver(){ 
		if(!GameObject.Find("hand")){master.showHand (true);}
		mouseover=true;
	}
	
	void OnMouseExit(){
		if(GameObject.Find("hand")){master.showHand (false);}
		mouseover=false;
	}
	// Use this for initialization
	void Start () {
		this.transform.position = master.setThisZ(this.transform.position,0.02f);
		this.gameObject.name="CP";
		master.setLayer("interface",this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if(!master.isFinish()){
			if(master.getChildFrom("Interface",this.gameObject)==null&&clicked==false){GetComponent<CircleCollider2D>().enabled=true;}
			if (Input.GetMouseButtonDown(0)&&mouseover==true){
				master.showInterface(this.gameObject.name,this.gameObject,this.gameObject.transform);
				GetComponent<CircleCollider2D>().enabled=false;
			}
		}
	}
}
