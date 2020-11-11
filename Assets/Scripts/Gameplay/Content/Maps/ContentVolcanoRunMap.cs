using System.Collections;
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

        m_playerUnlockLevel = 1;

        m_spawnCrystals = false;

        Init();
    }

    public override int GetNumEnemiesToSpawn()
    {
        int wave = GameHelper.GetCurrentWaveNum();
        int baseAmount = base.GetNumEnemiesToSpawn();

        if (wave == 2 || wave == 3)
        {
            baseAmount += 1;
        }

        if (wave == 4 || wave == 5 || wave == 6)
        {
            baseAmount += 2;
        }

        return baseAmount;
    }

    protected override void FillMapEvents()
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.MapEvents))
        {
            AddMapEvent(new ContentDeployCaravanEvent(0), 2);

            AddMapEvent(new ContentVolcanoEruptionEvent(1, true), 2);
            AddMapEvent(new ContentVolcanoEruptionEvent(1, false), 3);

            AddMapEvent(new ContentVolcanoEruptionEvent(2, true), 3);
            AddMapEvent(new ContentVolcanoEruptionEvent(2, false), 4);

            AddMapEvent(new ContentVolcanoEruptionEvent(3, true), 4);
            AddMapEvent(new ContentVolcanoEruptionEvent(3, false), 5);

            AddMapEvent(new ContentVolcanoEruptionEvent(4, true), 5);
            AddMapEvent(new ContentVolcanoEruptionEvent(4, false), 6);
        }
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
        m_totalEnemiesOnMap.Add(new ContentFlamesoulElementalEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentVolcanoCrabEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentToadEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentJackalEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentCrumblingAncientEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentBasiliskEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentSnakeEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentFlameImpEnemy(null));

        List<GameSpawnPoolData> defaultSpawnPoolDatas = new List<GameSpawnPoolData>();
        //Wave 1
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentBlindBeastEnemy(null), 1, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentScorchingSerpentEnemy(null), 1, 1, 0.75f));

        //Wave 2
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentBlindBeastEnemy(null), 2, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentScorchingSerpentEnemy(null), 2, 1, 0.5f));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentHellhoundEnemy(null), 2, 1, 0.75f));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentFlameImpEnemy(null), 2, 1, 0.25f));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentToadEnemy(null), 2, 1, 0.75f));

        //Wave 3
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentHellhoundEnemy(null), 3, 1, 2));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentBurningMonstrosityEnemy(null), 3, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentFlameImpEnemy(null), 3, 1, 0.5f));

        //Wave 4
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentHellhoundEnemy(null), 4, 0.75f, 1.25f));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentBurningMonstrosityEnemy(null), 4, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentDemonMagicianEnemy(null), 4, 1, 0.75f));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentVolcanoGolemEnemy(null), 4, 1, 0.75f));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentPhoenixEnemy(null), 4, 1.25f, 0.25f));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentFlameImpEnemy(null), 4, 0.75f, 0.5f));

        //Wave 5
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentBurningMonstrosityEnemy(null), 5, 0.75f, 0.5f));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentDemonMagicianEnemy(null), 5, 1, 0.75f));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentVolcanoGolemEnemy(null), 5, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentPhoenixEnemy(null), 5, 1.25f, 0.75f));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentLavaHellionEnemy(null), 5, 1.5f, 0.5f));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentFlameImpEnemy(null), 5, 0.5f, 0.5f));

        //Wave 6
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentBurningMonstrosityEnemy(null), 6, 0.75f, 0.5f));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentDemonMagicianEnemy(null), 6, 1, 0.75f));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentVolcanoGolemEnemy(null), 6, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentPhoenixEnemy(null), 6, 1, 0.75f));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentLavaHellionEnemy(null), 6, 1.25f, 0.5f));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentFlameImpEnemy(null), 5, 0.5f, 0.5f));


        List<GameSpawnPoolData> desertSpawnPoolDatas = new List<GameSpawnPoolData>();
        desertSpawnPoolDatas.AddRange(defaultSpawnPoolDatas);

        //Wave 3
        desertSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentBasiliskEnemy(null), 3, 1, 1));
        desertSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentCrumblingAncientEnemy(null), 3, 1, 0.75f));
        desertSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentSnakeEnemy(null), 3, 1, 0.25f));

        //Wave 4
        desertSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentBasiliskEnemy(null), 4, 1, 1));
        desertSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentCrumblingAncientEnemy(null), 4, 1, 1));
        desertSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentJackalEnemy(null), 4, 2, 0.5f));

        //Wave 5
        desertSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentBasiliskEnemy(null), 5, 0.75f, 1));
        desertSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentCrumblingAncientEnemy(null), 5, 0.75f, 1));
        desertSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentJackalEnemy(null), 5, 1, 1));

        m_defaultSpawnPool = new GameSpawnPool(defaultSpawnPoolDatas);
        m_spawnPointSpawnPools.Add(new GameSpawnPool(desertSpawnPoolDatas));
    }
}
