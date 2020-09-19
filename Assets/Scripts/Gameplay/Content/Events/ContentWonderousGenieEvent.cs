using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWonderousGenieEvent : GameEvent
{
    public ContentWonderousGenieEvent(GameTile tile)
    {
        m_name = "Wonderous Genie";
        m_eventDesc = "A strange genie offers you a choice of two ancient relics.  Choose carefully; you may come to regret not picking the other...";
        m_tile = tile;
        m_rarity = GameRarity.Common;

        m_optionOne = new GameEventTakeRelicOption();
        m_optionTwo = new GameEventTakeRelicOption(((GameEventTakeRelicOption)m_optionOne).m_relic);
        m_optionThree = new GameEventLeaveOption();

        LateInit();
    }
}

public class GameEventTakeRelicOption : GameEventOption
{
    public GameRelic m_relic;

    private GameRelic m_excludeRelic;

    public GameEventTakeRelicOption(GameRelic excludeRelic = null)
    {
        m_hasTooltip = true;
        m_excludeRelic = excludeRelic;
    }

    public override void Init()
    {
        m_relic = GameRelicFactory.GetRandomRelic(m_excludeRelic);

        m_message = "Take " + m_relic.m_name;
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        player.AddRelic(m_relic);

        EndEvent();
    }

    public override void BuildTooltip()
    {
        UIHelper.CreateRelicTooltip(m_relic);
    }
}