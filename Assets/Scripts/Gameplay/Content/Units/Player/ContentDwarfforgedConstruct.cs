using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDwarfforgedConstruct : GameUnit
{
    public ContentDwarfforgedConstruct()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_name = "Dwarfforged Construct";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconUnit(m_name);
        m_attackSFX = AudioHelper.MetalClangAttack;

        LateInit();
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 90;
        m_maxStamina = 4;
        m_staminaRegen = 3;
        m_power = 12;
    }
}