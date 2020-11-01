using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class ContentUndeadMammoth : GameUnit
{
    private int m_powerBuff;
    private int m_healthBuff;

    public ContentUndeadMammoth()
    {
        m_worldTilePositionAdjustment = new Vector3(0.3f, 0.5f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Starter;

        m_name = "Undead Mammoth";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconUnit(m_name);

        InitializeWithLevel(GetUnitLevel());

        AddKeyword(new GameDeathKeyword(new GameReturnToDeckBuffedAction(this, m_powerBuff, m_healthBuff)), false);

        LateInit();
    }

    public override void InitializeWithLevel(int level)
    {
        m_powerBuff = 3;
        m_healthBuff = 10;

        m_maxHealth = 12;
        m_maxStamina = 5;
        m_staminaRegen = 3;
        m_power = 4;

        if (level >= 1)
        {
            m_powerBuff = 8;
        }

        if (level >= 2)
        {
            m_maxStamina = 6;
            m_staminaRegen = 6;
        }
    }
}