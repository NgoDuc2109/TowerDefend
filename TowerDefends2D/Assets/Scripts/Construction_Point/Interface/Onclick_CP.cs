using UnityEngine;
using System.Collections;
using FThLib;
//This script is used on KT interface
public class Onclick_CP : MonoBehaviour {
	public Sprite clicked;
	public GameObject parent_;
	private Sprite aux;
	private bool mouseover=false;
	private Master_Instance masterPoint;

	// Use this for initialization
	void Start () {
		parent_=this.gameObject.transform.parent.transform.parent.gameObject;
		masterPoint = GameObject.Find("Instance_Point").GetComponent<Master_Instance>();
		if(masterPoint.countMoney()<masterPoint.getPrice(this.gameObject)){master.isHide(this.gameObject);}//---------------------------------prices
		aux = GetComponent<SpriteRenderer>().sprite;
		master.setLayer("interface",this.gameObject);
	}
	void OnMouseOver(){ 
		if(!GameObject.Find("hand")&&!GameObject.Find("circle")){master.showHand (true);}
		mouseover=true;
	}
	
	void OnMouseExit(){
		if(GameObject.Find("hand")){master.showHand (false);}
		mouseover=false;
	}

	// Update is called once per frame
	void Update () {

		if(this.gameObject.GetComponent<SpriteRenderer>().material.color.a!=0.5f){//---------------------------------prices
			if (Input.GetMouseButtonDown(0)&&mouseover==true){
				GetComponent<SpriteRenderer>().sprite = clicked;
				masterPoint.anybuttonclicked=true;
				this.gameObject.transform.parent.transform.parent.gameObject.GetComponent<CP_Controller>().clicked=true;
			}
			if (Input.GetMouseButtonUp(0)&&GetComponent<SpriteRenderer>().sprite==clicked){
				master.showHand(false);
				GetComponent<SpriteRenderer>().sprite = aux;
				masterPoint.removeMoney(masterPoint.getPrice(this.gameObject));//---------------------------------prices
				createTower();
			}
		}else{
			if (Input.GetMouseButtonDown(0)&&mouseover==true){//---------------------------------prices
				master.showHand(false);
			}
		}
		if (Input.GetMouseButtonDown(0)&&mouseover==false&&this.gameObject.GetComponent<SpriteRenderer>().enabled==true){
			Invoke ("mouseDownDelay",0.01f);
		}

	}

	void Instantiate_Tower(){
		Vector3 cpos = parent_.transform.position;
		cpos.y = cpos.y + 0.1f;
		GameObject Tower = Instantiate(Resources.Load(this.gameObject.name + "/" + this.gameObject.name + "0"), cpos, Quaternion.identity)as GameObject;
		Tower.name = this.gameObject.name + 0;
	}

	void createTower(){
		this.gameObject.transform.parent.gameObject.name="Loading";
		master.Instantiate_Progressbar(4f,this.gameObject.transform.parent.transform.parent.gameObject);
		masterPoint.anybuttonclicked=false;
		removeChildren();
		Invoke("onDestroy",4f);
	}

	void mouseDownDelay(){
		if(masterPoint.anybuttonclicked==false){
			this.gameObject.transform.parent.transform.parent.GetComponent<CircleCollider2D>().enabled=true;
			Destroy (this.gameObject.transform.parent.gameObject);
		}
	}

	private void removeChildren(){
		parent_.GetComponent<CircleCollider2D>().enabled=false;
		foreach(Transform child in gameObject.transform.parent.transform){
			child.gameObject.GetComponent<SpriteRenderer>().enabled=false;
			child.gameObject.GetComponent<BoxCollider2D>().enabled=false;
		}
		this.gameObject.transform.parent.gameObject.GetComponent<SpriteRenderer>().enabled=false;
	}

	private void onDestroy(){
		Instantiate_Tower();
		Destroy (parent_);
		Destroy (this.gameObject.transform.parent.gameObject);
	}
}
