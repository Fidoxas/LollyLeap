using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PowerInterface power;

    [FormerlySerializedAs("_jumpRed")] [SerializeField]
    private float jumpRed = 1;

    [FormerlySerializedAs("_jumpPink")] [SerializeField]
    private float jumpPink = 1;

    [FormerlySerializedAs("_jumpPurple")] [SerializeField]
    private float jumpPurple = 1;

    [FormerlySerializedAs("_jumpDarkPurple")] [SerializeField]
    private float jumpDarkPurple = 1;

    [FormerlySerializedAs("_jumpBlue")] [SerializeField]
    private float jumpBlue = 1;

    [FormerlySerializedAs("_jumpTeal")] [SerializeField]
    private float jumpTeal = 1;

    [SerializeField] SpriteRenderer spriteRenderer;

    [FormerlySerializedAs("_collider")] [SerializeField]
    Collider2D collider;

    [FormerlySerializedAs("TipArrowTeal")] [SerializeField]
    GameObject tipArrowTeal;

    [FormerlySerializedAs("TipArrowBlue")] [SerializeField]
    GameObject tipArrowBlue;

    [FormerlySerializedAs("TipArrowDarkPurple")] [SerializeField]
    GameObject tipArrowDarkPurple;

    [FormerlySerializedAs("TipArrowPurple")] [SerializeField]
    GameObject tipArrowPurple;

    [FormerlySerializedAs("TipArrowPink")] [SerializeField]
    GameObject tipArrowPink;

    [FormerlySerializedAs("TipArrowRed")] [SerializeField]
    GameObject tipArrowRed;

    [SerializeField] GameObject infoTextTeal;
    [SerializeField] GameObject infoTextblue;
    [SerializeField] GameObject infoTextDarkPurple;
    [SerializeField] GameObject infoTextPurple;
    [SerializeField] GameObject infoTextPink;
    [SerializeField] GameObject infoTextRed;

    private Rigidbody2D rb;
    public Revive revive;
    public Animator animator;
    public PhysicsMaterial2D deadPlayerPhysicsMaterial;
    public Collider2D boundaryCollider;
    public AudioSource audioSource;

    private int obstacleLayer;

    private float lerpSpeed = 5f;
    public float invincibleTime = 3f;
    public bool isAlive = true;
    private bool isInvincible = false;
    private float cVolume;
    private Vector3 startPosition;
    private Color originalColor;
    public Color invincibleColor;
    private bool ignoreObstacles;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        animator.SetBool("Death", false);
        cVolume = audioSource.volume;

        startPosition = transform.position;
        obstacleLayer = LayerMask.NameToLayer("Obstacle");
        originalColor = spriteRenderer.color;
        invincibleColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f);

        CloseTips();
    }

    private void CloseTips()
    {
        tipArrowRed.SetActive(false);
        infoTextRed.SetActive(false);

        tipArrowPink.SetActive(false);
        infoTextPink.SetActive(false);

        tipArrowPurple.SetActive(false);
        infoTextPurple.SetActive(false);

        tipArrowDarkPurple.SetActive(false);
        infoTextDarkPurple.SetActive(false);

        tipArrowBlue.SetActive(false);
        infoTextblue.SetActive(false);

        tipArrowTeal.SetActive(false);
        infoTextTeal.SetActive(false);
    }

    public void RedJump()
    {
        GameObject redLightning = GameObject.Find("RedLightning");
        if (redLightning != null)
        {
            // Zniszcz obiekt
            Destroy(redLightning);
        }

        if (isAlive)
        {
            rb.velocity = Vector2.down * jumpRed;
        }

        tipArrowRed.SetActive(false);
        infoTextRed.SetActive(false);
    }

    public void PinkJump()
    {
        GameObject pinkLightning = GameObject.Find("PinkLightning");
        if (pinkLightning != null)
        {
            Destroy(pinkLightning);
        }

        if (isAlive)
        {
            rb.velocity = Vector2.down * jumpPink;
        }

        tipArrowPink.SetActive(false);
        infoTextPink.SetActive(false);
    }

    public void PurpleJump()
    {
        GameObject purpleLightning = GameObject.Find("PurpleLightning");
        if (purpleLightning != null)
        {
            Destroy(purpleLightning);
        }

        if (isAlive)
        {
            rb.velocity = Vector2.down * jumpPurple;
        }

        tipArrowPurple.SetActive(false);
        infoTextPurple.SetActive(false);
    }

    public void DarkPurpleJump()
    {
        GameObject darkPurpleLightning = GameObject.Find("DarkPurpleLightning");
        if (darkPurpleLightning != null)
        {
            // Zniszcz obiekt
            Destroy(darkPurpleLightning);
        }

        if (isAlive)
        {
            rb.velocity = Vector2.up * jumpDarkPurple;
        }

        tipArrowDarkPurple.SetActive(false);
        infoTextDarkPurple.SetActive(false);
    }

    public void BlueJump()
    {
        GameObject blueLightning = GameObject.Find("BlueLightning");
        if (blueLightning != null)
        {
            // Zniszcz obiekt
            Destroy(blueLightning);
        }

        if (isAlive)
        {
            rb.velocity = Vector2.up * jumpBlue;
        }

        tipArrowBlue.SetActive(false);
        infoTextblue.SetActive(false);
    }

    public void TealJump()
    {
        GameObject tealLightning = GameObject.Find("TealLightning");
        if (tealLightning != null)
        {
            // Zniszcz obiekt
            Destroy(tealLightning);
        }

        if (isAlive)
        {
            rb.velocity = Vector2.up * jumpTeal;
        }

        tipArrowTeal.SetActive(false);
        infoTextTeal.SetActive(false);
    }


    internal void RevivePlayer()
    {
        isAlive = true;
        animator.SetBool("Death", false);
        collider.sharedMaterial = null;
        transform.position = startPosition;
        audioSource.volume = cVolume;
        audioSource.pitch = 1;
        gameObject.tag = "Player";
        isInvincible = true;
        StartCoroutine(InvincibilityTimer());

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    IEnumerator InvincibilityTimer()
    {
        ignoreObstacles = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
        ignoreObstacles = false;
    }

    public void GetHit()
    {
        if (!isInvincible && !PowerInterface.isShielded)
        {
            isAlive = false;
            animator.SetBool("Death", true);
            revive.WantToRevive();
            collider.sharedMaterial = deadPlayerPhysicsMaterial;
            audioSource.volume = 0.15f;
            audioSource.pitch = 0.7f;
            gameObject.tag = "Untagged";
        }
        else if (PowerInterface.isShielded)
        {
            power.DisableShield();
        }
    }

    private void Update()
    {
        if (isInvincible)
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, invincibleColor, Time.deltaTime * lerpSpeed);
            Physics2D.IgnoreLayerCollision(gameObject.layer, obstacleLayer, ignoreObstacles);
        }
        else
        {
            spriteRenderer.color = originalColor;
            Physics2D.IgnoreLayerCollision(gameObject.layer, obstacleLayer, ignoreObstacles);
        }
    }
}