using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFloodReceedingMapEvent : GameMapEvent
{
    private int m_markerToCheck;

    public ContentFloodReceedingMapEvent(int markerToCheck)
    {
        m_name = "Flood Receeded";
        m_desc = "The flood has receeded!";

        m_markerToCheck = markerToCheck;
    }

    public override void TriggerEvent()
    {
        List<GameTile> eventTiles = WorldGridManager.Instance.GetTilesWithEventMarker(m_markerToCheck);
        for (int i = 0; i < eventTiles.Count; i++)
        {
            eventTiles[i].SetTerrain(new ContentGrassPlainsTerrain(), true);
        }
    }
}
