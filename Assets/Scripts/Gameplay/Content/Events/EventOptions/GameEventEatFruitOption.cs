using System.Collections.Generic;

public class GameEventEatFruitOption : GameEventOption
{
    private GameTile m_tile;
    private int m_blastDamage = 5;
    private int m_powerIncrease = 1;
    private int m_healthIncrease = 1;
    private int m_tileRange = 2;

    public GameEventEatFruitOption(GameTile tile)
    {
        m_tile = tile;
    }

    public override void Init()
    {
        m_message = "Blast all tiles within + " + m_tileRange + " range, damaging nearby units for " + m_blastDamage + " damage and burning the tiles. Gain +" + m_powerIncrease + "/+" + m_healthIncrease + " on Knowledgeable.";
    }

    public override void AcceptOption()
    {
        m_tile.m_occupyingUnit.AddKeyword(new GameKnowledgeableKeyword(new GameGainStatsAction(m_tile.m_occupyingUnit, m_powerIncrease, m_healthIncrease)));

        List<GameTile> nearbyTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_tile, m_tileRange, 0);

        for (int i = 0; i < nearbyTiles.Count; i++)
        {
            if (nearbyTiles[i].IsOccupied())
            {
                nearbyTiles[i].m_occupyingUnit.GetHit(m_blastDamage);
            }
            
            if (nearbyTiles[i].GetTerrain().CanBurn())
            {
                nearbyTiles[i].SetTerrain(GameTerrainFactory.GetBurnedTerrainClone(nearbyTiles[i].GetTerrain()));
                continue;
            }
        }

        EndEvent();
    }
}