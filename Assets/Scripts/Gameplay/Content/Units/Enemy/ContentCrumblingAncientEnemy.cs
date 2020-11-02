using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCrumblingAncientEnemy : GameEnemyUnit
{
    private int m_brittleAmount = 15;

    public ContentCrumblingAncientEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 200;
        m_maxStamina = 4;
        m_staminaRegen = 3;
        m_power = 50;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Uncommon;

        m_minWave = 3;
        m_maxWave = 4;

        m_name = "Crumbling Ancient";
        m_desc = "";

        AddKeyword(new GameBrittleKeyword(m_brittleAmount), false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameDamageReductionKeyword(3), false);
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