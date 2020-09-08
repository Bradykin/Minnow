using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMetalGolem : GameEntity
{
    private int m_eatingRange;
    private int m_healingVal;

    public ContentMetalGolem()
    {
        m_eatingRange = 1;
        m_healingVal = 4;

        m_maxHealth = 25;
        m_maxAP = 4;
        m_apRegen = 4;
        m_power = 5;

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        m_name = "Metal Golem";
        m_desc = "At the end of the turn, heals for " + m_healingVal + " for each mountain in range " + m_eatingRange + ".";
        m_typeline = Typeline.Construct;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }

    public override void EndTurn()
    {
        base.EndTurn();

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingTiles(m_curTile, m_eatingRange, 0);

        int numMountains = 0;
        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameTerrainBase terrain = surroundingTiles[i].GetTerrain();

            if (terrain.IsMountain())
            {
                numMountains++;
            }
        }

        this.Heal(numMountains * m_healingVal);
    }
}