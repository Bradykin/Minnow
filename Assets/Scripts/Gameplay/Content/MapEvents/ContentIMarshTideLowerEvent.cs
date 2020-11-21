using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMarshTideLowerEvent : GameMapEvent
{
    private int m_markerToCheck;

    public ContentMarshTideLowerEvent(int markerToCheck)
    {
        m_name = "Marsh Tide Lower";
        m_desc = "The waters receed, exposing new land to explore.";

        m_markerToCheck = markerToCheck;
    }

    public override void TriggerEvent()
    {
        for (int i = 0; i < WorldGridManager.Instance.m_gridArray.Length; i++)
        {
            GameTile gameTile = WorldGridManager.Instance.m_gridArray[i].GetGameTile();
            if (gameTile.HasEventMarker(m_markerToCheck))
            {
                if (gameTile.GetTerrain().GetMarshTideLowerTerrainType() != null)
                {
                    gameTile.SetTerrain(GameTerrainFactory.GetMarshTideLowerTerrainClone(gameTile.GetTerrain()));
                    if (gameTile.GetBuilding() != null)
                    {
                        if (!gameTile.GetBuilding().IsValidTerrainToPlace(gameTile.GetTerrain(), gameTile))
                        {
                            gameTile.ClearBuilding();
                        }
                    }
                }
            }
        }
    }
}
