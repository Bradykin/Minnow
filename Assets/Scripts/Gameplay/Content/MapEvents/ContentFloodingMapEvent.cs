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
        List<GameTile> eventTiles = WorldGridManager.Instance.GetTilesWithEventMarker(m_markerToCheck);
        for (int i = 0; i < eventTiles.Count; i++)
        {
            eventTiles[i].SetTerrain(new ContentWaterTerrain(), true);
        }
    }
}
