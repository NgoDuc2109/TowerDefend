using UnityEngine;
using System.Collections;
using FThLib;

public class Kt_Buttons : MonoBehaviour {
	public Sprite clicked;
	//Private
	private bool mouseover=false;
	private Sprite aux;
	private Master_Instance masterPoint;
	//About actions
	private int life = 25; //Upgrade life 20 -> 25
	private int damage = 5; //Upgrade damage 3 -> 5
	KT_Controller instancer;

	void OnMouseOver(){ 
		if(!GameObject.Find("hand")&&!GameObject.Find("circle")){master.showHand (true);}
		mouseover=true;
	}
	
	void OnMouseExit(){
		if(GameObject.Find("hand")){master.showHand (false);}
		mouseover=false;
	}
	// Use this for initialization
	void Start () {
		if(this.gameObject.transform.parent.transform.parent.gameObject.GetComponent<Buttons_Clicked>().isClicked(this.gameObject.name)==true){//---------------------------------
			GameObject unclickeable = Instantiate(Resources.Load("Buttons/Unclickeable"), this.transform.position , Quaternion.identity)as GameObject;
			unclickeable.transform.parent = this.transform.parent;
			Destroy(this.gameObject);
		}else{
			instancer = this.gameObject.transform.parent.transform.parent.GetComponent<KT_Controller>();
			masterPoint = GameObject.Find("Instance_Point").GetComponent<Master_Instance>();
			if(masterPoint.countMoney()<masterPoint.getPrice(this.gameObject)){master.isHide(this.gameObject);}//---------------------------------prices
			aux = GetComponent<SpriteRenderer>().sprite;
			master.setLayer("interface",this.gameObject);
		}
	}
	
	// Update is called once per frame
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
		if(this.gameObject.name=="Damage"){
			instancer.damage = damage;
			instancer.setDamage();
		}
		if(this.gameObject.name=="Life"){
			this.gameObject.transform.parent.transform.parent.gameObject.GetComponent<SpriteRenderer>().sprite = this.gameObject.transform.parent.transform.parent.gameObject.GetComponent<KT_Controller>().lvl2;
			instancer.life=life;
			instancer.setShield();
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
