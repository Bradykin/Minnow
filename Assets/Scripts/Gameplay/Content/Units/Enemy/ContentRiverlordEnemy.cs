﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRiverlordEnemy : GameEnemyUnit
{
    public bool m_hasReanimated = false;

    public ContentRiverlordEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.BossStrength))
        {
            m_maxHealth = 600;
            m_maxStamina = 7;
            m_staminaRegen = 7;
            m_attack = 30;
        }
        else
        {
            m_maxHealth = 300;
            m_maxStamina = 5;
            m_staminaRegen = 5;
            m_attack = 15;
        }

        m_team = Team.Enemy;
        m_rarity = GameRarity.Special;
        m_isBoss = true;
        m_aoeRange = 3;
        m_attackSFX = AudioHelper.SlamHeavy;

        m_name = "Riverlord";
        m_desc = $"The final boss. Kill it, and win.\n";

        AddKeyword(new GameRangeKeyword(2), true, false);
        AddKeyword(new GameFlyingKeyword(), true, false);

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

        GetWorldTile().ClearSurroundingFog(2);

        if (!m_hasReanimated)
        {
            UIHelper.CreateHUDNotification("Boss Arrived", "The Lich has arrived and brought death to the world!");
        }
    }

    public override void Die(bool canRevive = true, DamageType damageType = DamageType.None)
    {
        base.Die(canRevive, damageType);

        GameHelper.GetGameController().m_activeBossUnits.Remove(this);

        GameHelper.EndLevel(RunEndType.Win);
    }
}