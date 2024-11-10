using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Move : MonoBehaviour
{
    Vector2 startPosition;

    [FormerlySerializedAs("_direction")] [SerializeField]
    Vector2 direction = Vector2.up;

    [FormerlySerializedAs("_minDistance")] [SerializeField]
    float minDistance = 1;

    [FormerlySerializedAs("_maxDistance")] [SerializeField]
    float maxDistance = 2;

    [FormerlySerializedAs("_minSpeed")] [SerializeField]
    float minSpeed = 1;

    [FormerlySerializedAs("_maxSpeed")] [SerializeField]
    float maxSpeed = 2;

    private float speed;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        speed = Random.Range(minSpeed, maxSpeed);
        maxDistance = Random.Range(minDistance, maxDistance);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction.normalized * Time.deltaTime * speed);
        var distance = Vector2.Distance(startPosition, transform.position);
        if (distance >= maxDistance)
        {
            ChangeDirection();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Sprawdzenie, czy kolizja dotyczy gracza
        if (collision.CompareTag("Bouncer"))
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        startPosition = transform.position;
        direction *= -1;
    }
}