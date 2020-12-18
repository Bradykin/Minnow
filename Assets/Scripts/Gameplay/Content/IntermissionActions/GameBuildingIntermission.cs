using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBuildingIntermission
{
    public int m_actionCost = 0;
    public GameWallet m_cost { get; protected set; }

    public GameBuildingBase m_building;

    public GameBuildingIntermission(GameBuildingBase building)
    {
        m_building = building;

        m_cost = building.GetCost();
    }

    public void Place()
    {
        Globals.m_selectedIntermissionBuilding = null;

        GamePlayer player = GameHelper.GetPlayer();
        if (player == null)
        {
            return;
        }

        player.SpendGold(m_cost.m_gold);
        player.SpendActions(m_actionCost);
    }

    public bool IsValidToPlay(GameTile gameTile)
    {
        if (gameTile.HasBuilding())
        {
            return false;
        }

        if (!m_building.IsValidTerrainToPlace(gameTile.GetTerrain(), gameTile))
        {
            return false;
        }

        return true;
    }

    public virtual bool CanAfford()
    {
        GamePlayer player = GameHelper.GetPlayer();
        if (player == null)
        {
            return false;
        }

        if (player.GetCurActions() < m_actionCost)
        {
            return false;
        }

        if (player.GetGold() < m_cost.m_gold)
        {
            return false;
        }

        return true;
    }
}