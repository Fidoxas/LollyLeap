using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BubbleMove : MonoBehaviour
{
    private Vector3 initialPosition;
    private float screenWidth;
    private float screenHeight;

    [FormerlySerializedAs("AmplitudeOffsetFactor")]
    public float amplitudeOffsetFactor = 0.2f;

    [FormerlySerializedAs("XSpeedFactor")] public float xSpeedFactor = 0.2f;

    [FormerlySerializedAs("MinYSpeedFactor")]
    public float minYSpeedFactor = 0.5f;

    [FormerlySerializedAs("MaxYSpeedFactor")]
    public float maxYSpeedFactor = 1.5f;

    [FormerlySerializedAs("MinAmplitudeFactor")]
    public float minAmplitudeFactor = 0.5f;

    [FormerlySerializedAs("MaxAmplitudeFactor")]
    public float maxAmplitudeFactor = 1.5f;

    private float amplitudeOffset;
    private float xSpeed;
    private float ySpeed;
    private float minAmplitude;
    private float maxAmplitude;

    void Start()
    {
        initialPosition = transform.position;
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        amplitudeOffset = amplitudeOffsetFactor * screenHeight;
        xSpeed = xSpeedFactor * screenWidth;
        ySpeed = Random.Range(minYSpeedFactor * screenHeight, maxYSpeedFactor * screenHeight);
        minAmplitude = minAmplitudeFactor * screenHeight;
        maxAmplitude = maxAmplitudeFactor * screenHeight;
    }

    void Update()
    {
        float newPositionY = initialPosition.y + Mathf.Sin(Time.time * ySpeed) * (maxAmplitude - minAmplitude) / 2 +
                             minAmplitude + amplitudeOffset;
        Vector3 newPosition = new Vector3(transform.position.x, newPositionY, transform.position.z);
        transform.position = newPosition;

        transform.Translate(Vector3.left * xSpeed * Time.deltaTime);
    }
}