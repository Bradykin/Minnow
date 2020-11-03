using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGladiator : GameUnit
{
    public ContentGladiator()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.3f, 0);

        m_maxHealth = 50;
        m_maxStamina = 8;
        m_staminaRegen = 1;
        m_power = 6;

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_name = "Gladiator";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        AddKeyword(new GameEnrageKeyword(new GameGainStaminaAction(this, 3)), false);
        AddKeyword(new GameTauntKeyword(), false);

        LateInit();
    }
}