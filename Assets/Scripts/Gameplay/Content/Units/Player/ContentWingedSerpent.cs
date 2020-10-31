using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWingedSerpent : GameUnit
{
    public ContentWingedSerpent()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.5f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Starter;

        m_name = "Winged Serpent";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconUnit(m_name);

        AddKeyword(new GameFlyingKeyword(), false);

        InitializeWithLevel(GetUnitLevel());

        LateInit();
    }

    public override void InitializeWithLevel(int level)
    {
        m_maxHealth = 3;
        m_maxStamina = 5;
        m_staminaRegen = 2;
        m_power = 1;

        if (level >= 1)
        {
            m_maxHealth = 8;
        }

        if (level >= 2)
        {
            m_staminaRegen = 3;
        }
    }
}