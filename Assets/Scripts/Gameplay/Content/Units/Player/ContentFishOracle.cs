using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFishOracle : GameUnit
{
    public ContentFishOracle()
    {
        m_worldTilePositionAdjustment = new Vector3(0.2f, -0.65f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;
        AddKeyword(new GameEnrageKeyword(new GameGainShivAction(1)), true, false);

        m_name = "Fish Oracle";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 25;
        m_maxStamina = 5;
        m_staminaRegen = 4;
        m_power = 2;
    }
}
