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

        AddKeyword(new GameTauntKeyword(), true, false);
        AddKeyword(new GameDamageReductionKeyword(2), true, false);

        LateInit();
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 35;
        m_maxStamina = 3;
        m_staminaRegen = 2;
        m_power = 5;
    }
}