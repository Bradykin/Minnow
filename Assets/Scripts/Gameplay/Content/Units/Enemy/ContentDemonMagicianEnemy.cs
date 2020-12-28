using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDemonMagicianEnemy : GameEnemyUnit
{
    int m_magicPowerLoseAmount = 1;
    
    public ContentDemonMagicianEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 24;
        m_maxStamina = 5;
        m_staminaRegen = 4;
        m_power = 8;
        m_attackSFX = AudioHelper.SpellAttackMedium;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Demon Magician";
        m_desc = "This unit cannot be targeted by spells.\n";

        AddKeyword(new GameRangeKeyword(2), true, false);
        AddKeyword(new GameLavawalkKeyword(), true, false);

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameMomentumKeyword(new GameLoseTempMagicPowerAction(m_magicPowerLoseAmount)), true, false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}