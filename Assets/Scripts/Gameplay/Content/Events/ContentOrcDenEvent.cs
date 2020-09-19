using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ContentOrcDenEvent : GameEvent
{
    public ContentOrcDenEvent(GameTile tile)
    {
        m_name = "Goblin Den";
        m_eventDesc = "You see a nest of sleeping orcs. There's a couple orcs away from the group with a small treasure chest, but you can see far more lucrative treasure at the center of the group. What will you do?";
        m_tile = tile;
        m_rarity = GameRarity.Common;

        m_optionOne = new GameEventOrcGoldOption(tile);
        m_optionTwo = new GameEventOrcRelicOption(tile);
        m_optionThree = new GameEventLeaveOption();

        LateInit();
    }
}

public class GameEventOrcGoldOption : GameEventOption
{
    GameTile m_tile;
    int m_goldAmount = 100;
    int m_orcsToSpawn = 2;
    ContentOrcEnemy m_orcCheckerEntity = new ContentOrcEnemy(null);

    public GameEventOrcGoldOption(GameTile tile)
    {
        m_tile = tile;
        m_hasTooltip = true;
    }

    public override string GetMessage()
    {
        m_message = "Go for the small treasure, taking " + m_goldAmount + " gold and waking a couple orcs from slumber.";

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        player.m_wallet.AddResources(new GameWallet(m_goldAmount));

        List<GameTile> nearbyTiles = WorldGridManager.Instance.GetSurroundingTiles(m_tile, 2);

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

            if (!shuffledTiles[i].IsPassable(m_orcCheckerEntity, false))
            {
                continue;
            }

            GameEnemyEntity newOrc = GameEntityFactory.GetEnemyEntityClone(m_orcCheckerEntity, GameHelper.GetOpponent());
            shuffledTiles[i].PlaceEntity(newOrc);
            GameHelper.GetOpponent().m_controlledEntities.Add(newOrc);
            orcsSpawned++;
            if (orcsSpawned >= m_orcsToSpawn)
            {
                break;
            }
        }

        EndEvent();
    }

    public override void BuildTooltip()
    {
        UIHelper.CreateEntityTooltip(m_orcCheckerEntity);
    }
}

public class GameEventOrcRelicOption : GameEventOption
{
    GameRelic m_relic;
    GameTile m_tile;
    int m_orcsToSpawn = 4;
    ContentOrcEnemy m_orcCheckerEntity = new ContentOrcEnemy(null);

    public GameEventOrcRelicOption(GameTile tile)
    {
        m_tile = tile;
        m_hasTooltip = true;
    }

    public override void Init()
    {
        m_relic = GameRelicFactory.GetRandomRelic();

        m_message = "Go for the large treasure, taking " + m_relic.m_name + " and waking a large number of orcs.";
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        player.AddRelic(m_relic);

        List<GameTile> nearbyTiles = WorldGridManager.Instance.GetSurroundingTiles(m_tile, 2);

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

            if (!shuffledTiles[i].IsPassable(m_orcCheckerEntity, false))
            {
                continue;
            }

            GameEnemyEntity newOrc = GameEntityFactory.GetEnemyEntityClone(m_orcCheckerEntity, GameHelper.GetOpponent());
            shuffledTiles[i].PlaceEntity(newOrc);
            GameHelper.GetOpponent().m_controlledEntities.Add(newOrc);
            orcsSpawned++;
            if (orcsSpawned >= m_orcsToSpawn)
            {
                break;
            }
        }

        EndEvent();
    }

    public override void BuildTooltip()
    {
        UIHelper.CreateRelicTooltip(m_relic);
        UIHelper.CreateEntityTooltip(m_orcCheckerEntity, true);
    }
}
