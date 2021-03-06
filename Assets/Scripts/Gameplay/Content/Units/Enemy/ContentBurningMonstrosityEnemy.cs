﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBurningMonstrosityEnemy : GameEnemyUnit
{
    private int m_explosionDamage = 15;

    public ContentBurningMonstrosityEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 10;
        m_maxStamina = 5;
        m_staminaRegen = 5;
        m_attack = 1;
        m_attackSFX = AudioHelper.PunchLight;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;
        m_aoeRange = 2;

        m_name = "Burning Monstrosity";
        m_desc = "";

        AddKeyword(new GameLavawalkKeyword(), true, false);
        AddKeyword(new GameMomentumKeyword(new GameDeathAction(this)), true, false);
        AddKeyword(new GameDeathKeyword(new GameExplodeAction(this, m_explosionDamage, m_aoeRange)), true, false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            m_desc  = $"All player units in the explosion radius are drained of all Stamina on death.\n";
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackOnceStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override void Die(bool canRevive = true, DamageType damageType = DamageType.None)
    {
        base.Die(canRevive, damageType);

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            List<GameTile> adjacentTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_gameTile, 1);
            for (int i = 0; i < adjacentTiles.Count; i++)
            {
                if (adjacentTiles[i].IsOccupied() && adjacentTiles[i].GetOccupyingUnit().GetTeam() == Team.Player && !adjacentTiles[i].GetOccupyingUnit().m_isDead)
                {
                    adjacentTiles[i].GetOccupyingUnit().SpendStamina(adjacentTiles[i].GetOccupyingUnit().GetCurStamina());
                }
            }
        }
    }
}