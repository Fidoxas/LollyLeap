using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

// Import the UnityEngine.UI namespace for the Image class

public class PowerInterface : MonoBehaviour
{
    public static bool isShielded;
    [SerializeField] private GameObject bshield;
    [SerializeField] private Image bubbleImage;
    [SerializeField] private GameObject stopWatchImage;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject slowTimeScreen;

    [FormerlySerializedAs("Stopwatches")] [SerializeField] private TextMeshProUGUI StopWatches;
    [SerializeField] private TextMeshProUGUI Shields;

    [FormerlySerializedAs("anim")] [SerializeField] Animator TSSanim;
    
    private bool canUnableS;
    private bool canUnableB;
    private Rigidbody2D rb;
    private int shieldsRemaining;
    private int stopWatchesRemaining;

    private void Start()
    {
        shieldsRemaining = PlayerPrefs.GetInt("Shields", 3);
        stopWatchesRemaining = PlayerPrefs.GetInt("StopWatches",3);
        
        Shields.text = shieldsRemaining.ToString();
        StopWatches.text = stopWatchesRemaining.ToString();
        
        Time.timeScale = 1; 
        isShielded = false;
        canUnableB = true;
        canUnableS = true;
        TSSanim = GetComponent<Animator>();
        rb = player.GetComponent<Rigidbody2D>();
        Debug.Log("isShielded" + isShielded);
    }

    public void PowersExtended()
    {
        var currentExtendedState = TSSanim.GetBool("Extended");
        TSSanim.SetBool("Extended", !currentExtendedState);
    }

    public void bubbleShield()
    {
        if (canUnableB && shieldsRemaining > 0)
        {
            isShielded = true;
            canUnableB = false;
            bubbleImage.enabled = false;
            shieldsRemaining--;
            Shields.text = shieldsRemaining.ToString();

            rb.constraints |= RigidbodyConstraints2D.FreezePositionX;
            bshield.SetActive(true);
        }
    }

    public void DisableShield()
    {
        bshield.SetActive(false);
        isShielded = false;

        rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
    }
    
    public void SlowDownGameButton()
    {
        if (canUnableS && stopWatchesRemaining > 0)
        {
            canUnableS = false;
            stopWatchesRemaining--;
            StopWatches.text = stopWatchesRemaining.ToString();
            StartCoroutine(SlowDownGame());
        }
    }

    public IEnumerator SlowDownGame()
    {
        Time.timeScale = 0.5f;
        stopWatchImage.SetActive(false);
        slowTimeScreen.SetActive(true);
        TSSanim.SetTrigger("TimeSlow");
        yield return new WaitForSeconds(5f);
        slowTimeScreen.SetActive(false);
        Time.timeScale = 1f; 
    }
}