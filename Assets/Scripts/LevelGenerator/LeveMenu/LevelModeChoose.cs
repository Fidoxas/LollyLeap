using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LevelModeChoose : MonoBehaviour
{
    [System.Serializable]
    public class CheckBox
    {
        public GameObject point;
        public Button button;
        public LevelGeneratorChooserScriptableObject generatorChooser;
    }

    public List<CheckBox> checkBoxes;

    [FormerlySerializedAs("LevelCount")] [SerializeField]
    LevelCount levelCount;

    public string currentModeIdentifier;

    private void Start()
    {
        foreach (var checkBox in checkBoxes)
        {
            checkBox.point.SetActive(false);
            checkBox.button.onClick.AddListener(() => OnCheckBoxClicked(checkBox));
        }

        // Sprawdzamy, czy istnieje zapisany identyfikator w PlayerPrefs
        // currentModeIdentifier = PlayerPrefs.GetString("LevelMode_", "Levels");
        currentModeIdentifier = PlayerPrefs.GetString("Mode_", "Levels");
        Debug.Log(currentModeIdentifier + " wczytywany Checbox:");

        UpdateCheckBoxes();
    }

    private void OnCheckBoxClicked(CheckBox clickedCheckBox)
    {
        DeletePrevious();
        string modeIdentifier = clickedCheckBox.generatorChooser.modeIdentifier;
        //string modeKey = "LevelMode_" + modeIdentifier;

        PlayerPrefs.SetString("Mode_", modeIdentifier);
        Debug.Log(modeIdentifier + " zapisany checbox:");
        currentModeIdentifier = modeIdentifier;

        UpdateCheckBoxes();
        levelCount.UpdateLevelText();
        PlayerPrefs.Save();
    }

    private void DeletePrevious()
    {
        foreach (var checkBox in checkBoxes)
        {
            checkBox.point.SetActive(false);
        }
    }

    private void UpdateCheckBoxes()
    {
        foreach (var checkBox in checkBoxes)
        {
            string modeIdentifier = checkBox.generatorChooser.modeIdentifier;
            checkBox.point.SetActive(modeIdentifier == currentModeIdentifier);
            //Debug.Log(modeIdentifier + " aktywowany checbox");
        }
    }
}