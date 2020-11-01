using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Target buildings over units
public class ContentPhoenixEnemy : GameEnemyUnit
{
    public ContentPhoenixEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 45;
        m_maxStamina = 6;
        m_staminaRegen = 4;
        m_power = 15;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Uncommon;

        m_name = "Phoenix";
        m_desc = "If this unit dies while on a lava tile, it will respawn with full health.";

        m_minWave = 4;
        m_maxWave = 6;

        AddKeyword(new GameFlyingKeyword(), false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameEnrageKeyword(new GameExplodeAction(this, 5, 1)), false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    protected override bool ShouldRevive(out int healthSurvivedAt)
    {
        bool shouldReviveBase = base.ShouldRevive(out healthSurvivedAt);

        bool isReviving = shouldReviveBase || GetGameTile().GetTerrain().IsLava();

        if (isReviving)
        {
            healthSurvivedAt = GetMaxHealth();
            SpendStamina(GetCurStamina());
        }

        return isReviving;
    }
}