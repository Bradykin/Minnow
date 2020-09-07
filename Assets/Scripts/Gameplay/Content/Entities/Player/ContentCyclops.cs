using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCyclops : GameEntity
{
    public ContentCyclops()
    {
        m_maxHealth = 50;
        m_maxAP = 3;
        m_apRegen = 1;
        m_power = 50;
        m_sightRange = 1;

        m_team = Team.Player;
        m_rarity = GameRarity.Rare;

        m_name = "Cyclops";
        m_desc = "Has a sight range of 1";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }
}