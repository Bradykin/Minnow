using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLichCastleBuilding : GameBuildingBase
{
    public ContentLichCastleBuilding()
    {
        m_name = "Lich Castle";
        m_desc = "The necropolis of the ancient lich. Destroy it to destroy his phylactery and force him into the open.";
        m_rarity = GameRarity.Common;
        m_buildingType = BuildingType.Defensive;

        m_maxHealth = 100;

        m_expandsPlaceRange = false;
        m_cost = new GameWallet(0);

        LateInit();

        m_team = Team.Enemy;
    }

    public override void Die()
    {
        ContentLichEnemy lichEnemy = new ContentLichEnemy(GameHelper.GetOpponent());
        lichEnemy.m_hasReanimated = true;
        GameHelper.GetGameController().m_activeBossUnits.Add(lichEnemy);
        GetGameTile().PlaceUnit(lichEnemy);
        lichEnemy.OnSummon();
        WorldController.Instance.m_gameController.m_gameOpponent.AddControlledUnit(lichEnemy);
        UIHelper.CreateHUDNotification("Phylactery Destroyed", "The necropolis has fallen and the Lich is forced out. Destroy it!");

        base.Die();
    }

    public override bool IsValidTerrainToPlace(GameTerrainBase terrain, GameTile tile)
    {
        if (terrain.IsWater())
        {
            return false;
        }

        if (terrain.IsMountain())
        {
            return false;
        }

        return true;
    }
}
