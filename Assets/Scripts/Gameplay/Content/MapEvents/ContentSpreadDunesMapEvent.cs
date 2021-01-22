using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSpreadDunesMapEvent : GameMapEvent
{
    private int m_markerToCheck;

    public ContentSpreadDunesMapEvent(int markerToCheck)
    {
        m_name = "Spread Dunes";
        m_desc = "The dunes have spread, covering more of the land!";

        m_markerToCheck = markerToCheck;
    }

    public override void TriggerEvent()
    {
        for (int i = 0; i < WorldGridManager.Instance.m_gridArray.Length; i++)
        {
            GameTile gameTile = WorldGridManager.Instance.m_gridArray[i].GetGameTile();

            if (!gameTile.GetTerrain().IsDunes())
            {
                continue;
            }

            List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(gameTile, 1);
            for (int k = 0; k < surroundingTiles.Count; k++)
            {
                if (surroundingTiles[k].HasBuilding())
                {
                    continue;
                }
                
                GameTerrainBase gameTerrain = surroundingTiles[k].GetTerrain();
                if (gameTerrain.IsDunes())
                {
                    continue;
                }

                if (gameTerrain.IsHill() || gameTerrain.IsMountain() || gameTerrain.IsWater())
                {
                    continue;
                }

                surroundingTiles[k].SetTerrain(new ContentDesertDunesTerrain(), false);
            }
        }



        List<GameTile> eventTiles = WorldGridManager.Instance.GetTilesWithEventMarker(m_markerToCheck);
        for (int i = 0; i < eventTiles.Count; i++)
        {
            eventTiles[i].SetTerrain(new ContentWaterTerrain(), true);
        }
    }
}
