using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMountainBeast : GameUnit
{
    public ContentMountainBeast() : base()
    {
        m_worldTilePositionAdjustment = new Vector3(-0.15f, 0, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        AddKeyword(new GameEnrageKeyword(new GameGainStatsAction(this, 3, 0)), true, false);
        AddKeyword(new GameEnrageKeyword(new GameGainTempKeywordAction(this, new GameRegenerateKeyword(2))), true, false);

        m_name = "Mountain Beast";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    public override AudioClip GetAttackSFX()
    {
        if (GetPower() >= 30)
        {
            return AudioHelper.SlamHeavy;
        }

        return AudioHelper.PunchLight;
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 30;
        m_maxStamina = 5;
        m_staminaRegen = 2;
        m_power = 5;
    }
}
