using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPolarWarriorEnemy : GameEnemyUnit
{
    int m_powerIncrease = 2;
    int m_healthIncrease = 2;
    int m_regenIncrease = 2;
    int m_enrageSelfDamageAmount = 1;
    
    public ContentPolarWarriorEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.1f, 0);

        m_maxHealth = 12;
        m_maxStamina = 5;
        m_staminaRegen = 5;
        m_power = 2;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Polar Warrior";
        m_desc = "";

        AddKeyword(new GameEnrageKeyword(new GameGainStatsAction(this, m_powerIncrease, m_healthIncrease)), false);
        AddKeyword(new GameEnrageKeyword(new GameGainKeywordAction(this, new GameRegenerateKeyword(m_regenIncrease))), false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameMomentumKeyword(new GameGetHitAction(this, m_enrageSelfDamageAmount)), false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}