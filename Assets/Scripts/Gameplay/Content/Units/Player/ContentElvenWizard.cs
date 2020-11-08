using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentElvenWizard : GameUnit
{
    public ContentElvenWizard()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.2f, 0);

        m_maxHealth = 15;
        m_maxStamina = 8;
        m_staminaRegen = 2;
        m_power = 9;

        m_team = Team.Player;
        m_rarity = GameRarity.Rare;

        AddKeyword(new GameRangeKeyword(3), false);
        AddKeyword(new GameSpellcraftKeyword(new GameGainStaminaAction(this, 1)), false);

        m_name = "Elven Wizard";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }
}