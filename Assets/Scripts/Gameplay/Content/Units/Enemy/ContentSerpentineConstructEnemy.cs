using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSerpentineConstructEnemy : GameEnemyUnit
{
    public ContentSerpentineConstructEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 38;
        m_maxStamina = 5;
        m_staminaRegen = 2;
        m_power = 18;
        m_attackSFX = AudioHelper.MetalClangAttack;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Serpentine Construct";
        
        m_desc = "By staying low to the ground, this unit cannot be targeted at range.\n";

        AddKeyword(new GameDamageReductionKeyword(4), true, false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameEnrageKeyword(new GameGainStaminaAction(this, 2)), true, false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}