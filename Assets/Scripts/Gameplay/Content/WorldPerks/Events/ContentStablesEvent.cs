using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentStablesEvent : GameEvent
{
    public ContentStablesEvent(GameTile tile)
    {
        m_name = "Stables";
        m_eventDesc = "You come across some abandoned stables in this wartorn land.  The troops could take the horses left behind, or you could sell them.";
        m_tile = tile;

        m_optionOne = new GameEventTakeHorsesOption(m_tile);
        m_optionTwo = new GameEventTakeGoldOption(75);
        m_optionThree = new GameEventLeaveOption();

        Init();
    }
}

public class GameEventTakeHorsesOption : GameEventOption
{
    private GameTile m_tile;
    private int m_staminaRegen;
    private int m_maxStamina;

    public GameEventTakeHorsesOption(GameTile tile)
    {
        m_staminaRegen = 1;
        m_maxStamina = 1;

        m_tile = tile;

        m_hasTooltip = true;
    }

    public override string GetMessage()
    {
        m_message = m_tile.m_occupyingUnit.GetName() + " gains " + m_maxStamina + " max Stamina and " + m_staminaRegen + " Stamina regen per turn.";

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        m_tile.m_occupyingUnit.AddStaminaRegen(m_staminaRegen);
        m_tile.m_occupyingUnit.AddMaxStamina(m_maxStamina);

        EndEvent();
    }

    public override void BuildTooltip()
    {
         UIHelper.CreateUnitTooltip(m_tile.m_occupyingUnit);
    }
}
