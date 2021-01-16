using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMechanizedBeast : GameUnit
{
    public ContentMechanizedBeast() : base()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.4f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Starter;
        m_startWithMaxStamina = true;

        m_name = "Mechanized Beast";
        m_desc = "Starts at full Stamina.\n";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconUnit(m_name);
        m_attackSFX = AudioHelper.MetalClangAttack;

        LateInit();
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 12;
        m_maxStamina = 6;
        m_staminaRegen = 2;
        m_attack = 8;
    }
}