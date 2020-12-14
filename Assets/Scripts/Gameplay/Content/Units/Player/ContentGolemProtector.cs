using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGolemProtector : GameUnit
{
    public ContentGolemProtector()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.5f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Rare;

        AddKeyword(new GameMountainwalkKeyword(), true, false);

        m_name = "Golem Protector";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 300;
        m_maxStamina = 3;
        m_staminaRegen = 3;
        m_power = 200;
    }
}