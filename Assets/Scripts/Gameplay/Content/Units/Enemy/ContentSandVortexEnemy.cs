using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSandVortexEnemy : GameEnemyUnit
{
    private int dunesAttackIncrease = 10;
    
    public ContentSandVortexEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 18;
        m_maxStamina = 4;
        m_staminaRegen = 4;
        m_attack = 8;
        m_attackSFX = AudioHelper.Roar;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Sand Vortex";
        m_desc = $"Get +{dunesAttackIncrease}/+0 when on a dunes tile.\nWhen this unit dies, it will turn the terrain it is on into Sand Dunes.";

        AddKeyword(new GameDuneswalkKeyword(), true, false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            m_desc = $"Get +{dunesAttackIncrease}/+0 and is invulnerable when on a dunes tile.\nWhen this unit dies, it will turn the terrain it is on into Sand Dunes.";
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override int GetAttack()
    {
        int toReturn = base.GetAttack();

        if (GetGameTile().GetTerrain().IsDunes())
        {
            toReturn += dunesAttackIncrease;
        }
        
        return toReturn;
    }

    public override void Die(bool canRevive = true, DamageType damageType = DamageType.None)
    {
        GameTile gameTile = GetGameTile();

        base.Die(canRevive, damageType);

        if (!m_isDead)
        {
            return;
        }

        if (gameTile.HasBuilding() && gameTile.GetBuilding().GetTeam() == GetTeam())
        {
            return;
        }

        gameTile.SetTerrain(new ContentDesertDunesTerrain(), true);
    }

    public override bool IsInvulnerable()
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            return GetGameTile().GetTerrain().IsDunes() || base.IsInvulnerable();
        }
        else
        {
            return base.IsInvulnerable();
        }
    }
}