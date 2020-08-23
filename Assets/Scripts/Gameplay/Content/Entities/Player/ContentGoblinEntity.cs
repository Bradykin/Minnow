﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGoblinEntity : GameEntity
{
    public ContentGoblinEntity()
    {
        m_maxHealth = 8;
        m_maxAP = 4;
        m_apRegen = 2;
        m_power = 2;

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_keywordHolder.m_keywords.Add(new GameVictoriousKeyword(new GameGainResourceAction(new GameWallet(10, 15, 10))));

        m_name = "Goblin";
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }
}