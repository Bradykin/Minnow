using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentInnBuilding : GameBuildingBase
{
    private GameWallet m_returns;

    public ContentInnBuilding()
    {
        m_returns = new GameWallet(50);

        m_name = "Inn";
        m_desc = "A bustling inn brings in the coin when times are good.  If it survives a wave, gives " + m_returns.m_gold + " coins.";
        m_rarity = GameRarity.Common;

        m_maxHealth = 25;

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
        if (terrain.IsFlatTerrain())
        {
            return true;
        }

        return false;
    }
}
