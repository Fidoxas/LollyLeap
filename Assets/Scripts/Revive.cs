using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Revive : MonoBehaviour
{
    private bool ableToRescue = true;
    [FormerlySerializedAs("ReviveTime")] public int reviveTime = 5;
    private bool isCountingDown;


    public PlayerMovement player;
    [FormerlySerializedAs("Tmp_counting")] public TextMeshProUGUI tmpCounting;
    public Button watchTvButton;

    [FormerlySerializedAs("GameOverPanelButton")]
    public Button gameOverPanelButton;

    [SerializeField] GameObject gameoverScreen;

    [FormerlySerializedAs("ReviveScreen")] [SerializeField]
    GameObject reviveScreen;

    [SerializeField] GameObject passLevelScreen;

    public void WantToRevive()
    {
        if (passLevelScreen.activeSelf)
        {
            return;
        }

        if (ableToRescue && !player.isAlive && !gameoverScreen.activeSelf)
        {
            WaitForRevive();
        }
        else if (!ableToRescue && !player.isAlive && !reviveScreen.activeSelf)
        {
            GameOver();
        }
    }

    private void WaitForRevive()
    {
        reviveScreen.SetActive(true);
        StartCoroutine(CountingDown());
    }


    IEnumerator CountingDown()
    {
        if (isCountingDown)
        {
            yield break; // Je�li odliczanie ju� si� rozpocz�o, przerywamy nowe odliczanie
        }

        isCountingDown = true; // Ustawiamy flag� na true, aby wiedzie�, �e odliczanie si� rozpocz�o

        while (reviveTime > 0 && !player.isAlive)
        {
            tmpCounting.text = reviveTime.ToString();
            reviveTime--;
            yield return new WaitForSeconds(1f);
        }

        if (!player.isAlive)
        {
            GameOver();
        }

        isCountingDown = false; // Resetujemy flag� na false, gdy odliczanie si� zako�czy�o
    }

    public void WatchTV()
    {
        Debug.Log("odpalam reklame");
        reviveScreen.gameObject.SetActive(false); // Wy��czamy przycisk po wywo�aniu WatchTV().
        player.RevivePlayer();
        ableToRescue = false;
    }


    public void GameOver()
    {
        gameoverScreen.SetActive(true);
        reviveScreen.SetActive(false);
    }
}