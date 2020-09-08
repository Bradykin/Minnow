using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSmithyBuilding : GameBuildingBase
{
    public ContentSmithyBuilding()
    {
        m_name = "Smithy";
        m_desc = "A grand boon to production; a smithy will give you a bonus action at the end of each wave it survived in.";
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

        player.AddBonusActions(1);
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
