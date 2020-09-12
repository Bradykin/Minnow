using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentStoneGolem : GameEntity
{
    public ContentStoneGolem()
    {
        m_maxHealth = 75;
        m_maxAP = 2;
        m_apRegen = 2;
        m_power = 5;


        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        m_name = "Stone Golem";
        m_typeline = Typeline.Construct;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }
}