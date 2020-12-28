using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Target buildings over units
public class ContentAngryBirdEnemy : GameEnemyUnit
{
    public ContentAngryBirdEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 5;
        m_maxStamina = 5;
        m_staminaRegen = 4;
        m_power = 5;
        m_attackSFX = AudioHelper.BirdFlap;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Angry Bird";
        m_desc = "";

        AddKeyword(new GameFlyingKeyword(), true, false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameDamageShieldKeyword(), true, false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}