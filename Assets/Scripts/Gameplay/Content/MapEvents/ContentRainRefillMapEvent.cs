using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRainRefillMapEvent : GameMapEvent
{
    private int m_markerToCheck;

    public ContentRainRefillMapEvent(int markerToCheck)
    {
        m_name = "Rain Refill";
        m_desc = "The waters have returned to their natural state.";

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
