using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct JsonGameControllerData
{
    public int currentWave;
    public int currentTurn;
    public int mapId;
    public int runExperienceAMount;

    public int randomSeed;

    public JsonGamePlayerData jsonGamePlayerData;
}