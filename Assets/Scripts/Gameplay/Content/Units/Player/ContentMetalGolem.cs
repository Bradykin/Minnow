using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ContentMetalGolem : GameUnit
{
    public ContentMetalGolem()
    {
        m_worldTilePositionAdjustment = new Vector3(0.1f, 0.3f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        m_name = "Metal Golem";
        m_desc = "At the start of each turn, gain <b>Damage Shield</b>.\n";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconUnit(m_name);

        AddKeyword(new GameMountainwalkKeyword(), true, false);

        LateInit();
    }

    public override void StartTurn()
    {
        base.EndTurn();

        if (GetDamageShieldKeyword() == null)
        {
            AddKeyword(new GameDamageShieldKeyword(), false, false);
        }
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 40;
        m_maxStamina = 6;
        m_staminaRegen = 5;
        m_power = 15;
    }
}