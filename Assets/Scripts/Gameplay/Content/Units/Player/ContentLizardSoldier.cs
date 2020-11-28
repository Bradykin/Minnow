using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLizardSoldier : GameUnit
{
    public ContentLizardSoldier()
    {
        m_worldTilePositionAdjustment = new Vector3(0.1f, 0.5f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Starter;

        AddKeyword(new GameWaterwalkKeyword(), true, false);

        m_name = "Lizard Soldier";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 9;
        m_maxStamina = 5;
        m_staminaRegen = 5;
        m_power = 7;
    }
}