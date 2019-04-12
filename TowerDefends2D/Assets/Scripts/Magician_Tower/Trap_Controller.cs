using UnityEngine;
using System.Collections;
using FThLib;

public class Trap_Controller : MonoBehaviour {
	//--Public
	public int duration = 13;
	public int damage = 1;
	//--Private
	private Animator anim;
	//private Master_Instance master;
	private bool attack=false;
	private float delay=2f;
	private bool on=false;
	// Use this for initialization
	void Start () {
		this.gameObject.name="trap_";
		//master = GameObject.Find("Instance_Point").GetComponent<Master_Instance>();
		this.gameObject.transform.position=master.setThisZ(this.transform.position,0.00f);
		master.setLayer("tower",this.gameObject);
		setChild(attack);
		Invoke("onDestroy",duration);
	}
	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag=="Respawn"&&other.GetComponent<Enemies_Controller>().type!="enemy3"){
			enemyreduceLife(other.gameObject);
			other.gameObject.GetComponent<PathFollower>().reduceSpeed();
		}
	}
	// Update is called once per frame
	void Update () {
		if(on==false){
			on=true;
			Invoke("setTrap",delay);
		}
	}
	private void enemyreduceLife(GameObject target){
		if(target!=null){
			Enemies_Controller properties = target.GetComponent<Enemies_Controller>();
			properties.reduceLife(damage);
		}
	}
	private void setChild(bool value){
		foreach(Transform child in gameObject.transform){
			child.gameObject.GetComponent<Animator> ().SetBool ("attack", value);
		}
	}
	void setTrap(){
		attack = !attack;
		setChild(attack);
		on=false;
		GetComponent<BoxCollider2D>().enabled=attack;
		Invoke ("disableTrigger",1f);
	}
	void disableTrigger(){
		GetComponent<BoxCollider2D>().enabled=false;
	}
	void onDestroy(){
		Destroy(this.gameObject);
	}
}
