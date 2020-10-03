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

        m_optionOne = new GameEventProphecyOfAdventureOption(m_tile);
        m_optionTwo = new GameEventDamageReductionOption(m_tile);
        m_optionThree = new GameEventProphecyTakeGoldOption(100);

        LateInit();

        m_minWaveToSpawn = 3;
        m_maxWaveToSpawn = 5;
    }
}

public class GameEventProphecyOfAdventureOption : GameEventOption
{
    private GameTile m_tile;
    private int m_powerGain = 5;

    public GameEventProphecyOfAdventureOption(GameTile tile)
    {
        m_tile = tile;
    }

    public override void Init()
    {
        m_message = "Receive the prophecy of adventure: " + m_tile.m_occupyingUnit.m_name + " gains 'Victorious: gain +" + m_powerGain + "/+0.'";
    }

    public override void AcceptOption()
    {
        m_tile.m_occupyingUnit.AddKeyword(new GameVictoriousKeyword(new GameGainPowerAction(m_tile.m_occupyingUnit, m_powerGain)));

        EndEvent();
    }
}

public class GameEventDamageReductionOption : GameEventOption
{
    private GameTile m_tile;
    private int m_heal = 5;

    public GameEventDamageReductionOption(GameTile tile)
    {
        m_tile = tile;
    }

    public override void Init()
    {
        m_message = "Receive the prophecy of dangers: " + m_tile.m_occupyingUnit.m_name + " gains 'Enrage: heal for " + m_heal + " health.'";
    }

    public override void AcceptOption()
    {
        m_tile.m_occupyingUnit.AddKeyword(new GameEnrageKeyword(new GameHealAction(m_tile.m_occupyingUnit, m_heal)));

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