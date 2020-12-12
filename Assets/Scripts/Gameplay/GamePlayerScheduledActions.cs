using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScheduledActionTime : int
{
    EndOfTurn,
    StartOfTurn,
    EndOfWave,
    StartIntermission,
    StartOfWave
}

[Serializable]
public struct GamePlayerScheduledActions
{
    public ScheduledActionTime scheduledActionTime;
    public GameAction gameAction;
}
