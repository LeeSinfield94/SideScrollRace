using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTimer : MonoBehaviour
{
    private static float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    public static float GetCurrentTime()
    {
        return timer;
    }
}
