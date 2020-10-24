using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentVolcanoEruptionEvent : GameMapEvent
{
    private int m_markerToCheck;

    public ContentVolcanoEruptionEvent(int markerToCheck)
    {
        m_name = "Volcano Eruption";
        m_desc = "Volcanoes erupt, burning the terrain and covering it with active lava. Run!";

        m_markerToCheck = markerToCheck;
    }

    public override void TriggerEvent()
    {
        for (int i = 0; i < WorldGridManager.Instance.m_gridArray.Length; i++)
        {
            GameTile gameTile = WorldGridManager.Instance.m_gridArray[i].GetGameTile();
            if (gameTile.m_gameEventMarkers.Contains(m_markerToCheck))
            {
                //Do thing
            }
        }
    }
}
