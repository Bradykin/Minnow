using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMage : GameUnit
{
    public ContentMage()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.3f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        AddKeyword(new GameRangeKeyword(3), true, false);
        AddKeyword(new GameKnowledgeableKeyword(new GameGainStatsAction(this, 3, 0)), true, false);

        m_name = "Mage";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 6;
        m_maxStamina = 5;
        m_staminaRegen = 3;
        m_power = 2;
    }
}