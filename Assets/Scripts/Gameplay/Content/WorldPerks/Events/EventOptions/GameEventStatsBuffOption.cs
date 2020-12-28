using System.Collections.Generic;
using UnityEngine;

public class GameEventStatsBuffOption : GameEventOption
{
    private GameTile m_tile;
    private int m_powerIncrease;
    private int m_maxHealthIncrease;

    public GameEventStatsBuffOption(GameTile tile, int powerIncrease, int healthIncrease)
    {
        m_powerIncrease = powerIncrease;
        m_maxHealthIncrease = healthIncrease;

        m_tile = tile;
    }

    public override string GetMessage()
    {
       m_message = "<b>Permanently</b> gain +" + m_powerIncrease + "/+" + m_maxHealthIncrease + ".";

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        if (!m_tile.IsOccupied())
        {
            return;
        }

        m_tile.GetOccupyingUnit().AddStats(m_powerIncrease, m_maxHealthIncrease, true, true);

        EndEvent();
    }

    //Intentionally left blank
    public override void DeclineOption()
    {

    }
}