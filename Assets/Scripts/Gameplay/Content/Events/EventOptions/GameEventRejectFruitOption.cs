using System.Collections.Generic;

public class GameEventRejectFruitOption : GameEventOption
{
    private GameTile m_tile;
    private int m_healAmount = 5;

    private int m_powerIncrease = 3;
    private int m_healthIncrease = 10;
    private int m_staminaIncrease = 2;
    private int m_knowledgeableDecrease = 2;
    private int m_tileRange = 2;

    public GameEventRejectFruitOption(GameTile tile)
    {
        m_tile = tile;
    }

    public override void Init()
    {
        m_message = "Heal all nearby ally units for " + m_healAmount +" health. Gain +" + m_powerIncrease + "/+" + m_healthIncrease + " and " + m_staminaIncrease + " current Stamina, but lose -" + m_knowledgeableDecrease + "/-" + m_knowledgeableDecrease + " on Knowledgeable.";
    }

    public override void AcceptOption()
    {
        GameUnit unit = m_tile.m_occupyingUnit;

        unit.AddPower(m_powerIncrease);
        unit.AddMaxHealth(m_healthIncrease);
        unit.GainStamina(m_staminaIncrease);

        unit.AddKeyword(new GameKnowledgeableKeyword(new GameGainPowerAction(unit, -m_knowledgeableDecrease)));
        unit.AddKeyword(new GameKnowledgeableKeyword(new GameGainMaxHealthAction(unit, -m_knowledgeableDecrease)));

        List<GameTile> nearbyTiles = WorldGridManager.Instance.GetSurroundingTiles(m_tile, m_tileRange, 0);

        for (int i = 0; i < nearbyTiles.Count; i++)
        {
            if (nearbyTiles[i].IsOccupied() && nearbyTiles[i].m_occupyingUnit.GetTeam() == Team.Player)
            {
                nearbyTiles[i].m_occupyingUnit.Heal(m_healAmount);
            }

            if (nearbyTiles[i].GetTerrain().CanUnburn())
            {
                nearbyTiles[i].SetTerrain(GameTerrainFactory.GetUnburnedTerrainClone(nearbyTiles[i].GetTerrain()));
                continue;
            }
        }

        EndEvent();
    }
}