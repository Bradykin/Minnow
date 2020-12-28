using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHomonculus : GameUnit
{
    private int m_effectAmount = 1;

    public ContentHomonculus()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.1f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_name = "Homonculus";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconUnit(m_name);
        m_attackSFX = AudioHelper.PunchLight;
        m_aoeRange = 2;

        AddKeyword(new GameKnowledgeableKeyword(new GameGainStaminaRangeAction(this, m_effectAmount, m_aoeRange)), true, false);

        LateInit();
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 4;
        m_maxStamina = 6;
        m_staminaRegen = 2;
        m_power = 2;
    }
}