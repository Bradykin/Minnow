using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//For AI:
//Favours staying in or near water
//Does a move-attack-move
public class ContentLizardmanEnemy : GameEnemyUnit
{
    public ContentLizardmanEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 15;
        m_maxStamina = 6;
        m_staminaRegen = 4;
        m_power = 12;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Lizardman";
        m_desc = "Lizardmen move on water tiles without spending Stamina.";

        m_minWave = 5;
        m_maxWave = 6;

        AddKeyword(new GameWaterwalkKeyword(), false);
        m_instantWaterMovement = true;
        AddKeyword(new GameDamageShieldKeyword(2), false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameMomentumKeyword(new GameGainPowerAction(this, 2)), false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AILizardmanChooseTargetToAttackStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AILizardmanFleeToWaterStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}