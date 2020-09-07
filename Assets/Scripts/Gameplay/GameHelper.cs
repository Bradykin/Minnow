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
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        targetTile.PlaceEntity(entity);

        player.AddControlledEntity(entity);
    }

    public static void MakePlayerBuilding(GameTile targetTile, GameBuildingBase building)
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        GameBuildingBase toPlace = GameBuildingFactory.GetBuildingClone(building);

        targetTile.PlaceBuilding(toPlace);
        player.AddControlledBuilding(toPlace);
    }

    public static GamePlayer GetPlayer()
    {
        if (WorldController.Instance == null)
        {
            return null;
        }

        if (WorldController.Instance.m_gameController == null)
        {
            return null;
        }

        return WorldController.Instance.m_gameController.m_player;
    }

    public static GameOpponent GetOpponent()
    {
        if (WorldController.Instance == null)
        {
            return null;
        }

        if (WorldController.Instance.m_gameController == null)
        {
            return null;
        }

        return WorldController.Instance.m_gameController.m_gameOpponent;
    }

    public static int RelicCount<T>()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return 0;
        }

        return player.GetRelics().GetNumRelics<T>();
    }
}
