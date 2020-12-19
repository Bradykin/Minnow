using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPyromage : GameUnit
{
    private int m_explodePower = 25;

    public ContentPyromage()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.5f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        AddKeyword(new GameRangeKeyword(2), true, false);
        AddKeyword(new GameKnowledgeableKeyword(new GameExplodeEnemiesAction(this, m_explodePower, 2)), true, false);

        m_name = "Pyromage";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);
        m_attackSFX = AudioHelper.SpellAttackMedium;

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