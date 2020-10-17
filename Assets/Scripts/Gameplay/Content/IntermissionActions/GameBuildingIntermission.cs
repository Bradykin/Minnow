using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBuildingIntermission
{
    public int m_actionCost = 0;
    public GameWallet m_cost { get; protected set; }

    public GameBuildingBase m_building;

    public GameBuildingIntermission(GameBuildingBase building, GameWallet cost)
    {
        m_building = building;

        m_cost = cost;
    }

    public void Place()
    {
        Globals.m_selectedIntermissionBuilding = null;

        GamePlayer player = GameHelper.GetPlayer();
        if (player == null)
        {
            return;
        }

        player.m_wallet.SubtractResources(m_cost);
        player.SpendActions(m_actionCost);
    }

    public bool IsValidToPlay(GameTile gameTile)
    {
        if (gameTile.HasBuilding())
        {
            return false;
        }

        if (gameTile.GetTerrain().IsEventTerrain())
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

        if (!player.m_wallet.CanAfford(m_cost))
        {
            return false;
        }

        return true;
    }
}