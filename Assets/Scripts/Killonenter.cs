using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killonenter : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Got collision");
        var player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player != null && !PowerInterface.isShielded)
        {
            player.GetHit();
        }
        else if (PowerInterface.isShielded)
        {
            Destroy(gameObject);
            player.GetHit();
        }
    }
}