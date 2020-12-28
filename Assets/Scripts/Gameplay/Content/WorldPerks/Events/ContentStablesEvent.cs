using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentStablesEvent : GameEvent
{
    public ContentStablesEvent(GameTile tile)
    {
        m_name = "Stables";
        m_eventDesc = "Intelligence reports that stables in this area have been left empty. Either using or selling the abandoned horses would be a boon.";
        m_tile = tile;

        Init();
    }

    public override void LateInit()
    {
        m_optionOne = new GameEventTakeHorsesOption(m_tile);
        m_optionTwo = new GameEventTakeGoldOption(75);

        base.LateInit();
    }

    public override string GetOptionOneTooltip()
    {
        return "Give the unit that goes here +2 maximum stamina and +2 stamina regen.";
    }

    public override string GetOptionTwoTooltip()
    {
        return "Gain 75 gold.";
    }
}

public class GameEventTakeHorsesOption : GameEventOption
{
    private GameTile m_tile;
    private int m_staminaRegen = 2;
    private int m_maxStamina = 2;

    public GameEventTakeHorsesOption(GameTile tile)
    {
        m_tile = tile;

        m_hasTooltip = true;
    }

    public override string GetMessage()
    {
        m_message = m_tile.GetOccupyingUnit().GetName() + " gains " + m_maxStamina + " max Stamina and " + m_staminaRegen + " Stamina regen per turn.";

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        m_tile.GetOccupyingUnit().AddStaminaRegen(m_staminaRegen, true);
        m_tile.GetOccupyingUnit().AddMaxStamina(m_maxStamina, true);

        EndEvent();
    }

    //Intentionally left blank
    public override void DeclineOption()
    {

    }

    public override void BuildTooltip()
    {
         UIHelper.CreateUnitTooltip(m_tile.GetOccupyingUnit());
    }
}
