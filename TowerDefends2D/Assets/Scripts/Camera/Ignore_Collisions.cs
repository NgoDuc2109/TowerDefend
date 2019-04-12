using UnityEngine;
using System.Collections;
using FThLib;

public class Ignore_Collisions : MonoBehaviour {
	bool errors = false;
	int auxx = 0;
	// Use this for initialization
	void Start () {
		ignoreLayers();
	}

	//void Update(){
	//	//Debug.Log(""+auxx);
	//	auxx++;
	//}

	void ignoreLayers(){
		if(getLayers("tower")==true){Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("tower"), LayerMask.NameToLayer("tower"), true);}
		if(getLayers("tower")==true){Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("map"), LayerMask.NameToLayer("tower"), true);}
		if(getLayers("tower")==true){Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("enemies"), LayerMask.NameToLayer("map"), true);}
		if(getLayers("enemies")==true){Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("enemies"), LayerMask.NameToLayer("enemies"), true);}
		if(getLayers("knight")==true){Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("knight"), LayerMask.NameToLayer("knight"), true);}
		if(getLayers("knight")==true){Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("interface"), LayerMask.NameToLayer("knight"), true);}
		//--
		if(getLayers("interface")==true){Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("interface"), LayerMask.NameToLayer("interface"), true);}
		if(getLayers("interface")==true){Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("interface"), LayerMask.NameToLayer("tower"), true);}
		if(getLayers("interface")==true){Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("interface"), LayerMask.NameToLayer("map"), true);}
		if(getLayers("interface")==true){Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("interface"), LayerMask.NameToLayer("enemies"), true);}
		//--
		if(getLayers("interface")==true){Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("circle"), LayerMask.NameToLayer("interface"), true);}
		if(getLayers("interface")==true){Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("circle"), LayerMask.NameToLayer("map"), true);}
		if(getLayers("interface")==true){Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("circle"), LayerMask.NameToLayer("enemies"), true);}


	}

	bool getLayers(string name){
		bool aux = false;
		if(LayerMask.NameToLayer(name)!=-1){
			aux=true;
		}else{
			Debug.Log("No ''"+ name +"'' layer was found, please create it on Inspector");
			aux=false;
		}
		return aux;
	}
}
