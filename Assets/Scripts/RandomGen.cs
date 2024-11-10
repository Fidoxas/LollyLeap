using Assets.Scripts.LevelGenerator;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[Serializable]
public class NormalLollySpawner
{
    public GameObject item;
    public float chance;
    public int chanceReductor;
}

[Serializable]
public class SpawnerList
{
    public GameObject item;
    public float chance;
    public int chanceReductor;
}

[Serializable]
public class SpecialLollySpawner
{
    public List<GameObject> specialLollyObjects = new List<GameObject>();
    public int chance = 1; // Ustaw szans� na 1 dla wszystkich special lollies
    public int chanceReductor = 0; // Nie pozw�l na redukcj� szansy
}

public class RandomGen : MonoBehaviour
{
    [FormerlySerializedAs("NormalLolly")] public NormalLollySpawner[] normalLolly;
    [FormerlySerializedAs("RedLolly")] public GameObject redLolly;
    [FormerlySerializedAs("PinkLolly")] public GameObject pinkLolly;
    [FormerlySerializedAs("PurpleLolly")] public GameObject purpleLolly;

    [FormerlySerializedAs("DarkPurpleLolly")]
    public GameObject darkPurpleLolly;

    public GameObject blueLolly;
    public GameObject tealLolly;
    SpecialLollySpawner specialLollySpawner;

    SpawnerList[] spawnlist;

    //public List<GameObject> objectGroup;
    public int numberOfObjectsToGenerate;
    public Vector3 spawnAreaSize;
    private Vector3 lastGeneratedPosition;
    private float timeBetweenSpawns;

    private int reductionThreshold;
    public static bool changingLevel;
    private const float ReductionAmount = 0.5f;
    private int objectsCreated;

    public void UpdateLevel()
    {
        //Debug.Log("Current level changed");
        //var lvl = CurrentScene.Level;
        //Debug.Log("Endless " + lvl.Endless);
        //Debug.Log("MaxSpread " + lvl.MaxSpread);
        //Debug.Log("HighestTimeBetweenSpawns " + lvl.HighestTimeBetweenSpawns);
        //Debug.Log("LowestTimeBetweenSpawns " + lvl.LowestTimeBetweenSpawns);
        LoadValues();
        SpecialLollyGroupCreator();
        ObjectGroupCreator();
    }

    public void OnLevelChange()
    {
        //Debug.Log("Level changed in RandomGen");

        if (CurrentScene.Level.Endless)
        {
            UpdateLevel();
        }
    }

    private void Start()
    {
        CurrentScene.OnLevelChange += OnLevelChange;
        UpdateLevel();
        StartCoroutine(GenerateObjectsWithDelay());
    }

    private void SpecialLollyGroupCreator()
    {
        
        LevelSettings levelSettings = CurrentScene.Level;
        int numberOfSpecialLollies = 1;


        if (levelSettings.RedLolly)
            numberOfSpecialLollies++;
        if (levelSettings.PinkLolly)
            numberOfSpecialLollies++;
        if (levelSettings.PurpleLolly)
            numberOfSpecialLollies++;
        if (levelSettings.DarkPurpleLolly)
            numberOfSpecialLollies++;
        if (levelSettings.BlueLolly)
            numberOfSpecialLollies++;
        if (levelSettings.TealLolly)
            numberOfSpecialLollies++;
        specialLollySpawner = new SpecialLollySpawner
        {
            chance = 1,
            chanceReductor = 0,
            specialLollyObjects = GetLollyPrefabs(levelSettings)
        };
    }


    private List<GameObject> GetLollyPrefabs(LevelSettings levelSettings)
    {
        List<GameObject> results = new List<GameObject>();
        if (levelSettings.RedLolly) results.Add(redLolly);
        if (levelSettings.PinkLolly) results.Add(pinkLolly);
        if (levelSettings.PurpleLolly) results.Add(purpleLolly);
        if (levelSettings.DarkPurpleLolly) results.Add(darkPurpleLolly);
        if (levelSettings.BlueLolly) results.Add(blueLolly);
        if (levelSettings.TealLolly) results.Add(tealLolly);
        return results;
    }


    private void LoadValues()
    {
        LevelSettings levelSettings = CurrentScene.Level;
        timeBetweenSpawns = levelSettings.TimeBetweenSpawns;
        reductionThreshold = levelSettings.ReductionThreshold;
        spawnAreaSize = levelSettings.MaxSpread;

        foreach (var obj in normalLolly)
        {
            obj.chance = levelSettings.NormalLollyChance;
            if (obj.chance <= 0)
                obj.chance = 0;
        }
    }

    private void ObjectGroupCreator()
    {
        LevelSettings levelSettings = CurrentScene.Level;
        int totalObjects = normalLolly.Length;
        if (levelSettings == null)
        {
            Debug.LogError("levelSettings is null in ObjectGroupCreator");
            return;
        }

        if (levelSettings == null || normalLolly == null)
        {
            Debug.LogError("levelSettings or NormalLolly is null in ObjectGroupCreator");
            return;
        }


        if (levelSettings.HasSpecial)
        {
            totalObjects += specialLollySpawner.specialLollyObjects.Count;
        }

        spawnlist = new SpawnerList[totalObjects];

        int currentIndex = 0;

        for (int i = 0; i < normalLolly.Length; i++)
        {
            spawnlist[currentIndex] = new SpawnerList
            {
                item = normalLolly[i].item,
                chance = normalLolly[i].chance,
                chanceReductor = normalLolly[i].chanceReductor
            };
            currentIndex++;
        }

        if (levelSettings.HasSpecial)
        {
            foreach (var specialLolly in specialLollySpawner.specialLollyObjects)
            {
                spawnlist[currentIndex] = new SpawnerList
                {
                    item = specialLolly,
                    chance = specialLollySpawner.chance,
                    chanceReductor = specialLollySpawner.chanceReductor
                };
                currentIndex++;
            }
        }

        //objectGroup = new List<GameObject>();
        //UpdateObjectGroup(levelSettings);
    }
    // private void UpdateObjectGroup(LevelSettings levelSettings)
    // {
    //     objectGroup.Clear();
    //
    //     foreach (var obj in NormalLolly)
    //     {
    //         if (obj.chance > 0)
    //         {
    //             for (int i = 0; i < obj.chance; i++)
    //             {
    //                 objectGroup.Add(obj.item);
    //             }
    //         }
    //     }
    //
    //     if (levelSettings.HasSpecial)
    //     {
    //         foreach (var obj in SpecialLollySpawner.specialLollyObjects)
    //         {
    //             objectGroup.Add(obj);
    //         }
    //     }
    // }

    // ReSharper disable Unity.PerformanceAnalysis
    public IEnumerator GenerateObjectsWithDelay()
    {
        LevelSettings levelSettings = CurrentScene.Level;
        float totalChance = 0;
        foreach (var obj in spawnlist)
        {
            totalChance += obj.chance;
        }

        for (int i = 0; i < numberOfObjectsToGenerate; i++)
        {
            if (totalChance <= 0)
            {
                Debug.LogWarning("Brak dost�pnych obiekt�w do wygenerowania.");
                yield break;
            }


            float randomChance = Random.Range(0, totalChance);

            int selectedObjIndex = -1;
            float cumulativeChance = 0;

            for (int j = 0; j < spawnlist.Length; j++)
            {
                cumulativeChance += spawnlist[j].chance;

                if (randomChance <= cumulativeChance)
                {
                    selectedObjIndex = j;
                    break;
                }
            }

            if (selectedObjIndex == -1)
            {
                selectedObjIndex = spawnlist.Length - 1;
            }

            Vector2 lastGeneratedPosition2D = new Vector2(lastGeneratedPosition.x, lastGeneratedPosition.y);

            Vector2 newObjectPosition;
            if (levelSettings.Tunnel)
            {
                float minY = Mathf.Max(lastGeneratedPosition.y - Mathf.Abs(levelSettings.Maxinterval),
                    transform.position.y - spawnAreaSize.y / 2f);
                float maxY = Mathf.Min(lastGeneratedPosition.y + Mathf.Abs(levelSettings.Maxinterval),
                    transform.position.y + spawnAreaSize.y / 2f);

                float newY = Random.Range(minY, maxY);
                newObjectPosition =
                    new Vector2(
                        Random.Range(transform.position.x - spawnAreaSize.x / 2f,
                            transform.position.x + spawnAreaSize.x / 2f), newY);
            }
            else
            {
                newObjectPosition = new Vector2(
                    Random.Range(transform.position.x - spawnAreaSize.x / 2f,
                        transform.position.x + spawnAreaSize.x / 2f),
                    Random.Range(transform.position.y - spawnAreaSize.y / 2f,
                        transform.position.y + spawnAreaSize.y / 2f));
            }

            // Pozosta�a cz�� kodu generowania obiekt�w
            GameObject newObject = Instantiate(spawnlist[selectedObjIndex].item,
                new Vector3(newObjectPosition.x, newObjectPosition.y, lastGeneratedPosition.z), Quaternion.identity);
            newObject.transform.parent = transform;
            lastGeneratedPosition = new Vector2(newObjectPosition.x, newObjectPosition.y);


            if (spawnlist[selectedObjIndex].chanceReductor > 0)
            {
                spawnlist[selectedObjIndex].chance -= spawnlist[selectedObjIndex].chanceReductor;

                if (spawnlist[selectedObjIndex].chance < 0)
                    spawnlist[selectedObjIndex].chance = 0;
            }

            totalChance -= spawnlist[selectedObjIndex].chanceReductor;
            objectsCreated++;
            Debug.Log(objectsCreated.ToString() +"objects Created");
           if (objectsCreated > reductionThreshold)
           {
               objectsCreated = 0;
               Debug.Log("resetObjects Created");
               if (timeBetweenSpawns! <= levelSettings.MinTimeBetweenSpawns &&
                   timeBetweenSpawns < levelSettings.MinTimeBetweenSpawns)
               {
                   timeBetweenSpawns -= ReductionAmount;
                   timeBetweenSpawns = levelSettings.TimeBetweenSpawns;
               }
           }


            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 center = transform.position;
        Vector3 size = new Vector3(spawnAreaSize.x, spawnAreaSize.y, 0);

        Gizmos.DrawWireCube(center, size);
    }
}