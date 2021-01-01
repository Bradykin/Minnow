using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMagicSchoolBuilding : GameBuildingBase
{
    public int m_magicIncrease = 2;

    public ContentMagicSchoolBuilding()
    {
        m_range = 0;

        m_name = "Magic School";
        m_desc = "Increase <b>Magic Power</b> by " + m_magicIncrease + ", empowering your spells!";
        m_rarity = GameRarity.Uncommon;
        m_buildingType = BuildingType.Economic;

        m_maxHealth = 15;
        m_cost = new GameWallet(70);

        m_expandsPlaceRange = false;

        LateInit();
    }

    public override bool IsValidTerrainToPlace(GameTerrainBase terrain, GameTile tile)
    {
        if (terrain.IsForest())
        {
            return true;
        }

        return false;
    }
}
