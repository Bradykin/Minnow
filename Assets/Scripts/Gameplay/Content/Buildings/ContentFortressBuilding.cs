using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public class ContentFortressBuilding : GameBuildingBase
{
    public ContentFortressBuilding()
    {
        m_range = 3;

        m_name = "Fortress";
        m_desc = $"Whenever a nearby allied unit in range {m_range} attacks a target, the Fortress will also attack it for half the damage that the unit did.";
        m_buildingType = BuildingType.Defensive;

        m_maxHealth = 40;
        m_cost = new GameWallet(70);
        m_rarity = GameRarity.Common;

        LateInit();
    }

    public override void OnOtherAttack(GameUnit attackingUnit, GameUnit attackedUnit, int damageAmount)
    {
        base.OnOtherAttack(attackingUnit, attackedUnit, damageAmount);

        if (attackedUnit.m_isDead)
        {
            return;
        }

        UIHelper.CreateWorldElementNotification("Fortress launches an attack!", true, GetWorldTile().gameObject);
        attackedUnit.GetHitByAbility(damageAmount / 2);
    }

    public override bool IsValidTerrainToPlace(GameTerrainBase terrain, GameTile tile)
    {
        if (terrain.IsMountain() && !terrain.IsVolcano())
        {
            return true;
        }

        return false;
    }
}
