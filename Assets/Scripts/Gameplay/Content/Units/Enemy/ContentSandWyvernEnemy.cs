using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSandWyvernEnemy : GameEnemyUnit
{
    public ContentSandWyvernEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 80;
        m_maxStamina = 7;
        m_staminaRegen = 4;
        m_power = 25;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Uncommon;

        m_name = "Sand Wyvern";
        m_desc = "";

        AddKeyword(new GameFlyingKeyword(), false);
        AddKeyword(new GameVictoriousKeyword(new GameGainStaminaAction(this, m_maxStamina)), false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameDamageShieldKeyword(4), false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}