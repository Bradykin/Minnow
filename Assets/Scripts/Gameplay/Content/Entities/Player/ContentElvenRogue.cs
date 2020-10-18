using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentElvenRogue : GameUnit
{
    public ContentElvenRogue()
    {
        m_maxHealth = 5;
        m_maxStamina = 4;
        m_staminaRegen = 4;
        m_power = 1;

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        AddKeyword(new GameRangeKeyword(2), false);
        AddKeyword(new GameMomentumKeyword(new GameGainPowerAction(this, 1)), false);

        m_name = "Elven Rogue";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }
}