using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;

    [SerializeField] string text1;
    [SerializeField] string text2;
    [SerializeField] string text3;
    [SerializeField] string text4;
    [SerializeField] string text5;
    [SerializeField] string text6;

    [SerializeField] string text8;
    [SerializeField] string text9;

    [SerializeField] GameObject blueLolly;
    [SerializeField] GameObject purpleLolly;

    [FormerlySerializedAs("Lolly")] [SerializeField]
    GameObject lolly;

    [FormerlySerializedAs("SlowLolly")] [SerializeField]
    GameObject slowLolly;

    [SerializeField] GameObject instruction;
    [SerializeField] GameObject score;

    [SerializeField] GameObject gen;
    [SerializeField] GameObject bubbleGen;
    [SerializeField] GameObject passlevelscreen;
    [SerializeField] GameObject blueTipArrow;

    [SerializeField] GameObject tealButton;
    [SerializeField] GameObject tealButt;
    [SerializeField] GameObject blueButton;
    [SerializeField] GameObject blueButt;
    [SerializeField] GameObject darkPurpleButton;
    [SerializeField] GameObject darkPurpleButt;
    [SerializeField] GameObject purpleButton;
    [SerializeField] GameObject purpleButt;
    [SerializeField] GameObject pinkButton;
    [SerializeField] GameObject pinkButt;
    [SerializeField] GameObject redButton;
    [SerializeField] GameObject redButt;

    void Start()
    {
        UnableTeal(false);
        UnableBlue(false);
        UnableDarkPurple(false);
        UnablePurple(false);
        UnablePink(false);
        UnableRed(false);

        bubbleGen.SetActive(false);
        gen.SetActive(false);
        instruction.SetActive(false);
        score.SetActive(false);

        tmp.text = text1;
        StartCoroutine(Tutorial());
    }


    private IEnumerator Tutorial()
    {
        var tutorialGen = gen.GetComponent<TutorialGen>();
        yield return new WaitForSeconds(3);
        tutorialGen.itemToGenerate = slowLolly;
        instruction.SetActive(true);
        UnableDarkPurple(true);
        UnablePurple(true);
        yield return new WaitForSeconds(6);
        score.SetActive(true);
        gen.SetActive(true);

        tmp.text = text2;
        while (Score.score < 3)
        {
            yield return null;
        }

        //yield return new WaitForSeconds(2);
        tmp.text = text3;
        tutorialGen.DeactivateGeneratedObjects();
        gen.SetActive(false);
        UnablePink(true);
        UnableBlue(true);
        yield return new WaitForSeconds(5);
        gen.SetActive(true);
        tutorialGen.StartCoroutine(tutorialGen.GenerateObjectsWithDelay());


        gen.transform.position = new Vector2(gen.transform.position.x, -2);
        tutorialGen.spawnAreaSize = new Vector3(tutorialGen.spawnAreaSize.x, 10, tutorialGen.spawnAreaSize.z);
        while (Score.score < 9)
        {
            yield return null;
        }

        //yield return new WaitForSeconds(2);
        tmp.text = text4;
        tutorialGen.DeactivateGeneratedObjects();
        gen.SetActive(false);
        UnableTeal(true);
        UnableRed(true);
        yield return new WaitForSeconds(5);
        gen.SetActive(true);
        tutorialGen.StartCoroutine(tutorialGen.GenerateObjectsWithDelay());
        gen.transform.position = new Vector2(gen.transform.position.x, 0);
        tutorialGen.spawnAreaSize = new Vector3(tutorialGen.spawnAreaSize.x, 10, tutorialGen.spawnAreaSize.z);
        while (Score.score < 12)
        {
            yield return null;
        }

        tutorialGen.itemToGenerate = purpleLolly;
        tutorialGen.DeactivateGeneratedObjects();
        gen.SetActive(false);
        gen.SetActive(true);
        tutorialGen.StartCoroutine(tutorialGen.GenerateObjectsWithDelay());
        tmp.text = text5;
        yield return new WaitForSeconds(3);
        tutorialGen.itemToGenerate = blueLolly;
        while (Score.score < 14)
        {
            yield return null;
        }

        blueTipArrow.SetActive(false);
        tmp.text = text6;
        tutorialGen.DeactivateGeneratedObjects();
        gen.SetActive(false);
        bubbleGen.SetActive(true);
        var tutorialBubbleGen = bubbleGen.GetComponent<TutorialBubbleGen>();
        tutorialBubbleGen.StartCoroutine(tutorialBubbleGen.GenerateObjectsWithDelay());
        yield return new WaitForSeconds(9);
        bubbleGen.SetActive(false);
        tmp.text = text8;
        yield return new WaitForSeconds(2.5f);
        tmp.text = text9;
        gen.SetActive(true);
        gen.GetComponent<UsualGen>().enabled = true;
        gen.GetComponent<UsualGen>().StartCoroutine(gen.GetComponent<UsualGen>().GenerateObjectsWithDelay());
        yield return new WaitForSeconds(1.5f);


        Passlv();

        yield return null;
    }

    private void Passlv()
    {
        Debug.Log("passlv");
        passlevelscreen.SetActive(true);
    }

    private void UnableTeal(bool unable)
    {
        tealButton.SetActive(unable);
        tealButt.SetActive(unable);
    }

    private void UnableBlue(bool unable)
    {
        blueButton.SetActive(unable);
        blueButt.SetActive(unable);
    }

    private void UnableDarkPurple(bool unable)
    {
        darkPurpleButton.SetActive(unable);
        darkPurpleButt.SetActive(unable);
    }

    private void UnablePurple(bool unable)
    {
        purpleButton.SetActive(unable);
        purpleButt.SetActive(unable);
    }

    private void UnablePink(bool unable)
    {
        pinkButton.SetActive(unable);
        pinkButt.SetActive(unable);
    }

    private void UnableRed(bool unable)
    {
        redButton.SetActive(unable);
        redButt.SetActive(unable);
    }
}