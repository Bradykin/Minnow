using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSnapThawMapEvent : GameMapEvent
{
    private int m_markerToCheck;

    public ContentSnapThawMapEvent(int markerToCheck)
    {
        m_name = "Thaw";
        m_desc = "The ice has thawed back out!";

        m_markerToCheck = markerToCheck;
    }

    public override void TriggerEvent()
    {
        for (int i = 0; i < WorldGridManager.Instance.m_gridArray.Length; i++)
        {
            GameTile gameTile = WorldGridManager.Instance.m_gridArray[i].GetGameTile();
            if (gameTile.m_gameEventMarkers.Contains(m_markerToCheck))
            {
                int r = Random.Range(0, 2);
                if (r == 0)
                {
                    gameTile.SetTerrain(new ContentIceWaterTerrain(), true);
                }
                else
                {
                    gameTile.SetTerrain(new ContentWaterTerrain(), true);
                }
            }
        }
    }
}
