using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelCount : MonoBehaviour
{
    [SerializeField] private LevelModeChoose levelModeChoose;
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private List<LevelGeneratorChooserScriptableObject> modes;

    [FormerlySerializedAs("Level")] public int level;

    void Start()
    {
        UpdateLevelText();
    }

    // Call this method whenever you want to update the level text
    public void UpdateLevelText()
    {
        string selectedMode = levelModeChoose.currentModeIdentifier;
        string levelKey = "LevelMode_" + selectedMode;
        level = PlayerPrefs.GetInt(levelKey, 1);
        tmp.text = level.ToString();
    }
}