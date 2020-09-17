using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGladiator : GameEntity
{
    public ContentGladiator()
    {
        m_maxHealth = 50;
        m_maxAP = 8;
        m_apRegen = 1;
        m_power = 6;

        m_team = Team.Player;
        m_rarity = GameRarity.Common;
        m_keywordHolder.m_keywords.Add(new GameEnrageKeyword(new GameGainAPAction(this, 3)));

        m_name = "Gladiator";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }
}