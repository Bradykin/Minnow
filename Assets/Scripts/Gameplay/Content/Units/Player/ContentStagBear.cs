using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentStagBear : GameUnit
{
    private int m_enrageStats = 1;
    private int m_momentumStats = 2;
    private int m_victoriousStats = 3;

    private int m_attackThreshhold = 60;
    private int m_bonusStamRegen = 2;

    public ContentStagBear() : base()
    {
        m_worldTilePositionAdjustment = new Vector3(-0.2f, 0.5f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        AddKeyword(new GameEnrageKeyword(new GameGainStatsAction(this, m_enrageStats, m_enrageStats)), true, false);
        AddKeyword(new GameMomentumKeyword(new GameGainStatsAction(this, m_momentumStats, m_momentumStats)), true, false);
        AddKeyword(new GameVictoriousKeyword(new GameGainStatsAction(this, m_victoriousStats, m_victoriousStats)), true, false);

        m_name = "Stag Bear";
        m_desc = $"If this has more than {m_attackThreshhold} attack, it gains +{m_bonusStamRegen} stamina regen.\n";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    public override AudioClip GetAttackSFX()
    {
        if (GetAttack() >= m_attackThreshhold)
        {
            return AudioHelper.SlamHeavy;
        }

        return AudioHelper.PunchLight;
    }

    public override int GetStaminaRegen()
    {
        int returnVal = base.GetStaminaRegen();

        if (GetAttack() > m_attackThreshhold)
        {
            returnVal += m_bonusStamRegen;
        }

        return returnVal;
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 20;
        m_maxStamina = 6;
        m_staminaRegen = 3;
        m_attack = 3;
    }
}
