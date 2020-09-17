using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContentTransmuteBeamsEvent : GameEvent
{
    public ContentTransmuteBeamsEvent(GameTile tile)
    {
        m_name = "Transmute Beams";
        m_eventDesc = "An expert on purple beams has offered you a chance to make some cash. Or lose some cash, they weren't very clear. What do you do?";
        m_tile = tile;
        m_rarity = GameRarity.Uncommon;

        m_optionOne = new GameEventSellGoldForPurpleBeamsOption();
        m_optionTwo = new GameEventSellPurpleBeamsForGoldOption();
        m_optionThree = new GameEventLeaveOption();

        LateInit();
    }
}

public class GameEventSellGoldForPurpleBeamsOption : GameEventOption
{
    public override void Init()
    {
        int goldAmount = GameHelper.GetPlayer().m_wallet.m_gold;
        int beamAmount = goldAmount / 10;

        if (goldAmount % 10 != 0)
        {
            beamAmount++;
        }

        m_message = "Sacrifice " + goldAmount + " gold to gain " + beamAmount + " to the purple beam count.";
    }

    public override void AcceptOption()
    {
        int goldAmount = GameHelper.GetPlayer().m_wallet.m_gold;
        int beamAmount = goldAmount / 10;

        if (goldAmount % 10 != 0)
        {
            beamAmount++;
        }

        GameHelper.GetPlayer().m_wallet.SubtractResources(new GameWallet(goldAmount));
        Globals.m_purpleBeamCount += beamAmount;

        EndEvent();
    }
}

public class GameEventSellPurpleBeamsForGoldOption : GameEventOption
{
    public override void Init()
    {
        int beamAmount = Globals.m_purpleBeamCount;
        int goldAmount = beamAmount * 10;

        m_message = "Sacrifice " + beamAmount + " from the purple beam count to gain " + goldAmount + " gold.";
    }

    public override void AcceptOption()
    {
        int beamAmount = Globals.m_purpleBeamCount;
        int goldAmount = beamAmount * 10;

        Globals.m_purpleBeamCount = 0;
        GameHelper.GetPlayer().m_wallet.AddResources(new GameWallet(goldAmount));

        EndEvent();
    }
}