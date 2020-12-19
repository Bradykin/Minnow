using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentNaturalScout : GameUnit
{
    public ContentNaturalScout()
    {
        m_worldTilePositionAdjustment = new Vector3(0.2f, -0.3f, 0);

        m_sightRange = 5;

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_name = "Natural Scout";
        m_desc = "Has sight range of " + m_sightRange + ".\n";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconUnit(m_name);
        m_attackSFX = AudioHelper.BoarAttack;

        AddKeyword(new GameFadeKeyword(), true, false);

        LateInit();
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 10;
        m_maxStamina = 4;
        m_staminaRegen = 3;
        m_power = 1;
    }
}
