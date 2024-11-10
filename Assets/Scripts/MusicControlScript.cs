using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MusicControlScript : MonoBehaviour
{
    public List<string> scenesWithMusic = new List<string>(); // Lista scen, na których ma być muzyka
    private AudioSource musicAudioSource; // Komponent AudioSource, na którym odtwarzana jest muzyka

    private void Awake()
    {
        musicAudioSource = FindObjectOfType<AudioSource>();
        DontDestroyOnLoad(gameObject); // Nie niszczaj tego obiektu przy zmianie sceny
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        CheckMusicStatus(currentScene);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string currentScene = scene.name;
        CheckMusicStatus(currentScene);
    }

    private void CheckMusicStatus(string sceneName)
    {
        if (musicAudioSource != null)
        {
            bool shouldPlayMusic = scenesWithMusic.Contains(sceneName);
            if (shouldPlayMusic)
            {
                if (!musicAudioSource.isPlaying)
                {
                    musicAudioSource.Play(); // Odtwórz muzykę, jeśli nie jest już odtwarzana
                }
            }
            else
            {
                musicAudioSource.Stop(); // Zatrzymaj muzykę, jeśli scena nie jest na liście
            }
        }
    }
}