using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Priority order: adjacent units, castle, buildings, other units
public class ContentMobolaEnemy : GameEnemyUnit
{
    public ContentMobolaEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 85;
        m_maxStamina = 8;
        m_staminaRegen = 2;
        m_power = 6;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Uncommon;

        m_name = "Mobola";
        m_desc = "";

        AddKeyword(new GameEnrageKeyword(new GameGainStatsAction(this, 5, 0)), true, false);
        AddKeyword(new GameMomentumKeyword(new GameGainStatsAction(this, 5, 0)), true, false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameRegenerateKeyword(30), true, false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}