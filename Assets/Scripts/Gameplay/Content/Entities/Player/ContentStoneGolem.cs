using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentStoneGolem : GameUnit
{
    public ContentStoneGolem()
    {
        m_maxHealth = 40;
        m_maxStamina = 2;
        m_staminaRegen = 1;
        m_power = 1;


        m_team = Team.Player;
        m_rarity = GameRarity.Starter;

        m_name = "Stone Golem";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }
}