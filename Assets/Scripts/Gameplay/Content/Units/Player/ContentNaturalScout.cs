using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentNaturalScout : GameUnit
{
    public ContentNaturalScout()
    {
        m_worldTilePositionAdjustment = new Vector3(0.2f, -0.3f, 0);

        m_maxHealth = 1;
        m_maxStamina = 10;
        m_staminaRegen = 5;
        m_power = 1;
        m_staminaToAttack = 5;
        m_sightRange = 5;

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_name = "Natural Scout";
        m_desc = "Has sight range of " + m_sightRange + ".\nTakes 5 Stamina to attack.";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }
}
