using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDrySeasonMapEvent : GameMapEvent
{
    private int m_markerToCheck;

    public ContentDrySeasonMapEvent(int markerToCheck)
    {
        m_name = "Dry Season";
        m_desc = "The waters have all dried up!";

        m_markerToCheck = markerToCheck;
    }

    public override void TriggerEvent()
    {
        for (int i = 0; i < WorldGridManager.Instance.m_gridArray.Length; i++)
        {
            GameTile gameTile = WorldGridManager.Instance.m_gridArray[i].GetGameTile();
            if (gameTile.HasEventMarker(m_markerToCheck))
            {
                gameTile.SetTerrain(new ContentScrublandPlainsTerrain(), true); //Should be muddy dirt or something
            }
        }
    }
}
