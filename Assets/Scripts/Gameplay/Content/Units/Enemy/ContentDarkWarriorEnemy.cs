using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDarkWarriorEnemy : GameEnemyUnit
{
    public ContentDarkWarriorEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 10 + GetHealthModByWave();
        m_maxStamina = 6;
        m_staminaRegen = 3 + GetStaminaRegenModByWave();
        m_power = 4 + GetPowerModByWave();

        m_team = Team.Enemy;
        m_rarity = GameRarity.Special;
        m_isElite = true;

        m_name = "Dark Warrior";
        m_desc = "An elite foe.  Defeat it and gain a relic!\n";

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.BossStrength))
        {
            m_maxHealth = Mathf.FloorToInt(m_maxHealth * 2f);
            m_power = Mathf.FloorToInt(m_power * 1.5f);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        m_experienceAmount = 50;

        LateInit();
    }

    public override void Die(bool canRevive = true, DamageType damageType = DamageType.None)
    {
        GamePlayer player = GameHelper.GetPlayer();

        UIHelper.TriggerRelicSelect();

        if (GameHelper.HasRelic<ContentHeroicTrophyRelic>())
        {
            for (int i = 0; i < GameHelper.GetPlayer().m_controlledUnits.Count; i++)
            {
                player.m_controlledUnits[i].AddStats(5, 5);
            }
        }

        if (GameHelper.HasRelic<ContentAncientCoinsRelic>())
        {
            player.m_wallet.AddResources(new GameWallet(75));
        }

        base.Die(canRevive, damageType);
    }

    private int GetHealthModByWave()
    {
        int waveNum = GameHelper.GetCurrentWaveNum();

        int scalingValue = waveNum;
        if (waveNum >= 3)
        {
            scalingValue += (waveNum - 2);
        }

        return scalingValue * 10;
    }

    private int GetStaminaRegenModByWave()
    {
        int waveNum = GameHelper.GetCurrentWaveNum();

        return Mathf.FloorToInt((float)waveNum * 0.5f);
    }

    private int GetPowerModByWave()
    {
        int waveNum = GameHelper.GetCurrentWaveNum();

        return waveNum * 4;
    }
}