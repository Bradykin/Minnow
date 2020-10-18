using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameMapEvent : GameElementBase
{
    public ScheduledActionTime m_triggerType = ScheduledActionTime.StartOfWave;

    public abstract void TriggerEvent();

    public string GetDesc()
    {
        return m_desc;
    }
}
