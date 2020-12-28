using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGriffonEnemy : GameEnemyUnit
{
    int m_statBoostAmount = 5;
    
    public ContentGriffonEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 20;
        m_maxStamina = 5;
        m_staminaRegen = 4;
        m_power = 12;
        m_attackSFX = AudioHelper.BirdFlap;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;
        m_aoeRange = 2;

        m_name = "Griffon";
        m_desc = $"When this unit dies, all other {m_name}s within range {m_aoeRange} gain +{m_statBoostAmount}/{m_statBoostAmount}.\n";

        AddKeyword(new GameFlyingKeyword(), true, false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            m_statBoostAmount += 5;
            m_desc = $"When this unit dies, all other {m_name}s within range {m_aoeRange} gain +{m_statBoostAmount}/{m_statBoostAmount} and are fully healed.\n";
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override void Die(bool canRevive = true, DamageType damageType = DamageType.None)
    {
        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_gameTile, m_aoeRange);

        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            if (surroundingTiles[i].IsOccupied() && surroundingTiles[i].GetOccupyingUnit() is ContentGriffonEnemy)
            {
                surroundingTiles[i].GetOccupyingUnit().AddStats(m_statBoostAmount, m_statBoostAmount, true, true);
                if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
                {
                    surroundingTiles[i].GetOccupyingUnit().Heal(surroundingTiles[i].GetOccupyingUnit().GetMaxHealth());
                }
            }
        }

        base.Die(canRevive, damageType);
    }
}