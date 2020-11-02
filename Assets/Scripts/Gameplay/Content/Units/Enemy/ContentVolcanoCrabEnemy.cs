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
        m_rarity = GameRarity.Event;
        m_isElite = true;

        m_minWave = 1;
        m_maxWave = 6;

        m_name = "Volcano Crab";
        m_desc = $"An elite foe.  Defeat it and gain a relic!\n";
        //At the start of each turn, this unit gets +{m_powerIncrease} Power, +{m_staminaRegenIncrease} Stamina Regen, and -{m_damageReductionDecrease} Damage Reduction.";

        AddKeyword(new GameEnrageKeyword(new GameGainStatsAction(this, m_powerIncrease, 0)), false);
        AddKeyword(new GameEnrageKeyword(new GameGainStaminaRegenAction(this, m_staminaRegenIncrease)), false);
        AddKeyword(new GameEnrageKeyword(new GameSubtractKeywordAction(this, new GameDamageReductionKeyword(m_damageReductionDecrease))), false);
        AddKeyword(new GameDamageReductionKeyword(m_maxDamageReduction), false);
        AddKeyword(new GameLavawalkKeyword(), false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            m_desc += " When this unit steps on a lava tile, it regains all Damage Reduction.";
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

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

        GameRarity rarity = GameRelicFactory.GetRandomRarity();

        GameRelic relicOne = GameRelicFactory.GetRandomRelicAtRarity(rarity);
        GameRelic relicTwo = GameRelicFactory.GetRandomRelicAtRarity(rarity, relicOne);

        UIRelicSelectController.Instance.Init(relicOne, relicTwo);

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

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility) && GetGameTile().GetTerrain().IsLava())
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

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility) && GetGameTile().GetTerrain().IsLava())
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
        int waveNum = GameHelper.GetGameController().m_currentWaveNumber;

        int scalingValue = waveNum;
        if (waveNum >= 3)
        {
            scalingValue += (waveNum - 2);
        }

        return scalingValue * 4;
    }

    private int GetDamageReductionModByWave()
    {
        int waveNum = GameHelper.GetGameController().m_currentWaveNumber;

        return waveNum * 3;
    }

    private int GetPowerModByWave()
    {
        int waveNum = GameHelper.GetGameController().m_currentWaveNumber;

        return waveNum * 2;
    }

    private int GetPowerIncreaseModByWave()
    {
        int waveNum = GameHelper.GetGameController().m_currentWaveNumber;

        return waveNum;
    }
}