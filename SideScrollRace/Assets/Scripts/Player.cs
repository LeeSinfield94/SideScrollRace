using Fusion;
using UnityEngine;
public class Player : NetworkBehaviour
{
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
