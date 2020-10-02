using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDwarfArchitect : GameEntity
{
    private int m_maxAPIncrease = 1;
    private int m_effectRange = 2;

    public ContentDwarfArchitect()
    {
        m_maxHealth = 20;
        m_maxAP = 5;
        m_apRegen = 3;
        m_power = 4;

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        m_name = "Dwarf Architect";
        m_desc = "When an allied <b>Creation</b> unit is summoned within " + m_effectRange + " range, give it +" + m_maxAPIncrease + " max AP and have it start at max.";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }

    public override void OnOtherSummon(GameEntity other)
    {
        base.OnOtherSummon(other);

        if (other.GetTypeline() == Typeline.Creation)
        {
            int distanceBetween = WorldGridManager.Instance.GetPathLength(GetGameTile(), other.GetGameTile(), true, false, true);
            if (distanceBetween <= m_effectRange)
            {
                other.AddMaxAP(m_maxAPIncrease);
                other.GainAP(other.GetMaxAP());
            }
        }
    }
}