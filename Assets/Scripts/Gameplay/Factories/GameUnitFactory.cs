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
        //Player Units 1-10 score (1 100% rework; 10 best)
        m_playerUnits.Add(new ContentOverlord()); //10
        m_playerUnits.Add(new ContentShadowWarlock()); //10
        m_playerUnits.Add(new ContentGladiator()); //10
        m_playerUnits.Add(new ContentGrasper()); //10
        m_playerUnits.Add(new ContentElvenWizard()); //9 - Maybe 4 energy cost
        m_playerUnits.Add(new ContentHomonculus()); //9
        m_playerUnits.Add(new ContentCyclops()); //9 - Big, bulky, obvious. Need to watch if players use it
        m_playerUnits.Add(new ContentInjuredTroll()); //9 - Possible scaling
        m_playerUnits.Add(new ContentHero()); //7 - Tweak Numbers
        m_playerUnits.Add(new ContentDevourer()); //7 - Tweak Numbers
        m_playerUnits.Add(new ContentDwarfArchitect()); //8 - Keep an eye on readability
        m_playerUnits.Add(new ContentElvenRogue()); // 7 - Keep an eye on how fun
        m_playerUnits.Add(new ContentElvenSentinel()); // 7 - Keep an eye on how fun
        m_playerUnits.Add(new ContentRaptor()); //6 - Leave it; but it doesn't play nicely with monsters

        m_playerUnits.Add(new ContentDwarfShivcaster()); //9 - It's shivs shouldn't trigger Spellcraft
        m_playerUnits.Add(new ContentGuardCaptain()); //6 - Needs scaling in some way from other humanoids
        m_playerUnits.Add(new ContentNaturalScout()); // 6 - Blind Beast - it; tweak stamina regen, remove attack restriction
        m_playerUnits.Add(new ContentSabobot()); //5 - Remove start at max stam; add shuffle back in on death
        m_playerUnits.Add(new ContentSkeleton()); // 6 - Needs to go back into deck on death; not stay around
        m_playerUnits.Add(new ContentWanderer()); //6 - Tweak how we get shivs (Maybe Momentum)

        m_playerUnits.Add(new ContentConjuredImp()); //4 - Rework? Not obvious how to use (Maybe auto-spawn 1 nearby; they share stats and stuff)
        m_playerUnits.Add(new ContentMiner()); // 1 - Rework; bad concept (Mountain explorer?)
        m_playerUnits.Add(new ContentMage()); //1 - Rework Too much cheap power, too similar to other things; just needs new concept
        m_playerUnits.Add(new ContentFishOracle()); //1 - Rework
        m_playerUnits.Add(new ContentMetalGolem()); //1 - Rework (Mountain explorer?)
        m_playerUnits.Add(new ContentGroundskeeper()); //1 - Rework (Maybe gains Taunt in forest?)
        m_playerUnits.Add(new ContentGoblin()); //1 - Rework (Basic unit with victorious)
        m_playerUnits.Add(new ContentRanger()); //6 - Rework; keep soul
        m_playerUnits.Add(new ContentStoneGolem()); //1 - Rework
        m_playerUnits.Add(new ContentWildfolk()); //1 - Rework (Monster synergy Humanoid)

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
                continue;
            }

            int spawnPoolIndex = spawnPoint.m_spawnPointMarkers[i] - 1;

            if (spawnPoolIndex < 0 || m_spawnPointSpawnPools.Count <= spawnPoolIndex)
            {
                Debug.LogError("GameUnitFactory received Spawn Point Marker " + spawnPoolIndex + " on a " + spawnPoint.m_tile.GetTerrain().GetBaseName() + " at coordinates " + spawnPoint.m_tile.m_gridPosition + " That does not exist");
            }

            if (!m_spawnPointSpawnPools[spawnPoolIndex].TryGetSpawnPoolForWave(curWave, out List<GameSpawnPoolData> spawnPoolAtWave))
            {
                Debug.LogWarning("Spawning an enemy from an invalid wave: " + curWave);
            }

            List<GameSpawnPoolData> specificSpawnPool = spawnPoolAtWave;
            list.AddRange(spawnPoolAtWave);

            for (int k = 0; k < specificSpawnPool.Count; k++)
            {
                if (!list.Any(u => u.GetType() == specificSpawnPool[k].GetType()))
                {
                    list.Add(specificSpawnPool[k]);
                }
            }
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