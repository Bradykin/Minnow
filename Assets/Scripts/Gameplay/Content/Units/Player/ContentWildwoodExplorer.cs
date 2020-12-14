using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWildwoodExplorer : GameUnit
{
    public ContentWildwoodExplorer()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.5f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_instantForestMovement = true;

        m_name = "Wildwood Explorer";
        m_desc = "Forests take no stamina to move through.\n";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 18;
        m_maxStamina = 5;
        m_staminaRegen = 2;
        m_power = 15;
    }
}