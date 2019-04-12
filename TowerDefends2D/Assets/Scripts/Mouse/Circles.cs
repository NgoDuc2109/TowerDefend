using UnityEngine;
using System.Collections;

public class Circles : MonoBehaviour {
	//public GameObject parent_=null;
	private GameObject zone=null;
	// Use this for initialization
	void Start () {
		if(GameObject.Find("hand")){
			Destroy (GameObject.Find("hand"));
		}
		setLayer();
		zone = getBrother("zone");
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if(coll.gameObject == zone){
			Cursor.visible = false;
			GetComponent<SpriteRenderer>().enabled=true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {//mientras collider con zone
		if(other.gameObject == zone){
			Cursor.visible = true;
			GetComponent<SpriteRenderer>().enabled=false;
		}
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(0)&&GetComponent<SpriteRenderer>().enabled==false){//Disable all interface and zone renderer
			Disable_all();
		}else{
			if (Input.GetMouseButtonUp(0)&&GetComponent<SpriteRenderer>().enabled==true){
				getBrother("flag").transform.position=new Vector3(this.gameObject.transform.parent.transform.position.x,this.gameObject.transform.parent.transform.position.y,this.gameObject.transform.parent.transform.position.y+2f);
				Disable_all();
			}
		}
		if(Camera.main){
			Vector3 aux = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			aux.z=aux.y-6f;
			this.transform.parent.position= aux;
		}
	}

	private void Disable_all(){
		//--
		KT_Controller ktproperties = this.transform.parent.transform.parent.GetComponent<KT_Controller>();
		ktproperties.Reset();
		//--
		this.transform.parent.transform.parent.GetComponent<CircleCollider2D>().enabled=true;
		getBrother("zoneImg").GetComponent<SpriteRenderer>().enabled=false;
		Cursor.visible = true;
		Destroy (this.gameObject.transform.parent.gameObject);
	}

	private GameObject getBrother(string name){
		GameObject aux = null;
		foreach(Transform child in gameObject.transform.parent.transform.parent.transform){
			if(child.name==name){aux=child.gameObject;}
		}
		return aux;
	}
	void setLayer(){if(LayerMask.NameToLayer("circle")!=-1){this.gameObject.layer = LayerMask.NameToLayer("circle");}}
}
