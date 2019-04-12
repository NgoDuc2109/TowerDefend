/*
 *	Path reader for the current enemy 
*/


using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using FThLib;

public class PathFollower : MonoBehaviour {
	public Transform[] path;	//Path with points
	public float speed = 0f;
	public int currentPoint = 0;
	public bool fighting=false; 
	public bool faceright=true;
	public bool auxfight=false;
	public GameObject target=null;
	private float auxspeed=0f;
	private Text life;				//Text on Canvas
	private Text money;				//Text on Canvas
	private GameObject LifeBtn;		//Button on Canvas
	private GameObject MoneyBtn;	//Button on Canvas
	public Vector3[] custom;		//Generated with path points (adding noise to the path)
	private bool Step=false;		//in direction to target
	private float seed = 0.2f;
	private bool randomized = false;
	private float rand=0f;
	private bool off=false;
	//public int knights = 0;
	// Use this for initialization
	void Start () {
		life = GameObject.Find("Life").GetComponent<Text>();
		money = GameObject.Find("Money").GetComponent<Text>();
		LifeBtn = GameObject.Find("Button");
	}

	// Update is called once per frame
	void Update () {
		if(!master.isFinish()){
			if(fighting==false){auxfight=false;}
			if(auxfight!=fighting){//Randomize the time to "stop and fight" when have a target...
				rand = Random.Range(0.001f, 2F);
				float randb = Random.Range(rand, rand+2f);
				Invoke("setFight",Random.Range(rand, randb));
			}
			if(speed!=0f&&auxfight==false&&off==false){
				if(randomized==false){
					auxspeed=speed;
					custom = new Vector3[path.Length];
					randomized=true;
					randomizePath();//Create a random path using points.
				}
				needFlip(custom[currentPoint]);
				transform.position = Vector2.MoveTowards (transform.position, custom[currentPoint], Time.deltaTime*speed);
				this.transform.position=new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.y);
				Vector2 patchPos = new Vector2 (this.transform.position.x,this.transform.position.y);
				Vector2 patchCustomPos = new Vector2 (custom[currentPoint].x,custom[currentPoint].y);

				if(patchPos==patchCustomPos){
					if(currentPoint == path.Length-1){
						int value = int.Parse (life.text);
						if(value>0){
							Animator anim = LifeBtn.GetComponent<Animator>();
							anim.Play("Size");
							value--;
							life.text = "" + value;
						}else{
							End();
						}
					}
					currentPoint++;
				}
				if(currentPoint>=path.Length){Destroy(this.gameObject);}
			}
			if(target==null){fighting=false;}
			if(fighting==true&&faceright==false){Flip();}
		}
	}

	public void Turn_Off(){
		off=true;
		Invoke("Turn_On",GameObject.Find("Instance_Point").GetComponent<Master_Instance>().Sorcerer_Runes_Time);
	}
	private void Turn_On(){
		off=false;
	}
	private void End(){
		GameObject.Find("Instance_Point").GetComponent<Waves_Creator_Controller>().end=true;
	}

	private void setFight(){
		auxfight=true;
		fighting=true;
	}

	public void reduceSpeed(){
		speed = speed/2;
		Invoke("setSpeed",2);
	}

	private void setSpeed(){speed=auxspeed;}

	void needFlip(Vector3 customPos){
		if(customPos.x>=this.transform.position.x&&faceright==false){Flip();}
		if(customPos.x<this.transform.position.x&&faceright==true){Flip();}
	}
	
	void randomizePath(){
		for(int i = 0;i < path.Length;i++){
			if(path[i].gameObject.name!="End"){
				custom[i] = new Vector3(path[i].position.x + Random.Range(-seed, seed),path[i].position.y + Random.Range(-seed, seed),path[i].position.y);
			}else{
				custom[i] = new Vector3(path[i].position.x ,path[i].position.y ,path[i].position.y);
			}
		}
	}

	void Stop(){Step=true;}

	void Flip(){
		faceright=!faceright;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}
