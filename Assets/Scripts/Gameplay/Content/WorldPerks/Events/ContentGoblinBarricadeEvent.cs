using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGoblinBarricadeEvent : GameEvent
{
    public ContentGoblinBarricadeEvent(GameTile tile)
    {
        m_name = "Goblin Barricade";
        m_eventDesc = "A rebellious goblin barricade has formed on these roads! Breaking it may cost some troops, but it'd help the trade in the region.";
        m_tile = tile;

        Init();
    }

    public override void LateInit()
    {
        m_optionOne = new GameEventBreakBarricadeOption(m_tile);

        base.LateInit();
    }

    public override string GetOptionOneTooltip()
    {
        return "Sacrifice the unit that goes here for the wave, but gain 100 gold.";
    }

    public override string GetOptionTwoTooltip()
    {
        return "";
    }
}

public class GameEventBreakBarricadeOption : GameEventOption
{
    private GameTile m_tile;
    private GameWallet m_wallet;

    public GameEventBreakBarricadeOption(GameTile tile)
    {
        m_tile = tile;
        m_wallet = new GameWallet(100);

        m_hasTooltip = true;
    }

    public override string GetMessage()
    {
        m_message = "Sacrifice " + m_tile.GetOccupyingUnit().GetName() + ", but gain " + m_wallet.m_gold + " gold.";

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        m_tile.GetOccupyingUnit().Die();

        player.m_wallet.AddResources(m_wallet);

        EndEvent();
    }

    public override void BuildTooltip()
    {
        if (m_tile.GetOccupyingUnit() == null)
        {
            return;
        }
        
        UIHelper.CreateUnitTooltip(m_tile.GetOccupyingUnit(), true);
    }
}
