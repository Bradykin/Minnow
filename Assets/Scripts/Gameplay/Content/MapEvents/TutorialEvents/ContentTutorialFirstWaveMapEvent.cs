using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTutorialFirstWaveMapEvent : GameMapEvent
{
    public ContentTutorialFirstWaveMapEvent()
    {
        m_name = "Explore";
        m_desc = "Explore the nearby area and protect your Castle!";

        m_triggerType = ScheduledActionTime.StartOfWave;
    }

    public override void TriggerEvent()
    {
        //Left as stub
    }
}
