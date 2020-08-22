using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCaveDragonEntity : GameEntity
{
    public GameCaveDragonEntity()
    {
        m_maxHealth = 50;
        m_maxAP = 9;
        m_apRegen = 5;
        m_range = 2;
        m_power = 12;

        m_team = Team.Player;

        m_name = "Cave Dragon";
        m_desc = "<b>Range</b> " + m_range + ".\n";
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }
}
