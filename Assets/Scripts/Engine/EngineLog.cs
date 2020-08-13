using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineLog : MonoBehaviour
{
    public static void LogError(string message)
    {
        print("ERROR!  " + message);
    }

    public static void LogWarning(string message)
    {
        print("Warning!  " + message);
    }

    public static void LogInfo(string message)
    {
        print("Info:  " + message);
    }
}
