using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Instruction : MonoBehaviour
{
    private bool needinstruction;
    private bool isAlive;

    [SerializeField] GameObject tipArrowRed;
    [SerializeField] GameObject tipArrowPink;
    [SerializeField] GameObject tipArrowPurple;
    [SerializeField] GameObject tipArrowDarkPurple;
    [SerializeField] GameObject tipArrowBlue;
    [SerializeField] GameObject tipArrowTeal;

    [SerializeField] GameObject infoTextTeal;
    [SerializeField] GameObject infoTextBlue;
    [SerializeField] GameObject infoTextDarkPurple;
    [SerializeField] GameObject infoTextPurple;
    [SerializeField] GameObject infoTextPink;
    [SerializeField] GameObject infoTextRed;

    [FormerlySerializedAs("Player")] [SerializeField]
    PlayerMovement player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Lightning lightningScript = collision.gameObject.GetComponent<Lightning>();
        if (needinstruction && isAlive)
        {
            if (lightningScript != null)
            {
                SetActiveWithNullCheck(tipArrowPink, lightningScript.type == Lightning.Type.Pink);
                SetActiveWithNullCheck(tipArrowTeal, lightningScript.type == Lightning.Type.Teal);
                SetActiveWithNullCheck(tipArrowRed, lightningScript.type == Lightning.Type.Red);
                SetActiveWithNullCheck(tipArrowBlue, lightningScript.type == Lightning.Type.Blue);
                SetActiveWithNullCheck(tipArrowPurple, lightningScript.type == Lightning.Type.Purple);
                SetActiveWithNullCheck(tipArrowDarkPurple, lightningScript.type == Lightning.Type.Darkpurple);

                SetActiveWithNullCheck(infoTextTeal, lightningScript.type == Lightning.Type.Teal);
                SetActiveWithNullCheck(infoTextBlue, lightningScript.type == Lightning.Type.Blue);
                SetActiveWithNullCheck(infoTextDarkPurple, lightningScript.type == Lightning.Type.Darkpurple);
                SetActiveWithNullCheck(infoTextPurple, lightningScript.type == Lightning.Type.Purple);
                SetActiveWithNullCheck(infoTextPink, lightningScript.type == Lightning.Type.Pink);
                SetActiveWithNullCheck(infoTextRed, lightningScript.type == Lightning.Type.Red);
            }
        }
    }

    private void SetActiveWithNullCheck(GameObject gameObject, bool active)
    {
        if (gameObject != null)
        {
            gameObject.SetActive(active);
        }
    }

    private void Start()
    {
        needinstruction = GameManager.Needinstruction;
        isAlive = player.isAlive;

        tipArrowRed.SetActive(false);
        tipArrowPink.SetActive(false);
        tipArrowPurple.SetActive(false);
        tipArrowDarkPurple.SetActive(false);
        tipArrowBlue.SetActive(false);
        tipArrowTeal.SetActive(false);

        infoTextTeal.SetActive(false);
        infoTextBlue.SetActive(false);
        infoTextDarkPurple.SetActive(false);
        infoTextPurple.SetActive(false);
        infoTextPink.SetActive(false);
        infoTextRed.SetActive(false);
    }
}