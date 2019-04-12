using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using FThLib;

public class MiniKT_Controller : MonoBehaviour {
	//Public
	public List<GameObject> enemies;
	public Sprite lvl2;
	//--Private
	//private Master_Instance master;
	private int towerlvl = 0;
	private float instancetime = 11f;//Time to be reusable
	private GameObject flag=null;
	private int damage = 2;
	private int life = 20;
	private int a=0;
	//About knights
	public bool shield =false;
	// Use this for initialization

	void Start () {
		//master = GameObject.Find("Instance_Point").GetComponent<Master_Instance>();
		master.setLayer("tower",this.gameObject);
		Init_();
		setKnights();
	}

	private void setKnights(){
		master.getChildFrom("Knight1",this.gameObject).GetComponent<Knights_Controller>().life=life;
		master.getChildFrom("Knight1",this.gameObject).GetComponent<Knights_Controller>().damage=damage;
		master.getChildFrom("Knight2",this.gameObject).GetComponent<Knights_Controller>().life=life;
		master.getChildFrom("Knight2",this.gameObject).GetComponent<Knights_Controller>().damage=damage;
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

	// Update is called once per frame
	void Update () {
		if(!master.isFinish()){
			if(master.getChildFrom("Knight1",this.gameObject)==null&&master.getChildFrom("Knight2",this.gameObject)==null){
				Destroy (this.gameObject);
			}else{
				remove_null();
				if(enemies.Count>0){getEnemy();}//If enemy on area and no fighting, call a knight
			}
		}
	}

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
		if(k1 == true){
			aux = master.getChildFrom ("Knight1",this.gameObject);
		}else{
			if(k2==true){
				aux = master.getChildFrom ("Knight2",this.gameObject);
			}
		}
		return aux;
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

	private void Init_(){
		flag = master.getChildFrom("flag",this.gameObject);
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
	
}
