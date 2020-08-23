using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentElvenRogue : GameEntity
{
    public ContentElvenRogue()
    {
        m_maxHealth = 5;
        m_maxAP = 4;
        m_apRegen = 4;
        m_power = 3;

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_keywordHolder.m_keywords.Add(new GameRangeKeyword(2));
        m_keywordHolder.m_keywords.Add(new GameMomentumKeyword(new GameGainPowerAction(this, 1)));

        m_name = "Elven Rogue";
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }
}