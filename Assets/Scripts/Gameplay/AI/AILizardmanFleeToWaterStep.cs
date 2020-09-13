using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AILizardmanFleeToWaterStep : AIStep
{
    public AILizardmanFleeToWaterStep(AIGameEnemyEntity AIGameEnemyEntity) : base(AIGameEnemyEntity) { }

    public override void TakeStep()
    {
        if (m_AIGameEnemyEntity.m_gameEnemyEntity.m_curTile.GetTerrain().IsWater())
        {
            return;
        }

        List<GameTile> tilesAtDistance = WorldGridManager.Instance.GetSurroundingTiles(m_AIGameEnemyEntity.m_gameEnemyEntity.m_curTile, 1, 1);
        for (int i = 0; i < tilesAtDistance.Count; i++)
        {
            if (tilesAtDistance[i].GetTerrain().IsWater())
            {
                m_AIGameEnemyEntity.m_gameEnemyEntity.MoveTo(tilesAtDistance[i]);
            }
        }
    }
}