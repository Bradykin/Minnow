using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFishOracle : GameUnit
{
    public ContentFishOracle()
    {
        m_worldTilePositionAdjustment = new Vector3(0.2f, -0.65f, 0);

        m_maxHealth = 8;
        m_maxStamina = 4;
        m_staminaRegen = 2;
        m_power = 2;

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;
        AddKeyword(new GameSpellcraftKeyword(new GameDrawCardAction(1)), false);

        m_name = "Fish Oracle";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }
}
