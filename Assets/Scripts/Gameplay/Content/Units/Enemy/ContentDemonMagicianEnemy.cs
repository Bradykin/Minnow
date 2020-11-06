using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDemonMagicianEnemy : GameEnemyUnit
{
    public ContentDemonMagicianEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 24;
        m_maxStamina = 5;
        m_staminaRegen = 4;
        m_power = 8;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_minWave = 4;
        m_maxWave = 4;

        m_name = "Demon Magician";
        m_desc = "This unit is immune to all spells.";

        AddKeyword(new GameRangeKeyword(2), false);
        AddKeyword(new GameLavawalkKeyword(), false);

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            //Momentum: -1 spellpower until end of round
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}