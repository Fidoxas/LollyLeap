using System;
using Assets.Scripts.LevelGenerator;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BubbleScore : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public static int bubbles = 0;
    private int writedBubbles = 0;

    private void Start()
    {
        bubbles = PlayerPrefs.GetInt("Bubbles", 0);
        tmp.text = bubbles.ToString();
    }
    void Update()
    {
        if (writedBubbles != bubbles)
        {
            writedBubbles = bubbles;
            tmp.text = bubbles.ToString();
            PlayerPrefs.SetInt("Bubbles",bubbles);
        }
    }
}