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
        m_buildingType = BuildingType.Economic;

        m_maxHealth = 10;

        m_expandsPlaceRange = false;

        LateInit();
    }

    //Not currently needed since temples add 1 max energy, and startturn sets cur energy to max
    /*public override void StartTurn()
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

        player.AddEnergy(1);
    }*/

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
