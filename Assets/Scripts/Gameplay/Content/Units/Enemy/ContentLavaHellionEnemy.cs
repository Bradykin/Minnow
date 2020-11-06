using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLavaHellionEnemy : GameEnemyUnit
{
    public ContentLavaHellionEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 70;
        m_maxStamina = 5;
        m_staminaRegen = 5;
        m_power = 12;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_minWave = 5;
        m_maxWave = 6;

        m_name = "Lava Hellion";
        m_desc = "";

        AddKeyword(new GameLavawalkKeyword(), false);
        AddKeyword(new GameVictoriousKeyword(new GameDoublePowerAction(this, 1)), false);
        AddKeyword(new GameDamageReductionKeyword(3), false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameEnrageKeyword(new GameGainStatsAction(this, 1, 0)), false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}