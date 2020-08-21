using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInjuredTrollEntity : GameEntity
{
    public GameInjuredTrollEntity()
    {
        m_maxHealth = 20;
        m_maxAP = 6;
        m_apRegen = 3;
        m_power = 4;
        m_regen = 4;

        m_team = Team.Player;

        m_name = "Injured Troll";
        m_desc = "<b>Regenerate</b> " + m_regen + ".\n" +
            "Starts at 1 health and 0 AP.";
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();

        m_curHealth = 1;
        m_curAP = 0;
    }
}
