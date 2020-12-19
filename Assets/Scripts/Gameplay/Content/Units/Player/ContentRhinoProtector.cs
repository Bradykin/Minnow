using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRhinoProtector : GameUnit
{
    public ContentRhinoProtector()
    {
        m_worldTilePositionAdjustment = new Vector3(0.2f, 0, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        AddKeyword(new GameForestwalkKeyword(), true, false);
        AddKeyword(new GameMomentumKeyword(new GameApplyKeywordToOtherOnMomentumAction(this, new GameRootedKeyword())), true, false);

        m_name = "Rhino Protector";
        m_desc = "Changes the terrain under this at the end of the turn to a verdant forest.\n";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconUnit(m_name);
        m_attackSFX = AudioHelper.PunchLight;

        LateInit();
    }

    public override void EndTurn()
    {
        base.EndTurn();

        if (GameHelper.IsUnitInWorld(this))
        {
            GetGameTile().SetTerrain(new ContentForestTerrain(), false);
        }
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 30;
        m_maxStamina = 5;
        m_staminaRegen = 4;
        m_power = 4;
    }
}
