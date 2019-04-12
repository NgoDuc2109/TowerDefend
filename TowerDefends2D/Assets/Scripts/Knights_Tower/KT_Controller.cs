using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using FThLib;

public class KT_Controller : MonoBehaviour {
	//Public
	public List<GameObject> enemies;
	public Sprite lvl2;
	public Sprite block;
	//--Private
	private int towerlvl = 0;
	private float instancetime = 11f;//######################### get
	private GameObject door=null;
	private GameObject opened=null;
	private GameObject closed=null;
	private GameObject spawner=null;
	private GameObject flag=null;
	private GameObject zone=null;
	private bool mouseover=false;
	//--About door
	private bool opening=false;
	private bool closing=false;
	private bool inprocess=false;
	private bool firstime=true;
	private int a=0;
	//About knights
	public int damage = 3;
	public int life = 20;
	public bool shield =false;
	private bool off=false; 		//Sorcerer can turn off
	// Use this for initialization
	void OnMouseOver(){ 
		if(!GameObject.Find("hand")){master.showHand (true);}
		mouseover=true;
	}
	
	void OnMouseExit(){
		if(GameObject.Find("hand")){master.showHand (false);}
		mouseover=false;
	}
	void OnTriggerEnter2D(Collider2D coll) {
		if(coll.name=="Knight1"||coll.name=="Knight2"||coll.name=="Knight3"){
			StartCoroutine(setZ(coll.gameObject, 1f));
		}
	}
	private void Init_(){
		door = master.getChildFrom("door",this.gameObject);
		opened = master.getChildFrom("opened",this.gameObject);
		closed = master.getChildFrom("closed",this.gameObject);
		spawner = master.getChildFrom("spawner",this.gameObject);
		flag = master.getChildFrom("flag",this.gameObject);
		zone = master.getChildFrom("zone",this.gameObject);
	}
	void Start () {
		this.transform.position = master.setThisZ(this.transform.position,0.02f);
		master.setLayer("tower",this.gameObject);
		setFlagZ();
		Init_();
	}

	void Update () {
		if(!master.isFinish()){
			if(master.getChildFrom("Interface",this.gameObject)==null&&!GameObject.Find("circle")){
				master.getChildFrom("zoneImg",this.gameObject).GetComponent<SpriteRenderer>().enabled=false;
				GetComponent<CircleCollider2D>().enabled=true;
			}
			if (Input.GetMouseButtonDown(0)&&mouseover==true){
				master.showInterface(this.gameObject.name,this.gameObject,zone.transform);
				GetComponent<CircleCollider2D>().enabled=false;
				master.getChildFrom("zoneImg",this.gameObject).GetComponent<SpriteRenderer>().enabled=true;
			}
			if(inprocess==false){knightCall();}//progressbar included
			doorisdoing();
			remove_null();
			if(enemies.Count>0){getEnemy();}//If enemy on area and no fighting, call a knight
		}
	}

	public void Turn_Off(){
		off=true;
		//ProgressBar
		foreach(Transform child in gameObject.transform){if(child.name=="ProgressBar"){Destroy(child.gameObject);}}
		CancelInvoke("Instantiate_Knight");
		inprocess=false;
		Invoke("Turn_On",GameObject.Find("Instance_Point").GetComponent<Master_Instance>().Sorcerer_Runes_Time);
	}
	private void Turn_On(){
		off=false;
	}

	void getEnemy(){
		for(int i=0; i<enemies.Count ;i++){
			if(enemies[i]!=null){
				PathFollower enemyProperties = enemies[i].GetComponent<PathFollower>();
				if (enemyProperties.fighting==false){
					enemyProperties.target=getKnight(enemies[i]);
					if(enemyProperties.target!=null){//if get a knight set enemy to fighting
						enemyProperties.fighting=true;
					}else{
						enemyProperties.fighting=false;
					}
				}
			}
		}
	}

	GameObject getKnight(GameObject target){//Knight stoped and no fighting
		GameObject aux = null;
		Knights_Controller k1properties;
		Knights_Controller k2properties;
		Knights_Controller k3properties;
		bool k1 =false;
		bool k2 =false;
		bool k3 =false;
		if(master.getChildFrom ("Knight1",this.gameObject)!=null){
			k1properties = master.getChildFrom("Knight1",this.gameObject).GetComponent<Knights_Controller>();
			k1 = knightCanFight("Knight1", target);
		}
		if(master.getChildFrom ("Knight2",this.gameObject)!=null&&k1==false){
			k2properties = master.getChildFrom("Knight2",this.gameObject).GetComponent<Knights_Controller>();
			k2 = knightCanFight("Knight2", target);
		}
		if(master.getChildFrom ("Knight3",this.gameObject)!=null&&k1==false&&k2==false){
			k3properties = master.getChildFrom("Knight3",this.gameObject).GetComponent<Knights_Controller>();
			k3 = knightCanFight("Knight3", target);
		}
		if(k1 == true){
			aux = master.getChildFrom ("Knight1",this.gameObject);
		}else{
			if(k2==true){
				aux = master.getChildFrom ("Knight2",this.gameObject);
			}else{
				if(k3==true){aux = master.getChildFrom ("Knight3",this.gameObject);}
			}
		}
		return aux;
	}


	public void setDamage(){
		if(master.getChildFrom("Knight1",this.gameObject)){
			master.getChildFrom("Knight1",this.gameObject).GetComponent<Knights_Controller>().damage=damage;
		}
		if(master.getChildFrom("Knight2",this.gameObject)){
			master.getChildFrom("Knight2",this.gameObject).GetComponent<Knights_Controller>().damage=damage;
		}
		if(master.getChildFrom("Knight3",this.gameObject)){
			master.getChildFrom("Knight3",this.gameObject).GetComponent<Knights_Controller>().damage=damage;
		}
	}

	public void setShield(){
		shield = true;
		if(master.getChildFrom("Knight1",this.gameObject)){
			master.getChildFrom("Knight1",this.gameObject).GetComponent<Knights_Controller>().shield=true;
			master.getChildFrom("Knight1",this.gameObject).GetComponent<Knights_Controller>().resetLife(life);
		}
		if(master.getChildFrom("Knight2",this.gameObject)){
			master.getChildFrom("Knight2",this.gameObject).GetComponent<Knights_Controller>().shield=true;
			master.getChildFrom("Knight2",this.gameObject).GetComponent<Knights_Controller>().resetLife(life);
		}
		if(master.getChildFrom("Knight3",this.gameObject)){
			master.getChildFrom("Knight3",this.gameObject).GetComponent<Knights_Controller>().shield=true;
			master.getChildFrom("Knight3",this.gameObject).GetComponent<Knights_Controller>().resetLife(life);
		}
	}

	public void Reset(){
		master.getChildFrom("TargetedZone",this.gameObject).transform.position = flag.transform.position;
		if(enemies.Count>0){
			for(int i=0; i<enemies.Count ;i++){
				PathFollower enemyProperties = enemies[i].GetComponent<PathFollower>();
				if (enemyProperties.fighting==true){
					enemyProperties.target=null;
					enemyProperties.fighting=false;
				}
				enemyRemove(enemies[i].name);
			}
		}
		if(master.getChildFrom("Knight1",this.gameObject)){
			Knights_Controller properties = master.getChildFrom("Knight1",this.gameObject).GetComponent<Knights_Controller>();
			if(properties.fighting==true&&properties.target!=null){
				properties.target.GetComponent<PathFollower>().fighting=false;
				properties.target.GetComponent<PathFollower>().target = null;
			}
			properties.fighting=false;
			properties.target=null;
		}
		if(master.getChildFrom("Knight2",this.gameObject)){
			Knights_Controller properties = master.getChildFrom("Knight2",this.gameObject).GetComponent<Knights_Controller>();
			if(properties.fighting==true&&properties.target!=null){
				properties.target.GetComponent<PathFollower>().fighting=false;
				properties.target.GetComponent<PathFollower>().target = null;
			}
			properties.fighting=false;
			properties.target=null;
		}
		if(master.getChildFrom("Knight3",this.gameObject)){
			Knights_Controller properties = master.getChildFrom("Knight3",this.gameObject).GetComponent<Knights_Controller>();
			if(properties.fighting==true&&properties.target!=null){
				properties.target.GetComponent<PathFollower>().fighting=false;
				properties.target.GetComponent<PathFollower>().target = null;
			}
			properties.fighting=false;
			properties.target=null;
		}
	}

	// Update is called once per frame
	bool knightCanFight(string name, GameObject target){
		bool aux = false;
		Knights_Controller kproperties = master.getChildFrom(name,this.gameObject).GetComponent<Knights_Controller>();
		if(kproperties.fighting==false){
			kproperties.fighting=true;
			kproperties.target=target;
			kproperties.move=true;
			aux = true;
		}
		return aux;
	}

	private void knightCall(){//Instantiate
		if(off==false){
			if(master.getChildFrom("Knight1",this.gameObject)&&master.getChildFrom("Knight2",this.gameObject)&&master.getChildFrom("Knight3",this.gameObject)){
				firstime=false;
			}else{
				if(firstime==true){//Fist time respawn is better
					inprocess=true;
					master.Instantiate_Progressbar(4f,this.gameObject);
					Invoke ("Instantiate_Knight",4f);
				}else{
					inprocess=true;
					master.Instantiate_Progressbar(instancetime,this.gameObject);
					Invoke ("Instantiate_Knight",instancetime);
				}
			}
		}
	}

	private void Instantiate_Knight(){
		inprocess=false;
		if(off==false){
			GameObject Knight = Instantiate(Resources.Load("Kt/Knight"), new Vector3(spawner.transform.position.x,spawner.transform.position.y,spawner.transform.position.y), Quaternion.identity)as GameObject;
			Knight.transform.SetParent(this.gameObject.transform);
			opening = true;
			Knights_Controller KnightProperties = Knight.GetComponent<Knights_Controller>();
			KnightProperties.flag=flag;
			KnightProperties.life=life;
			KnightProperties.shield = shield;
			KnightProperties.damage=damage;
			Knight.name=getKnightName();
		}
	}

	private string getKnightName(){
		string aux_ = "";
		if(master.getChildFrom("Knight1",this.gameObject)){
			if(master.getChildFrom("Knight2",this.gameObject)){
				if(master.getChildFrom("Knight3",this.gameObject)){
				}else{
					aux_="Knight3";
				}
			}else{
				aux_="Knight2";
			}
		}else{
			aux_="Knight1";
		}
		return aux_;
	}

	private IEnumerator  setZ(GameObject go, float delayTime){
		yield return new WaitForSeconds(delayTime);
		go.transform.position = new Vector3(go.transform.position.x,go.transform.position.y,0f);
	}

	//About list
	void remove_null(){for(int i=0; i<enemies.Count ;i++){if(enemies[i]==null){enemies.RemoveAt(i);}}}
	public void enemyAdd(GameObject other){enemies.Add (other);}
	public void enemyRemove(string other){
		for(int i=0; i<enemies.Count ;i++){
			if(enemies[i]!=null){
				if(enemies[i].name==other){enemies.RemoveAt(i);}
			}
		}
	}
	//###############--About door
	private void doorisdoing(){
		if(opening==true){
			getOpen (0);
		}else{
			if(closing==true){getOpen(1);}
		}
	}	
	private void getOpen(int value){//0 open, 1 close
		switch(value){
		case 0:
			if(door.transform.position != opened.transform.position){
				door.transform.position = Vector3.MoveTowards(door.transform.position, opened.transform.position, Time.deltaTime/4);
			}else{
				opening=false;
				Invoke("setclosing",2);
			}
			break;
		case 1:
			if(door.transform.position != closed.transform.position){
				door.transform.position = Vector3.MoveTowards(door.transform.position, closed.transform.position, Time.deltaTime/4);
			}else{
				closing=false;
			}
			break;
		}
	}
	private void setFlagZ(){master.getChildFrom("flag",this.gameObject).transform.position=new Vector3(master.getChildFrom("flag",this.gameObject).transform.position.x,master.getChildFrom("flag",this.gameObject).transform.position.y,master.getChildFrom("flag",this.gameObject).transform.position.y+0.2f);}
	private void setclosing(){closing=true;}
	//--End about door
}
