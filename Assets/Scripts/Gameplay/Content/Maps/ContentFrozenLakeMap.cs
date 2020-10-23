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
        m_spawnPool.Add(new ContentAngryBirdEnemy(null));
        m_spawnPool.Add(new ContentDarkWarriorEnemy(null));
        m_spawnPool.Add(new ContentLichEnemy(null));
        m_spawnPool.Add(new ContentLizardmanEnemy(null));
        m_spawnPool.Add(new ContentMobolaEnemy(null));
        m_spawnPool.Add(new ContentOrcEnemy(null));
        m_spawnPool.Add(new ContentSiegebreakerUnit(null));
        m_spawnPool.Add(new ContentSlimeEnemy(null));
        m_spawnPool.Add(new ContentShadeEnemy(null));
        m_spawnPool.Add(new ContentSnakeEnemy(null));
        m_spawnPool.Add(new ContentSpinnerEnemy(null));
        m_spawnPool.Add(new ContentToadEnemy(null));
        m_spawnPool.Add(new ContentWerewolfEnemy(null));
        m_spawnPool.Add(new ContentZombieEnemy(null));
        m_spawnPool.Add(new ContentOrcShamanEnemy(null));
        m_spawnPool.Add(new ContentYetiEnemy(null));


        List<GameEnemyUnit> specificSpawnPool1 = new List<GameEnemyUnit>();
        specificSpawnPool1.AddRange(m_spawnPool);

        List<GameEnemyUnit> specificSpawnPool2 = new List<GameEnemyUnit>();
        specificSpawnPool2.AddRange(m_spawnPool);

        List<GameEnemyUnit> specificSpawnPool3 = new List<GameEnemyUnit>();
        specificSpawnPool3.AddRange(m_spawnPool);

        List<GameEnemyUnit> specificSpawnPool4 = new List<GameEnemyUnit>();
        specificSpawnPool4.AddRange(m_spawnPool);

        m_specificSpawnPools.Add(specificSpawnPool1);
        m_specificSpawnPools.Add(specificSpawnPool2);
        m_specificSpawnPools.Add(specificSpawnPool3);
        m_specificSpawnPools.Add(specificSpawnPool4);
    }
}
