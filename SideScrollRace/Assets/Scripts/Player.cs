using Fusion;

public class Player : NetworkBehaviour
{
    private NetworkCharacterControllerPrototype cc;

    private void Awake()
    {
        cc = GetComponent<NetworkCharacterControllerPrototype>();
    }

    private void Update()
    {

    }
    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            data.direction.Normalize();
            cc.Move(5 * data.direction * Runner.DeltaTime);

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
