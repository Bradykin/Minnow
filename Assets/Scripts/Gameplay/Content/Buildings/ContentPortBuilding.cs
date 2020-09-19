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

        m_maxHealth = 8;

        m_expandsPlaceRange = false;

        LateInit();
    }

    public override void TriggerEndOfWave()
    {
        if (m_isDestroyed)
        {
            return;
        }

        base.TriggerEndOfWave();

        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        player.m_wallet.AddResources(new GameWallet(m_returns.m_gold));
    }

    protected override void Die()
    {
        m_isDestroyed = true;
    }

    public override bool IsValidTerrainToPlace(GameTerrainBase terrain)
    {
        if (terrain.IsWater())
        {
            return true;
        }

        return false;
    }
}
