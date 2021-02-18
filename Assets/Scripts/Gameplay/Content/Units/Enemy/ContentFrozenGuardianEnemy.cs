using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFrozenGuardianEnemy : GameEnemyUnit
{
    private int m_staminaLossAmount = 2;

    private int m_damageReductionAmount;
    private int m_statGain;

    public ContentFrozenGuardianEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 10 + GetHealthModByWave();
        m_maxStamina = 4 + GetStaminaRegenAndMaxStaminaModByWave();
        m_staminaRegen = 3 + GetStaminaRegenAndMaxStaminaModByWave();
        m_attack = 1 + GetAttackModByWave();
        m_attackSFX = AudioHelper.SlamHeavy;

        m_damageReductionAmount = 1 + GetDamageReductionModByWave();
        m_statGain = 1 + GetStatsGainModByWave();

        m_aoeRange = 3;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Special;
        m_isElite = true;

        m_name = "Frozen Guardian";
        m_desc = $"An elite foe.  Defeat it and gain a relic!\nAt the end of this unit's turn, nearby enemies in range {m_aoeRange} lose {m_staminaLossAmount} stamina. Gain +{m_statGain}/{m_statGain} for each enemy drained this way.";

        AddKeyword(new GameFrostwalkKeyword(), true, false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.BossStrength))
        {
            AddKeyword(new GameEnrageKeyword(new GameGainTempKeywordAction(this, new GameDamageReductionKeyword(m_damageReductionAmount))), true, false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        m_experienceAmount = 50;

        LateInit();
    }

    public override void EndTurn()
    {
        base.EndTurn();

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), m_aoeRange);

        int numEnemiesHit = 0;
        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            if (surroundingTiles[i].IsOccupied() && surroundingTiles[i].GetOccupyingUnit().GetTeam() != GetTeam())
            {
                if (surroundingTiles[i].GetOccupyingUnit().GetCurStamina() > 0)
                {
                    surroundingTiles[i].GetOccupyingUnit().SpendStamina(m_staminaLossAmount);
                    UIHelper.CreateWorldElementNotification("Stamina Drained by Frozen Guardian", false, surroundingTiles[i].GetWorldTile().gameObject);
                    numEnemiesHit++;
                }
            }
        }

        if (numEnemiesHit > 0)
        {
            AddStats(m_statGain * numEnemiesHit, m_statGain * numEnemiesHit, false, true);
        }
    }

    public override void Die(bool canRevive = true, DamageType damageType = DamageType.None)
    {
        GamePlayer player = GameHelper.GetPlayer();

        UIHelper.TriggerRelicSelect();

        if (GameHelper.HasRelic<ContentHeroicTrophyRelic>())
        {
            for (int i = 0; i < GameHelper.GetPlayer().m_controlledUnits.Count; i++)
            {
                player.m_controlledUnits[i].AddStats(5, 5, true, true);
                UIHelper.TriggerRelicAnimation<ContentHeroicTrophyRelic>();
            }
        }

        if (GameHelper.HasRelic<ContentAncientCoinsRelic>())
        {
            player.GainGold(75);
            UIHelper.TriggerRelicAnimation<ContentAncientCoinsRelic>();
        }

        GameNotificationManager.RecordEliteKill();

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

        return scalingValue * 6;
    }

    private int GetStaminaRegenAndMaxStaminaModByWave()
    {
        int waveNum = GameHelper.GetCurrentWaveNum();

        return waveNum / 5;
    }


    private int GetAttackModByWave()
    {
        int waveNum = GameHelper.GetCurrentWaveNum();

        return waveNum;
    }

    private int GetDamageReductionModByWave()
    {
        int waveNum = GameHelper.GetCurrentWaveNum();

        return waveNum / 3;
    }

    private int GetStatsGainModByWave()
    {
        int waveNum = GameHelper.GetCurrentWaveNum();

        return waveNum / 2;
    }
}