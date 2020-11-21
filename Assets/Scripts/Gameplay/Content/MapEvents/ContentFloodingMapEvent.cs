using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFloodingMapEvent : GameMapEvent
{
    private int m_markerToCheck;

    public ContentFloodingMapEvent(int markerToCheck)
    {
        m_name = "Flooding";
        m_desc = "The great river has flooded!";

        m_markerToCheck = markerToCheck;
    }

    public override void TriggerEvent()
    {
        for (int i = 0; i < WorldGridManager.Instance.m_gridArray.Length; i++)
        {
            GameTile gameTile = WorldGridManager.Instance.m_gridArray[i].GetGameTile();
            if (gameTile.HasEventMarker(m_markerToCheck))
            {
                gameTile.SetTerrain(new ContentWaterTerrain(), true);
            }
        }
    }
}
