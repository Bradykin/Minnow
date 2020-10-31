using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentInnBuilding : GameBuildingBase
{
    private GameWallet m_returns;

    public ContentInnBuilding()
    {
        m_returns = new GameWallet(75);

        m_name = "Inn";
        m_desc = "A bustling inn brings in the coin when times are good.  If it survives a wave, gives " + m_returns.m_gold + " coins.";
        m_rarity = GameRarity.Common;
        m_buildingType = BuildingType.Economic;

        m_maxHealth = 25;
        m_cost = new GameWallet(60);

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

    public override bool IsValidTerrainToPlace(GameTerrainBase terrain, GameTile tile)
    {
        if (terrain.IsPlains())
        {
            return true;
        }

        return false;
    }
}
