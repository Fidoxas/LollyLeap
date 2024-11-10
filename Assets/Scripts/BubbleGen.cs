using Assets.Scripts.LevelGenerator;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[Serializable]
public class BubbleToSpawn
{
    public GameObject item;
    public int chance;
}

public class BubbleGen : MonoBehaviour
{
    public BubbleToSpawn[] spawnerItems;

    [FormerlySerializedAs("BubbleMadnessItems")]
    public BubbleToSpawn[] bubbleMadnessItems;

    private List<GameObject> objectGroup;
    public Vector3 spawnAreaSize;
    public float timeBetweenSpawns;
    public int numberOfObjectsToGenerate;
    public int levelToGenerate = 1;


    private int objectsCreated = 0;
    private const int ReductionThreshold = 40;
    private const float ReductionAmount = 0.5f;
    private const float MinimumTimeBetweenSpawns = 3.5f;

    private void Start()
    {
        CurrentScene.OnLevelChange += OnLevelChange;
        LoadValues();
        ObjectGroupCreator();
        StartCoroutine(GenerateObjectsWithDelay());
    }

    public void OnLevelChange()
    {
        Debug.Log("Level changed in BubbleGen");

        if (CurrentScene.Level.Endless)
        {
            UpdateLevel();
        }
    }

    private void UpdateLevel()
    {
        var lvl = CurrentScene.Level;
        LoadValues();
        UpdateObjectGroup();
    }

    private void LoadValues()
    {
        var levelSettings = CurrentScene.Level;
        timeBetweenSpawns = levelSettings.TimeBetweenSpawns;
        foreach (var obj in spawnerItems)
        {
            obj.chance = levelSettings.ChanceToRespawnBubble;
            if (obj.chance <= 0)
                obj.chance = 0;
        }

        if (levelSettings.BubbleMadness)
            foreach (var obj in bubbleMadnessItems)
            {
                obj.chance = levelSettings.ChanceToRespawnBubble;
                if (obj.chance <= 0)
                    obj.chance = 0;
            }
    }

    private void ObjectGroupCreator()
    {
        objectGroup = new List<GameObject>();
        UpdateObjectGroup();
    }

    private void UpdateObjectGroup()
    {
        var levelSettings = CurrentScene.Level;
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

        if (levelSettings.BubbleMadness)
            foreach (var obj in bubbleMadnessItems)
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
        var levelSettings = CurrentScene.Level;
        for (int i = 0; i < numberOfObjectsToGenerate; i++)
        {
            int randomChance = Random.Range(1, 101);
            int selectedObjIndex = -1;

            if (randomChance <= levelSettings.ChanceToRespawnBubble)
            {
                selectedObjIndex = Random.Range(0, objectGroup.Count);
            }

            if (selectedObjIndex != -1)
            {
                Vector2 randomPosition = new Vector2(
                    Random.Range(transform.position.x - spawnAreaSize.x / 2f,
                        transform.position.x + spawnAreaSize.x / 2f),
                    Random.Range(transform.position.y - spawnAreaSize.y / 2f,
                        transform.position.y + spawnAreaSize.y / 2f));

                GameObject newObject = Instantiate(objectGroup[selectedObjIndex], randomPosition, Quaternion.identity);
                newObject.transform.parent = transform;
            }

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