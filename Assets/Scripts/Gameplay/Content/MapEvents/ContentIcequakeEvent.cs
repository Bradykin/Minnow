using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentIcequakeEvent : GameMapEvent
{
    private int m_markerToCheck;

    public ContentIcequakeEvent(int markerToCheck)
    {
        m_name = "Icequake";
        m_desc = "An earthquake has shook the land, breaking some of the ice around you!";

        m_markerToCheck = markerToCheck;
    }

    public override void TriggerEvent()
    {
        List<GameTile> tiles = new List<GameTile>();
        List<GameTile> visibleTiles = new List<GameTile>();
        for (int i = 0; i < WorldGridManager.Instance.m_gridArray.Length; i++)
        {
            GameTile gameTile = WorldGridManager.Instance.m_gridArray[i].GetGameTile();
            if (gameTile.GetTerrain().IsIceCracked())
            {
                tiles.Add(gameTile);

                if (gameTile.m_isFog == false)
                {
                    visibleTiles.Add(gameTile);
                }
            }
        }

        int numQuakes = 2;

        while (numQuakes > 0 && visibleTiles.Count > 0)
        {
            int index = Random.Range(0, visibleTiles.Count);
            GameTile gameTile = visibleTiles[index];

            if (gameTile.GetTerrain().IsIceCracked() && gameTile.GetTerrain().GetIceCrackedTerrainType() != null)
            {
                gameTile.SetTerrain(GameTerrainFactory.GetIceCrackedTerrainClone(gameTile.GetTerrain()));

                List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(gameTile, 1);
                for (int i = 0; i < surroundingTiles.Count; i++)
                {
                    if (surroundingTiles[i].GetTerrain().IsIce() && surroundingTiles[i].GetTerrain().GetIceCrackedTerrainType() != null)
                    {
                        surroundingTiles[i].SetTerrain(GameTerrainFactory.GetIceCrackedTerrainClone(surroundingTiles[i].GetTerrain()));
                    }
                }
                numQuakes--;
            }

            tiles.RemoveAt(index);
        }

        while (numQuakes > 0 && tiles.Count > 0)
        {
            int index = Random.Range(0, tiles.Count);
            GameTile gameTile = tiles[index];

            if (gameTile.GetTerrain().IsIceCracked() && gameTile.GetTerrain().GetIceCrackedTerrainType() != null)
            {
                gameTile.SetTerrain(GameTerrainFactory.GetIceCrackedTerrainClone(gameTile.GetTerrain()));

                List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(gameTile, 1);
                for (int i = 0; i < surroundingTiles.Count; i++)
                {
                    if (surroundingTiles[i].GetTerrain().IsIce() && surroundingTiles[i].GetTerrain().GetIceCrackedTerrainType() != null)
                    {
                        surroundingTiles[i].SetTerrain(GameTerrainFactory.GetIceCrackedTerrainClone(surroundingTiles[i].GetTerrain()));
                    }
                }
                numQuakes--;
            }

            tiles.RemoveAt(index);
        }
    }
}
