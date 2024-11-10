using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    private static BackgroundMusicController instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Zniszcz, jeśli jest już inny obiekt BackgroundMusicController
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Nie niszczaj tego obiektu przy przechodzeniu między scenami
    }
}