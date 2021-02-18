using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMarauderEnemy : GameEnemyUnit
{
    public ContentMarauderEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 35;
        m_maxStamina = 5;
        m_staminaRegen = 4;
        m_attack = 15;
        m_attackSFX = AudioHelper.MetalClangAttack;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Marauder";
        
        m_desc = "When this unit gets attacked, if it survives, it attacks back instantly.\n";

        AddKeyword(new GameCleaveKeyword(), true, false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            m_desc = "When this unit gets attacked, if it survives, it attacks back instantly without spending stamina.\n";
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override int GetHitByUnit(int damage, GameUnit gameUnit, bool canReturnThorns)
    {
        int damageTaken = base.GetHitByUnit(damage, gameUnit, canReturnThorns);

        if (!m_isDead && canReturnThorns)
        {
            if ((HasStaminaToAttack(gameUnit) && IsInRangeOfUnit(gameUnit)) || GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
            {
                HitUnit(gameUnit, GetDamageToDealTo(gameUnit), spendStamina: !GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility));
            }
        }

        return damageTaken;
    }
}