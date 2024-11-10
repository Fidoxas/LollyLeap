using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediaButton : MonoBehaviour
{
    public string source;

    public void OpenPage()
    {
        Application.OpenURL(source);
    }
}