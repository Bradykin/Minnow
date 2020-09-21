using System.Collections.Generic;

public class GameEventEatFruitOption : GameEventOption
{
    private GameTile m_tile;
    private int m_blastDamage = 5;
    private int m_powerIncrease = 1;
    private int m_healthIncrease = 3;
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
        m_tile.m_occupyingEntity.AddKeyword(new GameKnowledgeableKeyword(new GameGainPowerAction(m_tile.m_occupyingEntity, 1)));
        m_tile.m_occupyingEntity.AddKeyword(new GameKnowledgeableKeyword(new GameGainMaxHealthAction(m_tile.m_occupyingEntity, 3)));

        List<GameTile> nearbyTiles = WorldGridManager.Instance.GetSurroundingTiles(m_tile, m_tileRange, 0);

        for (int i = 0; i < nearbyTiles.Count; i++)
        {
            if (nearbyTiles[i].IsOccupied())
            {
                nearbyTiles[i].m_occupyingEntity.GetHit(m_blastDamage);
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