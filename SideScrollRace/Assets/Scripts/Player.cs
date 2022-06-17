using Fusion;

public class Player : NetworkBehaviour
{
    private NetworkCharacterControllerPrototype cc;

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
        }
    }
}
