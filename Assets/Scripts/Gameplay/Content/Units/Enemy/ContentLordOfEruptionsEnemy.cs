﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLordOfEruptionsEnemy : GameEnemyUnit
{
    public int m_teleportRange = 3;
    
    public ContentLordOfEruptionsEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.BossStrength))
        {
            m_maxHealth = 900;
            m_maxStamina = 6;
            m_staminaRegen = 6;
            m_attack = 25;
        }
        else
        {
            m_maxHealth = 600;
            m_maxStamina = 6;
            m_staminaRegen = 6;
            m_attack = 12;
        }

        m_team = Team.Enemy;
        m_rarity = GameRarity.Special;
        m_isBoss = true;
        m_alwaysIgnoreDifficultTerrain = true;
        m_attackSFX = AudioHelper.LazerAttack;

        m_name = "Lord of Eruptions";
        m_desc = $"The final boss.  Kill it, and win.\nThis boss moves up to {m_teleportRange} tiles per turn, but uses no stamina to move. This unit can traverse any terrain type except for water.\nThis unit can use its full turn to ignite an adjacent volcano.";

        AddKeyword(new GameRangeKeyword(3), true, false);
        AddKeyword(new GameLavawalkKeyword(), true, false);

        m_AIGameEnemyUnit.AddAIStep(new AILordOfEruptionsScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AILordOfEruptionsChooseTargetToAttackStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AILordOfEruptionsTryIgniteVolcanoStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AILordOfEruptionsMoveToTargetStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override void OnSummon()
    {
        base.OnSummon();

        GameHelper.GetGameController().m_activeBossUnits.Add(this);

        GetWorldTile().ClearSurroundingFog(2);
        UIHelper.CreateHUDNotification("Boss Arrived", "The Lord of Eruptions has emerged from the lava to spread his domain!");
    }

    public override void EndTurn()
    {
        base.EndTurn();

        GetWorldTile().ClearSurroundingFog(2);
    }

    public override void Die(bool canRevive = true, DamageType damageType = DamageType.None)
    {
        base.Die(canRevive, damageType);

        GameHelper.EndLevel(RunEndType.Win);
    }
}