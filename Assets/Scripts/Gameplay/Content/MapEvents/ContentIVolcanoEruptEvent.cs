using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentVolcanoEruptionEvent : GameMapEvent
{
    private int m_markerToCheck;
    private bool m_onlyVolcano;

    public ContentVolcanoEruptionEvent(int markerToCheck, bool onlyVolcano)
    {
        m_name = "Volcano Eruption";
        m_desc = "Volcanoes erupt, burning the terrain and covering it with active lava. Run!";

        m_onlyVolcano = onlyVolcano;

        m_markerToCheck = markerToCheck;
    }

    public override void TriggerEvent()
    {
        for (int i = 0; i < WorldGridManager.Instance.m_gridArray.Length; i++)
        {
            GameTile gameTile = WorldGridManager.Instance.m_gridArray[i].GetGameTile();

            if (gameTile.m_gameEventMarkers.Contains(m_markerToCheck))
            {
                if (gameTile.HasBuilding() && gameTile.GetBuilding().GetTeam() == Team.Player)
                {
                    gameTile.GetBuilding().Die();
                }

                if (gameTile.GetTerrain().IsVolcano())
                {
                    gameTile.SetTerrain(GameTerrainFactory.GetVolcanoEruptTerrainClone(gameTile.GetTerrain()));

                    if (m_onlyVolcano)
                    {
                        List<WorldTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingWorldTiles(gameTile.GetWorldTile(), 2, 0);
                        for (int k = 0; k < surroundingTiles.Count; k++)
                        {
                            surroundingTiles[k].ClearFog();
                        }
                    }
                }
                else if (!m_onlyVolcano)
                {
                    gameTile.SetTerrain(GameTerrainFactory.GetTerrainClone(new ContentLavaFieldActiveTerrain()));
                }
            }
        }
    }
}
