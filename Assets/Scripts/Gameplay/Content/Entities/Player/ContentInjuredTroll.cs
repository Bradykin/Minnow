using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentInjuredTroll : GameEntity
{
    public ContentInjuredTroll()
    {
        m_maxHealth = 20;
        m_maxAP = 6;
        m_apRegen = 3;
        m_power = 6;

        m_keywordHolder.m_keywords.Add(new GameRegenerateKeyword(5));

        m_team = Team.Player;
        m_rarity = GameRarity.Starter;

        m_name = "Injured Troll";
        m_desc = "Starts at 1 health and 0 AP.";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }

    public override void OnSummon()
    {
        base.OnSummon();

        m_curHealth = 1;
        m_curAP = 0;
    }
}
