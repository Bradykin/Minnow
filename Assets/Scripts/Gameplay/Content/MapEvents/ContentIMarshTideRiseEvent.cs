using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMarshTideRiseEvent : GameMapEvent
{
    private int m_markerToCheck;

    public ContentMarshTideRiseEvent(int markerToCheck)
    {
        m_name = "Marsh Tide Rise";
        m_desc = "The waters rise, sinking much land beneath the waves.";

        m_markerToCheck = markerToCheck;
    }

    public override void TriggerEvent()
    {
        for (int i = 0; i < WorldGridManager.Instance.m_gridArray.Length; i++)
        {
            GameTile gameTile = WorldGridManager.Instance.m_gridArray[i].GetGameTile();
            if (gameTile.HasEventMarker(m_markerToCheck))
            {
                if (gameTile.GetTerrain().GetMarshTideRiseTerrainType() != null)
                {
                    gameTile.SetTerrain(GameTerrainFactory.GetMarshTideRiseTerrainClone(gameTile.GetTerrain()));
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
