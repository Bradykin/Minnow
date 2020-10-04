using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameMapEvent : GameElementBase
{
    public abstract void TriggerEvent();
    public abstract void EndEvent();
}
