using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDwarfArchitect : GameEntity
{
    private int m_healingRange;
    private int m_healingPower;

    public ContentDwarfArchitect()
    {
        m_healingRange = 2;
        m_healingPower = 10;

        m_maxHealth = 6;
        m_maxAP = 3;
        m_apRegen = 1;
        m_power = 2;

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        m_name = "Dwarf Architect";
        m_desc = "At the end of each turn, heal all buildings within range " + m_healingRange + " for " + m_healingPower + " (and restoring all destroyed ones).";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }

    public override void EndTurn()
    {
        base.EndTurn();

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingTiles(m_curTile, m_healingRange, 0);

        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameBuildingBase building = surroundingTiles[i].GetBuilding();

            if (building == null)
            {
                continue;
            }

            building.GetHealed(m_healingPower);
        }
    }
}