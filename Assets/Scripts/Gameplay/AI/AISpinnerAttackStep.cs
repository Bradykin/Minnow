﻿using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpinnerAttackStep : AIStep
{
    public AISpinnerAttackStep(AIGameEnemyEntity AIGameEnemyEntity) : base(AIGameEnemyEntity) { }

    public override void TakeStep()
    {
        if (!m_AIGameEnemyEntity.m_gameEnemyEntity.HasAPToAttack())
        {
            return;
        }

        List<GameTile> adjacentTiles = m_AIGameEnemyEntity.m_gameEnemyEntity.GetGameTile().AdjacentTiles();
        bool hasSpentAP = false;

        for (int i = 0; i < adjacentTiles.Count; i++)
        {
            if (adjacentTiles[i].m_occupyingEntity != null && adjacentTiles[i].m_occupyingEntity.GetTeam() != Team.Enemy)
            {
                m_AIGameEnemyEntity.m_gameEnemyEntity.HitEntity(adjacentTiles[i].m_occupyingEntity, !hasSpentAP);
                hasSpentAP = true;
            }

            if (adjacentTiles[i].GetBuilding() != null)
            {
                m_AIGameEnemyEntity.m_gameEnemyEntity.HitBuilding(adjacentTiles[i].GetBuilding(), !hasSpentAP);
                hasSpentAP = true;
            }
        }
    }
}
