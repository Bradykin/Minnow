﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRestorationBrickEvent : GameEvent
{
    public ContentRestorationBrickEvent(GameTile tile)
    {
        m_name = "Restoration Brick";
        m_eventDesc = "Traders report a bizzare magical brick on the side of the road that is much heavier than expected can lift.";
        m_tile = tile;

        Init();
    }

    public override void LateInit()
    {
        m_optionOne = new GameEventTakeSpecificRelicOption(new ContentRestorationBrickRelic());
        m_optionTwo = new GameEventHealCastle(35);

        base.LateInit();
    }

    public override string GetOptionOneTooltip()
    {
        return "Gain the relic Restoration Brick.\n" + new ContentRestorationBrickRelic().GetDesc();
    }

    public override string GetOptionTwoTooltip()
    {
        return "Heal your castle for 35";
    }
}

public class GameEventHealCastle : GameEventOption
{
    private int m_toHeal = 35;

    public GameEventHealCastle(int toHeal)
    {
        m_toHeal = toHeal;
    }

    public override string GetMessage()
    {
        m_message = $"Restore {m_toHeal} health to your castle.";

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        if (GameHelper.GetPlayer().GetCastleGameElement() == null)
        {
            return;
        }

        if (GameHelper.GetPlayer().GetCastleGameElement() is ContentCastleBuilding castleBuilding)
        {
            castleBuilding.GetHealed(m_toHeal);
        }
        else if (GameHelper.GetPlayer().GetCastleGameElement() is ContentRoyalCaravan castleUnit)
        {
            castleUnit.Heal(m_toHeal);
        }

        EndEvent();
    }

    //Intentionally left blank
    public override void DeclineOption()
    {

    }
}