using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSnapFreezeMapEvent : GameMapEvent
{
    private int m_markerToCheck;

    public ContentSnapFreezeMapEvent(int markerToCheck)
    {
        m_name = "Snap Freeze";
        m_desc = "The waters have frozen over!";

        m_markerToCheck = markerToCheck;
    }

    public override void TriggerEvent()
    {
        for (int i = 0; i < WorldGridManager.Instance.m_gridArray.Length; i++)
        {
            GameTile gameTile = WorldGridManager.Instance.m_gridArray[i].GetGameTile();
            if (gameTile.m_gameEventMarkers.Contains(m_markerToCheck))
            {
                gameTile.SetTerrain(new ContentScrublandPlainsTerrain(), true);  //nmartino: This should be ice
            }
        }
    }
}
