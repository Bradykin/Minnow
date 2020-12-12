using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMiner : GameUnit
{
    public ContentMiner()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.3f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_name = "Miner";
        m_desc = "Has <b>Fade</b> when in mountains.\n";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        AddKeyword(new GameMountainwalkKeyword(), true, false);

        LateInit();
    }

    public override GameFadeKeyword GetFadeKeyword(bool getInactiveFade = false)
    {
        if (GameHelper.IsUnitInWorld(this))
        {
            if (m_gameTile.GetTerrain().IsMountain())
            {
                return new GameFadeKeyword();
            }
        }

        return base.GetFadeKeyword();
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 1;
        m_maxStamina = 8;
        m_staminaRegen = 4;
        m_power = 1;
    }
}