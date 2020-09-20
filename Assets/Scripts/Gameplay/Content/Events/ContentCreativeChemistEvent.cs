using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCreativeChemistEvent : GameEvent
{
    private int m_costOption2 = 50;
    private int m_costOption3 = 75;
    
    public ContentCreativeChemistEvent(GameTile tile)
    {
        m_name = "Creative Chemist";
        m_eventDesc = "You meet a suspicious individual offering a variety of services from his new chemistry store. Would you like a free sample, or are you willing to pay for power?";
        m_tile = tile;
        m_rarity = GameRarity.Common;

        m_optionOne = new GameEventChemistSmallStatsOption(tile);

        GamePlayer player = GameHelper.GetPlayer();

        if (player.m_wallet.m_gold >= m_costOption2)
        {
            m_optionTwo = new GameEventChemistEnergyHealOption(tile, m_costOption2);
        }

        if (player.m_wallet.m_gold >= m_costOption3)
        {
            m_optionThree = new GameEventChemistKnowledgeableSpellpowerOption(tile, m_costOption3);
        }

        LateInit();
    }
}