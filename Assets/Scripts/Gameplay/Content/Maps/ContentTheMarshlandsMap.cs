using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTheMarshlandsMap : GameMap
{
    public ContentTheMarshlandsMap()
    {
        m_name = "The Marshlands";
        m_desc = "Why anyone would live in this marsh, I do not know. Beware the changing tides.";

        m_difficulty = MapDifficulty.Medium;

        m_id = 9;

        m_playerUnlockLevel = 1;

        Init();
    }

    protected override void FillMapEvents()
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.MapEvents))
        {
            AddMapEvent(new ContentMarshTideRiseEvent(1), 2);
            AddMapEvent(new ContentMarshTideLowerEvent(1), 3);
            AddMapEvent(new ContentMarshTideLowerEvent(2), 4);
            AddMapEvent(new ContentMarshTideRiseEvent(2), 6);
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
        m_spawnPool.Add(new ContentOrcShamanEnemy(null));
        m_spawnPool.Add(new ContentLavaRhinoUnit(null));
        m_spawnPool.Add(new ContentSlimeEnemy(null));
        m_spawnPool.Add(new ContentSnakeEnemy(null));
        m_spawnPool.Add(new ContentSpinnerEnemy(null));
        m_spawnPool.Add(new ContentToadEnemy(null));
        m_spawnPool.Add(new ContentWerewolfEnemy(null));
        m_spawnPool.Add(new ContentYetiEnemy(null));
        m_spawnPool.Add(new ContentShadeEnemy(null));
        m_spawnPool.Add(new ContentZombieEnemy(null));

        List<GameEnemyUnit> m_specificSpawnPools1 = new List<GameEnemyUnit>();
        m_specificSpawnPools1.AddRange(m_spawnPool);
        m_specificSpawnPools.Add(m_specificSpawnPools1);
    }
}
