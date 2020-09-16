using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDwarfHealer : GameEntity
{
    private int m_healingRange;
    private int m_healingPower;

    public ContentDwarfHealer()
    {
        m_healingRange = 3;
        m_healingPower = 10;

        m_maxHealth = 10;
        m_maxAP = 4;
        m_apRegen = 2;
        m_power = 1;

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        m_name = "Dwarf Healer";
        m_desc = "At the end of each turn, heal all friendly entities within range " + m_healingRange + " for " + m_healingPower + ".";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }

    public override void EndTurn()
    {
        base.EndTurn();

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingTiles(m_gameTile, m_healingRange, 0);

        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameEntity entity = surroundingTiles[i].m_occupyingEntity;

            if (entity == null)
            {
                continue;
            }

            if (entity.GetTeam() != Team.Player)
            {
                continue;
            }

            entity.Heal(m_healingPower);
        }
    }
}