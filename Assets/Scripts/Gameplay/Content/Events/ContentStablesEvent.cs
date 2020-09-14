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
        m_rarity = GameRarity.Rare;

        m_optionOne = new GameEventTakeHorsesOption(m_tile);
        m_optionTwo = new GameEventTakeGoldOption(75);
        m_optionThree = new GameEventLeaveOption();

        LateInit();
    }
}

public class GameEventTakeHorsesOption : GameEventOption
{
    private GameTile m_tile;
    private int m_apRegen;
    private int m_maxAP;

    public GameEventTakeHorsesOption(GameTile tile)
    {
        m_apRegen = 1;
        m_maxAP = 1;

        m_tile = tile;

        m_hasTooltip = true;
    }

    public override string GetMessage()
    {
        m_message = m_tile.m_occupyingEntity.m_name + " gains " + m_maxAP + " max AP and " + m_apRegen + " AP regen per turn.";

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        m_tile.m_occupyingEntity.AddAPRegen(m_apRegen);
        m_tile.m_occupyingEntity.AddMaxAP(m_maxAP);

        EndEvent();
    }

    public override void BuildTooltip()
    {
         UIHelper.CreateEntityTooltip(m_tile.m_occupyingEntity);
    }
}
