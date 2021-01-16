using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPolarWarriorEnemy : GameEnemyUnit
{
    int m_attackIncrease = 2;
    int m_healthIncrease = 2;
    int m_regenIncrease = 2;
    int m_enrageSelfDamageAmount = 1;
    
    public ContentPolarWarriorEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.1f, 0);

        m_maxHealth = 12;
        m_maxStamina = 5;
        m_staminaRegen = 5;
        m_attack = 3;
        m_attackSFX = AudioHelper.SwordHeavy;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Polar Warrior";
        m_desc = "";

        AddKeyword(new GameEnrageKeyword(new GameGainStatsAction(this, m_attackIncrease, m_healthIncrease)), true, false);
        AddKeyword(new GameEnrageKeyword(new GameGainTempKeywordAction(this, new GameRegenerateKeyword(m_regenIncrease))), true, false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameMomentumKeyword(new GameGetHitAction(this, m_enrageSelfDamageAmount)), true, false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}