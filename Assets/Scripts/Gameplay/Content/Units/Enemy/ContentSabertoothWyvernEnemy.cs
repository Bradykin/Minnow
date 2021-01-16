using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSabertoothWyvernEnemy : GameEnemyUnit
{
    private int m_attackToGain = 3;
    
    public ContentSabertoothWyvernEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 45;
        m_maxStamina = 5;
        m_staminaRegen = 4;
        m_attack = 16;
        m_attackSFX = AudioHelper.BirdFlap;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Rare;

        m_name = "Sabertooth Wyvern";
        m_desc = "";

        AddKeyword(new GameFlyingKeyword(), true, false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameMomentumKeyword(new GameGainStatsAction(this, m_attackToGain, 0)), true, false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}