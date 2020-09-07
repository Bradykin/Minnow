using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentConjuredImp : GameEntity
{
    public ContentConjuredImp()
    {
        m_maxHealth = 3;
        m_maxAP = 4;
        m_apRegen = 2;
        m_power = 5;

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_name = "Conjured Imp";
        m_typeline = Typeline.Construct;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }
}
