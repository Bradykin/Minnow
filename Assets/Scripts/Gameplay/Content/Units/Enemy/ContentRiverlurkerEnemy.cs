using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRiverlurkerEnemy : GameEnemyUnit
{
    int m_range = 2;
    int m_magicPowerLoseAmount = 1;
    
    public ContentRiverlurkerEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 24;
        m_maxStamina = 5;
        m_staminaRegen = 4;
        m_power = 8;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Riverlurker";
        m_desc = "This unit is immune to all spells.\n";

        AddKeyword(new GameRangeKeyword(m_range), false);
        AddKeyword(new GameLavawalkKeyword(), false);

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameMomentumKeyword(new GameLoseTempMagicPowerAction(m_magicPowerLoseAmount)), false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}