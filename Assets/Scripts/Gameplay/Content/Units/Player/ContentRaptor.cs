using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRaptor : GameUnit
{
    public ContentRaptor() : base()
    {
        m_worldTilePositionAdjustment = new Vector3(0.2f, -0.6f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_name = "Raptor";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconUnit(m_name);
        m_attackSFX = AudioHelper.RaptorAttack;

        LateInit();
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 4;
        m_maxStamina = 5;
        m_staminaRegen = 3;
        m_power = 4;

        m_staminaToAttack = 1;
    }
}