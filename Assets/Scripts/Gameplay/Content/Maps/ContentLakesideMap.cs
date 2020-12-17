using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLakesideMap : GameMap
{
    public ContentLakesideMap()
    {
        m_name = "Lakeside";
        m_desc = "A strong chokepoint makes this an excellent learning experience!";

        m_difficulty = MapDifficulty.Introduction;

        m_id = 0;

        m_spawnCrystals = false;

        Init();
    }

    public override int GetNumEnemiesToSpawn()
    {
        return 4 + Mathf.FloorToInt(GameHelper.GetGameController().m_currentTurnNumber / 4);
    }

    protected override void FillMapEvents()
    {
        //These events are used for light tutorialization
        AddMapEvent(new ContentTutorialFirstWaveMapEvent(), 1);
        AddMapEvent(new ContentTutorialBossMapEvent(), 6);
    }

    protected override void FillSpawnPool()
    {
        m_totalEnemiesOnMap.Add(new ContentAngryBirdEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentDarkWarriorEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentOrcWarleaderEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentLizardmanEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentMobolaEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentOrcEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentOrcShamanEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentLavaRhinoEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentSlimeEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentSnakeEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentLancerEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentToadEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentWerewolfEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentYetiEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentShadeEnemy(null));


        /*m_totalEnemiesOnMap.Add(new ContentAngryTreantEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentArcticYetiEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentBlueMageEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentDarkMageEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentEnergizedTreantEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentFailedExperimentEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentFlamingWerewolfEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentFleshConstructEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentGiantBrawlerEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentGoblinShamanEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentGreenRockGiantEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentInfernalZombieEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentMetalCyclopsEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentNightmareWorkerEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentNorthernBerserkerEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentPoisonGargantuanEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentSerpentineConstructEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentSnowTrollEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentSwampwalkerEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentTreantMageEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentUndeadForestMonsterEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentZombieCrabEnemy(null));*/

        //--------------------------------------------------------------------------------------------------------//

        List<GameSpawnPoolData> defaultSpawnPoolData = new List<GameSpawnPoolData>();
        //Wave 1
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentSlimeEnemy(null), 1, 1, 1));

        //Wave 2
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentSlimeEnemy(null), 2, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentLancerEnemy(null), 2, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentToadEnemy(null), 2, 1, 0.5f));

        //Wave 3
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentLancerEnemy(null), 3, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentOrcEnemy(null), 3, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentOrcShamanEnemy(null), 3, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentAngryBirdEnemy(null), 3, 1, 1));

        //Wave 4
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentLancerEnemy(null), 4, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentOrcEnemy(null), 4, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentOrcShamanEnemy(null), 4, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentAngryBirdEnemy(null), 4, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentShadeEnemy(null), 4, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentSnakeEnemy(null), 4, 1, 0.5f));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentLavaRhinoEnemy(null), 4, 1, 0.25f));

        //Wave 5
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentLavaRhinoEnemy(null), 5, 1, 0.5f));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentWerewolfEnemy(null), 5, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentYetiEnemy(null), 5, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentMobolaEnemy(null), 5, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentLizardmanEnemy(null), 5, 1, 1));

        //Wave 6
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentLavaRhinoEnemy(null), 6, 1, 0.5f));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentWerewolfEnemy(null), 6, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentYetiEnemy(null), 6, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentMobolaEnemy(null), 6, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentLizardmanEnemy(null), 6, 1, 1));

        //--------------------------------------------------------------------------------------------------------//

        m_defaultSpawnPool = new GameSpawnPool(defaultSpawnPoolData);
    }

    protected override void FillExclusionCardPool()
    {
        
    }

    protected override void FillEventPool()
    {
        m_eventPool.Add(new ContentMagicianEvent(null));
        m_eventPool.Add(new ContentStablesEvent(null));
        m_eventPool.Add(new ContentGoldenFruitEvent(null));
        m_eventPool.Add(new ContentTraditionOrProgressEvent(null));
        m_eventPool.Add(new ContentAngelicGiftEvent(null));

        FillAltars();
    }

    protected override void FillExclusionRelicPool()
    {
        
    }
}
