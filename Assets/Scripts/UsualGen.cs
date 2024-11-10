using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class SpawnerItem
{
    public GameObject item;
    public int chance;
    public int chanceReductor;
}

public class UsualGen : MonoBehaviour
{
    public SpawnerItem[] spawnerItems;
    private List<GameObject> objectGroup;
    public int numberOfObjectsToGenerate;
    public Vector3 spawnAreaSize;
    public float timeBetweenSpawns;

    // private int objectsCreated = 0;
    // private const int reductionThreshold = 40;
    // private const float reductionAmount = 0.5f;

    private void Start()
    {
        ObjectGroupCreator();
        StartCoroutine(GenerateObjectsWithDelay());
    }

    private void ObjectGroupCreator()
    {
        objectGroup = new List<GameObject>();
        UpdateObjectGroup();
    }

    private void UpdateObjectGroup()
    {
        objectGroup.Clear();
        foreach (var obj in spawnerItems)
        {
            if (obj.chance > 0)
            {
                for (int i = 0; i < obj.chance; i++)
                {
                    objectGroup.Add(obj.item);
                }
            }
        }
    }

    public IEnumerator GenerateObjectsWithDelay()
    {
        int totalChance = 0;
        foreach (var obj in spawnerItems)
        {
            totalChance += obj.chance;
        }

        for (int i = 0; i < numberOfObjectsToGenerate; i++)
        {
            if (totalChance <= 0)
            {
                Debug.LogWarning("Brak dostępnych obiektów do wygenerowania.");
                yield break;
            }

            int randomChance = Random.Range(0, totalChance);

            int selectedObjIndex = -1;
            int cumulativeChance = 0;

            for (int j = 0; j < spawnerItems.Length; j++)
            {
                cumulativeChance += spawnerItems[j].chance;

                if (randomChance < cumulativeChance)
                {
                    selectedObjIndex = j;
                    break;
                }
            }

            Vector2 randomPosition = new Vector2(
                Random.Range(transform.position.x - spawnAreaSize.x / 2f, transform.position.x + spawnAreaSize.x / 2f),
                Random.Range(transform.position.y - spawnAreaSize.y / 2f, transform.position.y + spawnAreaSize.y / 2f));

            GameObject newObject =
                Instantiate(spawnerItems[selectedObjIndex].item, randomPosition, Quaternion.identity);

            newObject.transform.parent = transform;

            if (spawnerItems[selectedObjIndex].chanceReductor > 0)
            {
                spawnerItems[selectedObjIndex].chance -= spawnerItems[selectedObjIndex].chanceReductor;

                if (spawnerItems[selectedObjIndex].chance < 0)
                    spawnerItems[selectedObjIndex].chance = 0;
            }

            totalChance -= spawnerItems[selectedObjIndex].chanceReductor;


            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 center = transform.position;
        Vector3 size = new Vector3(spawnAreaSize.x, spawnAreaSize.y, 0);

        Gizmos.DrawWireCube(center, size);
    }
}