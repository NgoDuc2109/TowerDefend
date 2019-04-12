using UnityEngine;
using System.Collections;
using System.Collections.Generic;   //------------------------------------------------<<
using FThLib;

public class MT_Bullet : MonoBehaviour {
	//--Public
	public GameObject target=null;
	public int accuracy_mode=3;//1 the best
	public float maxLaunch = 4;
	public bool fire = false;
	public bool ice = false;
	public int Damage_=0;
	//--Private
	//private Master_Instance master;
	private bool activated = false;
	private bool sw =false;
	private float launch_placey=0f;
	private List<GameObject> firelist;	//--------------------------------------------------<<
	private bool on=false;

	private Vector3 latestpos = new Vector3(0,0,0);

	// Use this for initialization
	void Start () {
		firelist = GameObject.Find("Instance_Point").GetComponent<Fire_Pooling>().fireList;  //--------------------<<
		sw=true;
		master.setLayer("tower",this.gameObject);
		if(fire==false){
			master.getChildFrom("add3",this.gameObject).GetComponent<MT_AddFire>().enabled=false;
			master.getChildFrom("add4",this.gameObject).GetComponent<MT_AddFire>().enabled=false;
			
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		sw=false;
		GetComponent<Rigidbody2D>().isKinematic=true;
		GetComponent<Collider2D>().enabled=false;
		Invoke("onDestroy",0);
	}

	void FixedUpdate(){
		if (Application.platform == RuntimePlatform.Android){
			transform.Rotate(Vector3.forward * Time.deltaTime * 400);
			if(target!=null){
				latestpos = target.transform.position;
				if(activated==false){
					activated=true;
				}else{
					if(sw==true){	
						if (fire==true&&on==false){;
							on=true;
							//on=!on;
							//CreateFire();
							StartCoroutine(CreateFire_());
						}
						transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime*2);
					}
				}
			}else{
				Destroy(this.gameObject);
			}
		}
	}
	// Update is called once per frame
	void Update () {
		if (Application.platform != RuntimePlatform.Android){
			transform.Rotate(Vector3.forward * Time.deltaTime * 400);
			if(target!=null){
				latestpos = target.transform.position;
				if(activated==false){
					activated=true;
				}else{
					if(sw==true){	
						if (fire==true){;
							//on=!on;
							CreateFire();
						}
						transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime*2);
					}
				}
			}else{
				Destroy(this.gameObject);
			}
		}
	}

	private void Istantiate_Add(GameObject pos, string name){
		GameObject Bullet = Instantiate(Resources.Load("MT/Mfire"), pos.transform.position, Quaternion.identity)as GameObject;
		Bullet.transform.parent = GameObject.Find("Environment").transform;
		MT_Bullet BulletProperties = Bullet.GetComponent<MT_Bullet>();
		Bullet.name=name;
		//--
	}

	void CreateFire(){		//--------------------------------------------------------------------------<<
		if(on==false){
			//on=!on;
			for(int i = 0;i<firelist.Count;i++){
				if(!firelist[i].activeInHierarchy){
					//firelist[i].transform.localScale= new Vector3(0.06f,0.06f,0.06f);
					firelist[i].transform.localScale= new Vector3(0.03250099f,0.03250099f,0.03250099f);
					firelist[i].transform.position = this.transform.position;
					firelist[i].SetActive(true);
					break;
				}
			}
		}
	}

	IEnumerator CreateFire_(){
		yield return new WaitForSeconds(0);
			for(int i = 0;i<firelist.Count;i++){
				if(!firelist[i].activeInHierarchy){
					//firelist[i].transform.localScale= new Vector3(0.06f,0.06f,0.06f);
					firelist[i].transform.localScale= new Vector3(0.04f,0.04f,0.04f);
					firelist[i].transform.position = this.transform.position;
					firelist[i].SetActive(true);
					break;
				}
			}
		on=false;
	}

	void onDestroy(){
		Destroy (this.gameObject);
	}
}
