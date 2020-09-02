using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCaveDragonEntity : GameEntity
{
    public ContentCaveDragonEntity()
    {
        m_maxHealth = 50;
        m_maxAP = 9;
        m_apRegen = 5;
        m_power = 12;

        m_keywordHolder.m_keywords.Add(new GameRangeKeyword(2));
        m_keywordHolder.m_keywords.Add(new GameRegenerateKeyword(3));

        m_team = Team.Player;
        m_rarity = GameRarity.Event;

        m_name = "Cave Dragon";
        m_typeline = Typeline.Legend;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }
}
