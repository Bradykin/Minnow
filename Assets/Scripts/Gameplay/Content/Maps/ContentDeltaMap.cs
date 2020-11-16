using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDeltaMap : GameMap
{
    public ContentDeltaMap()
    {
        m_name = "Delta";
        m_desc = "An old region; many civilizations have left their remains behind along well trodden roads and in buried dunes.  Will you be different?";

        m_difficulty = MapDifficulty.Easy;

        m_id = 5;

        Init();
    }

    protected override void FillMapEvents()
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.MapEvents))
        {
            AddMapEvent(new ContentFloodingMapEvent(0), 2);
            AddMapEvent(new ContentFloodReceedingMapEvent(0), 4);
        }
    }

    public override bool TrySpawnBoss(List<GameTile> tilesAtFogEdge)
    {
        GameOpponent gameOpponent = GameHelper.GetOpponent();

        if (gameOpponent.m_hasSpawnedBoss)
        {
            return true;
        }

        if (GameHelper.GetGameController().m_currentTurnNumber < Constants.SpawnBossTurn || GameHelper.GetCurrentWaveNum() != Constants.FinalWaveNum)
        {
            return false;
        }

        for (int i = 0; i < WorldGridManager.Instance.m_gridArray.Length; i++)
        {
            if (WorldGridManager.Instance.m_gridArray[i].GetGameTile().m_gameEventMarkers.Contains(1))
            {
                return gameOpponent.ForceSpawnNearPosition(GameUnitFactory.GetRandomBossEnemy(gameOpponent), WorldGridManager.Instance.m_gridArray[i].GetGameTile());
            }
        }

        Debug.LogError("No event tiles with marker 10 to spawn Lord of Eruptions");
        return false;
    }

    protected override void FillSpawnPool()
    {
        /*m_totalEnemiesOnMap.Add(new ContentLordOfChaosEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentSandWyvernEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentBasiliskEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentJackalEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentCrumblingAncientEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentSnakeEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentSandVortexEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentDjinnEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentMummyEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentRiverlurkerEnemy(null));


        m_totalEnemiesOnMap.Add(new ContentScorchingSerpentEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentDemonMagicianEnemy(null));


        m_totalEnemiesOnMap.Add(new ContentGoblinWarriorEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentZombieEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentShadeEnemy(null));*/

        m_totalEnemiesOnMap.Add(new ContentAngryBirdEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentDarkWarriorEnemy(null));
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
        m_totalEnemiesOnMap.Add(new ContentZombieEnemy(null));

        List<GameSpawnPoolData> defaultSpawnPoolDatas = new List<GameSpawnPoolData>();
        //Wave 1
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentSlimeEnemy(null), 1, 1, 1));

        //Wave 2
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentSlimeEnemy(null), 2, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentLancerEnemy(null), 2, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentToadEnemy(null), 2, 1, 1));

        //Wave 3
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentLancerEnemy(null), 3, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentOrcEnemy(null), 3, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentOrcShamanEnemy(null), 3, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentAngryBirdEnemy(null), 3, 1, 1));

        //Wave 4
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentLancerEnemy(null), 4, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentOrcEnemy(null), 4, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentOrcShamanEnemy(null), 4, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentAngryBirdEnemy(null), 4, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentShadeEnemy(null), 4, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentSnakeEnemy(null), 4, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentLavaRhinoEnemy(null), 4, 1, 0.25f));

        //Wave 5
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentLavaRhinoEnemy(null), 5, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentWerewolfEnemy(null), 5, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentYetiEnemy(null), 5, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentMobolaEnemy(null), 5, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentLizardmanEnemy(null), 5, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentZombieEnemy(null), 5, 1, 0.5f));

        //Wave 6
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentLavaRhinoEnemy(null), 6, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentWerewolfEnemy(null), 6, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentYetiEnemy(null), 6, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentMobolaEnemy(null), 6, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentLizardmanEnemy(null), 6, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentZombieEnemy(null), 6, 1, 0.5f));

        m_defaultSpawnPool = new GameSpawnPool(defaultSpawnPoolDatas);
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
        m_eventPool.Add(new ContentGemsOfProphecyEvent(null));
        m_eventPool.Add(new ContentOverturnedCartEvent(null));
        m_eventPool.Add(new ContentMillitiaEvent(null));
        m_eventPool.Add(new ContentLibraryOfDenumianEvent(null));

        FillAltars();
    }

    protected override void FillExclusionRelicPool()
    {

    }
}