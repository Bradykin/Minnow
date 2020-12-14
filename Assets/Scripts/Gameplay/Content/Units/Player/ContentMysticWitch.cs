using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMysticWitch : GameUnit
{
    public ContentMysticWitch()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.5f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_name = "Mystic Witch";
        m_desc = "Spell cards cost 1 less to cast.\n";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 5;
        m_maxStamina = 3;
        m_staminaRegen = 2;
        m_power = 1;
    }
}