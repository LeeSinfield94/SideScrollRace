using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct ObjectsToSpawn
{
    public int amountToSpawn;
    public GameObject objectToSpawn;
}
public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private List<ObjectsToSpawn> objectsToSpawn = new List<ObjectsToSpawn>();

    public List<GameObject> slowObjects = new List<GameObject>();
    public List<GameObject> spikeObjects = new List<GameObject>();
    public static ObjectPooler instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        foreach(ObjectsToSpawn objectToSpawn in objectsToSpawn)
        {
            for(int i = 0; i < objectToSpawn.amountToSpawn; i++)
            {
                GameObject go = Instantiate(objectToSpawn.objectToSpawn);
                AddToList(go);
                go.SetActive(false);
            }
        }
    }
    public void AddToList(GameObject go)
    {
        if (go.CompareTag("Slow"))
        {
            slowObjects.Add(go);
        }
        else
        {
            spikeObjects.Add(go);
        }
    }
    public GameObject GetObject(ObstacleType type)
    {
        switch (type)
        {
            case ObstacleType.SLOW:
                return GetSlowObject();
            case ObstacleType.SPIKE:
                return GetSpikeObject();
        }
        return null;
    }

    public GameObject GetSlowObject()
    {
        return null;
    }

    public GameObject GetSpikeObject()
    {
        return null;
    }
}
