using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCastleBuilding : GameBuildingBase
{
    public ContentCastleBuilding()
    {
        m_name = "Castle";
        m_desc = "This is your home base.  Lose this, and it's game over!";
        m_rarity = GameRarity.Unique;

        m_maxHealth = 100;

        LateInit();
    }

    protected override void Die()
    {
        m_isDestroyed = true;

        UIHelper.CreateWorldElementNotification("Your castle has been destroyed, you have lost!", false, m_curTile);
    }
}
