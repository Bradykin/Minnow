using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSandwalker : GameUnit
{
    public ContentSandwalker()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.5f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Starter;

        m_name = "Sandwalker";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconUnit(m_name);

        AddKeyword(new GameVictoriousKeyword(new GameGainStatsAction(this, 1, 0)), false);

        InitializeWithLevel(GetUnitLevel());

        LateInit();
    }

    public override void InitializeWithLevel(int level)
    {
        m_maxHealth = 5;
        m_maxStamina = 5;
        m_staminaRegen = 3;
        m_power = 3;

        if (level >= 1)
        {
            m_maxHealth = 8;
        }

        if (level >= 2)
        {
            AddKeyword(new GameMomentumKeyword(new GameHealAction(this, 2)), false);
        }
    }
}