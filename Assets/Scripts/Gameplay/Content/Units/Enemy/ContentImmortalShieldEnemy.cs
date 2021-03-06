﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentImmortalShieldEnemy : GameEnemyUnit
{
    private int m_attackIncreaseAmount = 10;
    private int m_damageReductionIncrease = 3;

    public ContentImmortalShieldEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.BossStrength))
        {
            m_maxHealth = 350;
            m_maxStamina = 7;
            m_staminaRegen = 7;
            m_attack = 25;
        }
        else
        {
            m_maxHealth = 150;
            m_maxStamina = 5;
            m_staminaRegen = 5;
            m_attack = 12;
        }

        m_team = Team.Enemy;
        m_rarity = GameRarity.Special;
        m_isBoss = true;
        m_aoeRange = 3;
        m_attackSFX = AudioHelper.MetalClangAttack;

        m_name = "Immortal Shield";
        m_desc = $"One of the final bosses. If all three Immortals die, you win. If any are alive at the start of their turn, the others will respawn.\nOther enemies in range {m_aoeRange} get +{m_attackIncreaseAmount}/+0 and {m_damageReductionIncrease} <b>Damage Reduction</b>.\n";


        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override void OnSummon()
    {
        base.OnSummon();

        GameHelper.GetGameController().m_activeBossUnits.Add(this);

        GetWorldTile().ClearSurroundingFog(1);
    }

    public override void Die(bool canRevive = true, DamageType damageType = DamageType.None)
    {
        base.Die(canRevive, damageType);

        if (m_isDead)
        {
            GameController gameController = GameHelper.GetGameController();
            gameController.m_activeBossUnits.Remove(this);

            if (GameHelper.GetBoss<ContentImmortalSpearEnemy>() != null || GameHelper.GetBoss<ContentImmortalBowEnemy>() != null)
            {
                //At least one other immortal is alive, the game is not over
                return;
            }

            //The other members of the Immortals are dead, player has won
            GameHelper.EndLevel(RunEndType.Win);
        }
    }
}