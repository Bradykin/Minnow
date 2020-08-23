using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentElvenWizard : GameEntity
{
    public ContentElvenWizard()
    {
        m_maxHealth = 5;
        m_maxAP = 6;
        m_apRegen = 2;
        m_power = 5;

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        m_keywordHolder.m_keywords.Add(new GameRangeKeyword(3));
        m_keywordHolder.m_keywords.Add(new GameSpellcraftKeyword(new GameGainAPAction(this, 2)));

        m_name = "Elven Wizard";
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }
}