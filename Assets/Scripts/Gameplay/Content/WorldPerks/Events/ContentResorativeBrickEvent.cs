using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRestorationBrickEvent : GameEvent
{
    public ContentRestorationBrickEvent(GameTile tile)
    {
        m_name = "Restoration Brick";
        m_eventDesc = "You find a bizzare brick on the side of the road.  It seems to be magical!  You can use it to restore your castle, or take it with you to improve building capabilities!";
        m_tile = tile;

        m_optionOne = new GameEventTakeSpecificRelicOption(new ContentRestorationBrickRelic());
        m_optionTwo = new GameEventHealCastle(35);

        Init();
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
        m_message = "Restore " + m_toHeal + " health to your castle.";

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
}