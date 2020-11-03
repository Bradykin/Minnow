using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGoblinBarricadeEvent : GameEvent
{
    public ContentGoblinBarricadeEvent(GameTile tile)
    {
        m_name = "Goblin Barricade";
        m_eventDesc = "A goblin barricade stands before you!  Breaking it may cost some troops, but it'd help the trade in the region.";
        m_tile = tile;

        m_optionOne = new GameEventBreakBarricadeOption(m_tile);
        m_optionTwo = new GameEventLeaveOption();

        LateInit();
    }
}

public class GameEventBreakBarricadeOption : GameEventOption
{
    private GameTile m_tile;
    private GameWallet m_wallet;

    public GameEventBreakBarricadeOption(GameTile tile)
    {
        m_tile = tile;
        m_wallet = new GameWallet(150);
    }

    public override string GetMessage()
    {
        m_message = "Sacrifice " + m_tile.m_occupyingUnit.GetName() + ", but gain " + m_wallet.m_gold + " gold.";

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        m_tile.m_occupyingUnit.Die();

        player.m_wallet.AddResources(m_wallet);

        EndEvent();
    }
}
