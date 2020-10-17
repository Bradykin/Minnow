using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMiner : GameUnit
{
    private int m_miningRange;
    private int m_miningVal;

    public ContentMiner()
    {
        m_miningRange = 1;
        m_miningVal = 5;

        m_maxHealth = 4;
        m_maxStamina = 4;
        m_staminaRegen = 1;
        m_power = 1;

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_name = "Miner";
        m_desc = "At the end of each turn, gain " + m_miningVal + " gold for each mountain in range " + m_miningRange + ".";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    public override void EndTurn()
    {
        base.EndTurn();

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_gameTile, m_miningRange, 0);

        int numMountains = 0;
        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameTerrainBase terrain = surroundingTiles[i].GetTerrain();

            if (terrain.IsMountain())
            {
                numMountains++;
            }
        }

        GameHelper.GetPlayer().m_wallet.AddResources(new GameWallet(numMountains * m_miningVal));
    }
}