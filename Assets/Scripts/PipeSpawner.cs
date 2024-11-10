using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PipeSpawner : MonoBehaviour
{
    [FormerlySerializedAs("_timeToSpawn")] [SerializeField]
    private float timeToSpawn = 1;

    [FormerlySerializedAs("_pipe")] [SerializeField]
    private GameObject pipe;

    [FormerlySerializedAs("_height")] [SerializeField]
    private float height;

    private float timer;

    private void Update()
    {
        if (timer > timeToSpawn)
        {
            GameObject newPipe = Instantiate(pipe);
            newPipe.transform.position = transform.position + new Vector3(0, Random.Range(-height, height), 0);
            Destroy(newPipe, 33f);
            timer = 0;
        }

        timer += Time.deltaTime;
    }
}