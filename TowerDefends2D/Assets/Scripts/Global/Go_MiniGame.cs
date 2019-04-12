
/*
 	It loads the Example scene
*/

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Go_MiniGame : MonoBehaviour
{
    private Sprite auxClicked;
    [SerializeField]
    private Animator doorAnim;
    // Use this for initialization
    void Start()
    {
        auxClicked = GetComponent<SpriteRenderer>().sprite;
    }

    void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().sprite = auxClicked;
        Exit();
    }

    public void Exit()
    {
        Invoke("CrossfadeDelayed", 0);
    }

    private void CrossfadeDelayed()
    {
        doorAnim.Play("DoorClose");
        Invoke("ExitDelayed", 1f);
    }

    private void ExitDelayed()
    {
        Fader.Instance.FadeIn().Pause().LoadLevel("SelectLv").FadeOut(1) ;
    }
}
