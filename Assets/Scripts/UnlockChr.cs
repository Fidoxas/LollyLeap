using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UnlockChr : MonoBehaviour
{
    public CharacterDataBase characterDB;

    [FormerlySerializedAs("YT")] public Button yt;
    [FormerlySerializedAs("TT")] public Button tt;
    [FormerlySerializedAs("FB")] public Button fb;

    private bool ytClicked = false;
    private bool ttClicked = false;
    private bool fbClicked = false;

    private void Start()
    {
        // Dodaj nasłuchiwanie na kliknięcie przycisków
        yt.onClick.AddListener(OnYTClicked);
        tt.onClick.AddListener(OnTTClicked);
        fb.onClick.AddListener(OnFBClicked);
    }

    private void OnYTClicked()
    {
        ytClicked = true;
        TryUnlockCharacter();
    }

    private void OnTTClicked()
    {
        ttClicked = true;
        TryUnlockCharacter();
    }

    private void OnFBClicked()
    {
        fbClicked = true;
        TryUnlockCharacter();
    }

    private void TryUnlockCharacter()
    {
        if (ytClicked && ttClicked && fbClicked)
        {
            UnlockCharacter();
        }
    }

    private void UnlockCharacter()
    {
        if (characterDB.CharacterCount >= 1)
        {
            Character
                selectedCharacter =
                    characterDB.GetCharacter(1); // Zakładając, że postać do odblokowania to pierwsza na liście
            selectedCharacter.locked = false;
            Debug.Log("Odblokowano postać: " + selectedCharacter.characterName);
        }
        else
        {
            Debug.Log("Brak postaci do odblokowania.");
        }
    }
}