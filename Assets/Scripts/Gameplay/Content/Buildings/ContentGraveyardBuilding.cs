using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGraveyardBuilding : GameBuildingBase
{
    public int m_goldToGain;

    public ContentGraveyardBuilding()
    {
        m_goldToGain = 5;

        m_name = "Graveyard";
        m_desc = "Certainty is always great in business.  Gain " + m_goldToGain + " gold whenever an entity dies.";
        m_rarity = GameRarity.Rare;

        m_maxHealth = 5;

        m_expandsPlaceRange = false;

        LateInit();
    }

    protected override void Die()
    {
        m_isDestroyed = true;
    }

    public override bool IsValidTerrainToPlace(GameTerrainBase terrain)
    {
        if (terrain is ContentGrassPlainsTerrain)
        {
            return true;
        }

        return false;
    }
}
