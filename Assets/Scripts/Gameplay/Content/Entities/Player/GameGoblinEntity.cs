using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGoblinEntity : GameEntity
{
    public GameGoblinEntity()
    {
        m_maxHealth = 8;
        m_maxAP = 4;
        m_apRegen = 2;
        m_power = 2;

        m_team = Team.Player;

        m_name = "Goblin";
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }
}
