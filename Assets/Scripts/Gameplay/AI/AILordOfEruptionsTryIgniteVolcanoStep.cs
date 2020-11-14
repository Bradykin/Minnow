using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AILordOfEruptionsTryIgniteVolcanoStep : AIStep
{
    public AILordOfEruptionsTryIgniteVolcanoStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    public override IEnumerator TakeStepCoroutine()
    {
        if (m_AIGameEnemyUnit.m_gameEnemyUnit.GetCurStamina() < m_AIGameEnemyUnit.m_gameEnemyUnit.GetMaxStamina())
        {
            yield break;
        }

        List<GameTile> unitSurroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), 1, 0);
        for (int i = 0; i < unitSurroundingTiles.Count; i++)
        {
            if (!(unitSurroundingTiles[i].GetTerrain() is ContentVolcanoInactiveTerrain))
            {
                continue;
            }

            UICameraController.Instance.SmoothCameraTransitionToGameObject(m_AIGameEnemyUnit.m_gameEnemyUnit.GetWorldTile().gameObject);
            while (UICameraController.Instance.IsCameraSmoothing())
            {
                yield return null;
            }

            UIHelper.CreateWorldElementNotification($"{m_AIGameEnemyUnit.m_gameEnemyUnit.GetName()} ignites a nearby volcano!", false, m_AIGameEnemyUnit.m_gameEnemyUnit.GetWorldTile().gameObject);
            m_AIGameEnemyUnit.m_gameEnemyUnit.SpendStamina(m_AIGameEnemyUnit.m_gameEnemyUnit.GetCurStamina());

            int volcanoEventMarker = unitSurroundingTiles[i].m_gameEventMarkers[0];

            for (int c = 0; c < WorldGridManager.Instance.m_gridArray.Length; c++)
            {
                GameTile gameTile = WorldGridManager.Instance.m_gridArray[c].GetGameTile();

                if (gameTile.m_gameEventMarkers.Contains(volcanoEventMarker))
                {
                    if (gameTile.GetTerrain().IsVolcano())
                    {
                        gameTile.SetTerrain(GameTerrainFactory.GetVolcanoEruptTerrainClone(gameTile.GetTerrain()));

                        List<WorldTile> volcanoSurroundingTiles = WorldGridManager.Instance.GetSurroundingWorldTiles(gameTile.GetWorldTile(), 2, 0);
                        for (int k = 0; k < volcanoSurroundingTiles.Count; k++)
                        {
                            volcanoSurroundingTiles[k].ClearFog();
                        }
                    }
                    else
                    {
                        if (gameTile.HasBuilding() && gameTile.GetBuilding().GetTeam() == Team.Player)
                        {
                            gameTile.GetBuilding().Die();
                        }

                        gameTile.SetTerrain(GameTerrainFactory.GetTerrainClone(new ContentLavaFieldActiveTerrain()));
                    }
                }
            }

            yield return new WaitForSeconds(0.5f);
            yield break;
        }

        yield break;
    }

    public override void TakeStepInstant()
    {
        if (m_AIGameEnemyUnit.m_gameEnemyUnit.GetCurStamina() < m_AIGameEnemyUnit.m_gameEnemyUnit.GetMaxStamina())
        {
            return;
        }

        List<GameTile> unitSurroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), 1, 0);
        for (int i = 0; i < unitSurroundingTiles.Count; i++)
        {
            if (!(unitSurroundingTiles[i].GetTerrain() is ContentVolcanoInactiveTerrain))
            {
                continue;
            }

            UIHelper.CreateWorldElementNotification($"{m_AIGameEnemyUnit.m_gameEnemyUnit.GetName()} ignites a nearby volcano!", false, m_AIGameEnemyUnit.m_gameEnemyUnit.GetWorldTile().gameObject);
            m_AIGameEnemyUnit.m_gameEnemyUnit.SpendStamina(m_AIGameEnemyUnit.m_gameEnemyUnit.GetCurStamina());

            int volcanoEventMarker = unitSurroundingTiles[i].m_gameEventMarkers[0];

            for (int c = 0; c < WorldGridManager.Instance.m_gridArray.Length; c++)
            {
                GameTile gameTile = WorldGridManager.Instance.m_gridArray[c].GetGameTile();

                if (gameTile.m_gameEventMarkers.Contains(volcanoEventMarker))
                {
                    if (gameTile.GetTerrain().IsVolcano())
                    {
                        gameTile.SetTerrain(GameTerrainFactory.GetVolcanoEruptTerrainClone(gameTile.GetTerrain()));

                        List<WorldTile> volcanoSurroundingTiles = WorldGridManager.Instance.GetSurroundingWorldTiles(gameTile.GetWorldTile(), 2, 0);
                        for (int k = 0; k < volcanoSurroundingTiles.Count; k++)
                        {
                            volcanoSurroundingTiles[k].ClearFog();
                        }
                    }
                    else
                    {
                        if (gameTile.HasBuilding() && gameTile.GetBuilding().GetTeam() == Team.Player)
                        {
                            gameTile.GetBuilding().Die();
                        }

                        gameTile.SetTerrain(GameTerrainFactory.GetTerrainClone(new ContentLavaFieldActiveTerrain()));
                    }
                }
            }

            return;
        }
    }
}