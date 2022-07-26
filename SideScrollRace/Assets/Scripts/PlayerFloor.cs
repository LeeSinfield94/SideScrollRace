using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloor : NetworkBehaviour
{
    [SerializeField] private ObstacleType obstacleType;
    private Player myPlayer;
    public void SpawnSlowObstacleOnFloor()
    {
        SlowObstacle go = Instantiate<SlowObstacle>(obstacleType.slowObstacle, transform);
    }
}
