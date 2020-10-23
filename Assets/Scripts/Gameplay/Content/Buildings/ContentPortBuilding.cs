using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPortBuilding : GameBuildingBase
{
    private GameWallet m_returns;

    public ContentPortBuilding()
    {
        m_returns = new GameWallet(90);

        m_name = "Port";
        m_desc = "A port brings in the coin when times are good.  If it survives a wave, gives " + m_returns.m_gold + " coins.";
        m_rarity = GameRarity.Common;
        m_buildingType = BuildingType.Economic;

        m_maxHealth = 8;
        m_cost = new GameWallet(50);

        m_expandsPlaceRange = false;

        LateInit();
    }

    public override void TriggerEndOfWave()
    {   
        if (!m_isDestroyed)
        {
            GamePlayer player = GameHelper.GetPlayer();

            if (player == null)
            {
                return;
            }

            player.m_wallet.AddResources(new GameWallet(m_returns.m_gold));
        }

        base.TriggerEndOfWave();
    }

    protected override void Die()
    {
        m_isDestroyed = true;
    }

    public override bool IsValidTerrainToPlace(GameTerrainBase terrain, GameTile tile)
    {
        if (terrain.IsWater())
        {
            if (tile != null)
            {
                List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(tile, 1);
                for (int i = 0; i < surroundingTiles.Count; i++)
                {
                    if (!surroundingTiles[i].GetTerrain().IsWater())
                    {
                        return true;
                    }
                }
                return false;
            }
            
            return true;
        }

        return false;
    }
}
