using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using FThLib;

public class AT_Controller : MonoBehaviour {
	//--Public
	public List<GameObject> enemies;
	public Sprite block;
	//--Private
	private GameObject zone=null;
	private GameObject spawner=null;
	public bool shot_ = false;
	private bool mouseover=false;
	private float searchvalue = 0.1f;// down value is best... but the performance may be affected (used to detect min speed to hit the enemy)
	//Public properties
	public float s_timer = 0.9f;
	public int accuracy_mode = 4;//Bassically it is used by the bullet, and add a force in direction to the target.
	public int Damage_ = 1;
	public bool fire = false;
	private bool off=false;		//Sorcerer can turn off
	
	void OnMouseOver(){ 
		if(!GameObject.Find("hand")){master.showHand (true);}
		mouseover=true;
	}

	void OnMouseExit(){
		if(GameObject.Find("hand")){master.showHand (false);}
		mouseover=false;
	}

	void Start () {
		this.transform.position = master.setThisZ(this.transform.position,0.02f);
		spawner = master.getChildFrom("spawner",this.gameObject);
		zone = master.getChildFrom("zone",this.gameObject);
		master.setLayer("tower",this.gameObject);
	}

	// Update is called once per frame
	void Update () {
		if(!master.isFinish()){
			if(master.getChildFrom("Interface",this.gameObject)==null){
				master.getChildFrom("zoneImg",this.gameObject).GetComponent<SpriteRenderer>().enabled=false;
				GetComponent<CircleCollider2D>().enabled=true;
			}
			if (Input.GetMouseButtonDown(0)&&mouseover==true){
					master.showInterface(this.gameObject.name,this.gameObject,zone.transform);
					GetComponent<CircleCollider2D>().enabled=false;
					master.getChildFrom("zoneImg",this.gameObject).GetComponent<SpriteRenderer>().enabled=true;
			}
			remove_null();
			if(enemies.Count>0){
				if(shot_==false){
					shot_=true;
					Invoke("shot",s_timer);
				}
			}
		}
	}

	public void Turn_Off(){
		off=true;
		Invoke("Turn_On",GameObject.Find("Instance_Point").GetComponent<Master_Instance>().Sorcerer_Runes_Time);
	}
	private void Turn_On(){
		off=false;
	}
	private void shot(){
		shot_=false;
		if(off==false){
			if(enemies.Count>0){Instantiate_Bullet();}
		}
	}

	private void Instantiate_Bullet(){
		GameObject Bullet = Instantiate(Resources.Load("AT/arrow"), new Vector3(spawner.transform.position.x,spawner.transform.position.y,spawner.transform.position.z), Quaternion.identity)as GameObject;
		Parabolic_shot_Controller BulletProperties = Bullet.GetComponent<Parabolic_shot_Controller>();
		Bullet.GetComponent<Damage>().Damage_ = Damage_;
		//############# Bullet properties --
		BulletProperties.target = enemies[enemies.Count-1];
		if(enemies[0]!=null){
			BulletProperties.maxLaunch = getminSpeed((int)master.angle_(spawner.transform.position,enemies[0].transform.position));
		}else{
			Destroy(Bullet);
		}

		BulletProperties.accuracy_mode=accuracy_mode;
		BulletProperties.fire = fire;
		Bullet.name="Arrow";
	}
	private float getminSpeed(int angle){
		float aux = 0.1f;
		while(moreSpeed(aux)==true){aux = aux + searchvalue;}
		return aux;
	}

	private bool moreSpeed(float speed){
		bool aux = false;
		float xTarget = enemies[0].transform.position.x;
		float yTarget = enemies[0].transform.position.y; 
		float xCurrent = transform.position.x;
		float yCurrent = transform.position.y;
		float xDistance = Math.Abs(xTarget - xCurrent);
		float yDistance = yTarget - yCurrent;
		float fireAngle = 1.57075f - (float)(Math.Atan((Math.Pow(speed, 2f)+ Math.Sqrt(Math.Pow(speed, 4f) - 9.8f * (9.8f * Math.Pow(xDistance, 2f) + 2f * yDistance * Math.Pow(speed, 2f) )))/(9.8f * xDistance)));
		float xSpeed = (float)Math.Sin(fireAngle) * speed;
		float ySpeed = (float)Math.Cos(fireAngle) * speed;
		if ((xTarget - xCurrent) < 0f){xSpeed = - xSpeed;}
		if(float.IsNaN(xSpeed)||float.IsNaN(ySpeed)){aux = true;}
		return aux;
	}

	//--About enemy list
	public void enemyAdd(GameObject other){enemies.Add (other);}
	public void enemyRemove(string other){
		for(int i=0; i<enemies.Count ;i++){
			if(enemies[i]!=null){
				if(enemies[i].name==other){enemies.RemoveAt(i);}
			}
		}
	}
	void remove_null(){for(int i=0; i<enemies.Count ;i++){if(enemies[i]==null){enemies.RemoveAt(i);}}}

}
