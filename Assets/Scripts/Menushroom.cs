using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public Animator animator;
    [FormerlySerializedAs("Bg_animator")] public Animator bgAnimator;
    public GameObject[] objectsToDisable;
    public Image buttonImage;


    public void StartAnimationAndLoadScene(string menu)
    {
        StartCoroutine(AnimationAndLoadSceneCoroutine(menu));
    }

    private IEnumerator AnimationAndLoadSceneCoroutine(string menu)
    {
        // Odtwarzanie animacji
        animator.SetTrigger("Squeeze");

        // Poczekaj na zakończenie animacji
        bgAnimator.SetTrigger("BgScroll");

        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }


        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(menu);
    }
}