using System.Collections.Generic;
using UnityEngine;

public class GameEventStatsBuffOption : GameEventOption
{
    private enum StatsBuffEventType
    {
        Standard,
        Blast
    }

    private StatsBuffEventType m_eventType;
    private GameTile m_tile;
    private int m_powerIncrease;
    private int m_maxHealthIncrease;

    private int m_blastDamage = 10;
    private int m_blastRange = 2;

    public GameEventStatsBuffOption(GameTile tile, int powerIncrease, int healthIncrease)
    {
        m_powerIncrease = powerIncrease;
        m_maxHealthIncrease = healthIncrease;

        m_eventType = StatsBuffEventType.Standard;

        m_tile = tile;
    }

    public GameEventStatsBuffOption(GameTile tile, int powerIncrease, int healthIncrease, int blastDamage, int blastRange)
    {
        m_powerIncrease = powerIncrease;
        m_maxHealthIncrease = healthIncrease;

        m_blastDamage = blastDamage;
        m_blastRange = blastRange;

        m_eventType = StatsBuffEventType.Blast;

        m_tile = tile;
    }

    public override string GetMessage()
    {
        if (m_eventType == StatsBuffEventType.Standard)
        {
            m_message = "Gain +" + m_powerIncrease + "/+" + m_maxHealthIncrease + ".";
        }
        else if (m_eventType == StatsBuffEventType.Blast)
        {
            m_message = "Blast all tiles within + " + m_blastRange + " range, damaging other nearby units for " + m_blastDamage + " damage and burning the tiles. Gain +" + m_powerIncrease + "/+" + m_maxHealthIncrease + ".";
        }

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        if (!m_tile.IsOccupied())
        {
            return;
        }

        m_tile.GetOccupyingUnit().AddStats(m_powerIncrease, m_maxHealthIncrease);

        if (m_eventType == StatsBuffEventType.Blast)
        {
            List<GameTile> nearbyTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_tile, m_blastRange, 1);

            for (int i = 0; i < nearbyTiles.Count; i++)
            {
                if (nearbyTiles[i].IsOccupied())
                {
                    nearbyTiles[i].GetOccupyingUnit().GetHitByAbility(m_blastDamage);
                }

                if (nearbyTiles[i].GetTerrain().CanBurn())
                {
                    nearbyTiles[i].SetTerrain(GameTerrainFactory.GetBurnedTerrainClone(nearbyTiles[i].GetTerrain()));
                    continue;
                }
            }
        }

        EndEvent();
    }
}