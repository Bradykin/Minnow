using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentVolcanoRunMap : GameMap
{
    public ContentVolcanoRunMap()
    {
        m_name = "Volcano Run";
        m_desc = "The valley seems so idyllic and perfect. I'm sure nothing will go wrong...";

        m_difficulty = MapDifficulty.Hard;

        m_id = 8;

        m_playerUnlockLevel = 1;

        Init();
    }

    protected override void FillMapEvents()
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.MapEvents))
        {
            AddMapEvent(new ContentDeployCaravanEvent(0), 2);
            AddMapEvent(new ContentVolcanoEruptionEvent(1), 3);
            AddMapEvent(new ContentVolcanoEruptionEvent(2), 4);
            AddMapEvent(new ContentVolcanoEruptionEvent(3), 5);
            AddMapEvent(new ContentVolcanoEruptionEvent(4), 6);
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
        m_spawnPool.Add(new ContentBlindBeastEnemy(null)); //Waves 1-2
        m_spawnPool.Add(new ContentScorchingSerpentEnemy(null)); //Waves 2-3
        m_spawnPool.Add(new ContentHellhoundEnemy(null)); //Waves 3-5
        m_spawnPool.Add(new ContentBurningMonstrosityEnemy(null)); //Waves 3-4
        m_spawnPool.Add(new ContentVolcanoGolemEnemy(null)); //Waves 4-6
        m_spawnPool.Add(new ContentPhoenixEnemy(null)); //Waves 4-6
        m_spawnPool.Add(new ContentLavaHellionEnemy(null)); //Waves 5-6
        m_spawnPool.Add(new ContentDemonMagicianEnemy(null)); //Waves 4-4
        m_spawnPool.Add(new ContentFlamesoulElementalEnemy(null)); //Boss
        m_spawnPool.Add(new ContentVolcanoCrabEnemy(null)); //Elite

        //Supporting normal enemies
        List<GameEnemyUnit> baseTerrainSpawnPool = new List<GameEnemyUnit>();
        baseTerrainSpawnPool.Add(new ContentOrcEnemy(null)); //Waves 3-4
        baseTerrainSpawnPool.Add(new ContentOrcShamanEnemy(null)); //Waves 3-4
        baseTerrainSpawnPool.Add(new ContentMobolaEnemy(null)); //Waves 5-6
        baseTerrainSpawnPool.Add(new ContentWerewolfEnemy(null)); //Waves 5-6
        baseTerrainSpawnPool.Add(new ContentToadEnemy(null)); //Wave 2, only spawn in the boomerang basic terrain region and the bottom right
        m_specificSpawnPools.Add(baseTerrainSpawnPool);

        //Supporting Desert Enemies
        List<GameEnemyUnit> desertTerrainSpawnPool = new List<GameEnemyUnit>();
        desertTerrainSpawnPool.Add(new ContentJackalEnemy(null)); //Waves 4-5?
        desertTerrainSpawnPool.Add(new ContentCrumblingAncientEnemy(null)); //Waves 3-4
        m_specificSpawnPools.Add(desertTerrainSpawnPool);

        //Idea use desert spawns for waves 3-4 from the desert area, use basic spawns for waves 3-4 from the bottom right Re-examine creature pool with that idea

        //Old wave enemy distribution:
        //Wave 1 - 1
        //Wave 2 - 3
        //Wave 3 - 4
        //Wave 4 - 7
        //Wave 5 - 7
        //Wave 6 - 7

        //New wave enemy distribution:
        //Wave 1 - 1
        //Wave 2 - 3
        //Wave 3 - 5
        //Wave 4 - 7
        //Wave 5 - 6
        //Wave 6 - 5
    }
}
