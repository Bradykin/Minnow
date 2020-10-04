using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDrySeasonMapEvent : GameMapEvent
{
    private List<GameTile> m_affectedTiles = new List<GameTile>();

    public ContentDrySeasonMapEvent()
    {
        m_name = "Dry Season";
        m_desc = "The waters have all dried up!";
    }

    public override void TriggerEvent()
    {
        for (int i = 0; i < WorldGridManager.Instance.m_gridArray.Length; i++)
        {
            GameTile gameTile = WorldGridManager.Instance.m_gridArray[i].GetGameTile();
            if (gameTile.GetTerrain().IsWater())
            {
                m_affectedTiles.Add(gameTile);
            }
        }
        
        for (int i = 0; i < m_affectedTiles.Count; i++)
        {
            m_affectedTiles[i].SetTerrain(new ContentScrublandPlainsTerrain(), true);
        }
    }

    public override void EndEvent()
    {
        for (int i = 0; i < m_affectedTiles.Count; i++)
        {
            m_affectedTiles[i].SetTerrain(new ContentWaterTerrain(), true);
        }
    }
}
