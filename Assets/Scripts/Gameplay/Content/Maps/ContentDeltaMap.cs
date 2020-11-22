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
            if (WorldGridManager.Instance.m_gridArray[i].GetGameTile().HasEventMarker(1))
            {
                return gameOpponent.ForceSpawnNearPosition(GameUnitFactory.GetRandomBossEnemy(gameOpponent), WorldGridManager.Instance.m_gridArray[i].GetGameTile());
            }
        }

        Debug.LogError("No event tiles with marker 10 to spawn Lord of Eruptions");
        return false;
    }

    protected override void FillSpawnPool()
    {
        m_totalEnemiesOnMap.Add(new ContentLordOfChaosEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentSandWyvernEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentBasiliskEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentJackalEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentCrumblingAncientEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentSnakeEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentSandVortexEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentDjinnEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentMummyEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentMummyPharaohEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentRiverlurkerEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentMobolaEnemy(null));

        m_totalEnemiesOnMap.Add(new ContentScorchingSerpentEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentDemonMagicianEnemy(null));

        m_totalEnemiesOnMap.Add(new ContentGoblinWarriorEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentLancerEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentToadEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentZombieEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentShadeEnemy(null));

        //--------------------------------------------------------------------------------------------------------//

        List<GameSpawnPoolData> defaultSpawnPoolDatas = new List<GameSpawnPoolData>();
        //Wave 1
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentGoblinWarriorEnemy(null), 1, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentScorchingSerpentEnemy(null), 1, 1, 0.25f));

        //Wave 2
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentGoblinWarriorEnemy(null), 2, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentLancerEnemy(null), 2, 1, 0.75f));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentBasiliskEnemy(null), 2, 1, 0.25f));

        //Wave 3
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentLancerEnemy(null), 3, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentBasiliskEnemy(null), 3, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentSandVortexEnemy(null), 3, 1, 0.75f));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentCrumblingAncientEnemy(null), 3, 1, 0.5f));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentScorchingSerpentEnemy(null), 3, 0.75f, 0.5f));

        //Wave 4
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentBasiliskEnemy(null), 4, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentSandVortexEnemy(null), 4, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentCrumblingAncientEnemy(null), 4, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentShadeEnemy(null), 4, 1, 1));

        //Wave 5
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentDemonMagicianEnemy(null), 5, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentDjinnEnemy(null), 5, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentMobolaEnemy(null), 5, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentSandWyvernEnemy(null), 5, 1, 0.5f));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentZombieEnemy(null), 5, 1, 0.5f));

        //Wave 6
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentDemonMagicianEnemy(null), 6, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentDjinnEnemy(null), 6, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentMobolaEnemy(null), 6, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentSandWyvernEnemy(null), 6, 1, 0.75f));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentZombieEnemy(null), 6, 1, 0.5f));

        //--------------------------------------------------------------------------------------------------------//

        List<GameSpawnPoolData> riverShoresSpawnPoolDatas = new List<GameSpawnPoolData>();

        //Wave 1
        riverShoresSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentGoblinWarriorEnemy(null), 1, 1, 1));
        riverShoresSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentScorchingSerpentEnemy(null), 1, 1, 0.25f));

        //Wave 2
        riverShoresSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentGoblinWarriorEnemy(null), 2, 1, 1));
        riverShoresSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentLancerEnemy(null), 2, 1, 0.75f));
        riverShoresSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentToadEnemy(null), 2, 1, 1));

        //Wave 3
        riverShoresSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentLancerEnemy(null), 3, 1, 1));
        riverShoresSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentBasiliskEnemy(null), 3, 1, 1));
        riverShoresSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentScorchingSerpentEnemy(null), 3, 0.75f, 0.5f));

        //Wave 4
        riverShoresSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentSnakeEnemy(null), 4, 1, 1));
        riverShoresSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentShadeEnemy(null), 4, 1, 1));

        //Wave 5
        riverShoresSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentDemonMagicianEnemy(null), 5, 1, 1));
        riverShoresSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentDjinnEnemy(null), 5, 1, 1));
        riverShoresSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentMobolaEnemy(null), 5, 1, 1));
        riverShoresSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentSandWyvernEnemy(null), 5, 1, 0.5f));
        riverShoresSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentZombieEnemy(null), 5, 1, 0.5f));

        //Wave 6
        riverShoresSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentDemonMagicianEnemy(null), 6, 1, 1));
        riverShoresSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentDjinnEnemy(null), 6, 1, 1));
        riverShoresSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentMobolaEnemy(null), 6, 1, 1));
        riverShoresSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentSandWyvernEnemy(null), 6, 1, 0.75f));
        riverShoresSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentZombieEnemy(null), 6, 1, 0.5f));

        //--------------------------------------------------------------------------------------------------------//

        List<GameSpawnPoolData> inRiverSpawnPoolDatas = new List<GameSpawnPoolData>();

        //Wave 1
        inRiverSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentScorchingSerpentEnemy(null), 1, 1, 0.25f));

        //Wave 3
        inRiverSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentRiverlurkerEnemy(null), 3, 2.0f, 0.75f));
        inRiverSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentScorchingSerpentEnemy(null), 3, 0.75f, 0.5f));

        //Wave 4
        inRiverSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentRiverlurkerEnemy(null), 4, 1.5f, 1));

        //Wave 5
        inRiverSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentRiverlurkerEnemy(null), 5, 1, 1));

        //Wave 6
        inRiverSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentRiverlurkerEnemy(null), 6, 0.75f, 1));

        //--------------------------------------------------------------------------------------------------------//

        m_defaultSpawnPool = new GameSpawnPool(defaultSpawnPoolDatas);
        m_spawnPointSpawnPools.Add(new GameSpawnPool(riverShoresSpawnPoolDatas));
        m_spawnPointSpawnPools.Add(new GameSpawnPool(inRiverSpawnPoolDatas));
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