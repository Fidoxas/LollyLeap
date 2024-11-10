using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class TutorialGen : MonoBehaviour
{
    public int numberOfObjectsToGenerate;
    public Vector3 spawnAreaSize;
    public float timeBetweenSpawns;
    public GameObject itemToGenerate; // Object to generate
    private int objectsCreated = 0;
    private const int ReductionThreshold = 40;
    private const float ReductionAmount = 0.5f;

    private const float MinimumTimeBetweenSpawns = 4.0f;

    // ... (istniejące elementy)
    private void Start()
    {
        StartCoroutine(GenerateObjectsWithDelay());
    }

    public IEnumerator GenerateObjectsWithDelay()
    {
        for (int i = 0; i < numberOfObjectsToGenerate; i++)
        {
            Vector2 randomPosition = new Vector2(
                Random.Range(transform.position.x - spawnAreaSize.x / 2f, transform.position.x + spawnAreaSize.x / 2f),
                Random.Range(transform.position.y - spawnAreaSize.y / 2f, transform.position.y + spawnAreaSize.y / 2f));

            GameObject newObject = Instantiate(itemToGenerate, randomPosition, Quaternion.identity);

            // Setting the parent of the newly created object to this GameObject
            newObject.transform.SetParent(transform);

            objectsCreated++;
            if (objectsCreated >= ReductionThreshold)
            {
                objectsCreated = 0;
                timeBetweenSpawns -= ReductionAmount;
                if (timeBetweenSpawns < MinimumTimeBetweenSpawns)
                {
                    timeBetweenSpawns = MinimumTimeBetweenSpawns;
                }
            }

            // Pause the execution for a specified duration before generating the next object
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    public void DeactivateGeneratedObjects()
    {
        // Iteruj przez wszystkie dzieci i dezaktywuj je
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        // Drawing a wire cube gizmo to visualize the spawn area
        Gizmos.color = Color.red;

        Vector3 center = transform.position;
        Vector3 size = new Vector3(spawnAreaSize.x, spawnAreaSize.y, 0);

        Gizmos.DrawWireCube(center, size);
    }
}