using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public StageDatabase Stagedb;

    [FormerlySerializedAs("Bg")] public Image bg;
    public GameObject lollyGen;
    private int currentStage;


    private void Start()
    {
        if (!PlayerPrefs.HasKey("currentStage"))
        {
            currentStage = 0;
        }
        else
        {
            Load();
        }

        UpdateStage(currentStage);
    }

    private void UpdateStage(int currentStage)
    {
        Stages stages = Stagedb.GetStage(currentStage);
        //lollyGenLoader();
        bg = stages.backGround;
    }

    private void LollyGenLoader()
    {
        //  SpawnerItem[0].chance = 3;
    }

    private void Load()
    {
        currentStage = PlayerPrefs.GetInt("currentStage");
    }
}