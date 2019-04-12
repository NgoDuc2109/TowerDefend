/*
 	Return to main menu scene
*/

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Go_Main : MonoBehaviour {

    [SerializeField]
    private Animator doorAnim;
    void OnMouseDown() {
		Invoke("CrossfadeDelayed",0);
	}
		
	private void CrossfadeDelayed(){
        doorAnim.Play("DoorClose");
        Invoke("ExitDelayed",1);
	}

	private void ExitDelayed(){
        Fader.Instance.FadeIn().Pause().LoadLevel("MainMenu").FadeOut(1);
    }

}
