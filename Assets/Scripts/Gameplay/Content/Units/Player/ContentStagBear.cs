using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentStagBear : GameUnit
{
    public ContentStagBear()
    {
        m_worldTilePositionAdjustment = new Vector3(-0.15f, 0, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_name = "Stag Bear";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 20;
        m_maxStamina = 5;
        m_staminaRegen = 3;
        m_power = 6;
    }
}
