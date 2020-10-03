using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentInjuredTroll : GameEntity
{
    public ContentInjuredTroll()
    {
        m_maxHealth = 45;
        m_maxStamina = 8;
        m_staminaRegen = 4;
        m_power = 12;

        m_keywordHolder.m_keywords.Add(new GameRegenerateKeyword(20));

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        m_name = "Injured Troll";
        m_desc = "Starts at 1 health and 0 Stamina.";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }

    public override void OnSummon()
    {
        base.OnSummon();

        m_curHealth = 1;
        m_curStamina = 0;
    }
}
