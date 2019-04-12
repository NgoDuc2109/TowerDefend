using UnityEngine;
using System.Collections;
using FThLib;

public class At_Buttons : MonoBehaviour {
	public Sprite clicked;
	//Private
	private bool mouseover=false;
	private Sprite aux;
	private Master_Instance masterPoint;
	//Arrow improvable properties
	private float s_timer = 0.6f; // Ratio, time between shots
	private int accuracy_mode = 1;// Bassically it is used by the bullet, and add a force in direction to the target.
	//private int Damage_ = 3;
	private int damage = 4; //Upgrade damage, to damage with fire 3 -> 4

	void OnMouseOver(){ 
		if(!GameObject.Find("hand")&&!GameObject.Find("circle")){master.showHand (true);}
		mouseover=true;
	}
	
	void OnMouseExit(){
		if(GameObject.Find("hand")){master.showHand (false);}
		mouseover=false;
	}

	void Start () {
		if(this.gameObject.transform.parent.transform.parent.gameObject.GetComponent<Buttons_Clicked>().isClicked(this.gameObject.name)==true){
			GameObject unclickeable = Instantiate(Resources.Load("Buttons/Unclickeable"), this.transform.position , Quaternion.identity)as GameObject;
			unclickeable.transform.parent = this.transform.parent;
			Destroy(this.gameObject);
		}else{
			masterPoint = GameObject.Find("Instance_Point").GetComponent<Master_Instance>();
			if(masterPoint.countMoney()<masterPoint.getPrice(this.gameObject)){master.isHide(this.gameObject);}//---------------------------------prices
			aux = GetComponent<SpriteRenderer>().sprite;
			master.setLayer("interface",this.gameObject);
		}
	}

	void Update () {
		if(this.gameObject.GetComponent<SpriteRenderer>().material.color.a!=0.5f){//---------------------------------prices
			if (Input.GetMouseButtonDown(0)&&mouseover==true){
				GetComponent<SpriteRenderer>().sprite = clicked;
				masterPoint.anybuttonclicked=true;
			}
			if (Input.GetMouseButtonUp(0)&&GetComponent<SpriteRenderer>().sprite==clicked){
				master.showHand(false);
				GetComponent<SpriteRenderer>().sprite = aux;
				masterPoint.removeMoney(masterPoint.getPrice(this.gameObject));//---------------------------------prices
				action();
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

	private void action(){
		if(this.gameObject.name=="sell"){
			masterPoint.addMoney((int)(masterPoint.getPrice(this.gameObject.transform.parent.transform.parent.gameObject)/3)*2);
			master.sellTower(this.gameObject.transform.parent.transform.parent.gameObject);
		}
		if(this.gameObject.name=="Fire"){
			this.gameObject.transform.parent.transform.parent.GetComponent<AT_Controller>().fire=true;
			this.gameObject.transform.parent.transform.parent.GetComponent<AT_Controller>().Damage_ = damage;
		}
		if(this.gameObject.name=="Ratio"){
			this.gameObject.transform.parent.transform.parent.GetComponent<AT_Controller>().s_timer=s_timer;
		}
		if(this.gameObject.name=="Accuracy"){//It is better to use accuracy after Fire, because final damage result = 5, in other way =4...
			this.gameObject.transform.parent.transform.parent.GetComponent<AT_Controller>().accuracy_mode=accuracy_mode;
			this.gameObject.transform.parent.transform.parent.GetComponent<AT_Controller>().Damage_ =this.gameObject.transform.parent.transform.parent.GetComponent<AT_Controller>().Damage_ + 1;
		}
		this.gameObject.transform.parent.transform.parent.gameObject.GetComponent<Buttons_Clicked>().addButton(this.gameObject.name);
		this.gameObject.transform.parent.transform.parent.GetComponent<CircleCollider2D>().enabled=true;
		master.getChildFrom("zoneImg",this.transform.parent.transform.parent.gameObject).GetComponent<SpriteRenderer>().enabled=false;
		masterPoint.anybuttonclicked=false;
		Destroy (this.gameObject.transform.parent.gameObject);
	}

	void mouseDownDelay(){
		if(masterPoint.anybuttonclicked==false){
			this.gameObject.transform.parent.transform.parent.GetComponent<CircleCollider2D>().enabled=true;
			master.getChildFrom("zoneImg",this.transform.parent.transform.parent.gameObject).GetComponent<SpriteRenderer>().enabled=false;
			Destroy (this.gameObject.transform.parent.gameObject);
		}
	}
}
