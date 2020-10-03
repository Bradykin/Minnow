using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRaptor : GameEntity
{
    public ContentRaptor()
    {
        m_maxHealth = 4;
        m_maxStamina = 5;
        m_staminaRegen = 3;
        m_power = 4;
        m_staminaToAttack = 1;

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_name = "Raptor";
        m_desc = "Only takes 1 Stamina to attack.";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }
}