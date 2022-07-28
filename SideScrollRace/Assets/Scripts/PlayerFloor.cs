using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloor : NetworkBehaviour
{
    [SerializeField] private ObstacleType obstacleType;
    private Player myPlayer;
    
    public void GetObjects()
    {
        ObjectPooler.instance.SpawnObjects();
        for(int i = 0; i < ObjectPooler.instance.spikeObjects.Count; i++)
        {
            ObjectPooler.instance.spikeObjects[i].transform.parent = this.transform;
        }
        for (int i = 0; i < ObjectPooler.instance.slowObjects.Count; i++)
        {
            ObjectPooler.instance.slowObjects[i].transform.parent = this.transform;
        }
    }
    public void SpawnObstacleOnFloor(ObstacleType type)
    {
        GameObject go = ObjectPooler.instance.GetObject(type);
        go.SetActive(true);
        go.transform.SetParent(this.transform);
    }
}
