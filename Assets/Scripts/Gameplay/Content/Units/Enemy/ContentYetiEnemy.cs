using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Refuse to stay out of fog of war????
//If no fog of war near player targets that are closish to them, head to the mountains?
public class ContentYetiEnemy : GameEnemyUnit
{
    public ContentYetiEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.5f, 0);

        m_maxHealth = 25;
        m_maxStamina = 4;
        m_staminaRegen = 3;
        m_attack = 9;
        m_attackSFX = AudioHelper.SlamHeavy;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Yeti";
        m_desc = "";

        AddKeyword(new GameRangeKeyword(3), true, false);
        AddKeyword(new GameMountainwalkKeyword(), true, false);

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameMomentumKeyword(new GameGainStatsAction(this, 3, 0)), true, false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIYetiChooseTargetToAttackStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}