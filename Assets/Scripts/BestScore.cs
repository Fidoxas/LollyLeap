using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class BestScore : MonoBehaviour
{
    [FormerlySerializedAs("_tmp")] [SerializeField]
    TextMeshProUGUI tmp;

    private void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    private void UpdateBestScore(int newBestScore)
    {
        tmp.text = newBestScore.ToString();
    }
}