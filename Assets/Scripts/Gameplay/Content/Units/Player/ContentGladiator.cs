using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGladiator : GameUnit
{
    public ContentGladiator()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.3f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_name = "Gladiator";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);
        m_attackSFX = AudioHelper.SwordHeavy;

        AddKeyword(new GameEnrageKeyword(new GameGainStaminaAction(this, 3)), true, false);
        AddKeyword(new GameTauntKeyword(), true, false);

        LateInit();
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 50;
        m_maxStamina = 8;
        m_staminaRegen = 1;
        m_power = 6;
    }
}