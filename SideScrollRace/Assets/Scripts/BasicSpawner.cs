using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicSpawner : MonoBehaviour, INetworkRunnerCallbacks
{

    [SerializeField] private NetworkPrefabRef playerPrefab;
    [SerializeField] private GameObject playerFloor;

    private bool PButton = false;
    private Transform playerPrefabSpawnLocation;
    private NetworkRunner runner;
    private Dictionary<PlayerRef, NetworkObject> spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();

    private void OnGUI()
    {
        if (runner == null)
        {
            if (GUI.Button(new Rect(0, 0, 200, 40), "Host"))
            {
                StartGame(GameMode.Host);
            }
            if (GUI.Button(new Rect(0, 40, 200, 40), "Join"))
            {
                StartGame(GameMode.Client);
            }
        }
    }
    async void StartGame(GameMode mode)
    {
        // Create the Fusion runner and let it know that we will be providing user input
        runner = gameObject.AddComponent<NetworkRunner>();
        runner.ProvideInput = true;

        // Start or join (depends on gamemode) a session with a specific name
        await runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            SessionName = "SideScrollerRaceRoom",
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }
    private void Update()
    {
        PButton = PButton | Input.GetKey(KeyCode.P);
    }
    public void OnConnectedToServer(NetworkRunner runner)
    {
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        var data = new NetworkInputData();

        if(!data.canMoveForward)
        {
            data.direction += Vector3.forward;
        }
        else
        {
            print("Can't Move Forward");
        }
        if (Input.GetKey(KeyCode.A))
        {
            data.direction += Vector3.left;
        }
        if(Input.GetKey(KeyCode.D))
        {
            data.direction -= Vector3.left;
        }
        if (PButton)
        {
            data.buttons |= NetworkInputData.P;
        }
        PButton = false;

        input.Set(data);
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        // Create a unique position for the player
        GameObject newFloor = Instantiate(playerFloor, new Vector3(player.PlayerId * 20, -1, 0), Quaternion.identity);
        newFloor.name = player.PlayerId + "Floor";
        playerPrefabSpawnLocation = newFloor.GetComponentInChildren<SpawnLocation>().transform;
        NetworkObject networkPlayerObject = runner.Spawn(playerPrefab, playerPrefabSpawnLocation.position, Quaternion.identity, player);
        // Keep track of the player avatars so we can remove it when they disconnect
        spawnedCharacters.Add(player, networkPlayerObject);
        if(networkPlayerObject.HasInputAuthority)
        {
            Camera.main.GetComponent<CameraFollow>().Init(networkPlayerObject.transform);
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        // Find and remove the players avatar
        if (spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
        {
            runner.Despawn(networkObject);
            spawnedCharacters.Remove(player);
        }
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
    }
}
