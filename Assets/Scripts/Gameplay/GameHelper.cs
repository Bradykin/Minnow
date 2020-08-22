using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameHelper
{
    //Returns true if it hits the chance, false if it does not
    public static bool PercentChanceRoll(int percent)
    {
        return (Random.Range(1, 101) <= percent);
    }

    public static void MakePlayerEntity(GameTile targetTile, GameEntity entity)
    {
        targetTile.PlaceEntity(entity);
        WorldController.Instance.m_gameController.m_player.AddControlledEntity(entity);
    }

    public static void MakePlayerBuilding(GameTile targetTile, GameBuildingBase building)
    {
        targetTile.PlaceBuilding(building);
        WorldController.Instance.m_gameController.m_player.AddControlledBuilding(building);
    }
}
