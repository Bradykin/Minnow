﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentVolcanoRunMap : GameMap
{
    public ContentVolcanoRunMap()
    {
        m_name = "Volcano Run";
        m_desc = "The valley seems so idyllic and perfect. I'm sure nothing will go wrong...";

        m_difficulty = MapDifficulty.Easy;

        m_id = 8;

        Init();
    }

    public override int GetNumEnemiesToSpawn()
    {
        return 5;
    }

    protected override void FillMapEvents()
    {
        AddMapEvent(new ContentDeployCaravanVolcanoRunEvent(0), 2);

        AddMapEvent(new ContentVolcanoEruptionEvent(1, true), 2);
        AddMapEvent(new ContentVolcanoEruptionEvent(1, false), 3);

        AddMapEvent(new ContentVolcanoEruptionEvent(2, true), 3);
        AddMapEvent(new ContentVolcanoEruptionEvent(2, false), 4);

        /*if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.MapEvents))
        {*/
            AddMapEvent(new ContentVolcanoEruptionEvent(3, true), 4);
            AddMapEvent(new ContentVolcanoEruptionEvent(3, false), 5);

            AddMapEvent(new ContentVolcanoEruptionEvent(4, true), 5);
            AddMapEvent(new ContentVolcanoEruptionEvent(4, false), 6);
        //}
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
            if (WorldGridManager.Instance.m_gridArray[i].GetGameTile().HasEventMarker(10))
            {
                return gameOpponent.ForceSpawnNearPosition(GameUnitFactory.GetRandomBossEnemy(gameOpponent), WorldGridManager.Instance.m_gridArray[i].GetGameTile());
            }
        }

        Debug.LogError("No event tiles with marker 10 to spawn Lord of Eruptions");
        return false;
    }

    protected override void FillExclusionCardPool()
    {
        
    }

    protected override void FillEventPool()
    {
        FillBasicEventPool();
    }

    protected override void FillExclusionRelicPool()
    {
        
    }

    protected override void FillSpawnPool()
    {
        //All enemies on map
        m_totalEnemiesOnMap.Add(new ContentBlindBeastEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentScorchingSerpentEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentHellhoundEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentBurningMonstrosityEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentVolcanoGolemEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentPhoenixEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentLavaHellionEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentDemonMagicianEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentLordOfEruptionsEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentVolcanoCrabEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentToadEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentJackalEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentCrumblingAncientEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentBasiliskEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentSnakeEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentFlameImpEnemy(null));

        //--------------------------------------------------------------------------------------------------------//

        List<GameSpawnPoolData> defaultSpawnPoolData = new List<GameSpawnPoolData>();
        //Wave 1
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentBlindBeastEnemy(null), 1, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentScorchingSerpentEnemy(null), 1, 1, 0.75f));

        //Wave 2
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentBlindBeastEnemy(null), 2, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentScorchingSerpentEnemy(null), 2, 1, 0.5f));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentHellhoundEnemy(null), 2, 1, 0.75f));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentFlameImpEnemy(null), 2, 1, 0.25f));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentToadEnemy(null), 2, 1, 0.75f));

        //Wave 3
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentHellhoundEnemy(null), 3, 1, 2));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentBurningMonstrosityEnemy(null), 3, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentFlameImpEnemy(null), 3, 1, 0.5f));

        //Wave 4
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentHellhoundEnemy(null), 4, 0.75f, 1.25f));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentBurningMonstrosityEnemy(null), 4, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentDemonMagicianEnemy(null), 4, 1, 0.75f));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentVolcanoGolemEnemy(null), 4, 1, 0.75f));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentPhoenixEnemy(null), 4, 1.25f, 0.25f));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentFlameImpEnemy(null), 4, 0.75f, 0.5f));

        //Wave 5
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentBurningMonstrosityEnemy(null), 5, 0.75f, 0.5f));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentDemonMagicianEnemy(null), 5, 1, 0.75f));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentVolcanoGolemEnemy(null), 5, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentPhoenixEnemy(null), 5, 1.25f, 0.75f));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentLavaHellionEnemy(null), 5, 1.5f, 0.5f));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentFlameImpEnemy(null), 5, 0.5f, 0.5f));

        //Wave 6
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentBurningMonstrosityEnemy(null), 6, 0.75f, 0.5f));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentDemonMagicianEnemy(null), 6, 1, 0.75f));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentVolcanoGolemEnemy(null), 6, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentPhoenixEnemy(null), 6, 1, 0.75f));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentLavaHellionEnemy(null), 6, 1.25f, 0.5f));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentFlameImpEnemy(null), 5, 0.5f, 0.5f));

        //--------------------------------------------------------------------------------------------------------//

        List<GameSpawnPoolData> desertSpawnPoolData = new List<GameSpawnPoolData>();
        desertSpawnPoolData.AddRange(defaultSpawnPoolData);

        //Wave 3
        desertSpawnPoolData.Add(new GameSpawnPoolData(new ContentBasiliskEnemy(null), 3, 1, 1));
        desertSpawnPoolData.Add(new GameSpawnPoolData(new ContentCrumblingAncientEnemy(null), 3, 1, 0.75f));
        desertSpawnPoolData.Add(new GameSpawnPoolData(new ContentSnakeEnemy(null), 3, 1, 0.25f));

        //Wave 4
        desertSpawnPoolData.Add(new GameSpawnPoolData(new ContentBasiliskEnemy(null), 4, 1, 1));
        desertSpawnPoolData.Add(new GameSpawnPoolData(new ContentCrumblingAncientEnemy(null), 4, 1, 1));
        desertSpawnPoolData.Add(new GameSpawnPoolData(new ContentJackalEnemy(null), 4, 2, 0.5f));

        //Wave 5
        desertSpawnPoolData.Add(new GameSpawnPoolData(new ContentBasiliskEnemy(null), 5, 0.75f, 1));
        desertSpawnPoolData.Add(new GameSpawnPoolData(new ContentCrumblingAncientEnemy(null), 5, 0.75f, 1));
        desertSpawnPoolData.Add(new GameSpawnPoolData(new ContentJackalEnemy(null), 5, 1, 1));

        //--------------------------------------------------------------------------------------------------------//

        m_defaultSpawnPool = new GameSpawnPool(defaultSpawnPoolData);
        m_spawnPointSpawnPools.Add(new GameSpawnPool(desertSpawnPoolData));
    }
}
