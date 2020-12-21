using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAngelicGiftEvent : GameEvent
{
    public ContentAngelicGiftEvent(GameTile tile)
    {
        m_name = "Angelic Gift";
        m_eventDesc = "An angel from the Caverns has taken up residence here.";
        m_tile = tile;

        Init();
    }

    public override void LateInit()
    {
        m_optionOne = new GameEventTakeRandomRelicOption(GameElementBase.GameRarity.Rare);
        m_optionTwo = new GameEventAngelicWings(m_tile);

        base.LateInit();
    }

    public override string GetOptionOneTooltip()
    {
        return "Gain a random rare relic.";
    }

    public override string GetOptionTwoTooltip()
    {
        return "Give the unit that goes here <b>Flying</b>.";
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
        m_message = m_tile.GetOccupyingUnit().GetName() + " <b>permanently</b> gains Flying!.";

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        m_tile.GetOccupyingUnit().AddKeyword(new GameFlyingKeyword(), true, true);

        EndEvent();
    }

    //Intentionally left blank
    public override void DeclineOption()
    {

    }

    public override void BuildTooltip()
    {
        GameFlyingKeyword keyword = new GameFlyingKeyword();
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip(keyword.GetName(), keyword.m_focusInfoText, m_tile.GetOccupyingUnit().GetTeam()));
    }
}