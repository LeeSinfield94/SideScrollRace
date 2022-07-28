using Fusion;
using UnityEngine;
public class Player : NetworkBehaviour
{
    [SerializeField] private float startSpeed;
    [SerializeField] private PlayerFloor myFloor;
    [SerializeField] private PlayerFloor opponentsFloor;
    public PlayerFloor MyFloor
    {
        set 
        { 
            myFloor = value;
        }
    }
    public PlayerFloor OpponentsFloor
    {
        set
        {
            opponentsFloor = value;
        }
    }
    private float slowSpeed = 2;


    private NetworkCharacterControllerPrototype cc;
    private bool canMoveForward = true;
    public bool CanMoveForward
    {
        set { canMoveForward = value; }
    }

    private Vector3 currentForward;
    private void Awake()
    {
        cc = GetComponent<NetworkCharacterControllerPrototype>();
        cc.acceleration = startSpeed;
        cc.maxSpeed = startSpeed;
    }

    public void SetSpeed(bool isSlow)
    {
        cc.acceleration = isSlow ? slowSpeed : startSpeed;
        cc.maxSpeed = isSlow ? slowSpeed : startSpeed;
    }
    private void Update()
    {
        if (canMoveForward)
        {
            currentForward += Vector3.forward;
        }
        else
        {
            currentForward = new Vector3(0, 0, 0);
        }
    }

    public void SpawnSpikeForOtherPlayer()
    {
        opponentsFloor.SpawnObstacleOnFloor(ObstacleType.SPIKE);
    }
    public void SpawnSlowForOtherPlayer()
    {
        opponentsFloor.SpawnObstacleOnFloor(ObstacleType.SLOW);
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            data.direction.Normalize();
            cc.Move(5 * data.direction + currentForward * Runner.DeltaTime);
            if ((data.buttons & NetworkInputData.P) != 0)
            {
                SetMyTime();
            }
        }
    }

    public void SetMyTime()
    {
        GameManager.SetPlayerTime(this);
        print(GameManager.GetPlayersCurrentTime(this));
    }
}
