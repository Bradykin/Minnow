using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGoblin : GameEntity
{
    private int m_effectRange = 2;
    private int m_effectIncrease = 4;
    
    public ContentGoblin()
    {
        m_maxHealth = 18;
        m_maxStamina = 4;
        m_staminaRegen = 3;
        m_power = 8;

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_name = "Goblin";
        m_desc = "Gains +" + m_effectIncrease + " power per allied <b>Monster</b> unit within " + m_effectRange + " range.";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }

    public override int GetPower()
    {
        int basePower = base.GetPower();

        if (GetGameTile() != null)
        {
            List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingTiles(GetGameTile(), m_effectRange);

            for (int i = 0; i < surroundingTiles.Count; i++)
            {
                if (surroundingTiles[i].IsOccupied() && !surroundingTiles[i].m_occupyingEntity.m_isDead &&
                    surroundingTiles[i].m_occupyingEntity.GetTeam() == Team.Player && surroundingTiles[i].m_occupyingEntity.GetTypeline() == Typeline.Monster)
                {
                    basePower += m_effectIncrease;
                }
            }
        }

        return basePower;
    }
}
