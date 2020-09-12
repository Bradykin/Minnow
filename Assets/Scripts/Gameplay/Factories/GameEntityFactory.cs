using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntityFactory
{
    private static List<GameEntity> m_playerEntities = new List<GameEntity>();


    private static List<GameEnemyEntity> m_enemies = new List<GameEnemyEntity>();
    private static List<GameEnemyEntity> m_commonEnemies = new List<GameEnemyEntity>();

    private static bool m_hasInit = false;
    
    public static void Init()
    {
        //Player Entities
        m_playerEntities.Add(new ContentCaveDragonEntity());
        m_playerEntities.Add(new ContentConjuredImp());
        m_playerEntities.Add(new ContentCyclops());
        m_playerEntities.Add(new ContentDemonSoldier());
        m_playerEntities.Add(new ContentDevourer());
        m_playerEntities.Add(new ContentDwarfArchitect());
        m_playerEntities.Add(new ContentDwarfHealer());
        m_playerEntities.Add(new ContentDwarvenSoldier());
        m_playerEntities.Add(new ContentElvenRogue());
        m_playerEntities.Add(new ContentElvenSentinel());
        m_playerEntities.Add(new ContentElvenWizard());
        m_playerEntities.Add(new ContentFishOracle());
        m_playerEntities.Add(new ContentGladiator());
        m_playerEntities.Add(new ContentGoblinEntity());
        m_playerEntities.Add(new ContentGrasper());
        m_playerEntities.Add(new ContentGroundskeeper());
        m_playerEntities.Add(new ContentGuardCaptain());
        m_playerEntities.Add(new ContentHero());
        m_playerEntities.Add(new ContentHomonculus());
        m_playerEntities.Add(new ContentInjuredTrollEntity());
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
        m_enemies.Add(new ContentSeigebreakerEntity(null));
        m_enemies.Add(new ContentShadeEnemy(null));
        m_enemies.Add(new ContentSlimeEnemy(null));
        m_enemies.Add(new ContentSnakeEnemy(null));
        m_enemies.Add(new ContentSpinnerEnemy(null));
        m_enemies.Add(new ContentToadEnemy(null));
        m_enemies.Add(new ContentWerewolfEnemy(null));
        m_enemies.Add(new ContentYetiEnemy(null));
        m_enemies.Add(new ContentZombieEnemy(null));

        m_hasInit = true;
    }
    
    public static GameEnemyEntity GetRandomEnemy(GameOpponent gameOpponent)
    {
        if (!m_hasInit)
        {
            Init();
        }
        
        int r = UnityEngine.Random.Range(0, m_enemies.Count);

        return (GameEnemyEntity)Activator.CreateInstance(m_enemies[r].GetType(), gameOpponent);
    }

    public static GameEntity GetEntityFromJson(JsonGameEntityData jsonData)
    {
        if (!m_hasInit)
            Init();

        int i = m_playerEntities.FindIndex(t => t.m_name == jsonData.name);

        GameEntity newPlayerEntity = (GameEntity)Activator.CreateInstance(m_playerEntities[i].GetType());
        newPlayerEntity.LoadFromJson(jsonData);

        return newPlayerEntity;
    }

    public static GameEnemyEntity GetEnemyFromJson(JsonGameEntityData jsonData, GameOpponent gameOpponent)
    {
        if (!m_hasInit)
            Init();

        int i = m_enemies.FindIndex(t => t.m_name == jsonData.name);

        GameEnemyEntity newEnemy = (GameEnemyEntity)Activator.CreateInstance(m_playerEntities[i].GetType());
        newEnemy.LoadFromJson(jsonData);

        return newEnemy;
    }
}