using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGoblinLegend : GameUnit
{
    private int m_attackToGain = 10;
    private int m_healthToGain = 10;
    
    public ContentGoblinLegend() : base()
    {
        m_worldTilePositionAdjustment = new Vector3(-0.3f, -0.5f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_name = "Goblin Legend";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconUnit(m_name);

        AddKeyword(new GameVictoriousKeyword(new GameGainStatsAction(this, m_attackToGain, m_healthToGain)), true, false);

        LateInit();
    }

    public override AudioClip GetAttackSFX()
    {
        if (GetAttack() >= 40)
        {
            return AudioHelper.SpearHeavy;
        }

        return AudioHelper.SpearLight;
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 15;
        m_maxStamina = 6;
        m_staminaRegen = 4;
        m_attack = 2;
    }
}
