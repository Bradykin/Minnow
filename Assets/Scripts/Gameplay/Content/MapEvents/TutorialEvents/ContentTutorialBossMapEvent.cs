using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTutorialBossMapEvent : GameMapEvent
{
    public ContentTutorialBossMapEvent()
    {
        m_name = "Boss Arrival!";
        m_desc = "Defeat the boss that appears this wave to win the run!";

        m_triggerType = ScheduledActionTime.EndOfWave;
    }

    public override void TriggerEvent()
    {
        //Left as stub
    }
}
