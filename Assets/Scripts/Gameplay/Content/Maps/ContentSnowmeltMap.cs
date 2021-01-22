using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSnowmeltMap : GameMap
{
    public ContentSnowmeltMap()
    {
        m_name = "Snowmelt";
        m_desc = "Enemies swarm; but an abundance of ruins should help to hold this frozen wasteland.";

        m_difficulty = MapDifficulty.Medium;

        m_id = 3;

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
            AddMapEvent(new ContentSnapFreezeMapEvent(0), 3);
            AddMapEvent(new ContentSnapThawMapEvent(0), 4);

            AddMapEvent(new ContentSnapFreezeMapEvent(0), 5);
            AddMapEvent(new ContentSnapThawMapEvent(0), 6);
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
        m_totalEnemiesOnMap.Add(new ContentChillflameBeastEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentFrostGiantEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentFrozenGuardianEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentFrozenImpEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentGreatFrostlizardEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentGriffonEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentIceWurmEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentBerserkerEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentLordOfFrostEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentOrcEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentOrcShamanEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentPolarWarriorEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentSabertoothWyvernEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentSnowprowlerEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentValgulaEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentYetiEnemy(null));

        m_totalEnemiesOnMap.Add(new ContentHellhoundEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentBurningMonstrosityEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentPhoenixEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentLavaHellionEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentDemonMagicianEnemy(null));

        //--------------------------------------------------------------------------------------------------------//

        List<GameSpawnPoolData> defaultSpawnPoolData = new List<GameSpawnPoolData>();
        //Wave 1
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentBerserkerEnemy(null), 1, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentValgulaEnemy(null), 1, 2, 0.66f));

        //Wave 2
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentFrozenImpEnemy(null), 2, 1, 0.75f));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentBerserkerEnemy(null), 2, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentSnowprowlerEnemy(null), 2, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentValgulaEnemy(null), 2, 1, 1));

        //Wave 3
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentFrozenImpEnemy(null), 3, 1, 0.5f));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentGriffonEnemy(null), 3, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentBerserkerEnemy(null), 3, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentSnowprowlerEnemy(null), 3, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentPolarWarriorEnemy(null), 3, 1, 1));

        //Wave 4
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentChillflameBeastEnemy(null), 4, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentFrozenImpEnemy(null), 4, 0.5f, 0.33f));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentGriffonEnemy(null), 4, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentIceWurmEnemy(null), 4, 1.5f, 0.75f));

        //Wave 5
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentChillflameBeastEnemy(null), 5, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentFrozenImpEnemy(null), 5, 0.5f, 0.33f));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentGreatFrostlizardEnemy(null), 5, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentIceWurmEnemy(null), 5, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentSabertoothWyvernEnemy(null), 5, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentYetiEnemy(null), 5, 1, 1));

        //Wave 6
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentGreatFrostlizardEnemy(null), 6, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentIceWurmEnemy(null), 6, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentSabertoothWyvernEnemy(null), 6, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentYetiEnemy(null), 6, 1, 1));

        //--------------------------------------------------------------------------------------------------------//

        List<GameSpawnPoolData> volcanoIslandSpawnPoolData = new List<GameSpawnPoolData>();

        //Wave 1

        //Wave 2
        volcanoIslandSpawnPoolData.Add(new GameSpawnPoolData(new ContentHellhoundEnemy(null), 2, 1.5f, 0.66f));

        //Wave 3
        volcanoIslandSpawnPoolData.Add(new GameSpawnPoolData(new ContentHellhoundEnemy(null), 3, 1, 1));

        //Wave 4
        volcanoIslandSpawnPoolData.Add(new GameSpawnPoolData(new ContentChillflameBeastEnemy(null), 4, 1, 0.25f));
        volcanoIslandSpawnPoolData.Add(new GameSpawnPoolData(new ContentHellhoundEnemy(null), 4, 1, 1));

        //Wave 5
        volcanoIslandSpawnPoolData.Add(new GameSpawnPoolData(new ContentChillflameBeastEnemy(null), 5, 1, 1));
        volcanoIslandSpawnPoolData.Add(new GameSpawnPoolData(new ContentDemonMagicianEnemy(null), 5, 1, 1));
        volcanoIslandSpawnPoolData.Add(new GameSpawnPoolData(new ContentLavaHellionEnemy(null), 5, 1.5f, 1));
        volcanoIslandSpawnPoolData.Add(new GameSpawnPoolData(new ContentPhoenixEnemy(null), 5, 1.5f, 1));

        //Wave 6
        volcanoIslandSpawnPoolData.Add(new GameSpawnPoolData(new ContentChillflameBeastEnemy(null), 6, 1, 1));
        volcanoIslandSpawnPoolData.Add(new GameSpawnPoolData(new ContentDemonMagicianEnemy(null), 6, 1, 1));
        volcanoIslandSpawnPoolData.Add(new GameSpawnPoolData(new ContentLavaHellionEnemy(null), 6, 1.5f, 1));
        volcanoIslandSpawnPoolData.Add(new GameSpawnPoolData(new ContentPhoenixEnemy(null), 6, 1.5f, 1));

        //--------------------------------------------------------------------------------------------------------//

        m_defaultSpawnPool = new GameSpawnPool(defaultSpawnPoolData);
        m_spawnPointSpawnPools.Add(new GameSpawnPool(volcanoIslandSpawnPoolData));
    }
}