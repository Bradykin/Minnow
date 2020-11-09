using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AICharybdisIcebreakStep : AIStep
{
    public AICharybdisIcebreakStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    public override IEnumerator TakeStep(bool shouldYield)
    {
        if (m_AIGameEnemyUnit.m_gameEnemyUnit.GetCurStamina() < m_AIGameEnemyUnit.m_gameEnemyUnit.GetMaxStamina())
        {
            yield break;
        }

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), 1);
        if (!surroundingTiles.Any(t => t.GetTerrain().IsIce()))
        {
            yield break;
        }    

        if (shouldYield)
        {
            UICameraController.Instance.SmoothCameraTransitionToGameObject(m_AIGameEnemyUnit.m_gameEnemyUnit.GetWorldTile().gameObject);
            while (UICameraController.Instance.IsCameraSmoothing())
            {
                yield return null;
            }
        }

        UIHelper.CreateWorldElementNotification($"{m_AIGameEnemyUnit.m_gameEnemyUnit.GetName()} smashes nearby ice!", false, m_AIGameEnemyUnit.m_gameEnemyUnit.GetWorldTile().gameObject);
        m_AIGameEnemyUnit.m_gameEnemyUnit.SpendStamina(m_AIGameEnemyUnit.m_gameEnemyUnit.GetCurStamina());

        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            if (surroundingTiles[i].GetTerrain().IsIce())
            {
                surroundingTiles[i].SetTerrain(GameTerrainFactory.GetIceCrackedTerrainClone(surroundingTiles[i].GetTerrain()));
            }
        }

        if (shouldYield)
        {
            yield return new WaitForSeconds(0.5f);
        }
    }
}
