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
        for (int i = 0; i < WorldGridManager.Instance.m_gridArray.Length; i++)
        {
            GameTile gameTile = WorldGridManager.Instance.m_gridArray[i].GetGameTile();
            if (gameTile.m_gameEventMarkers.Contains(m_markerToCheck))
            {
                gameTile.SetTerrain(new ContentGrassPlainsTerrain(), true);
            }
        }
    }
}
