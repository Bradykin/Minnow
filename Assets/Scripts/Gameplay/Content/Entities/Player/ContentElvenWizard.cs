using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentElvenWizard : GameEntity
{
    public ContentElvenWizard()
    {
        m_maxHealth = 15;
        m_maxAP = 8;
        m_apRegen = 2;
        m_power = 9;

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        m_keywordHolder.m_keywords.Add(new GameRangeKeyword(3));
        m_keywordHolder.m_keywords.Add(new GameSpellcraftKeyword(new GameGainAPAction(this, 2)));

        m_name = "Elven Wizard";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }
}