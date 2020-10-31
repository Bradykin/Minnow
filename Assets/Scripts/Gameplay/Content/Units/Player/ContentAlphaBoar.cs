using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAlphaBoar : GameUnit
{
    public ContentAlphaBoar()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Starter;

        m_name = "Alpha Boar";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconUnit(m_name);

        AddKeyword(new GameTauntKeyword(), false);
        AddKeyword(new GameDamageReductionKeyword(1), false);
        AddKeyword(new GameThornsKeyword(4), false);

        InitializeWithLevel(GetUnitLevel());

        LateInit();
    }

    public override void InitializeWithLevel(int level)
    {
        m_maxHealth = 30;
        m_maxStamina = 3;
        m_staminaRegen = 2;
        m_power = 5;

        if (level >= 1)
        {
            m_maxHealth = 60;
        }

        if (level >= 2)
        {
            m_power = 10;
            m_maxStamina = 6;
            AddKeyword(new GameEnrageKeyword(new GameGainEnergyAction(1)));
        }
    }
}