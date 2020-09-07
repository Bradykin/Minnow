using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDemonSoldier : GameEntity
{
    public ContentDemonSoldier()
    {
        m_maxHealth = 20;
        m_maxAP = 4;
        m_apRegen = 2;
        m_power = 4;

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;
        m_keywordHolder.m_keywords.Add(new GameFlyingKeyword());

        m_name = "Demon Soldier";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }
}