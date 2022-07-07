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
    private void Awake()
    {
        cc = GetComponent<NetworkCharacterControllerPrototype>();
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            data.direction.Normalize();
            cc.Move(5 * data.direction * Runner.DeltaTime);
            data.canMoveForward = canMoveForward;
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
