using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMetalProtector : GameUnit
{
    public ContentMetalProtector() : base()
    {
        m_worldTilePositionAdjustment = new Vector3(-0.15f, 0, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Rare;

        m_name = "Metal Protector";
        m_desc = "Has stamina regen equal to max stamina.\n";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconUnit(m_name);
        m_attackSFX = AudioHelper.MetalClangAttack;

        LateInit();
    }

    public override int GetStaminaRegen()
    {
        return GetMaxStamina();
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 70;
        m_maxStamina = 1;
        m_staminaRegen = GetMaxStamina();
        m_power = 30;
    }
}
