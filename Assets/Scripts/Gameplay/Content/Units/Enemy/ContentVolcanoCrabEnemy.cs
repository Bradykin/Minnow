using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentVolcanoCrabEnemy : GameEnemyUnit
{
    int m_powerIncrease;
    int m_staminaRegenIncrease = 1;
    int m_damageReductionDecrease = 1;

    int m_maxDamageReduction;
    
    public ContentVolcanoCrabEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 8 + GetHealthModByWave();
        m_maxStamina = 6;
        m_staminaRegen = 3;
        m_power = 2 + GetPowerModByWave();

        m_powerIncrease = 2 + GetPowerIncreaseModByWave();
        m_maxDamageReduction = 3 + GetDamageReductionModByWave();

        m_team = Team.Enemy;
        m_rarity = GameRarity.Special;
        m_isElite = true;

        m_name = "Volcano Crab";
        m_desc = $"An elite foe.  Defeat it and gain a relic!\n";

        AddKeyword(new GameEnrageKeyword(new GameGainStatsAction(this, m_powerIncrease, 0)), false);
        AddKeyword(new GameEnrageKeyword(new GameGainStaminaRegenAction(this, m_staminaRegenIncrease)), false);
        AddKeyword(new GameEnrageKeyword(new GameLoseKeywordAction(this, new GameDamageReductionKeyword(m_damageReductionDecrease))), false);
        AddKeyword(new GameDamageReductionKeyword(m_maxDamageReduction), false);
        AddKeyword(new GameLavawalkKeyword(), false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.BossStrength))
        {
            m_desc += " When this unit steps on a lava tile, it regains all Damage Reduction.\n";
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        m_experienceAmount = 50;

        LateInit();
    }

    public override void OnSummon()
    {
        base.OnSummon();

        SpendStamina(GetCurStamina() - 1);
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

    public override void OnMoveEnd()
    {
        base.OnMoveEnd();

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.BossStrength) && GetGameTile().GetTerrain().IsLava())
        {
            GameDamageReductionKeyword gameDamageReductionKeyword = GetDamageReductionKeyword();

            if (gameDamageReductionKeyword == null)
            {
                AddKeyword(new GameDamageReductionKeyword(m_maxDamageReduction));
            }
            else
            {
                AddKeyword(new GameDamageReductionKeyword(m_maxDamageReduction - gameDamageReductionKeyword.m_damageReduction));
            }
        }
    }

    public override void EndTurn()
    {
        base.EndTurn();

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.BossStrength) && GetGameTile().GetTerrain().IsLava())
        {
            GameDamageReductionKeyword gameDamageReductionKeyword = GetDamageReductionKeyword();

            if (gameDamageReductionKeyword == null)
            {
                AddKeyword(new GameDamageReductionKeyword(m_maxDamageReduction));
            }
            else
            {
                AddKeyword(new GameDamageReductionKeyword(m_maxDamageReduction - gameDamageReductionKeyword.m_damageReduction));
            }
        }
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

    private int GetDamageReductionModByWave()
    {
        int waveNum = GameHelper.GetCurrentWaveNum();

        return waveNum * 3;
    }

    private int GetPowerModByWave()
    {
        int waveNum = GameHelper.GetCurrentWaveNum();

        return waveNum * 2;
    }

    private int GetPowerIncreaseModByWave()
    {
        int waveNum = GameHelper.GetCurrentWaveNum();

        return waveNum;
    }
}