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
        for (int i = 0; i < WorldGridManager.Instance.m_gridArray.Length; i++)
        {
            GameTile gameTile = WorldGridManager.Instance.m_gridArray[i].GetGameTile();
            if (gameTile.m_gameEventMarkers.Contains(m_markerToCheck))
            {
                tiles.Add(gameTile);
            }
        }

        int numQuakes = tiles.Count / 6;

        while (numQuakes > 0 && tiles.Count > 0)
        {
            int index = Random.Range(0, tiles.Count);
            GameTile gameTile = tiles[index];

            if (gameTile.GetTerrain().IsIce() && gameTile.GetTerrain().GetIceCrackedTerrainType() != null)
            {
                gameTile.SetTerrain(GameTerrainFactory.GetIceCrackedTerrainClone(gameTile.GetTerrain()));
                numQuakes--;
            }
            else
            {
                tiles.RemoveAt(index);
            }
        }
    }
}
