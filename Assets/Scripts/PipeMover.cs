using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.LevelGenerator;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PipeMover : MonoBehaviour
{
    [FormerlySerializedAs("_minSpeed")] [SerializeField]
    private int minSpeed;

    [FormerlySerializedAs("_maxSpeed")] [SerializeField]
    private int maxSpeed;

    [SerializeField] private int rushSpeed;

    [SerializeField] float wiggleSpeed;
    [SerializeField] float wiggleAmplitude;
    [SerializeField] private float wiggleTime;


    private int speed = 0;
    private bool isWiggling;


    private void Start()
    {
        LevelSettings levelSettings = CurrentScene.Level;
        if (levelSettings != null && levelSettings.Rushmode)
        {
            // ReSharper disable once HeapView.ObjectAllocation
            StartCoroutine(RushLollies());
        }
        else
            speed = Random.Range(minSpeed, maxSpeed);
    }

    IEnumerator RushLollies()
    {
        yield return null;

        var transform1 = transform;
        transform1.position = new Vector2(8, transform1.position.y);
        // ReSharper disable once HeapView.ObjectAllocation
        StartCoroutine(WiggleCoroutine());
        yield return new WaitForSeconds(wiggleTime);
        isWiggling = false;
        speed = rushSpeed;
    }

    IEnumerator WiggleCoroutine()
    {
        isWiggling = true;

        float elapsedTime = 0f;
        float initialAmplitude = wiggleAmplitude;
        Vector2 initialPosition = transform.position;

        while (isWiggling && elapsedTime < wiggleTime)
        {
            float newY = initialPosition.y + Mathf.Sin(Time.time * wiggleSpeed) *
                Mathf.Lerp(initialAmplitude, 0f, elapsedTime / wiggleTime);
            transform.position = new Vector2(transform.position.x, newY);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Resetuj pozycję po zakończeniu wiggling
        transform.position = initialPosition;

        // Coroutine zostanie automatycznie zakończona po wyjściu z pętli while
        yield break;
    }

    private void Update()
    {
        transform.position += Vector3.left * (speed * Time.deltaTime);
    }
}