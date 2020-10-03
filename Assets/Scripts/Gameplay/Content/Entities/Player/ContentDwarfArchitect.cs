using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDwarfArchitect : GameUnit
{
    private int m_maxStaminaIncrease = 1;
    private int m_effectRange = 2;

    public ContentDwarfArchitect()
    {
        m_maxHealth = 20;
        m_maxStamina = 5;
        m_staminaRegen = 3;
        m_power = 4;

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        m_name = "Dwarf Architect";
        m_desc = "When an allied <b>Creation</b> unit is summoned within " + m_effectRange + " range, give it +" + m_maxStaminaIncrease + " max Stamina and have it start at full.";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    public override void OnOtherSummon(GameUnit other)
    {
        base.OnOtherSummon(other);

        if (other.GetTypeline() == Typeline.Creation)
        {
            int distanceBetween = WorldGridManager.Instance.GetPathLength(GetGameTile(), other.GetGameTile(), true, false, true);
            if (distanceBetween <= m_effectRange)
            {
                other.AddMaxStamina(m_maxStaminaIncrease);
                other.GainStamina(other.GetMaxStamina());
            }
        }
    }
}