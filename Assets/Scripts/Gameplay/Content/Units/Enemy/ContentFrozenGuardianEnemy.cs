using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFrozenGuardianEnemy : GameEnemyUnit
{
    private int m_staminaLossRange = 3;
    private int m_staminaLossAmount = 2;

    private int m_damageReductionAmount;
    private int m_statGain;

    public ContentFrozenGuardianEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 10 + GetHealthModByWave();
        m_maxStamina = 4 + GetStaminaRegenAndMaxStaminaModByWave();
        m_staminaRegen = 3 + GetStaminaRegenAndMaxStaminaModByWave();
        m_power = 1 + GetPowerModByWave();

        m_damageReductionAmount = 1 + GetDamageReductionModByWave();
        m_statGain = 1 + GetStatsGainModByWave();

        m_team = Team.Enemy;
        m_rarity = GameRarity.Special;
        m_isElite = true;

        m_name = "Frozen Guardian";
        m_desc = $"An elite foe.  Defeat it and gain a relic!\nAt the end of this unit's turn, nearby enemies in range {m_staminaLossRange} lose {m_staminaLossAmount} stamina. Gain +{m_statGain}/{m_statGain} for each enemy drained this way.";

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.BossStrength))
        {
            AddKeyword(new GameEnrageKeyword(new GameGainKeywordAction(this, new GameDamageReductionKeyword(m_damageReductionAmount))), false);
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

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), m_staminaLossRange);

        int numEnemiesHit = 0;
        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            if (surroundingTiles[i].IsOccupied() && surroundingTiles[i].m_occupyingUnit.GetTeam() != GetTeam())
            {
                if (surroundingTiles[i].m_occupyingUnit.GetCurStamina() > 0)
                {
                    surroundingTiles[i].m_occupyingUnit.SpendStamina(m_staminaLossAmount);
                    UIHelper.CreateWorldElementNotification("Stamina Drained by Frozen Guardian", false, surroundingTiles[i].GetWorldTile().gameObject);
                    numEnemiesHit++;
                }
            }
        }

        if (numEnemiesHit > 0)
        {
            AddStats(m_statGain * numEnemiesHit, m_statGain * numEnemiesHit);
        }
    }

    public override void Die(bool canRevive = true)
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

        base.Die(canRevive);
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


    private int GetPowerModByWave()
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