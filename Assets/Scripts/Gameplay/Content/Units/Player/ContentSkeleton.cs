using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSkeleton : GameUnit
{
    private int m_powerBonus = 5;
    private int m_healthBonus = 12;

    public ContentSkeleton()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.5f, 0);
        
        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_name = "Skeleton";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconUnit(m_name);
        m_attackSFX = AudioHelper.SwordHeavy;

        AddKeyword(new GameDeathKeyword(new GameReturnToDeckBuffedAction(this, m_powerBonus, m_healthBonus)), true, false);

        LateInit();
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 5;
        m_maxStamina = 5;
        m_staminaRegen = 3;
        m_power = 3;
    }
}