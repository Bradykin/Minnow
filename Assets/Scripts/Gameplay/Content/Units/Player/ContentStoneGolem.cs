using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentStoneGolem : GameUnit
{
    public ContentStoneGolem()
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.4f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Starter;

        m_name = "Stone Golem";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconUnit(m_name);

        AddKeyword(new GameTauntKeyword(), false);
        AddKeyword(new GameDamageReductionKeyword(1), false);
        AddKeyword(new GameThornsKeyword(2), false);

        InitializeWithLevel(GetUnitLevel());

        LateInit();
    }

    public override void InitializeWithLevel(int level)
    {
        m_maxHealth = 40;
        m_maxStamina = 2;
        m_staminaRegen = 1;
        m_power = 1;

        if (level >= 1)
        {
            m_maxStamina = 4;
            m_staminaRegen = 2;
        }

        if (level >= 2)
        {
            m_power = 10;
            m_maxHealth = 60;
        }
    }
}