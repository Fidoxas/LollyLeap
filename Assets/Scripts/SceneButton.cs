using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SceneButton : MonoBehaviour
{
    [FormerlySerializedAs("SceneName")] public string sceneName;

    public void Changescene()
    {
        SceneManager.LoadScene(sceneName);
    }
}