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
        List<GameTile> eventTiles = WorldGridManager.Instance.GetTilesWithEventMarker(m_markerToCheck);
        for (int i = 0; i < eventTiles.Count; i++)
        {
            eventTiles[i].SetTerrain(new ContentScrublandPlainsTerrain(), true); // Should be muddy dirt or something
        }
    }
}
