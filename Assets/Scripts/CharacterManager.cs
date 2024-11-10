using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class CharacterManager : MonoBehaviour
{
    public TMP_Text description;
    public CharacterDataBase characterDB;
    public TMP_Text nameText;
    public Image artworkSprite;
    public Outline outline;


    private int selectedOption = 0;

    private void Start()
    {
        LoadSelectedOption();
        UpdateCharacter(selectedOption);
    }

    public void NextOption()
    {
        ChangeCharacterOption(1);
    }

    public void BackOption()
    {
        ChangeCharacterOption(-1);
    }

    private void ChangeCharacterOption(int delta)
    {
        selectedOption = (selectedOption + delta + characterDB.CharacterCount) % characterDB.CharacterCount;
        UpdateCharacter(selectedOption);
    }

    private void UpdateCharacter(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);

        if (character.locked)
        {
            description.text = character.characterLockDescription;
            artworkSprite.sprite = character.lockedSprite;
            nameText.text = "Locked";
            outline.enabled = false;
        }
        else
        {
            description.text = character.characterDescription;
            artworkSprite.sprite = character.characterSprite;
            nameText.text = character.characterName;
            outline.enabled = true;
        }
    }

    private void LoadSelectedOption()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption", 0);
    }

    private void SaveSelectedOption()
    {
        PlayerPrefs.SetInt("selectedOption", selectedOption);
    }

    public void ChangeScene()
    {
        if (!characterDB.GetCharacter(selectedOption).locked)
        {
            SaveSelectedOption();
            SceneManager.LoadScene("Menu");
        }
        else
        {
            Debug.Log("Character is locked");
        }
    }
}