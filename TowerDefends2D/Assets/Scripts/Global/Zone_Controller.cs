using UnityEngine;
using System.Collections;

public class Zone_Controller : MonoBehaviour {
	private GameObject parent_;
	private AT_Controller ATProperties;
	private KT_Controller KTProperties;
	private MiniKT_Controller MiniKTProperties;
	private MT_Controller MTProperties;
	// Use this for initialization
	void Start () {
		parent_ = this.transform.parent.gameObject;
		if(parent_.name=="AT"+0||parent_.name=="AT"+1||parent_.name=="AT"+2){
			ATProperties = parent_.GetComponent<AT_Controller>();
		}
		if(parent_.name=="KT"+0||parent_.name=="KT"+1||parent_.name=="KT"+2){
			KTProperties = parent_.GetComponent<KT_Controller>();
		}
		if(parent_.name=="MT0"){
			MTProperties = parent_.GetComponent<MT_Controller>();
		}
		if(parent_.name=="MiniKT0"){
			MiniKTProperties = parent_.GetComponent<MiniKT_Controller>();
		}


	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag=="Respawn"){
			if(parent_.name=="AT"+0||parent_.name=="AT"+1||parent_.name=="AT"+2){
				ATProperties.enemyAdd(other.gameObject);
			}
			if(parent_.name=="KT"+0||parent_.name=="KT"+1||parent_.name=="KT"+2){
				if(other.gameObject.GetComponent<Enemies_Controller>().type!="enemy3"){
					KTProperties.enemyAdd(other.gameObject);
				}
			}
			if(parent_.name=="MT0"){
				MTProperties.enemyAdd(other.gameObject);
			}
			if(parent_.name=="MiniKT0"){
				if(other.gameObject.GetComponent<Enemies_Controller>().type!="enemy3"){
					MiniKTProperties.enemyAdd(other.gameObject);
				}
			}
		}

	}
	void OnTriggerExit2D(Collider2D other) {
		if(other.tag=="Respawn"){
			if(parent_.name=="AT"+0||parent_.name=="AT"+1||parent_.name=="AT"+2){
				ATProperties.enemyRemove(other.gameObject.name);
			}
			if(parent_.name=="KT"+0||parent_.name=="KT"+1||parent_.name=="KT"+2){
				if(other.gameObject.GetComponent<Enemies_Controller>().type!="enemy3"){
					KTProperties.enemyRemove(other.gameObject.name);
				}
			}
			if(parent_.name=="MT0"){
				MTProperties.enemyRemove(other.gameObject.name);
			}
			if(parent_.name=="MiniKT0"){
				if(other.gameObject.GetComponent<Enemies_Controller>().type!="enemy3"){
					MiniKTProperties.enemyRemove(other.gameObject.name);
				}
			}
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
	
}
