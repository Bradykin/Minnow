using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAngelicGiftEvent : GameEvent
{
    public ContentAngelicGiftEvent(GameTile tile)
    {
        m_name = "Angelic Gift";
        m_eventDesc = "An angel comes down from the sky and offers you an angelic choice:";
        m_tile = tile;
        m_rarity = GameRarity.Rare;

        m_optionOne = new GameEventTakeRelicOption();
        m_optionTwo = new GameEventAngelicWings(tile);
        m_optionThree = new GameEventLeaveOption();

        LateInit();
    }
}

public class GameEventAngelicWings : GameEventOption
{
    private GameTile m_tile;

    public GameEventAngelicWings(GameTile tile)
    {
        m_tile = tile;

        m_hasTooltip = true;
    }

    public override string GetMessage()
    {
        m_message = "Grant " + m_tile.m_occupyingEntity.m_name + " flying!.";

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        m_tile.m_occupyingEntity.GetKeywordHolder().m_keywords.Add(new GameFlyingKeyword());

        EndEvent();
    }

    public override void BuildTooltip()
    {
        GameFlyingKeyword keyword = new GameFlyingKeyword();
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip(keyword.m_name, keyword.m_desc, m_tile.m_occupyingEntity.GetTeam()));
    }
}