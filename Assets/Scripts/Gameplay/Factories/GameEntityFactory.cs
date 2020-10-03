using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntityFactory
{
    private static List<GameUnit> m_playerEntities = new List<GameUnit>();


    private static List<GameEnemyUnit> m_enemies = new List<GameEnemyUnit>();
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

    private static bool m_hasInit = false;
    
    public static void Init()
    {
        //Player Entities
        m_playerEntities.Add(new ContentConjuredImp());
        m_playerEntities.Add(new ContentCyclops());
        m_playerEntities.Add(new ContentDemonSoldier());
        m_playerEntities.Add(new ContentDevourer());
        m_playerEntities.Add(new ContentDwarfArchitect());
        m_playerEntities.Add(new ContentDwarfShivcaster());
        m_playerEntities.Add(new ContentDwarvenSoldier());
        m_playerEntities.Add(new ContentElvenRogue());
        m_playerEntities.Add(new ContentElvenSentinel());
        m_playerEntities.Add(new ContentElvenWizard());
        m_playerEntities.Add(new ContentFishOracle());
        m_playerEntities.Add(new ContentGladiator());
        m_playerEntities.Add(new ContentGoblin());
        m_playerEntities.Add(new ContentGrasper());
        m_playerEntities.Add(new ContentGroundskeeper());
        m_playerEntities.Add(new ContentGuardCaptain());
        m_playerEntities.Add(new ContentHero());
        m_playerEntities.Add(new ContentHomonculus());
        m_playerEntities.Add(new ContentInjuredTroll());
        m_playerEntities.Add(new ContentMage());
        m_playerEntities.Add(new ContentMetalGolem());
        m_playerEntities.Add(new ContentMiner());
        m_playerEntities.Add(new ContentNaturalScout());
        m_playerEntities.Add(new ContentOverlord());
        m_playerEntities.Add(new ContentRanger());
        m_playerEntities.Add(new ContentRaptor());
        m_playerEntities.Add(new ContentSabobot());
        m_playerEntities.Add(new ContentShadowWarlock());
        m_playerEntities.Add(new ContentSkeleton());
        m_playerEntities.Add(new ContentStoneGolem());
        m_playerEntities.Add(new ContentWanderer());
        m_playerEntities.Add(new ContentWildfolk());

        //Enemy Entities
        m_enemies.Add(new ContentAngryBirdEnemy(null));
        m_enemies.Add(new ContentDarkWarriorEnemy(null));
        m_enemies.Add(new ContentLichEnemy(null));
        m_enemies.Add(new ContentLizardmanEnemy(null));
        m_enemies.Add(new ContentMobolaEnemy(null));
        m_enemies.Add(new ContentOrcEnemy(null));
        m_enemies.Add(new ContentOrcShamanEnemy(null));
        m_enemies.Add(new ContentSiegebreakerEntity(null));
        m_enemies.Add(new ContentShadeEnemy(null));
        m_enemies.Add(new ContentSlimeEnemy(null));
        m_enemies.Add(new ContentSnakeEnemy(null));
        m_enemies.Add(new ContentSpinnerEnemy(null));
        m_enemies.Add(new ContentToadEnemy(null));
        m_enemies.Add(new ContentWerewolfEnemy(null));
        m_enemies.Add(new ContentYetiEnemy(null));
        m_enemies.Add(new ContentZombieEnemy(null));

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

        m_hasInit = true;
    }

    public static GameEnemyUnit GetRandomEnemy(GameOpponent gameOpponent, int curWave)
    {
        if (!m_hasInit)
        {
            Init();
        }

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

    public static GameEnemyUnit GetEnemyEntityClone(GameEnemyUnit enemyEntity, GameOpponent gameOpponent)
    {
        if (!m_hasInit)
            Init();

        return (GameEnemyUnit)Activator.CreateInstance(enemyEntity.GetType(), gameOpponent);
    }

    public static GameEnemyUnit GetRandomEliteEnemy(GameOpponent gameOpponent)
    {
        if (!m_hasInit)
        {
            Init();
        }

        int r = UnityEngine.Random.Range(0, m_eliteEnemies.Count);

        return (GameEnemyUnit)Activator.CreateInstance(m_eliteEnemies[r].GetType(), gameOpponent);
    }

    public static GameEnemyUnit GetRandomBossEnemy(GameOpponent gameOpponent)
    {
        if (!m_hasInit)
        {
            Init();
        }

        int r = UnityEngine.Random.Range(0, m_bossEnemies.Count);

        return (GameEnemyUnit)Activator.CreateInstance(m_bossEnemies[r].GetType(), gameOpponent);
    }

    public static GameUnit GetEntityFromJson(JsonGameUnitData jsonData)
    {
        if (!m_hasInit)
            Init();

        int i = m_playerEntities.FindIndex(t => t.m_name == jsonData.name);

        GameUnit newPlayerEntity = (GameUnit)Activator.CreateInstance(m_playerEntities[i].GetType());
        newPlayerEntity.LoadFromJson(jsonData);

        return newPlayerEntity;
    }

    public static GameEnemyUnit GetEnemyFromJson(JsonGameUnitData jsonData, GameOpponent gameOpponent)
    {
        if (!m_hasInit)
            Init();

        int i = m_enemies.FindIndex(t => t.m_name == jsonData.name);

        GameEnemyUnit newEnemy = (GameEnemyUnit)Activator.CreateInstance(m_enemies[i].GetType(), gameOpponent);
        newEnemy.LoadFromJson(jsonData);

        return newEnemy;
    }
}