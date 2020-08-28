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
        m_optionTwo = new GameEventTakeRelicOption();
        m_optionThree = new GameEventLeaveOption();

        LateInit();
    }
}

public class GameEventTakeRelicOption : GameEventOption
{
    private GameRelic m_relic;

    public GameEventTakeRelicOption()
    {
        m_relic = GameRelicFactory.GetRandomRelic(); //nmartino - Allow exclude of the other option

        m_message = "Take " + m_relic.m_name;

        m_hasTooltip = true;
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        player.m_relics.AddRelic(m_relic);

        EndEvent();
    }

    public override void BuildTooltip()
    {
        UIHelper.CreateRelicTooltip(m_relic);
    }
}