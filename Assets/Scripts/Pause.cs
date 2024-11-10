using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseScreen;
    public List<GameObject> bubbleKits = new List<GameObject>();

    private GameObject activeBubbleKit;

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);

            if (bubbleKits.Count > 0)
            {
                int randomIndex = Random.Range(0, bubbleKits.Count);
                activeBubbleKit = bubbleKits[randomIndex];
                SetActiveRecursively(activeBubbleKit, true); // Aktywuj obiekt i jego dzieci
            }
        }
    }

    public void MenuLevBut()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
        }

        pauseScreen.SetActive(false);
        SceneManager.LoadScene("LevelsMenu");
    }

    public void ResetGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
        }

        pauseScreen.SetActive(false);
        SceneManager.LoadScene("Game");
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseScreen.SetActive(false);

        if (activeBubbleKit != null)
        {
            SetActiveRecursively(activeBubbleKit, false); // Dezaktywuj obiekt i jego dzieci
            activeBubbleKit = null;
        }
    }

    // Rekursywna funkcja do aktywowania/dezaktywowania obiektu i jego dzieci
    private void SetActiveRecursively(GameObject obj, bool isActive)
    {
        obj.SetActive(isActive);
        foreach (Transform child in obj.transform)
        {
            SetActiveRecursively(child.gameObject, isActive);
        }
    }
}