using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGoblinEntity : GameEntityBase
{
    public GameGoblinEntity()
    {
        m_maxHealth = 8;
        m_maxAP = 4;
        m_apRegen = 2;
        m_power = 2;
        m_toughness = 0;

        m_team = Team.Player;

        m_name = "Goblin";
        m_desc = "This goblin can do some work!  And just *some* work.";
        m_icon = null;

        LateInit();
    }
}
