using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContentGemsOfProphecyEvent : GameEvent
{
    public ContentGemsOfProphecyEvent(GameTile tile)
    {
        m_name = "Gems of Prophecy";
        m_eventDesc = "A fortune teller wanders the lands offering to tell fate with gems.";
        m_tile = tile;

        Init();
    }

    public override void LateInit()
    {
        m_optionOne = new GameEventProphecyOfAdventureOption(m_tile);
        m_optionTwo = new GameEventDamageReductionOption(m_tile);

        base.LateInit();
    }

    public override string GetOptionOneTooltip()
    {
        return "Give the unit that goes here '<b>Victorious</b>: +5/+0' <b>permanently</b> .";
    }

    public override string GetOptionTwoTooltip()
    {
        return "Give the unit that goes here <b>Damage Reduction</b> 3 <b>permanently</b> .";
    }
}

public class GameEventProphecyOfAdventureOption : GameEventOption
{
    private GameTile m_tile;
    private int m_attackGain = 5;

    public GameEventProphecyOfAdventureOption(GameTile tile)
    {
        m_tile = tile;
    }

    public override void Init()
    {
        m_message = $"Receive the prophecy of adventure: {m_tile.GetOccupyingUnit().GetName()} <b>permanently</b> gains '<b>Victorious</b>: gain +{m_attackGain}/+0.'";
    }

    public override void AcceptOption()
    {
        m_tile.GetOccupyingUnit().AddKeyword(new GameVictoriousKeyword(new GameGainStatsAction(m_tile.GetOccupyingUnit(), m_attackGain, 0)), true, true);

        EndEvent();
    }

    //Intentionally left blank
    public override void DeclineOption()
    {

    }
}

public class GameEventDamageReductionOption : GameEventOption
{
    private GameTile m_tile;
    private int m_damageReduction = 3;

    public GameEventDamageReductionOption(GameTile tile)
    {
        m_tile = tile;
    }

    public override void Init()
    {
        m_message = $"Receive the prophecy of dangers: {m_tile.GetOccupyingUnit().GetName()} gains <b>Damage Reduction</b> {m_damageReduction} <b>permanently</b>";
    }

    public override void AcceptOption()
    {
        m_tile.GetOccupyingUnit().AddKeyword(new GameDamageReductionKeyword(m_damageReduction), true, true);

        EndEvent();
    }

    //Intentionally left blank
    public override void DeclineOption()
    {

    }
}

public class GameEventProphecyTakeGoldOption : GameEventOption
{
    private int m_value;

    public GameEventProphecyTakeGoldOption(int value)
    {
        m_value = value;

        m_message = $"Receive the prophecy of wealth: Take {m_value} gold.";
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        player.GainGold(m_value);
        EndEvent();
    }

    //Intentionally left blank
    public override void DeclineOption()
    {

    }
}