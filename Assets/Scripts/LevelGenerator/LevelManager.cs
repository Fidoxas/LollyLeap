using UnityEngine;

public class LevelManager : MonoBehaviour
{
    void OnEnable()
    {
        string selectedMode = PlayerPrefs.GetString("Mode_", "Levels");
        string levelKey = "LevelMode_" + selectedMode;
        int level = PlayerPrefs.GetInt(levelKey, 1);
        level++;
        PlayerPrefs.SetInt(levelKey, level);
        PlayerPrefs.Save();
        Debug.Log("Nowy poziom: " + level);
    }
}