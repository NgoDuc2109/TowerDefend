using UnityEngine;
using System.Collections;

namespace FThLib {
	public class master : MonoBehaviour {
		public bool vare=false;
		//###################################[Public Lib Section]###################################
		public static void Instantiate_Progressbar(float time_, GameObject parent_){
			Vector3 barposition = getChildFrom("opened",parent_).transform.position;
			barposition.z = barposition.y-3f;
			GameObject Pb = Instantiate(Resources.Load("Buttons/ProgressBar"), barposition, Quaternion.identity)as GameObject;
			Pb.name="ProgressBar";
			GameObject bar = getChildFrom("bar",Pb);
			ProgressBar BarProperties = bar.GetComponent<ProgressBar>();
			BarProperties.time = time_;
			Pb.transform.parent = parent_.transform;
		}
		public static void ClickEffect(GameObject go, MonoBehaviour instance){
			go.transform.localScale = go.transform.localScale/1.2f;
			instance.StartCoroutine(ReturnEffect(go));
		}
		static IEnumerator ReturnEffect(GameObject go){
			yield return new WaitForSeconds(0.1f);
			go.transform.localScale = go.transform.localScale*1.2f;
		}
		public static Vector3 setThisZ(Vector3 vec, float increment){
			vec = new Vector3(vec.x,vec.y,vec.y+increment);
			return vec;
		}
		public static void sellTower(GameObject tower){
			Destroy (tower);
			Instantiate_CP(tower.transform);
		}
		public static void showInterface(string interfacename , GameObject parent_, Transform pos){
			if(GameObject.Find("UI_Exit").GetComponent<Canvas>().enabled==false){
				if(GameObject.Find("Interface")){
					other_Interfaces_off();
				}
				GameObject interface_ = Instantiate(Resources.Load("Interface/"+ interfacename), GameObject.Find("Out").transform.position, Quaternion.identity)as GameObject;
				interface_.name="Interface";
				interface_.transform.SetParent(parent_.transform);
				interface_.transform.position = new Vector3(pos.transform.position.x, pos.transform.position.y, pos.transform.position.y-3f);
			}
		}
		public static void showHand(bool value){//show hand cursor
			if(GameObject.Find("UI_Exit").GetComponent<Canvas>().enabled==false){
				if(value==true){
					if(Camera.main){
						Vector3 aux = Camera.main.ScreenToWorldPoint(Input.mousePosition);
						aux.z=aux.y-4f;
						GameObject hand = Instantiate(Resources.Load("Cursor/hand"), aux , Quaternion.identity)as GameObject;
                        hand.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
						hand.name="hand";
					}else{
						Debug.Log("errr");
					}
				}else{
					Destroy (GameObject.Find("hand"));
				}
			}
		}
		public static void other_Interfaces_off(){
			if(getChildFrom("zoneImg",GameObject.Find("Interface").transform.parent.gameObject)!=null){
				if(!GameObject.Find("circle")){
					getChildFrom("zoneImg",GameObject.Find("Interface").transform.parent.gameObject).GetComponent<SpriteRenderer>().enabled=false;
				}
				GameObject.Find("Interface").transform.parent.gameObject.GetComponent<CircleCollider2D>().enabled=true;
			}
			Destroy (GameObject.Find("Interface"));
		}
		public static GameObject getChildFrom(string name, GameObject from){
			GameObject aux=null;
			foreach(Transform child in from.transform){if(child.name==name){aux=child.gameObject;}}
			return aux;
		}
		public static void setLayer(string name, GameObject go){if(LayerMask.NameToLayer(name)!=-1){go.layer = LayerMask.NameToLayer(name);}}
		public static float angle_(Vector3 a, Vector3 b){
			float angle = Mathf.Atan2(a.y-b.y,a.x-b.x)*180 / Mathf.PI;
			return angle;
		}
		public static void isHide(GameObject go){
			Color col = go.GetComponent<SpriteRenderer>().material.color;
			col.a = 0.5f;
			go.GetComponent<SpriteRenderer>().material.color = col;
		}
		public static bool isFinish(){//Implemented for a future use
			bool aux = false;
			/*if(GameObject.Find("GameOver")){
				aux=true;
				other_Interfaces_off();
			}*/
			return aux;
		}
		public static void setGameOver(){//Disable all scripts
			GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>() ;
			foreach(GameObject go in allObjects){
				if(go.name!="GameOver"&&go.tag!="Respawn"&&go.name!="Instance_Point"&&go.name!="Canvas"){//Disable all scripts except...
					if(go.layer != LayerMask.NameToLayer("UI")){
						MonoBehaviour[] scripts = go.GetComponents<MonoBehaviour>();
						foreach(MonoBehaviour script in scripts){script.enabled = false;}
					}
				}
			}
		}
		//###################################[Private Lib Section]###################################
		private static void Instantiate_CP(Transform pos){
			Vector3 cpos = pos.position;
			cpos.y = cpos.y - 0.1f;
			GameObject CP = Instantiate(Resources.Load("CP/CP"), cpos, Quaternion.identity)as GameObject;
			CP.name="CP";
		}
	}
}