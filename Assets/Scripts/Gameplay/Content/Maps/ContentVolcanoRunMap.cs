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
        m_spawnPool.Add(new ContentYetiEnemy(null));
        m_spawnPool.Add(new ContentShadeEnemy(null));
        m_spawnPool.Add(new ContentZombieEnemy(null));
    }
}
