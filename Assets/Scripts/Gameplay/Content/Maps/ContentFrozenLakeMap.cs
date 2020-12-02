using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFrozenLakeMap : GameMap
{
    public ContentFrozenLakeMap()
    {
        m_name = "Frozen Lake";
        m_desc = "Your base has natural defenses, but beware the ice! Cracks in the ice can break apart, and who knows what lays beneath...";

        m_difficulty = MapDifficulty.Hard;

        m_id = 7;

        Init();
    }

    protected override void FillMapEvents()
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.MapEvents))
        {
            AddMapEvent(new ContentIcequakeEvent(0), 2);
            AddMapEvent(new ContentIcequakeEvent(0), 3);
            AddMapEvent(new ContentIcequakeEvent(0), 4);
            AddMapEvent(new ContentIcequakeEvent(0), 5);
            AddMapEvent(new ContentIcequakeEvent(0), 6);
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
        m_totalEnemiesOnMap.Add(new ContentCharybdisEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentChillflameBeastEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentFrostGiantEnemy(null)); // not fully implemented
        m_totalEnemiesOnMap.Add(new ContentFrozenGuardianEnemy(null)); 
        m_totalEnemiesOnMap.Add(new ContentFrozenImpEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentGreatFrostlizardEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentGriffonEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentIcefisherEnemy(null)); // not fully implemented
        m_totalEnemiesOnMap.Add(new ContentIceWurmEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentLordOfWinterEnemy(null)); // not implemented
        m_totalEnemiesOnMap.Add(new ContentPolarWarriorEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentSabertoothWyvernEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentSnowprowlerEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentSpiralSerpentEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentValgulaEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentYetiEnemy(null));

        //--------------------------------------------------------------------------------------------------------//

        List<GameSpawnPoolData> defaultSpawnPoolData = new List<GameSpawnPoolData>();
        //Wave 1
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentSlimeEnemy(null), 1, 1, 1));

        //Wave 2
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentSlimeEnemy(null), 2, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentLancerEnemy(null), 2, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentToadEnemy(null), 2, 1, 1));

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
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentSnakeEnemy(null), 4, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentLavaRhinoEnemy(null), 4, 1, 0.5f));

        //Wave 5
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentLavaRhinoEnemy(null), 5, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentWerewolfEnemy(null), 5, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentYetiEnemy(null), 5, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentMobolaEnemy(null), 5, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentLizardmanEnemy(null), 5, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentZombieEnemy(null), 5, 1, 1));

        //Wave 6
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentLavaRhinoEnemy(null), 6, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentWerewolfEnemy(null), 6, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentYetiEnemy(null), 6, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentMobolaEnemy(null), 6, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentLizardmanEnemy(null), 6, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentZombieEnemy(null), 6, 1, 1));

        //--------------------------------------------------------------------------------------------------------//

        List<GameSpawnPoolData> frozenWatersSpawnPoolData = new List<GameSpawnPoolData>();

        //Wave 1
        frozenWatersSpawnPoolData.Add(new GameSpawnPoolData(new ContentGoblinWarriorEnemy(null), 1, 1, 1));
        frozenWatersSpawnPoolData.Add(new GameSpawnPoolData(new ContentScorchingSerpentEnemy(null), 1, 1, 0.25f));

        //Wave 2
        frozenWatersSpawnPoolData.Add(new GameSpawnPoolData(new ContentGoblinWarriorEnemy(null), 2, 1, 1));
        frozenWatersSpawnPoolData.Add(new GameSpawnPoolData(new ContentLancerEnemy(null), 2, 1, 1));
        frozenWatersSpawnPoolData.Add(new GameSpawnPoolData(new ContentToadEnemy(null), 2, 1, 1));

        //Wave 3
        frozenWatersSpawnPoolData.Add(new GameSpawnPoolData(new ContentLancerEnemy(null), 3, 1, 1));
        frozenWatersSpawnPoolData.Add(new GameSpawnPoolData(new ContentBasiliskEnemy(null), 3, 1, 1));
        frozenWatersSpawnPoolData.Add(new GameSpawnPoolData(new ContentScorchingSerpentEnemy(null), 3, 0.75f, 0.5f));

        //Wave 4
        frozenWatersSpawnPoolData.Add(new GameSpawnPoolData(new ContentDjinnEnemy(null), 6, 1, 0.5f));
        frozenWatersSpawnPoolData.Add(new GameSpawnPoolData(new ContentSnakeEnemy(null), 4, 1, 1));
        frozenWatersSpawnPoolData.Add(new GameSpawnPoolData(new ContentShadeEnemy(null), 4, 1, 1));

        //Wave 5
        frozenWatersSpawnPoolData.Add(new GameSpawnPoolData(new ContentDemonMagicianEnemy(null), 5, 1, 1));
        frozenWatersSpawnPoolData.Add(new GameSpawnPoolData(new ContentDjinnEnemy(null), 5, 1, 0.5f));
        frozenWatersSpawnPoolData.Add(new GameSpawnPoolData(new ContentMobolaEnemy(null), 5, 1, 1));
        frozenWatersSpawnPoolData.Add(new GameSpawnPoolData(new ContentSandWyvernEnemy(null), 5, 1, 0.25f));
        frozenWatersSpawnPoolData.Add(new GameSpawnPoolData(new ContentZombieEnemy(null), 5, 1, 0.5f));

        //Wave 6
        frozenWatersSpawnPoolData.Add(new GameSpawnPoolData(new ContentDemonMagicianEnemy(null), 6, 1, 1));
        frozenWatersSpawnPoolData.Add(new GameSpawnPoolData(new ContentMobolaEnemy(null), 6, 1, 1));
        frozenWatersSpawnPoolData.Add(new GameSpawnPoolData(new ContentSandWyvernEnemy(null), 6, 1, 0.5f));
        frozenWatersSpawnPoolData.Add(new GameSpawnPoolData(new ContentZombieEnemy(null), 6, 1, 0.5f));

        //--------------------------------------------------------------------------------------------------------//

        m_defaultSpawnPool = new GameSpawnPool(defaultSpawnPoolData);
        m_spawnPointSpawnPools.Add(new GameSpawnPool(defaultSpawnPoolData));
        m_spawnPointSpawnPools.Add(new GameSpawnPool(defaultSpawnPoolData));
        m_spawnPointSpawnPools.Add(new GameSpawnPool(defaultSpawnPoolData));
        m_spawnPointSpawnPools.Add(new GameSpawnPool(defaultSpawnPoolData));
        m_spawnPointSpawnPools.Add(new GameSpawnPool(defaultSpawnPoolData));
    }
}
