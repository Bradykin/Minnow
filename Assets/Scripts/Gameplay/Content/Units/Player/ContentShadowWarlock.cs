using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentShadowWarlock : GameUnit
{
    public ContentShadowWarlock()
    {
        m_maxHealth = 12;
        m_maxStamina = 5;
        m_staminaRegen = 2;
        m_power = 2;

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;
        AddKeyword(new GameSpellcraftKeyword(new GameGainStatsAction(this, 2, 0)), false);
        AddKeyword(new GameRangeKeyword(2), false);

        m_name = "Shadow Warlock";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }
}