using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScroller : MonoBehaviour
{
    float distance;

    [Range(0f, 10f)] public float speed = 10f;

    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }
}