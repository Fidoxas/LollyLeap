using Assets.Scripts.LevelGenerator;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LevelSynchronizator : MonoBehaviour
{
    [SerializeField] GameObject bubbleGen;
    [SerializeField] RandomGen randomGenerator;
    [SerializeField] LevelGeneratorChooserScriptableObject levelChoser;
    [FormerlySerializedAs("Level")] public int level = 1;
    [SerializeField] public bool testmode;
    [SerializeField] private BackGroundLoader bgLoader;

    public List<LevelGeneratorChooserScriptableObject> modes;
    public GameObject passLevelScreen;

    void Awake()
    {
        if (!testmode)
        {
            string selectedMode = PlayerPrefs.GetString("Mode_", "Levels");
            string levelKey = "LevelMode_" + selectedMode;
            level = PlayerPrefs.GetInt(levelKey, 1);
            if (level == 1 && selectedMode == "Levels")
            {
                SceneManager.LoadScene("Tutorial");
            }

            levelChoser = modes.Find(mode => mode.modeIdentifier == selectedMode);
            if (selectedMode != "Endless")
            {
                level = PlayerPrefs.GetInt(levelKey, 1);
            }
            else if (selectedMode == "Endless")
            {
                level = 1;
            }
        }

        CurrentScene.UpdateLevel(levelChoser.GenerateLevel(level));
        bgLoader.LoadBg();
    }

    void Update()
    {
        if ((Score.score >= CurrentScene.Level.PointsToPass) && (level < 29) && (Score.score != 0))
        {
            Debug.Log(Score.score);
            Score.score = 0;
            level++;
            Debug.Log("JD");
            OnLevelChange();
            CurrentScene.UpdateLevel(levelChoser.GenerateLevel(level));
            string levelKey = "LevelMode_" + levelChoser.modeIdentifier;
            PlayerPrefs.SetInt(levelKey, level);
            //Debug.Log("Pr�g_" + CurrentScene.Level.pointsToPass);
            //Debug.Log("Zwi�kszam poziom");
        }
    }

    public void OnLevelChange()
    {
        // Debug.Log("Level changed in RandomGen");

        if (!CurrentScene.Level.Endless)
        {
            passLevelScreen.SetActive(true);
            //SceneManager.LoadScene("Game");
        }
    }
    public LevelGeneratorChooserScriptableObject GetLevelChoser()
    {
        return levelChoser;
    }
}