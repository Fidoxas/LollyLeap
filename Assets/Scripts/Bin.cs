using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        DestroyWithChildren(other.gameObject);
    }

    private void DestroyWithChildren(GameObject obj)
    {
        foreach (Transform child in obj.transform)
        {
            DestroyWithChildren(child.gameObject);
        }

        Destroy(obj);
    }
}