/*
 	Enemies attack controller, attack, life, animations and get damage, show blood when arrow
*/

using UnityEngine;
using System.Collections;
using FThLib;

public class Enemies_Controller : MonoBehaviour {
	//public
	public int life=0;
	public float attackDelay = 2f;
	public int moneyWhenKill = 20;//Money when enemy is destroyed
	public string type = "";
	//private
	private Master_Instance masterPoint;
	private float point=0f;
	private GameObject lifebar = null;
	private bool Attack = false;
	private PathFollower properties_;
	private int damage=3;
	private int auxlife=0;
	private Animator anim;
	private bool off=false;

	// Use this for initialization
	void Start () {
		this.gameObject.tag="Respawn";
		Init();
	}

	private void Init(){
		masterPoint = GameObject.Find("Instance_Point").GetComponent<Master_Instance>();
		master.setLayer("enemies",this.gameObject);
		lifebar = master.getChildFrom("Lifebar",this.gameObject);
		getPoint();
		properties_ = GetComponent<PathFollower>();
		anim = this.gameObject.GetComponent<Animator> ();
		anim.SetBool ("walk", false);
		anim.SetBool ("dead", false);
		anim.SetBool ("attack", false);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.name=="Arrow"){reduceLife(other.GetComponent<Damage>().Damage_);}
		if(other.name=="Magic"){
			GameObject blood = Instantiate(Resources.Load("Global/blood"), other.transform.position, Quaternion.identity)as GameObject;
			reduceLife(other.GetComponent<MT_Bullet>().Damage_);
		}
		//IF THIS ENEMY IS A SORCERER ======================
		if(type=="enemy2"&&other.name=="zone"){
			if(getChild(other.transform.root.gameObject)==null){
				anim.SetBool ("walk", false);
				anim.SetBool ("dead", false);
				anim.SetBool ("attack", false);
				other.transform.parent.SendMessage("Turn_Off");
				this.gameObject.SendMessage("Turn_Off");
				off=true;
				Invoke("Turn_On",masterPoint.Sorcerer_Runes_Time);
				CreateRunes(other.gameObject.transform.parent.gameObject);	//Instantiate runes
			}
		}
		//==================================
	}

	//About SORCERER and runes creation
	private void Turn_On(){off=false;}
	private void CreateRunes(GameObject go){
		GameObject runes = Instantiate(Resources.Load("Enemies/StopTime"), new Vector3(go.transform.position.x,go.transform.position.y-0.3f,go.transform.position.z-1f), Quaternion.identity)as GameObject;
		runes.name="Runes";
		runes.transform.parent = go.transform;
	}
	//======================================

	// Update is called once per frame
	void Update () {
		if(!master.isFinish()){
			if(point!=0f&&auxlife==0){auxlife = life;}
			if(type!="enemy3"){
				if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Attack")) {anim.SetBool ("attack", false);}
				if(properties_.auxfight==true){
					anim.SetBool ("walk", false);
					if(properties_.target!=null){
						Knights_Controller knight = properties_.target.GetComponent<Knights_Controller>();
						if(knight.move==false&&Attack==false){//is near
							Attack=true;
							anim.SetBool ("attack", true);
							Invoke ("enemyreduceLife",0.1f);
							Invoke ("attack_delay",attackDelay);
							knight.reduceLife(damage);
						}
					}
				}else{
					if(off==false){
						anim.SetBool ("walk", true);
					}
				}
			}
		}
	}

	private void attack_delay(){Attack=false;}

	public void increaseLife(int value){
		float aux = value * point;
		Vector3 aux_ = lifebar.transform.localScale;
		aux_.x = aux_.x + aux;
			lifebar.transform.localScale = aux_;
	}

	public void reduceLife(int value){
		GameObject blood = Instantiate(Resources.Load("Global/blood"), this.transform.position, Quaternion.identity)as GameObject;
		float aux = value * point;
		life = life - value;
		Vector3 aux_ = lifebar.transform.localScale;
		aux_.x = aux_.x - aux;
		//================ IF AIR UNIT, DYING ANIMATION
		if(aux_.x<0){
			if(type=="enemy3"){
				Invoke("DelayedDying",0.5f);
				anim.SetBool("dead",true);
			}else{
				Destroy(this.gameObject);
			}
			masterPoint.addMoney(moneyWhenKill);
		}else{
			lifebar.transform.localScale = aux_;
		}
		//=============================================
	}

	private void enemyreduceLife(){
		if(properties_.target!=null){
			Knights_Controller properties = properties_.target.GetComponent<Knights_Controller>();
			properties.reduceLife(damage);
		}
	}

	void getPoint(){
		float aux = lifebar.transform.localScale.x;
		point = aux/life;
	}

	private void DelayedDying(){
		Destroy(this.gameObject);
	}

	GameObject getChild(GameObject go){
		GameObject aux=null;
		foreach(Transform child in go.transform){if(child.name=="Runes"){aux=child.gameObject;}}
		return aux;
	}

}
