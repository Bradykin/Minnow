using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCastleBuilding : GameBuildingBase
{
    public ContentCastleBuilding()
    {
        m_name = "Castle";
        m_desc = "This is your home base.  Lose this, and it's game over!";
        m_rarity = GameRarity.Starter;
        m_buildingType = BuildingType.Critical;

        m_maxHealth = 100;

        m_expandsPlaceRange = true;

        LateInit();
    }

    protected override void Die()
    {
        m_isDestroyed = true;

        UIHelper.CreateWorldElementNotification("Your castle has been destroyed, you have lost!", false, m_gameTile.GetWorldTile().gameObject);
        //SceneLoader.ActivateScene("LevelSelectScene", "LevelScene");
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
