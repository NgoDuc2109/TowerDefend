using UnityEngine;
using System.Collections;
using FThLib;

public class Knights_Controller : MonoBehaviour {
	//public
	public int life=0;
	public int damage=3;
	public GameObject flag=null;
	public GameObject target=null;	
	public bool fighting=false;
	public bool move=false;
	public bool faceright = true;
	public bool shield = false;
	//private
	private Animator anim;
	private float point=0f;
	private GameObject lifebar = null;
	private bool isActive=false;
	private bool Attack = false;
	private float delay = 3f;
	private int auxlife=0;
	//About healing
	private bool healing = false;
	private float healingdelay = 2f;//Change it for fast healing
	private int healingvalue = 1;
	private Vector3 auxbar = new Vector3 (0,0,0);
	// Use this for initialization
	void Start () {
		Init();
	}

	private void Init(){
		Invoke("activation",2f);
		master.setLayer("tower",this.gameObject);
		lifebar = master.getChildFrom("Lifebar", this.gameObject);
		auxbar = lifebar.transform.localScale;
		anim = this.gameObject.GetComponent<Animator> ();
		anim.SetBool ("walk", false);
		anim.SetBool ("dead", false);
		anim.SetBool ("attack", false);
	}

	// Update is called once per frame
	void Update () {
		if(!master.isFinish()){
			this.transform.position=new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.y);
			if(shield==true){anim.SetLayerWeight(1, 1);}
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Attack")) {anim.SetBool ("attack", false);}
			if(life!=0&&auxlife==0){getPoint();}
			if(point!=0f&&auxlife==0){auxlife = life;}

			if(isActive==true){
				Vector3 customPos = master.getChildFrom(this.gameObject.name + "p", flag).transform.position;
				if(fighting==false){
					Vector2 patchPos = new Vector2 (this.transform.position.x,this.transform.position.y);
					Vector2 patchCustomPos = new Vector2 (customPos.x,customPos.y);
					if(patchPos!=patchCustomPos){
						needFlip(customPos);
						transform.position = Vector2.MoveTowards(patchPos, patchCustomPos, Time.deltaTime/3);
						this.transform.position=new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.y);
					}else{
						anim.SetBool ("walk", false);
					}
				}

				if(fighting==true){
						if(target!=null){//By default the knight fighting place is at right of enemy
							PathFollower enemyProperties = target.GetComponent<PathFollower>();
							enemyProperties.fighting=true;
							GameObject rightp = master.getChildFrom("RightPoint",target);
							GameObject leftp = master.getChildFrom("LeftPoint",target);
							if(enemyProperties.faceright==true){//go to right point
								Vector2 patchPos = new Vector2 (this.transform.position.x,this.transform.position.y);
								Vector2 patchCustomPos_ = new Vector2 (rightp.transform.position.x,rightp.transform.position.y);

								if(patchPos!=patchCustomPos_){
									needFlip(rightp.transform.position);
									transform.position = Vector2.MoveTowards (patchPos, patchCustomPos_, Time.deltaTime/3);
									this.transform.position=new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.y);
								}else{
									needFlip(target.transform.position);
									anim.SetBool ("walk", false);
									move=false;
								}
								if(move==false&&Attack==false){
									anim.SetBool ("attack", true);
									Attack=true;
									Invoke ("enemyreduceLife",0.1f);
									Invoke ("attack_delay",delay);
								}
							}else{
								if(transform.position != leftp.transform.position){
								}else{
									move=false;
								}
							}
						}else{
							fighting=false;
							move=false;
							Attack=false;
						}

				}else{
					if(life<auxlife&&healing==false){
						healing=true;
						Invoke("increaseLife",healingdelay);
					}
				}
			}
		}
	}

	public void resetLife(int newlife){
		lifebar.transform.localScale = auxbar;
		auxlife = newlife;
		life = newlife;
		getPoint();
	}

	void getPoint(){
		float aux = lifebar.transform.localScale.x;
		point = aux/life;
	}

	public void increaseLife(){
		life=life+healingvalue;
		float aux = healingvalue * point;
		Vector3 aux_ = lifebar.transform.localScale;
		aux_.x = aux_.x + aux;
		lifebar.transform.localScale = aux_;
		healing = false;
	}

	public void reduceLife(int value){
		GameObject blood = Instantiate(Resources.Load("Global/blood"), this.transform.position, Quaternion.identity)as GameObject;
		life=life-value;
		float aux = value * point;
		Vector3 aux_ = lifebar.transform.localScale;
		aux_.x = aux_.x - aux;
		if(aux_.x<0){
			aux_.x=0;
			lifebar.transform.localScale = aux_;
			anim.SetBool("dead",true);
			Destroy (master.getChildFrom("Shadow",this.gameObject));
			isActive=false;
			Invoke("onDestroy",1);
		}else{
			lifebar.transform.localScale = aux_;
		}
	}

	private void enemyreduceLife(){
		if(target!=null){
			Enemies_Controller properties = target.GetComponent<Enemies_Controller>();
			properties.reduceLife(damage);
		}
	}

	private void attack_delay(){Attack=false;}
	void activation(){isActive=true;}
	void onDestroy(){Destroy (this.gameObject);}
	void needFlip(Vector3 customPos){
		if(customPos.x>=this.transform.position.x&&faceright==false){Flip();}
		if(customPos.x<this.transform.position.x&&faceright==true){Flip();}
		anim.SetBool ("walk", true);
	}
	void Flip(){
		faceright=!faceright;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
