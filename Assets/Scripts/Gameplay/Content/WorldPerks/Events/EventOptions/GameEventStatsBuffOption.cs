using System.Collections.Generic;
using UnityEngine;

public class GameEventStatsBuffOption : GameEventOption
{
    private GameTile m_tile;
    private int m_attackIncrease;
    private int m_maxHealthIncrease;

    public GameEventStatsBuffOption(GameTile tile, int attackIncrease, int healthIncrease)
    {
        m_attackIncrease = attackIncrease;
        m_maxHealthIncrease = healthIncrease;

        m_tile = tile;
    }

    public override string GetMessage()
    {
       m_message = "<b>Permanently</b> gain +" + m_attackIncrease + "/+" + m_maxHealthIncrease + ".";

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        if (!m_tile.IsOccupied())
        {
            return;
        }

        m_tile.GetOccupyingUnit().AddStats(m_attackIncrease, m_maxHealthIncrease, true, true);

        EndEvent();
    }

    //Intentionally left blank
    public override void DeclineOption()
    {

    }
}