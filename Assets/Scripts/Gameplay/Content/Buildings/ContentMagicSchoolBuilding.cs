using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMagicSchoolBuilding : GameBuildingBase
{
    public int m_magicIncrease;

    public ContentMagicSchoolBuilding()
    {
        m_magicIncrease = 1;

        m_name = "Magic School";
        m_desc = "Train your spellcasters to increase spell power by " + m_magicIncrease + ".";
        m_rarity = GameRarity.Uncommon;
        m_buildingType = BuildingType.Economic;

        m_maxHealth = 12;

        m_expandsPlaceRange = false;

        LateInit();
    }

    protected override void Die()
    {
        m_isDestroyed = true;
    }

    public override bool IsValidTerrainToPlace(GameTerrainBase terrain)
    {
        if (terrain.IsForest())
        {
            return true;
        }

        return false;
    }
}
