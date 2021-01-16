using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRhinoProtector : GameUnit
{
    public ContentRhinoProtector() : base()
    {
        m_worldTilePositionAdjustment = new Vector3(0.2f, 0, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        AddKeyword(new GameForestwalkKeyword(), true, false);

        m_name = "Rhino Protector";
        m_desc = "When this unit attacks another, the target gets <b>Rooted</b> until end of their turn.\nChanges the terrain under this at the end of the turn to a verdant forest.\n";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconUnit(m_name);
        m_attackSFX = AudioHelper.PunchLight;

        LateInit();
    }

    public override int HitUnit(GameUnit other, int damageAmount, bool spendStamina = true, bool isThornsAttack = false, bool canCleave = true)
    {
        int damageTaken = base.HitUnit(other, damageAmount, spendStamina, isThornsAttack, canCleave);

        if (!other.m_isDead)
        {
            other.AddKeyword(new GameRootedKeyword(), false, false);
            GameHelper.GetPlayer().AddScheduledAction(ScheduledActionTime.StartOfTurn, new GameLoseKeywordAction(other, new GameRootedKeyword()));
        }

        return damageTaken;
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
        m_attack = 4;
    }
}
