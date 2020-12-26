using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMummyEnemy : GameEnemyUnit
{
    public ContentMummyEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 1 + GetHealthModByWave();
        m_maxStamina = 3 + GetStaminaRegenModByWave();
        m_staminaRegen = 3 + GetStaminaRegenModByWave();
        m_power = 0 + GetPowerModByWave();

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Mummy";
        m_desc = "Minion of the Pharaoh.\nThis unit does not increment the kill counter.\n";

        m_incrementsKillCounter = false;

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

        return scalingValue * 4;
    }

    private int GetStaminaRegenModByWave()
    {
        int waveNum = GameHelper.GetCurrentWaveNum();

        return Mathf.FloorToInt((float)waveNum / 3);
    }

    private int GetPowerModByWave()
    {
        int waveNum = GameHelper.GetCurrentWaveNum();

        return waveNum * 3;
    }
}