using System.Collections.Generic;
using UnityEngine;

public class GameEventChemistSmallStatsOption : GameEventOption
{
    GameTile m_tile;
    int m_powerIncrease = 2;
    int m_maxHealthIncrease = 5;

    public GameEventChemistSmallStatsOption(GameTile tile)
    {
        m_tile = tile;
    }

    public override string GetMessage()
    {
        m_message = "Free samples: Gain +" + m_powerIncrease + "/+" + m_maxHealthIncrease + ".";

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        if (!m_tile.IsOccupied())
        {
            return;
        }

        m_tile.m_occupyingUnit.AddPower(m_powerIncrease);
        m_tile.m_occupyingUnit.AddMaxHealth(m_maxHealthIncrease);

        EndEvent();
    }
}