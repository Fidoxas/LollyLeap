using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BackGroundLoader : MonoBehaviour
{
    [SerializeField] private LevelSynchronizator levelSynchronizator;
    [SerializeField] private SpriteRenderer bgRenderer;
    [SerializeField] private List<SpriteRenderer> grassRenderersList;
    private LevelGeneratorChooserScriptableObject levelChoser;
    [SerializeField] Sprite levelbg;


    // Start is called before the first frame update
    //private void Start()
    //{
    //    Debug.Log("Start method called");
    //    Debug.Log("levelbg: " + levelbg);
    //    Debug.Log("bgRenderer: " + bgRenderer);
    //    bgRenderer.sprite = levelbg;
    //}

    public void LoadBg()
    {
        levelChoser = levelSynchronizator.GetLevelChoser();

        if (levelChoser != null)
        {
            string modeIdentifierValue = levelChoser.modeIdentifier;
            levelbg = levelChoser.BackGround;
            foreach (var grassRenderer  in grassRenderersList)
            {
                grassRenderer.color = levelChoser.backgroundGrassColor; // Ustawienie koloru trawy
            }
            bgRenderer.sprite = levelbg;
        }
        else
        {
            Debug.LogError("LevelChoser jest pusty.");
        }
        
        
    }
}
