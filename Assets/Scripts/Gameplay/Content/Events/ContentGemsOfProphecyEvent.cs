using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContentGemsOfProphecyEvent : GameEvent
{
    public ContentGemsOfProphecyEvent(GameTile tile)
    {
        m_name = "Gems of Prophecy";
        m_eventDesc = "You find a set of prophetic gems. Which one shall tell your fate?";
        m_tile = tile;
        m_rarity = GameRarity.Rare;

        m_optionOne = new GameEventRevealRuinsOption();
        m_optionTwo = new GameEventDamageReductionOption(m_tile);
        m_optionThree = new GameEventProphecyTakeGoldOption(100);

        LateInit();
    }
}

public class GameEventRevealRuinsOption : GameEventOption
{
    public override void Init()
    {
        m_message = "Receive the prophecy of adventure: Reveal the locations of all ruins on the map.";
    }

    public override void AcceptOption()
    {
        WorldTile[] worldTiles = WorldGridManager.Instance.m_gridArray;
        for (int i = 0; i < worldTiles.Length; i++)
        {
            if (worldTiles[i].GetGameTile().m_event)
            {
                worldTiles[i].ClearFog();
            }
        }

        EndEvent();
    }
}

public class GameEventDamageReductionOption : GameEventOption
{
    private GameTile m_tile;

    public GameEventDamageReductionOption(GameTile tile)
    {
        m_tile = tile;
    }

    public override void Init()
    {
        m_message = "Receive the prophecy of dangers: " + m_tile.m_occupyingEntity.m_name + " gains Enrage: heal for 2 health.";
    }

    public override void AcceptOption()
    {
        m_tile.m_occupyingEntity.AddKeyword(new GameEnrageKeyword(new GameHealAction(m_tile.m_occupyingEntity, 2)));

        EndEvent();
    }
}

public class GameEventProphecyTakeGoldOption : GameEventOption
{
    private int m_value;

    public GameEventProphecyTakeGoldOption(int value)
    {
        m_value = value;

        m_message = "Receive the prophecy of wealth: Take " + m_value + " gold.";
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        player.m_wallet.m_gold += m_value;
        EndEvent();
    }
}