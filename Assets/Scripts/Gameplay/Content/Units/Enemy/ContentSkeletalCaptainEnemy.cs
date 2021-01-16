using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSkeletalCaptainEnemy : GameEnemyUnit
{
    public ContentSkeletalCaptainEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 4 + GetHealthModByWave();
        m_maxStamina = 4;
        m_staminaRegen = 3;
        m_attack = 1 + GetAttackModByWave();
        m_attackSFX = AudioHelper.SwordHeavy;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Skeletal Captain";
        m_desc = "This unit is part of the pirate crew.\n";

        AddKeyword(new GameMomentumKeyword(new GameApplyKeywordToOtherOnMomentumAction(this, new GameBleedKeyword())), true, false);

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            //TODO Alex make a chaos ability
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
    private int GetHealthModByWave()
    {
        int waveNum = GameHelper.GetCurrentWaveNum();

        int scalingValue = waveNum;
        if (waveNum >= 3)
        {
            scalingValue += (waveNum - 2);
        }

        return scalingValue * 7;
    }

    private int GetAttackModByWave()
    {
        int waveNum = GameHelper.GetCurrentWaveNum();

        return waveNum * 4;
    }
}