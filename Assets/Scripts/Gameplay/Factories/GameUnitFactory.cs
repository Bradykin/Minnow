using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameUnitFactory
{
    private static List<GameUnit> m_playerUnits = new List<GameUnit>();

    private static List<GameEnemyUnit> m_enemies = new List<GameEnemyUnit>();
    private static GameSpawnPool m_defaultSpawnPool;
    private static List<GameSpawnPool> m_spawnPointSpawnPools;

    private static List<GameEnemyUnit> m_standardEnemies = new List<GameEnemyUnit>();
    private static List<GameEnemyUnit> m_eliteEnemies = new List<GameEnemyUnit>();
    private static List<GameEnemyUnit> m_bossEnemies = new List<GameEnemyUnit>();

    private static bool m_hasInit = false;

    public static void Init()
    {
        m_playerUnits.Add(new ContentOverlord());
        m_playerUnits.Add(new ContentShadowWarlock());
        m_playerUnits.Add(new ContentGladiator());
        m_playerUnits.Add(new ContentGrasper());
        m_playerUnits.Add(new ContentElvenWizard());
        m_playerUnits.Add(new ContentHomonculus());
        m_playerUnits.Add(new ContentCyclops());
        m_playerUnits.Add(new ContentInjuredTroll());
        m_playerUnits.Add(new ContentHero());
        m_playerUnits.Add(new ContentDevourer());
        m_playerUnits.Add(new ContentDwarfArchitect());
        m_playerUnits.Add(new ContentElvenRogue());
        m_playerUnits.Add(new ContentElvenSentinel());
        m_playerUnits.Add(new ContentRaptor());
        m_playerUnits.Add(new ContentGuardCaptain());
        m_playerUnits.Add(new ContentNaturalScout());
        m_playerUnits.Add(new ContentWanderer());
        m_playerUnits.Add(new ContentSabobot());
        m_playerUnits.Add(new ContentSkeleton());
        m_playerUnits.Add(new ContentMage());
        m_playerUnits.Add(new ContentGroundskeeper());
        m_playerUnits.Add(new ContentRanger());
        m_playerUnits.Add(new ContentDwarfShivcaster());
        m_playerUnits.Add(new ContentMiner());
        m_playerUnits.Add(new ContentBonecaster());
        m_playerUnits.Add(new ContentGoblinLegend());
        m_playerUnits.Add(new ContentWildfolk());
        m_playerUnits.Add(new ContentMetalGolem());
        m_playerUnits.Add(new ContentStoneGolem());
        m_playerUnits.Add(new ContentConjuredImp());
        m_playerUnits.Add(new ContentArmouredMonk());
        m_playerUnits.Add(new ContentMetalProtector());
        m_playerUnits.Add(new ContentPirateCaptain());
        m_playerUnits.Add(new ContentPolarHunter());
        m_playerUnits.Add(new ContentMountainBeast());
        m_playerUnits.Add(new ContentPyromage());
        m_playerUnits.Add(new ContentRhinoProtector());
        m_playerUnits.Add(new ContentDesertSwordsman());
        m_playerUnits.Add(new ContentFrogShaman());

        //New Units
        //Implemented
        m_playerUnits.Add(new ContentEnergyConstruct());
        m_playerUnits.Add(new ContentStormChanneler());
        m_playerUnits.Add(new ContentWarriorPriestess());

        //WIP
        m_playerUnits.Add(new ContentWildwoodExplorer());
        m_playerUnits.Add(new ContentDwarfforgedConstruct());
        m_playerUnits.Add(new ContentGolemProtector());
        m_playerUnits.Add(new ContentEtherealStag());
        m_playerUnits.Add(new ContentGuardianOfTheForest());
        m_playerUnits.Add(new ContentSpelldancer());
        m_playerUnits.Add(new ContentStagBear());
        m_playerUnits.Add(new ContentWildwoodSkirmisher());

        //Starter Units
        m_playerUnits.Add(new ContentLizardSoldier());
        m_playerUnits.Add(new ContentUndeadMammoth());
        m_playerUnits.Add(new ContentSandwalker());
        m_playerUnits.Add(new ContentMechanizedBeast());
        m_playerUnits.Add(new ContentAlphaBoar());
        m_playerUnits.Add(new ContentDwarvenSoldier());

        //Special Units
        m_playerUnits.Add(new ContentRoyalCaravan());

        m_hasInit = true;
    }

    public static void Init(List<GameEnemyUnit> totalEnemiesOnMap, GameSpawnPool defaultSpawnPool, List<GameSpawnPool> spawnPointSpawnPools)
    {
        //Enemy Units
        m_enemies = totalEnemiesOnMap;
        m_defaultSpawnPool = defaultSpawnPool;
        m_spawnPointSpawnPools = spawnPointSpawnPools;

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
        }
    }

    public static void DeInit()
    {
        m_enemies.Clear();
        m_defaultSpawnPool = null;
        m_spawnPointSpawnPools = null;
        m_bossEnemies.Clear();
        m_eliteEnemies.Clear();
    }

    public static GameSpawnPoolData GetRandomEnemyFromDefaultSpawnPool(GameOpponent gameOpponent, int curWave)
    {
        if (!m_defaultSpawnPool.TryGetSpawnPoolForWave(curWave, out List<GameSpawnPoolData> list))
        {
            Debug.LogWarning("Spawning an enemy from an invalid wave: " + curWave);
        }

        if (list.Count == 0)
        {
            return null;
        }

        float sumPriorityWeights = 0.0f;

        for (int i = 0; i < list.Count; i++)
        {
            sumPriorityWeights += list[i].m_priorityWeight;
        }

        float priorityRandomValue = UnityEngine.Random.Range(0.0f, sumPriorityWeights);

        float priorityValueIterator = 0.0f;
        for (int i = 0; i < list.Count; i++)
        {
            priorityValueIterator += list[i].m_priorityWeight;

            if (priorityValueIterator >= priorityRandomValue)
            {
                return list[i];
            }
        }

        Debug.LogError("Went past the priority value iterator's max point, something broke in the math");

        int r = UnityEngine.Random.Range(0, list.Count);
        return list[r];
    }

    public static GameSpawnPoolData GetRandomEnemyFromSpawnPoint(GameOpponent gameOpponent, int curWave, GameSpawnPoint spawnPoint)
    {
        if (spawnPoint.m_spawnPointMarkers.Count == 0 || (spawnPoint.m_spawnPointMarkers.Count == 1 && spawnPoint.m_spawnPointMarkers[0] == 0))
        {
            return GetRandomEnemyFromDefaultSpawnPool(gameOpponent, curWave);
        }

        List<GameSpawnPoolData> list = new List<GameSpawnPoolData>();
        for (int i = 0; i < spawnPoint.m_spawnPointMarkers.Count; i++)
        {
            if (spawnPoint.m_spawnPointMarkers[i] == 0)
            {
                if (!m_defaultSpawnPool.TryGetSpawnPoolForWave(curWave, out List<GameSpawnPoolData> defaultSpawnPool))
                {
                    Debug.LogWarning("Spawning an enemy from an invalid wave: " + curWave);
                    continue;
                }

                List<GameSpawnPoolData> defaultSpawnPoolData = defaultSpawnPool;
                list.AddRange(defaultSpawnPoolData);

                continue;
            }

            int spawnPoolIndex = spawnPoint.m_spawnPointMarkers[i] - 1;

            if (spawnPoolIndex < 0 || m_spawnPointSpawnPools.Count <= spawnPoolIndex)
            {
                Debug.LogError("GameUnitFactory received Spawn Point Marker " + spawnPoolIndex + " on a " + spawnPoint.m_tile.GetTerrain().GetBaseName() + " at coordinates " + spawnPoint.m_tile.m_gridPosition + " That does not exist");
            }

            if (!m_spawnPointSpawnPools[spawnPoolIndex].CheckTrySpawn())
            {
                continue;
            }

            if (!m_spawnPointSpawnPools[spawnPoolIndex].TryGetSpawnPoolForWave(curWave, out List<GameSpawnPoolData> spawnPoolAtWave))
            {
                Debug.LogWarning("Spawning an enemy from an invalid wave: " + curWave);
            }

            List<GameSpawnPoolData> specificSpawnPool = spawnPoolAtWave;
            list.AddRange(spawnPoolAtWave);
        }

        if (list.Count == 0)
        {
            return null;
        }

        float sumPriorityWeights = 0.0f;

        for (int i = 0; i < list.Count; i++)
        {
            sumPriorityWeights += list[i].m_priorityWeight;
        }

        float priorityRandomValue = UnityEngine.Random.Range(0.0f, sumPriorityWeights);

        float priorityValueIterator = 0.0f;
        for (int i = 0; i < list.Count; i++)
        {
            priorityValueIterator += list[i].m_priorityWeight;

            if (priorityValueIterator >= priorityRandomValue)
            {
                return list[i];
            }
        }

        Debug.LogError("Went past the priority value iterator's max point, something broke in the math");

        int r = UnityEngine.Random.Range(0, list.Count);
        return list[r];
    }

    public static GameEnemyUnit GetEnemyUnitClone(GameEnemyUnit unit)
    {
        return (GameEnemyUnit)Activator.CreateInstance(unit.GetType(), WorldController.Instance.m_gameController.m_gameOpponent);
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
        if (!m_hasInit)
        {
            Init();
        }
        
        int i = m_playerUnits.FindIndex(t => t.GetBaseName() == jsonData.baseName);

        GameUnit newPlayerUnit = (GameUnit)Activator.CreateInstance(m_playerUnits[i].GetType());
        newPlayerUnit.LoadFromJson(jsonData);

        return newPlayerUnit;
    }

    public static GameEnemyUnit GetEnemyFromJson(JsonGameUnitData jsonData, GameOpponent gameOpponent)
    {
        int i = m_enemies.FindIndex(t => t.GetBaseName() == jsonData.baseName);

        GameEnemyUnit newEnemy = (GameEnemyUnit)Activator.CreateInstance(m_enemies[i].GetType(), gameOpponent);
        newEnemy.LoadFromJson(jsonData);

        return newEnemy;
    }

    public static GameEnemyUnit GetEnemyFromName(string name)
    {
        for (int i = 0; i < m_enemies.Count; i++)
        {
            if (m_enemies[i].GetName().ToLower() == name.ToLower())
            {
                return GetEnemyUnitClone(m_enemies[i]);
            }
        }

        return null;
    }
}