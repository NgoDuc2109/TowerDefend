using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLv : MonoBehaviour {
    public int level;
    private Sprite auxClicked;
    [SerializeField]
    private Animator doorAnim;

    void OnMouseUp()
    {
        StartCoroutine(CrossfadeDelayed());
    }


    private IEnumerator CrossfadeDelayed()
    {
        doorAnim.Play("DoorClose");
        yield return new WaitForEndOfFrame();
        Invoke("ExitDelayed", 1f);
    }

    private void ExitDelayed()
    {
        Fader.Instance.FadeIn().Pause().LoadLevel("Level_" + level).FadeOut(1);
    }

}
