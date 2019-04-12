using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {
	public Texture2D cursorTexture;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;
	// Use this for initialization
	void OnGUI(){

	}

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Camera.current!=null){
			Vector3 aux = Camera.current.ScreenToWorldPoint(Input.mousePosition);
			aux.z=aux.y-6f;
			this.transform.position = aux;
		}

	}
}
