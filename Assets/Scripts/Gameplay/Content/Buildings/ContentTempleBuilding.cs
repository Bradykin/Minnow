using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTempleBuilding : GameBuildingBase
{
    public ContentTempleBuilding()
    {
        m_name = "Temple";
        m_desc = "Faith in the gods grants you an extra energy every turn this isn't destroyed.";
        m_rarity = GameRarity.Uncommon;

        m_maxHealth = 10;

        m_expandsPlaceRange = false;

        LateInit();
    }

    public override void StartTurn()
    {
        if (m_isDestroyed)
        {
            return;
        }

        base.StartTurn();

        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        player.AddBonusEnergy(1);
    }

    protected override void Die()
    {
        m_isDestroyed = true;
    }

    public override bool IsValidTerrainToPlace(GameTerrainBase terrain)
    {
        if (terrain is ContentGrassTerrain)
        {
            return true;
        }

        return false;
    }
}
