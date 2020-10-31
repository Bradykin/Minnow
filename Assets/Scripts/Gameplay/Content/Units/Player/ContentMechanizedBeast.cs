using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMechanizedBeast : GameUnit
{
    public ContentMechanizedBeast()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.4f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Starter;
        m_startWithMaxStamina = true;

        m_name = "Mechanized Beast";
        m_desc = "Starts at full Stamina.";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconUnit(m_name);

        InitializeWithLevel(GetUnitLevel());

        LateInit();
    }

    public override void InitializeWithLevel(int level)
    {
        m_maxHealth = 5;
        m_maxStamina = 6;
        m_staminaRegen = 2;
        m_power = 5;

        if (level >= 1)
        {
            m_maxStamina = 10;
        }

        if (level >= 2)
        {
            AddKeyword(new GameMountainwalkKeyword(), false);
        }
    }
}