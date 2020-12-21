using System.Collections.Generic;
using UnityEngine;

public class GameEventOrcRelicOption : GameEventOption
{
    private GameRelic m_relic;
    private GameTile m_tile;
    private int m_orcsToSpawn = 4;
    private ContentOrcEnemy m_orcCheckerUnit = new ContentOrcEnemy(null);

    public GameEventOrcRelicOption(GameTile tile)
    {
        m_tile = tile;
        m_hasTooltip = true;
    }

    public override void Init()
    {
        m_relic = GameRelicFactory.GetRandomRelicAtRarity(GameElementBase.GameRarity.Rare);

        m_message = "Go for the large treasure, taking " + m_relic.GetName() + " and waking " + m_orcsToSpawn + " orcs from slumber.";
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        GameNotificationManager.RecordRelicSingleChoice(m_relic, true);

        player.AddRelic(m_relic);

        List<GameTile> nearbyTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_tile, 2);

        List<GameTile> shuffledTiles = new List<GameTile>();
        while (nearbyTiles.Count > 0)
        {
            int index = Random.Range(0, nearbyTiles.Count);
            shuffledTiles.Add(nearbyTiles[index]);
            nearbyTiles.RemoveAt(index);
        }
        int n = shuffledTiles.Count;
        int orcsSpawned = 0;

        for (int i = 0; i < n; i++)
        {
            if (shuffledTiles[i].IsOccupied())
            {
                continue;
            }

            if (!shuffledTiles[i].IsPassable(m_orcCheckerUnit, false))
            {
                continue;
            }

            GameEnemyUnit newOrc = GameUnitFactory.GetEnemyUnitClone(m_orcCheckerUnit, GameHelper.GetOpponent());
            shuffledTiles[i].PlaceUnit(newOrc);
            GameHelper.GetOpponent().m_controlledUnits.Add(newOrc);
            orcsSpawned++;
            if (orcsSpawned >= m_orcsToSpawn)
            {
                break;
            }
        }

        EndEvent();
    }

    public override void DeclineOption()
    {
        GameNotificationManager.RecordRelicSingleChoice(m_relic, false);
    }

    public override void BuildTooltip()
    {
        UIHelper.CreateRelicTooltip(m_relic);
    }
}
