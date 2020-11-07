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

    public int numRareUnitOptionsOffered;
    public int previousRareUnitOptionWave;

    public bool savedInIntermission;
    public JsonGameCardData jsonIntermissionCardDataOne;
    public JsonGameCardData jsonIntermissionCardDataTwo;
    public JsonGameCardData jsonIntermissionCardDataThree;


    public JsonGamePlayerData jsonGamePlayerData;
    public JsonGameOpponentData jsonGameOpponentData;
}