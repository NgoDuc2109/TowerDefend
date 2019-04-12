using UnityEngine;
using System.Collections;
using FThLib;

public class Mt_Buttons : MonoBehaviour {
	public Sprite clicked;
	//Private
	private bool mouseover=false;
	private Sprite aux;
	private Master_Instance masterPoint;
	//Arrow improvable properties
	private float s_timer = 0.4f; // Ratio, time between shots
	//private int accuracy_mode = 1;// 1 the best, (used to correct the trajectory)(values > 0), 
	private int damage = 5; //Upgrade damage 3 -> 5

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
			masterPoint = GameObject.Find("Instance_Point").GetComponent<Master_Instance>();
			if(masterPoint.countMoney()<masterPoint.getPrice(this.gameObject)){master.isHide(this.gameObject);}//---------------------------------prices
			if(this.gameObject.name=="Trap"){
				enableTrap();
			}
			aux = GetComponent<SpriteRenderer>().sprite;
			master.setLayer("interface",this.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(this.gameObject.name=="Trap"){
			enableTrap();
		}
		if(this.gameObject.GetComponent<SpriteRenderer>().material.color.a!=0.5f){//---------------------------------prices
			if (Input.GetMouseButtonDown(0)&&mouseover==true){
					GetComponent<SpriteRenderer>().sprite = clicked;
				masterPoint.anybuttonclicked=true;
			}
			if (Input.GetMouseButtonUp(0)&&GetComponent<SpriteRenderer>().sprite==clicked){
				master.showHand(false);
				masterPoint.removeMoney(masterPoint.getPrice(this.gameObject));//---------------------------------prices
				GetComponent<SpriteRenderer>().sprite = aux;
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
			actionEnd();}
		if(this.gameObject.name=="MTFire"){
			this.gameObject.transform.parent.transform.parent.GetComponent<MT_Controller>().fire=true;
			this.gameObject.transform.parent.transform.parent.GetComponent<MT_Controller>().Damage_ = damage;
			actionEnd();
		}
		if(this.gameObject.name=="Ice"){
			this.gameObject.transform.parent.transform.parent.GetComponent<MT_Controller>().ice=true;
			actionEnd();
		}
		if(this.gameObject.name=="Trap"){
			if(master.getChildFrom("trap_",this.transform.parent.transform.parent.gameObject)==null){
				showTrap(true);
				this.gameObject.transform.parent.transform.parent.GetComponent<MT_Controller>().trap=true;

			}
			actionEnd();
		}
	}

	private void actionEnd(){
		this.gameObject.transform.parent.transform.parent.gameObject.GetComponent<Buttons_Clicked>().addButton(this.gameObject.name);//--------------------------
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

	public void enableTrap(){
		if(master.getChildFrom("trap_",this.gameObject.transform.parent.transform.parent.gameObject)){
			Color col = this.gameObject.GetComponent<SpriteRenderer>().material.color;
			col.a = 0.5f;
			this.gameObject.GetComponent<SpriteRenderer>().material.color = col;
		}else{Color col = this.gameObject.GetComponent<SpriteRenderer>().material.color;
			col.a = 1f;
			this.gameObject.GetComponent<SpriteRenderer>().material.color = col;
		}

	}

	public void showTrap(bool value){//show hand cursor
		if(value==true){
			Cursor.visible = false;
			if(Camera.main){
				Vector3 aux = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				aux.z=-0.5f;
				GameObject trap_ = Instantiate(Resources.Load("Cursor/trap"), aux , Quaternion.identity)as GameObject;
				trap_.name="trap_";
				trap_.gameObject.transform.parent = this.transform.parent.transform.parent.gameObject.transform;
			}else{
				Debug.Log("errr");
			}
		}else{
			Cursor.visible = true;
			Destroy (GameObject.Find("trap"));
		}
	}
}
