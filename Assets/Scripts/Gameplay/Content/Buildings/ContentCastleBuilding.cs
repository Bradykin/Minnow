using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCastleBuilding : GameBuildingBase
{
    public ContentCastleBuilding()
    {
        m_name = "Castle";
        m_desc = "This is your home base. Lose this, and it's game over!";
        m_rarity = GameRarity.Starter;
        m_buildingType = BuildingType.Critical;

        m_maxHealth = 100;
        m_cost = new GameWallet(0);

        m_expandsPlaceRange = true;

        LateInit();
    }

    public override void Die()
    {
        ContentTailOfLifeRelic tailRelic = (ContentTailOfLifeRelic)(GameHelper.GetPlayer().GetRelics().GetRelic<ContentTailOfLifeRelic>());
        if (tailRelic != null && !tailRelic.HasUsed())
        {
            m_curHealth = 50;
            tailRelic.Use();
            UIHelper.CreateWorldElementNotification("The castle is spared by the Tail of Life!", false, m_gameTile.GetWorldTile().gameObject);
            return;
        }

        base.Die();

        GameHelper.ReturnToLevelSelectFromLevelScene();
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
