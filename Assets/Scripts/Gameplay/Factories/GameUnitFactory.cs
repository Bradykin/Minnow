using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameUnitFactory
{
    private static List<GameUnit> m_playerUnits = new List<GameUnit>();

    private static List<GameEnemyUnit> m_enemies = new List<GameEnemyUnit>();
    private static List<List<GameEnemyUnit>> m_specificSpawnPoolEnemies = new List<List<GameEnemyUnit>>();
    private static List<GameEnemyUnit> m_standardEnemies = new List<GameEnemyUnit>();
    private static List<GameEnemyUnit> m_eliteEnemies = new List<GameEnemyUnit>();
    private static List<GameEnemyUnit> m_bossEnemies = new List<GameEnemyUnit>();

    private static List<GameEnemyUnit> m_standardWaveOneEnemies = new List<GameEnemyUnit>();
    private static List<GameEnemyUnit> m_standardWaveTwoEnemies = new List<GameEnemyUnit>();
    private static List<GameEnemyUnit> m_standardWaveThreeEnemies = new List<GameEnemyUnit>();
    private static List<GameEnemyUnit> m_standardWaveFourEnemies = new List<GameEnemyUnit>();
    private static List<GameEnemyUnit> m_standardWaveFiveEnemies = new List<GameEnemyUnit>();
    private static List<GameEnemyUnit> m_standardWaveSixEnemies = new List<GameEnemyUnit>();
    private static List<GameEnemyUnit> m_standardWaveSevenEnemies = new List<GameEnemyUnit>();

    public static void Init(List<GameEnemyUnit> spawnPool, List<List<GameEnemyUnit>> specificSpawnPoolEnemies)
    {
        //Player Units
        m_playerUnits.Add(new ContentConjuredImp());
        m_playerUnits.Add(new ContentCyclops());
        m_playerUnits.Add(new ContentDemonSoldier());
        m_playerUnits.Add(new ContentDevourer());
        m_playerUnits.Add(new ContentDwarfArchitect());
        m_playerUnits.Add(new ContentDwarfShivcaster());
        m_playerUnits.Add(new ContentDwarvenSoldier());
        m_playerUnits.Add(new ContentElvenRogue());
        m_playerUnits.Add(new ContentElvenSentinel());
        m_playerUnits.Add(new ContentElvenWizard());
        m_playerUnits.Add(new ContentFishOracle());
        m_playerUnits.Add(new ContentGladiator());
        m_playerUnits.Add(new ContentGoblin());
        m_playerUnits.Add(new ContentGrasper());
        m_playerUnits.Add(new ContentGroundskeeper());
        m_playerUnits.Add(new ContentGuardCaptain());
        m_playerUnits.Add(new ContentHero());
        m_playerUnits.Add(new ContentHomonculus());
        m_playerUnits.Add(new ContentInjuredTroll());
        m_playerUnits.Add(new ContentMage());
        m_playerUnits.Add(new ContentMetalGolem());
        m_playerUnits.Add(new ContentMiner());
        m_playerUnits.Add(new ContentNaturalScout());
        m_playerUnits.Add(new ContentOverlord());
        m_playerUnits.Add(new ContentRanger());
        m_playerUnits.Add(new ContentRaptor());
        m_playerUnits.Add(new ContentRoyalCaravan());
        m_playerUnits.Add(new ContentSabobot());
        m_playerUnits.Add(new ContentShadowWarlock());
        m_playerUnits.Add(new ContentSkeleton());
        m_playerUnits.Add(new ContentStoneGolem());
        m_playerUnits.Add(new ContentWanderer());
        m_playerUnits.Add(new ContentWildfolk());

        m_playerUnits.Add(new ContentLizardSoldier());
        m_playerUnits.Add(new ContentUndeadMammoth());
        m_playerUnits.Add(new ContentWingedSerpent());
        m_playerUnits.Add(new ContentMechanizedBeast());

        //Enemy Units
        m_enemies = spawnPool;
        m_specificSpawnPoolEnemies = specificSpawnPoolEnemies;

        for (int i = 0; i < m_enemies.Count; i++)
        {
            if (m_enemies[i].m_isBoss)
            {
                m_bossEnemies.Add(m_enemies[i]);
            }
            else if (m_enemies[i].m_isElite)
            {
                m_eliteEnemies.Add(m_enemies[i]);
            }
            else
            {
                m_standardEnemies.Add(m_enemies[i]);

                if (m_enemies[i].m_minWave <= 1 && m_enemies[i].m_maxWave >= 1)
                {
                    m_standardWaveOneEnemies.Add(m_enemies[i]);
                }
                if (m_enemies[i].m_minWave <= 2 && m_enemies[i].m_maxWave >= 2)
                {
                    m_standardWaveTwoEnemies.Add(m_enemies[i]);
                }
                if (m_enemies[i].m_minWave <= 3 && m_enemies[i].m_maxWave >= 3)
                {
                    m_standardWaveThreeEnemies.Add(m_enemies[i]);
                }
                if (m_enemies[i].m_minWave <= 4 && m_enemies[i].m_maxWave >= 4)
                {
                    m_standardWaveFourEnemies.Add(m_enemies[i]);
                }
                if (m_enemies[i].m_minWave <= 5 && m_enemies[i].m_maxWave >= 5)
                {
                    m_standardWaveFiveEnemies.Add(m_enemies[i]);
                }
                if (m_enemies[i].m_minWave <= 6 && m_enemies[i].m_maxWave >= 6)
                {
                    m_standardWaveSixEnemies.Add(m_enemies[i]);
                }
            }
        }
    }

    public static GameEnemyUnit GetRandomEnemy(GameOpponent gameOpponent, int curWave)
    {
        List<GameEnemyUnit> list = m_standardEnemies;
        if (curWave == 1)
        {
            list = m_standardWaveOneEnemies;
        }
        else if (curWave == 2)
        {
            list = m_standardWaveTwoEnemies;
        }
        else if (curWave == 3)
        {
            list = m_standardWaveThreeEnemies;
        }
        else if (curWave == 4)
        {
            list = m_standardWaveFourEnemies;
        }
        else if (curWave == 5)
        {
            list = m_standardWaveFiveEnemies;
        }
        else if (curWave == 6)
        {
            list = m_standardWaveSixEnemies;
        }
        else if (curWave == 7)
        {
            list = m_standardWaveSevenEnemies;
        }
        else
        {
            Debug.LogWarning("Spawning an enemy from an invalid wave: " + curWave);
        }

        int r = UnityEngine.Random.Range(0, list.Count);

        return (GameEnemyUnit)Activator.CreateInstance(list[r].GetType(), gameOpponent);
    }

    public static GameEnemyUnit GetRandomEnemyFromSpawnPoint(GameOpponent gameOpponent, int curWave, GameSpawnPoint m_spawnPoint)
    {
        if (m_spawnPoint.m_spawnPointMarkers.Count == 0 || (m_spawnPoint.m_spawnPointMarkers.Count == 1 && m_spawnPoint.m_spawnPointMarkers[0] == 0))
        {
            return GetRandomEnemy(gameOpponent, curWave);
        }

        List<GameEnemyUnit> list = new List<GameEnemyUnit>();
        for (int i = 0; i < m_spawnPoint.m_spawnPointMarkers.Count; i++)
        {
            if (m_spawnPoint.m_spawnPointMarkers[i] == 0)
            {
                continue;
            }

            int spawnPoolIndex = m_spawnPoint.m_spawnPointMarkers[i] - 1;

            if (spawnPoolIndex < 0 || m_specificSpawnPoolEnemies.Count <= spawnPoolIndex)
            {
                Debug.LogError("GameUnitFactory received Spawn Point Marker " + spawnPoolIndex + " That does not exist");
            }

            List<GameEnemyUnit> specificSpawnPool = m_specificSpawnPoolEnemies[spawnPoolIndex];

            for (int k = 0; k < specificSpawnPool.Count; k++)
            {
                if (!list.Any(u => u.GetType() == specificSpawnPool[k].GetType()))
                {
                    list.Add(specificSpawnPool[k]);
                }
            }
        }

        List<GameEnemyUnit> waveList = m_standardEnemies;
        if (curWave == 1)
        {
            waveList = m_standardWaveOneEnemies;
        }
        else if (curWave == 2)
        {
            waveList = m_standardWaveTwoEnemies;
        }
        else if (curWave == 3)
        {
            waveList = m_standardWaveThreeEnemies;
        }
        else if (curWave == 4)
        {
            waveList = m_standardWaveFourEnemies;
        }
        else if (curWave == 5)
        {
            waveList = m_standardWaveFiveEnemies;
        }
        else if (curWave == 6)
        {
            waveList = m_standardWaveSixEnemies;
        }
        else if (curWave == 7)
        {
            waveList = m_standardWaveSevenEnemies;
        }
        else
        {
            Debug.LogWarning("Spawning an enemy from an invalid wave: " + curWave);
        }

        list = list.Where(u => waveList.Any(l => l.GetType() == u.GetType() && m_spawnPoint.m_tile.IsPassable(l, false))).ToList();

        if (list.Count == 0)
        {
            return null;
        }

        int r = UnityEngine.Random.Range(0, list.Count);

        return (GameEnemyUnit)Activator.CreateInstance(list[r].GetType(), gameOpponent);
    }

    public static GameUnit GetEnemyUnitClone(GameUnit unit)
    {
        return (GameUnit)Activator.CreateInstance(unit.GetType());
    }

    public static GameEnemyUnit GetEnemyUnitClone(GameEnemyUnit enemyUnit, GameOpponent gameOpponent)
    {
        return (GameEnemyUnit)Activator.CreateInstance(enemyUnit.GetType(), gameOpponent);
    }

    public static GameEnemyUnit GetRandomEliteEnemy(GameOpponent gameOpponent)
    {
        int r = UnityEngine.Random.Range(0, m_eliteEnemies.Count);

        return (GameEnemyUnit)Activator.CreateInstance(m_eliteEnemies[r].GetType(), gameOpponent);
    }

    public static GameEnemyUnit GetRandomBossEnemy(GameOpponent gameOpponent)
    {
        int r = UnityEngine.Random.Range(0, m_bossEnemies.Count);

        return (GameEnemyUnit)Activator.CreateInstance(m_bossEnemies[r].GetType(), gameOpponent);
    }

    public static GameUnit GetUnitFromJson(JsonGameUnitData jsonData)
    {
        int i = m_playerUnits.FindIndex(t => t.GetName() == jsonData.name);

        GameUnit newPlayerUnit = (GameUnit)Activator.CreateInstance(m_playerUnits[i].GetType());
        newPlayerUnit.LoadFromJson(jsonData);

        return newPlayerUnit;
    }

    public static GameEnemyUnit GetEnemyFromJson(JsonGameUnitData jsonData, GameOpponent gameOpponent)
    {
        int i = m_enemies.FindIndex(t => t.GetName() == jsonData.name);

        GameEnemyUnit newEnemy = (GameEnemyUnit)Activator.CreateInstance(m_enemies[i].GetType(), gameOpponent);
        newEnemy.LoadFromJson(jsonData);

        return newEnemy;
    }
}