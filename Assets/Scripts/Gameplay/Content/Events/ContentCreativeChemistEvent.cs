using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCreativeChemistEvent : GameEvent
{
    private int m_costOption2 = 10;
    private int m_costOption3 = 40;
    
    public ContentCreativeChemistEvent(GameTile tile)
    {
        m_name = "Creative Chemist";
        m_eventDesc = "You meet a suspicious individual offering a variety of services from his new chemistry store. Would you like a free sample, or are you willing to pay for power?";
        m_tile = tile;
        m_rarity = GameRarity.Common;

        m_optionOne = new GameEventChemistSmallStatsOption(tile);
        m_optionTwo = new GameEventChemistEnergyHealOption(tile, m_costOption2);
        m_optionThree = new GameEventChemistKnowledgeableSpellpowerOption(tile, m_costOption3);

        LateInit();

        m_minWaveToSpawn = 1;
        m_maxWaveToSpawn = Constants.FinalWaveNum;
    }

    public override bool IsValidToSpawn(GameTile tile)
    {
        bool baseValid = base.IsValidToSpawn(tile);

        if (!baseValid)
        {
            return false;
        }

        int playerGold = GameHelper.GetPlayer().m_wallet.m_gold;

        return playerGold >= m_costOption3;
    }
}