using Assets.Scripts.LevelGenerator;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelNumber : MonoBehaviour
{
    [SerializeField] private LevelSynchronizator levelSynchronizator;
    public TextMeshProUGUI tmp;
    private int lv = 1;

    void Start()
    {
        if (levelSynchronizator != null)
        {
            tmp.text = levelSynchronizator.level.ToString();
        }
        else
            tmp.text = lv.ToString();
    }

    void Update()
    {
        if (levelSynchronizator != null)
        {
            if (lv != levelSynchronizator.level)
            {
                lv = levelSynchronizator.level;
                tmp.text = levelSynchronizator.level.ToString();
            }
        }
    }
}