using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGriffonEnemy : GameEnemyUnit
{
    int m_statBoostAmount = 5;
    int m_statBoostRange = 2;
    
    public ContentGriffonEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 20;
        m_maxStamina = 5;
        m_staminaRegen = 4;
        m_power = 12;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Griffon";
        m_desc = $"When this unit dies, all other {m_name}s within range {m_statBoostRange} gain +{m_statBoostAmount}/{m_statBoostAmount}.\n";

        AddKeyword(new GameFlyingKeyword(), false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            m_statBoostAmount += 5;
            m_desc = $"When this unit dies, all other {m_name}s within range {m_statBoostRange} gain +{m_statBoostAmount}/{m_statBoostAmount} and are fully healed.\n";
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override void Die(bool canRevive = true)
    {
        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_gameTile, m_statBoostRange);

        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            if (surroundingTiles[i].IsOccupied() && surroundingTiles[i].m_occupyingUnit is ContentGriffonEnemy)
            {
                surroundingTiles[i].m_occupyingUnit.AddStats(m_statBoostAmount, m_statBoostAmount);
                if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
                {
                    surroundingTiles[i].m_occupyingUnit.Heal(surroundingTiles[i].m_occupyingUnit.GetMaxHealth());
                }
            }
        }

        base.Die(canRevive);
    }
}