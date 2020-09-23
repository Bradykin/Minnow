using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ContentMetalGolem : GameEntity
{
    private int m_eatingRange;

    public ContentMetalGolem()
    {
        m_eatingRange = 1;

        m_maxHealth = 25;
        m_maxAP = 4;
        m_apRegen = 4;
        m_power = 5;

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        m_name = "Metal Golem";
        m_desc = "At the end of the turn, gain a damage shield for each mountain in range " + m_eatingRange + ".";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }

    public override void EndTurn()
    {
        base.EndTurn();

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingTiles(m_gameTile, m_eatingRange, 0);

        int numMountains = 0;
        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameTerrainBase terrain = surroundingTiles[i].GetTerrain();

            if (terrain.IsMountain())
            {
                numMountains++;
            }
        }

        if (numMountains == 0)
        {
            return;
        }

        GameDamageShieldKeyword damageShieldKeyword = GetKeyword<GameDamageShieldKeyword>();

        if (damageShieldKeyword == null)
        {
            AddKeyword(new GameDamageShieldKeyword(numMountains));
        }
        else
        {
            damageShieldKeyword.IncreaseShield(numMountains);
        }
    }
}