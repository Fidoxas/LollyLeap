using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBooble : MonoBehaviour
{
    public GameObject bubble;

    public void Burst()
    {
        gameObject.SetActive(false);
    }
}