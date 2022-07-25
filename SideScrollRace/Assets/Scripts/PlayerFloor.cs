using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloor : NetworkBehaviour
{
    [SerializeField] private ObstacleType obstacleType;
    public void SpawnSlowObstacleOnFloor()
    {
        GameObject go = Instantiate(obstacleType.slowObstacle).gameObject;
        go.transform.SetParent(this.transform);
    }
}
