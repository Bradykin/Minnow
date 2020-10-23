using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWonderousGenieEvent : GameEvent
{
    public ContentWonderousGenieEvent(GameTile tile)
    {
        m_name = "Wonderous Genie";
        m_eventDesc = "A strange genie offers you a choice of two ancient relics.  Choose carefully; you may come to regret not picking the other...";
        m_tile = tile;
        m_rarity = GameRarity.Uncommon;

        GameRarity rarity = GameRelicFactory.GetRandomRarity();

        m_optionOne = new GameEventTakeRandomRelicChoiceOption(rarity);

        m_minWaveToSpawn = 1;
        m_maxWaveToSpawn = 6;

        LateInit();
    }
}