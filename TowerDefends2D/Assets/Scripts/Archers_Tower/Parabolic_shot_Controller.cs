using UnityEngine;
using System.Collections;
using System.Collections.Generic;   //------------------------------------------------<<
using System;
using FThLib;

public class Parabolic_shot_Controller : MonoBehaviour {
	//--Public
	public GameObject target=null;
	public int accuracy_mode=3;//1 the best
	public float maxLaunch = 4;
	private bool activated = false;
	//--Private
	private bool sw =false;
	private float launch_placey=0f;
	public bool fire = false;
	private Vector3 latestpos = new Vector3(0,0,0);
	private List<GameObject> firelist;	//--------------------------------------------------<<
	// Use this for initialization
	void Start () {
		firelist = GameObject.Find("Instance_Point").GetComponent<Fire_Pooling>().fireList;  //--------------------<<
		sw=true;
		master.setLayer("tower",this.gameObject);
	}
	
	void OnTriggerEnter2D(Collider2D coll) {
		sw=false;
		GetComponent<Rigidbody2D>().isKinematic=true;
		GetComponent<Collider2D>().enabled=false;
		this.transform.position = master.setThisZ(this.transform.position,0);
		Invoke("onDestroy",1);
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		
	}
	void FixedUpdate(){
		if(sw==true){	
			if (fire==true){
				CreateFire();
			}
		}
	}
	// Update is called once per frame
	void Update () {
		if(target!=null){

			latestpos = target.transform.position;
			if(activated==false){
				activated=true;
				PreLaunch();
			}else{
				if(sw==true){	
					//if (fire==true){CreateFire();}
					if(GetComponent<Rigidbody2D>().isKinematic==false){simulateRotation();}
					transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Time.deltaTime/accuracy_mode);
					if(GetComponent<Rigidbody2D>().velocity.y<0){isFalling();}
					this.transform.position = master.setThisZ(this.transform.position,-0.7f);
				}
			}
		}else{
			find_error();
		}
	}

	void CreateFire(){		//--------------------------------------------------------------------------<<
		for(int i = 0;i<firelist.Count;i++){
			if(!firelist[i].activeInHierarchy){
				firelist[i].transform.localScale= new Vector3(0.03250099f,0.03250099f,0.03250099f);
				firelist[i].transform.position = this.transform.position;
				firelist[i].SetActive(true);
				break;
			}
		}
	}

	void isFalling(){//do when is falling
		float auxv = 0f;
		float speedy = target.GetComponent<Rigidbody2D>().velocity.y;
		float speedx = target.GetComponent<Rigidbody2D>().velocity.x;
		if(Math.Sqrt(speedy*speedy)>Math.Sqrt(speedx*speedx)){
			auxv=speedy;
		}else{
			auxv=speedx;
		}
		if(auxv<0){auxv = -auxv;}
		if(accuracy_mode==1){
			transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Time.deltaTime*1);
		}

		find_error();//Not Hit Detection
	}
	
	private void find_error(){
		if(target!=null){
			if(this.transform.position.y<target.transform.position.y){
				sw=false;
				Vector3 rotation_ = this.gameObject.transform.eulerAngles;
				GetComponent<Rigidbody2D>().isKinematic=true;
				GetComponent<Collider2D>().enabled=false;
				this.gameObject.transform.localEulerAngles = rotation_;
				Invoke("onDestroy",1);
			}
		}else{
			if(this.transform.position.y<latestpos.y){
				sw=false;
				Vector3 rotation_ = this.gameObject.transform.eulerAngles;
				GetComponent<Rigidbody2D>().isKinematic=true;
				GetComponent<Collider2D>().enabled=false;
				this.gameObject.transform.localEulerAngles = rotation_;
				Invoke("onDestroy",1);
			}else{
				if(GetComponent<Rigidbody2D>().isKinematic==false){
					simulateRotation();
				}
			}
		}
	}

	private void Calculation(float speedy){//Air Time
		next_position(Time.time%((speedy/9.81f)*2));
	}
	
	void next_position(float airtime){
		float xTarget = target.transform.position.x;
		float yTarget = target.transform.position.y;
		float speedy = target.GetComponent<Rigidbody2D>().velocity.y;
		float speedx = target.GetComponent<Rigidbody2D>().velocity.x;
		Launch (xTarget+(speedx*airtime),yTarget+(speedy*airtime));
	}
	
	private void Launch(float xTarget, float yTarget){ 
		GetComponent<Rigidbody2D>().isKinematic=false;
		float xCurrent = transform.position.x;
		float yCurrent = transform.position.y;
		float xDistance = Math.Abs(xTarget - xCurrent);
		float yDistance = yTarget - yCurrent;
		float fireAngle = 1.57075f - (float)(Math.Atan((Math.Pow(maxLaunch, 2f)+ Math.Sqrt(Math.Pow(maxLaunch, 4f) - 9.8f * (9.8f * Math.Pow(xDistance, 2f) + 2f * yDistance * Math.Pow(maxLaunch, 2f) )))/(9.8f * xDistance)));
		float xSpeed = (float)Math.Sin(fireAngle) * maxLaunch;
		float ySpeed = (float)Math.Cos(fireAngle) * maxLaunch;
		if ((xTarget - xCurrent) < 0f){xSpeed = - xSpeed;}
		if(!float.IsNaN(xSpeed)&&!float.IsNaN(ySpeed)){
			GetComponent<Rigidbody2D>().velocity = new Vector3(xSpeed,ySpeed,0f);
		}else{
			maxLaunch = maxLaunch + 0.3f;
			PreLaunch();
		}
	}
	
	private void PreLaunch(){ 
		float xTarget = target.transform.position.x;
		float yTarget = target.transform.position.y;
		float xCurrent = transform.position.x;
		float yCurrent = transform.position.y;
		float xDistance = Math.Abs(xTarget - xCurrent);
		float yDistance = yTarget - yCurrent;
		float fireAngle = 1.57075f - (float)(Math.Atan((Math.Pow(maxLaunch, 2f)+ Math.Sqrt(Math.Pow(maxLaunch, 4f) - 9.8f * (9.8f * Math.Pow(xDistance, 2f) + 2f * yDistance * Math.Pow(maxLaunch, 2f) )))/(9.8f * xDistance)));
		float xSpeed = (float)Math.Sin(fireAngle) * maxLaunch;
		float ySpeed = (float)Math.Cos(fireAngle) * maxLaunch;
		if ((xTarget - xCurrent) < 0f){xSpeed = - xSpeed;}
		Calculation (ySpeed);
		sw=true;
	}

	void simulateRotation(){
		Vector3 velocity = GetComponent<Rigidbody2D>().velocity;
		float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
		Quaternion tempRotation = Quaternion.AngleAxis(angle, Vector3.forward);
		Quaternion newRotation = this.gameObject.transform.rotation;
		newRotation.x = Mathf.LerpAngle(newRotation.x, tempRotation.x, 1000000);
		newRotation.y = Mathf.LerpAngle(newRotation.y, tempRotation.y, 1000000);
		newRotation.z = Mathf.LerpAngle(newRotation.z, tempRotation.z, 1000000);
		newRotation.w = Mathf.LerpAngle(newRotation.w, tempRotation.w, 1000000);
		this.gameObject.transform.rotation = newRotation;
	}

	private GameObject getChild(string name){
		GameObject aux=null;
		foreach(Transform child in gameObject.transform){if(child.name==name){aux=child.gameObject;}}
		return aux;
	}
	
	void onDestroy(){
		Destroy (this.gameObject);
	}
}
