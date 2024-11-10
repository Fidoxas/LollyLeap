using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TutorialBubbleGen : MonoBehaviour
{
    public BubbleToSpawn[] spawnerItems;

    [FormerlySerializedAs("BubbleMadnessItems")]
    public BubbleToSpawn[] bubbleMadnessItems;

    private List<GameObject> objectGroup;
    public Vector3 spawnAreaSize;
    public float timeBetweenSpawns;
    public int numberOfObjectsToGenerate;

    private int objectsCreated = 0;
    private const int ReductionThreshold = 40;
    private const float ReductionAmount = 0.5f;
    private const float MinimumTimeBetweenSpawns = 3.5f;

    [FormerlySerializedAs("BubbleMadness")] [SerializeField]
    private bool bubbleMadness;

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
            if (obj.item != null)
            {
                for (int i = 0; i < obj.chance; i++)
                {
                    objectGroup.Add(obj.item);
                }
            }
        }

        if (bubbleMadness)
        {
            foreach (var obj in bubbleMadnessItems)
            {
                if (obj.item != null)
                {
                    for (int i = 0; i < obj.chance; i++)
                    {
                        objectGroup.Add(obj.item);
                    }
                }
            }
        }
    }

    public IEnumerator GenerateObjectsWithDelay()
    {
        Debug.Log("Tworzenie obiektu bąbelka");
        for (int i = 0; i < numberOfObjectsToGenerate; i++)
        {
            int selectedObjIndex = Random.Range(0, objectGroup.Count);

            if (selectedObjIndex >= 0 && selectedObjIndex < objectGroup.Count)
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