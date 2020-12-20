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

    public override int GetNumEnemiesToSpawn()
    {
        return 5;
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
        m_totalEnemiesOnMap.Add(new ContentFrostGiantEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentFrozenGuardianEnemy(null)); 
        m_totalEnemiesOnMap.Add(new ContentFrozenImpEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentGreatFrostlizardEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentGriffonEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentIcefisherEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentIceWurmEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentLordOfWinterEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentPolarWarriorEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentSabertoothWyvernEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentSnowprowlerEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentSpiralSerpentEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentValgulaEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentYetiEnemy(null));

        //--------------------------------------------------------------------------------------------------------//

        List<GameSpawnPoolData> defaultSpawnPoolData = new List<GameSpawnPoolData>();
        //Wave 1
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentSnowprowlerEnemy(null), 1, 1.5f, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentValgulaEnemy(null), 1, 1.5f, 1));

        //Wave 2
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentFrozenImpEnemy(null), 2, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentIcefisherEnemy(null), 2, 2, 0.33f));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentPolarWarriorEnemy(null), 2, 1.25f, 0.75f));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentSnowprowlerEnemy(null), 2, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentValgulaEnemy(null), 2, 1, 1));

        //Wave 3
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentChillflameBeastEnemy(null), 3, 2, 0.33f));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentFrozenImpEnemy(null), 3, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentGriffonEnemy(null), 3, 1.25f, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentIcefisherEnemy(null), 3, 1.5f, 0.75f));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentSnowprowlerEnemy(null), 3, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentPolarWarriorEnemy(null), 3, 1, 1));

        //Wave 4
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentChillflameBeastEnemy(null), 4, 1, 0.66f));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentFrozenImpEnemy(null), 4, 0.5f, 0.75f));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentGriffonEnemy(null), 4, 1, 2));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentIcefisherEnemy(null), 4, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentIceWurmEnemy(null), 4, 1, 1));

        //Wave 5
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentChillflameBeastEnemy(null), 5, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentFrostGiantEnemy(null), 5, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentFrozenImpEnemy(null), 5, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentGreatFrostlizardEnemy(null), 5, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentIceWurmEnemy(null), 5, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentSabertoothWyvernEnemy(null), 5, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentYetiEnemy(null), 5, 1, 1));

        //Wave 6
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentFrostGiantEnemy(null), 6, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentGreatFrostlizardEnemy(null), 6, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentIceWurmEnemy(null), 6, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentSabertoothWyvernEnemy(null), 6, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentYetiEnemy(null), 6, 1, 1));

        //--------------------------------------------------------------------------------------------------------//

        List<GameSpawnPoolData> frozenWatersSpawnPoolData = new List<GameSpawnPoolData>();

        //Wave 1

        //Wave 2
        frozenWatersSpawnPoolData.Add(new GameSpawnPoolData(new ContentCharybdisEnemy(null), 2, 0.5f, 0.5f));
        frozenWatersSpawnPoolData.Add(new GameSpawnPoolData(new ContentSpiralSerpentEnemy(null), 2, 0.25f, 1));

        //Wave 3
        frozenWatersSpawnPoolData.Add(new GameSpawnPoolData(new ContentCharybdisEnemy(null), 3, 0.5f, 0.75f));
        frozenWatersSpawnPoolData.Add(new GameSpawnPoolData(new ContentSpiralSerpentEnemy(null), 3, 0.25f, 0.75f));

        //Wave 4
        frozenWatersSpawnPoolData.Add(new GameSpawnPoolData(new ContentCharybdisEnemy(null), 4, 0.5f, 1));
        frozenWatersSpawnPoolData.Add(new GameSpawnPoolData(new ContentSpiralSerpentEnemy(null), 4, 0.25f, 0.5f));

        //Wave 5
        frozenWatersSpawnPoolData.Add(new GameSpawnPoolData(new ContentCharybdisEnemy(null), 5, 0.5f, 1));

        //Wave 6
        frozenWatersSpawnPoolData.Add(new GameSpawnPoolData(new ContentCharybdisEnemy(null), 6, 0.5f, 1));

        //--------------------------------------------------------------------------------------------------------//

        m_defaultSpawnPool = new GameSpawnPool(defaultSpawnPoolData);
        m_spawnPointSpawnPools.Add(new GameSpawnPool(frozenWatersSpawnPoolData, 0.5f));
        m_spawnPointSpawnPools.Add(new GameSpawnPool(defaultSpawnPoolData));
        m_spawnPointSpawnPools.Add(new GameSpawnPool(defaultSpawnPoolData));
        m_spawnPointSpawnPools.Add(new GameSpawnPool(defaultSpawnPoolData));
        m_spawnPointSpawnPools.Add(new GameSpawnPool(defaultSpawnPoolData));
    }
}
