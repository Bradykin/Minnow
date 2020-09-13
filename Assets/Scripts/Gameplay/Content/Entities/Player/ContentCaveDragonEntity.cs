using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCaveDragonEntity : GameEntity
{
    public ContentCaveDragonEntity()
    {
        m_maxHealth = 30;
        m_maxAP = 6;
        m_apRegen = 3;
        m_power = 6;

        m_keywordHolder.m_keywords.Add(new GameRangeKeyword(2));
        m_keywordHolder.m_keywords.Add(new GameFlyingKeyword());

        m_team = Team.Player;
        m_rarity = GameRarity.Event;

        m_name = "Cave Dragon";
        m_typeline = Typeline.Monster;

        LateInit();
    }
}
