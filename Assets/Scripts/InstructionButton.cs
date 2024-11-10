using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionButton : MonoBehaviour
{
    public Animator animator;

    public void HintButtonSwitch()
    {
        GameManager.Needinstruction = !GameManager.Needinstruction;

        if (animator != null)
        {
            animator.SetBool("Needinstruction", GameManager.Needinstruction);
        }

        int needinstructionValue = GameManager.Needinstruction ? 1 : 0;
        PlayerPrefs.SetInt(GameManager.NeedinstructionKey, needinstructionValue);
        PlayerPrefs.Save();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey(GameManager.NeedinstructionKey))
        {
            int needinstructionValue = PlayerPrefs.GetInt(GameManager.NeedinstructionKey);
            GameManager.Needinstruction = (needinstructionValue == 1);

            if (needinstructionValue == 1)
            {
                animator.SetBool("Needinstruction", GameManager.Needinstruction);
            }
            else
            {
                animator.SetBool("needinstruction", GameManager.Needinstruction);
            }
        }
        else
            animator.SetBool("Needinstruction", false);
    }
}