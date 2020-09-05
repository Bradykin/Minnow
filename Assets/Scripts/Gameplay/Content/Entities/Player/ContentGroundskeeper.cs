using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGroundskeeper : GameEntity
{
    public ContentGroundskeeper()
    {
        m_maxHealth = 50;
        m_maxAP = 2;
        m_apRegen = 1;
        m_power = 1;


        m_team = Team.Player;
        m_rarity = GameRarity.Starter;

        m_name = "Groundskeeper";
        m_typeline = Typeline.Mystic;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }
}