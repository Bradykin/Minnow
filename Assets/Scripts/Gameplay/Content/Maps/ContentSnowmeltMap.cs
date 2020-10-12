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

        m_playerUnlockLevel = 2;

        Init();
    }

    protected override void FillMapEvents()
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddMapEvents))
        {
            AddMapEvent(new ContentDrySeasonMapEvent(), 3);
            AddMapEvent(new ContentDrySeasonMapEvent(), 5);
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
        m_spawnPool.Add(new ContentSiegebreakerUnit(null));
        m_spawnPool.Add(new ContentSlimeEnemy(null));
        m_spawnPool.Add(new ContentSnakeEnemy(null));
        m_spawnPool.Add(new ContentSpinnerEnemy(null));
        m_spawnPool.Add(new ContentToadEnemy(null));
        m_spawnPool.Add(new ContentWerewolfEnemy(null));
        m_spawnPool.Add(new ContentZombieEnemy(null));

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.ModifySpawnPool))
        {
            m_spawnPool.Add(new ContentShadeEnemy(null));
            m_spawnPool.Add(new ContentYetiEnemy(null));
        }
    }
}