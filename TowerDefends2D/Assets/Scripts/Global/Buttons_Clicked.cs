using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Buttons_Clicked : MonoBehaviour {
	public List<String> buttons;

	public void addButton(string name){
		if(name=="Trap"){

		}else{
			buttons.Add(name);
		}
	}

	public bool isClicked(string name){
		bool aux = false;
		for(int i=0; i<buttons.Count ;i++){
			if(buttons[i]==name){
				aux = true;
			}
		}
		return aux;
	}
	
}
