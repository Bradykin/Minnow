using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDarkWarriorEnemy : GameEnemyEntity
{
    public ContentDarkWarriorEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 10 + GetHealthModByWave();
        m_maxStamina = 8;
        m_staminaRegen = 4 + GetStaminaRegenModByWave();
        m_power = 4 + GetPowerModByWave();

        m_team = Team.Enemy;
        m_rarity = GameRarity.Event;
        m_isElite = true;

        m_minWave = 1;
        m_maxWave = 6;

        m_name = "Dark Warrior";
        m_desc = "An elite foe.  Defeat it and gain a relic!";

        m_AIGameEnemyEntity.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyEntity));

        LateInit();
    }

    public override void Die()
    {
        GamePlayer player = GameHelper.GetPlayer();

        GameRarity rarity = GameRelicFactory.GetRandomRarity();

        GameRelic relicOne = GameRelicFactory.GetRandomRelicAtRarity(rarity);
        GameRelic relicTwo = GameRelicFactory.GetRandomRelicAtRarity(rarity, relicOne);

        UIRelicSelectController.Instance.Init(relicOne, relicTwo);

        base.Die();
    }

    private int GetHealthModByWave()
    {
        int waveNum = GameHelper.GetGameController().m_waveNum;

        int scalingValue = waveNum;
        if (waveNum >= 3)
        {
            scalingValue += (waveNum - 2);
        }

        return scalingValue * 10;
    }

    private int GetStaminaRegenModByWave()
    {
        int waveNum = GameHelper.GetGameController().m_waveNum;

        return Mathf.FloorToInt((float)waveNum * 0.5f);
    }

    private int GetPowerModByWave()
    {
        int waveNum = GameHelper.GetGameController().m_waveNum;

        return waveNum * 3;
    }
}