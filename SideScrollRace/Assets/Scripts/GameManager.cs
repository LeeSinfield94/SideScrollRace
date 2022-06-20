using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private static Dictionary<Player, float> playerTime = new Dictionary<Player, float>();

    public static void SetPlayerTime(Player player)
    {
        if(!playerTime.ContainsKey(player))
        {
            playerTime.Add(player, RaceTimer.GetCurrentTime());
        }
    }

    public static float GetPlayersCurrentTime(Player player)
    {
        float time;
        playerTime.TryGetValue(player, out time);
        return time;
    }
}
