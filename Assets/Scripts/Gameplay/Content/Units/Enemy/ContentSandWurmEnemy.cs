using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSandWurmEnemy : GameEnemyUnit
{
    public ContentSandWurmEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 36;
        m_maxStamina = 4;
        m_staminaRegen = 3;
        m_attack = 13;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Sand Wurm";
        
        m_desc = "This unit can move on dunes tiles without spending Stamina.\n";

        AddKeyword(new GameDuneswalkKeyword(), true, false);
        m_instantDunesMovement = true;
        AddKeyword(new GameVictoriousKeyword(new GameFullHealAction(this)), true, false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameMomentumKeyword(new GameGainStatsAction(this, 2, 2)), true, false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}