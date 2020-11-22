using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMummyPharaohEnemy : GameEnemyUnit
{
    private int m_spawnRange = 2;
    private int m_numMummiesSpawned = 3;
    private int m_mummyResurrectionRange = 2;

    public ContentMummyPharaohEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 5 + GetHealthModByWave();
        m_maxStamina = 2 + GetStaminaRegenModByWave();
        m_staminaRegen = 2 + GetStaminaRegenModByWave();
        m_power = 2 + GetPowerModByWave();

        m_team = Team.Enemy;
        m_rarity = GameRarity.Special;
        m_isElite = true;

        m_name = "Mummy Pharaoh";
        m_desc = $"An elite foe. Defeat it and gain a relic!\n";

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.BossStrength))
        {
            m_desc += $"Any non-Mummy units that die with range {m_mummyResurrectionRange} get resurrected as Mummies.";
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        m_experienceAmount = 50;

        LateInit();
    }

    public override void OnSummon()
    {
        base.OnSummon();

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), m_spawnRange);

        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameTile temp = surroundingTiles[i];
            int randomIndex = UnityEngine.Random.Range(i, surroundingTiles.Count);
            surroundingTiles[i] = surroundingTiles[randomIndex];
            surroundingTiles[randomIndex] = temp;
        }

        GameOpponent gameOpponent = GameHelper.GetOpponent();

        int numMummiesSpawned = 0;
        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            if (surroundingTiles[i].IsPassable(this, false))
            {
                GameEnemyUnit newEnemyUnit = GameUnitFactory.GetEnemyUnitClone(new ContentMummyEnemy(gameOpponent), WorldController.Instance.m_gameController.m_gameOpponent);
                surroundingTiles[i].PlaceUnit(newEnemyUnit);
                newEnemyUnit.OnSummon();
                gameOpponent.AddControlledUnit(newEnemyUnit);
                numMummiesSpawned++;
                if (numMummiesSpawned >= m_numMummiesSpawned)
                {
                    break;
                }
            }
        }
    }

    public override void OnOtherDie(GameUnit other, GameTile deathLocation)
    {
        base.OnOtherDie(other, deathLocation);

        if (other is ContentMummyEnemy)
        {
            return;
        }

        if (WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(GetGameTile(), deathLocation) > m_mummyResurrectionRange)
        {
            return;
        }

        GameOpponent gameOpponent = GameHelper.GetOpponent();

        GameEnemyUnit newEnemyUnit = GameUnitFactory.GetEnemyUnitClone(new ContentMummyEnemy(gameOpponent), WorldController.Instance.m_gameController.m_gameOpponent);
        deathLocation.PlaceUnit(newEnemyUnit);
        newEnemyUnit.OnSummon();
        gameOpponent.AddControlledUnit(newEnemyUnit);
        UIHelper.CreateWorldElementNotification("Resurrected by the Pharaoh", false, deathLocation.GetWorldTile().gameObject);
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

        return scalingValue * 9;
    }

    private int GetStaminaRegenModByWave()
    {
        int waveNum = GameHelper.GetCurrentWaveNum();

        return Mathf.FloorToInt((float)waveNum * 0.5f);
    }

    private int GetPowerModByWave()
    {
        int waveNum = GameHelper.GetCurrentWaveNum();

        return waveNum * 3;
    }
}