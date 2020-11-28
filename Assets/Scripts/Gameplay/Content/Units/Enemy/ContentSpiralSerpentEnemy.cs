using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSpiralSerpentEnemy : GameEnemyUnit
{
    public ContentSpiralSerpentEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(-0.1f, -0.2f, 0);

        m_maxHealth = 25;
        m_maxStamina = 4;
        m_staminaRegen = 4;
        m_power = 14;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Spiral Serpent";
        
        m_desc = "";

        AddKeyword(new GameWaterboundKeyword(), true, false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameRangeKeyword(2), true, false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}