using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBurningMonstrosityEnemy : GameEnemyUnit
{
    private int m_explosionDamage = 20;
    private int m_explosionRange = 1;

    public ContentBurningMonstrosityEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 4;
        m_maxStamina = 7;
        m_staminaRegen = 5;
        m_power = 10;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_minWave = 3;
        m_maxWave = 4;

        m_name = "Burning Monstrosity";
        m_desc = "";

        AddKeyword(new GameLavawalkKeyword(), false);
        AddKeyword(new GameMomentumKeyword(new GameDeathAction(this)), false);
        AddKeyword(new GameDeathKeyword(new GameExplodeAction(this, m_explosionDamage, m_explosionRange)), false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            m_desc  = $"All player units in the explosion radius are drained of all Stamina on death.";
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override void Die(bool canRevive = true)
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            List<GameTile> adjacentTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_gameTile, 1);
            for (int i = 0; i < adjacentTiles.Count; i++)
            {
                if (adjacentTiles[i].IsOccupied() && adjacentTiles[i].m_occupyingUnit.GetTeam() == Team.Player && !adjacentTiles[i].m_occupyingUnit.m_isDead)
                {
                    adjacentTiles[i].m_occupyingUnit.SpendStamina(adjacentTiles[i].m_occupyingUnit.GetCurStamina());
                }
            }
        }

        base.Die(canRevive);
    }
}