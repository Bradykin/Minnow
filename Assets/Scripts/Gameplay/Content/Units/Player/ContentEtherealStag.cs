using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentEtherealStag : GameUnit
{
    public ContentEtherealStag() : base()
    {
        m_worldTilePositionAdjustment = new Vector3(0.2f, 0, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_name = "Ethereal Stag";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconUnit(m_name);
        m_attackSFX = AudioHelper.SlamHeavy;

        LateInit();
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 50;
        m_maxStamina = 6;
        m_staminaRegen = 5;
        m_power = 20;
    }
}
