using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIIcefisherIcebreakStep : AIStep
{
    public AIIcefisherIcebreakStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    public override IEnumerator TakeStepCoroutine()
    {
        if (m_AIGameEnemyUnit.m_gameEnemyUnit.GetCurStamina() < m_AIGameEnemyUnit.m_gameEnemyUnit.GetMaxStamina())
        {
            yield break;
        }

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), 1);
        if (m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile().GetTerrain().IsWater() || surroundingTiles.Any(t => t.GetTerrain().IsWater()))
        {
            yield break;
        }

        List<GameTile> surroundingIceCrackedTiles = surroundingTiles.Where(t => t.GetTerrain().IsIceCracked()).ToList();
        if (surroundingIceCrackedTiles.Count == 0)
        {
            yield break;
        }

        UICameraController.Instance.SmoothCameraTransitionToGameObject(m_AIGameEnemyUnit.m_gameEnemyUnit.GetWorldTile().gameObject);
        while (UICameraController.Instance.IsCameraSmoothing())
        {
            yield return null;
        }

        int index = Random.Range(0, surroundingIceCrackedTiles.Count);

        UIHelper.CreateWorldElementNotification($"{m_AIGameEnemyUnit.m_gameEnemyUnit.GetName()} smashes nearby ice to look for fish!", false, m_AIGameEnemyUnit.m_gameEnemyUnit.GetWorldTile().gameObject);
        m_AIGameEnemyUnit.m_gameEnemyUnit.SpendStamina(m_AIGameEnemyUnit.m_gameEnemyUnit.GetCurStamina());

        GameTile centerTileToBreak = surroundingIceCrackedTiles[index];
        centerTileToBreak.SetTerrain(GameTerrainFactory.GetIceCrackedTerrainClone(centerTileToBreak.GetTerrain()));
        List<GameTile> surroundingTilesOfTileToBreak = WorldGridManager.Instance.GetSurroundingGameTiles(centerTileToBreak, 1);
        for (int i = 0; i < surroundingTilesOfTileToBreak.Count; i++)
        {
            if (surroundingTilesOfTileToBreak[i].GetTerrain().IsIce() && !surroundingTilesOfTileToBreak[i].GetTerrain().IsIceCracked())
            {
                surroundingTilesOfTileToBreak[i].SetTerrain(GameTerrainFactory.GetIceCrackedTerrainClone(surroundingTilesOfTileToBreak[i].GetTerrain()));
            }
        }

        yield return new WaitForSeconds(0.5f);
    }

    public override void TakeStepInstant()
    {
        if (m_AIGameEnemyUnit.m_gameEnemyUnit.GetCurStamina() < m_AIGameEnemyUnit.m_gameEnemyUnit.GetMaxStamina())
        {
            return;
        }

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), 1, 0);
        if (surroundingTiles.Any(t => t.GetTerrain().IsWater()))
        {
            return;
        }

        List<GameTile> surroundingIceCrackedTiles = surroundingTiles.Where(t => t.GetTerrain().IsIceCracked()).ToList();
        if (surroundingIceCrackedTiles.Count == 0)
        {
            return;
        }

        int index = Random.Range(0, surroundingIceCrackedTiles.Count);

        UIHelper.CreateWorldElementNotification($"{m_AIGameEnemyUnit.m_gameEnemyUnit.GetName()} smashes nearby ice to look for fish!", false, m_AIGameEnemyUnit.m_gameEnemyUnit.GetWorldTile().gameObject);
        m_AIGameEnemyUnit.m_gameEnemyUnit.SpendStamina(m_AIGameEnemyUnit.m_gameEnemyUnit.GetCurStamina());

        GameTile centerTileToBreak = surroundingIceCrackedTiles[index];
        centerTileToBreak.SetTerrain(GameTerrainFactory.GetIceCrackedTerrainClone(centerTileToBreak.GetTerrain()));
        List<GameTile> surroundingTilesOfTileToBreak = WorldGridManager.Instance.GetSurroundingGameTiles(centerTileToBreak, 1);
        for (int i = 0; i < surroundingTilesOfTileToBreak.Count; i++)
        {
            if (surroundingTilesOfTileToBreak[i].GetTerrain().IsIce() && !surroundingTilesOfTileToBreak[i].GetTerrain().IsIceCracked())
            {
                surroundingTilesOfTileToBreak[i].SetTerrain(GameTerrainFactory.GetIceCrackedTerrainClone(surroundingTilesOfTileToBreak[i].GetTerrain()));
            }
        }
    }
}
