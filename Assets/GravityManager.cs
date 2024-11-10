using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.LevelGenerator;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    private int chanceToChangeGravity;

    [SerializeField] private Rigidbody2D playerRigidBody;
    [SerializeField] private Animator GravityAnim;
    private bool gravityup;

    private void Start()
    {
        LevelSettings levelSettings = CurrentScene.Level;
        Debug.Log("OnEnable called for GravityManager");

        if (levelSettings != null)
        {
            Debug.Log("GravityMode value: " + levelSettings.GravityMode);

            if (levelSettings.GravityMode)
            {
                Debug.Log("Condition met");
                chanceToChangeGravity = levelSettings.GravityChanceToChange;
                Score.OnScoreChange += HandleScoreChange;  // Subscribe to the event
                Debug.Log("Gravity values loaded");
            }
            else
            {
                Debug.Log("Object deactivated");
                gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.LogError("CurrentScene or CurrentScene.Level is null. Ensure that they are properly initialized.");
        }
    }

    private void OnDisable()
    {
        {
            Score.OnScoreChange -= HandleScoreChange;  // Unsubscribe from the event
        }
    }

    private void HandleScoreChange(int newScore)
    {
        Debug.Log("Score changed: " + newScore);
        float randomValue = Random.Range(0f, 100f);

        if (randomValue < chanceToChangeGravity)
        {
            StartCoroutine(GravityChange());
            
        }
    }

    private IEnumerator GravityChange()
    {
        if (gravityup)
        {
            GravityAnim.SetTrigger("ChangeGravityDown");
            gravityup = false;
        }
        else
        {
            GravityAnim.SetTrigger("ChangeGravityUP");
            gravityup = true;
        }

        // Poczekaj, aż animacja się zakończy
        yield return new WaitForSeconds(0.7f);

        playerRigidBody.gravityScale *= -1;

        yield return null;
    }

}