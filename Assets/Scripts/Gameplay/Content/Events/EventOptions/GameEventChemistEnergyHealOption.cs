using System.Collections.Generic;
using UnityEngine;

public class GameEventChemistEnergyHealOption : GameEventOption
{
    private GameTile m_tile;
    private int m_goldCost;

    public GameEventChemistEnergyHealOption(GameTile tile, int goldCost)
    {
        m_tile = tile;
        m_goldCost = goldCost;
    }

    public override string GetMessage()
    {
        m_message = "Spend " + m_goldCost + " gold: Heal to full health and gain max Stamina.";

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        if (!m_tile.IsOccupied())
        {
            return;
        }

        if (GameHelper.GetPlayer().m_wallet.m_gold < m_goldCost)
        {
            return;
        }

        GameHelper.GetPlayer().m_wallet.SubtractResources(new GameWallet(m_goldCost));

        m_tile.m_occupyingUnit.Heal(m_tile.m_occupyingUnit.GetMaxHealth());
        m_tile.m_occupyingUnit.GainStamina(m_tile.m_occupyingUnit.GetMaxStamina());

        EndEvent();
    }
}