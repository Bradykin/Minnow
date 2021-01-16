using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGreatFrostlizardEnemy : GameEnemyUnit
{
    public ContentGreatFrostlizardEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 60;
        m_maxStamina = 5;
        m_staminaRegen = 5;
        m_attack = 14;
        m_attackSFX = AudioHelper.Roar;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Great Frostlizard";
        m_desc = "";

        AddKeyword(new GameCleaveKeyword(), true, false);
        AddKeyword(new GameDamageReductionKeyword(3), true, false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameMomentumKeyword(new GameGainDamageShieldAction(this)), true, false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}