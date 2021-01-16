using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLurkerEnemy : GameEnemyUnit
{
    public ContentLurkerEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 8;
        m_maxStamina = 4;
        m_staminaRegen = 4;
        m_attack = 2;
        m_attackSFX = AudioHelper.RaptorAttack;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Lurker";
        m_desc = "";

        AddKeyword(new GameEnrageKeyword(new GameGainStatsAction(this, 2, 0)), true, false);
        AddKeyword(new GameDamageShieldKeyword(), true, false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameEnrageKeyword(new GameGainStatsAction(this, 3, 3)), true, false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}