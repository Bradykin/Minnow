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

        m_minWaveToSpawn = 1;
        m_maxWaveToSpawn = Constants.FinalWaveNum;
    }

    public override bool IsValidToSpawn()
    {
        bool baseValid = base.IsValidToSpawn();

        if (!baseValid)
        {
            return false;
        }

        int playerPurpleBeams = Globals.m_purpleBeamCount;

        return playerPurpleBeams >= 5;
    }
}