using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMountainPassMap : GameMap
{
    public ContentMountainPassMap()
    {
        m_name = "Mountain Pass";
        m_desc = "Terrain is on your side, but numbers are not.  Hold the pass!";

        m_difficulty = MapDifficulty.Easy;

        m_id = 6;
        m_fogSpawningActive = false;

        Init();
    }

    public override bool TrySpawnElite(List<GameTile> tilesAtFogEdge)
    {
        GameOpponent gameOpponent = GameHelper.GetOpponent();
        List<GameEnemyUnit> immortalEnemyUnits = new List<GameEnemyUnit>();
        immortalEnemyUnits.Add(new ContentImmortalBladeEnemy(null));
        immortalEnemyUnits.Add(new ContentImmortalBowEnemy(null));
        immortalEnemyUnits.Add(new ContentImmortalBannerEnemy(null));

        if (GetFogSpawningActive())
        {
            if (GameHelper.GetGameController().m_currentTurnNumber >= (gameOpponent.m_eliteSpawnWaveModifier + Constants.SpawnEliteTurn))
            {
                GameEnemyUnit gameEnemyUnit = GameUnitFactory.GetRandomEliteEnemy(gameOpponent);
                gameOpponent.TryForceSpawnAtEdgeOfFog(gameEnemyUnit, tilesAtFogEdge);
                return true;
            }
        }
        else
        {
            for (int i = 0; i < gameOpponent.m_spawnPoints.Count; i++)
            {
                GameSpawnPoint temp = gameOpponent.m_spawnPoints[i];
                int randomIndex = UnityEngine.Random.Range(i, gameOpponent.m_spawnPoints.Count);
                gameOpponent.m_spawnPoints[i] = gameOpponent.m_spawnPoints[randomIndex];
                gameOpponent.m_spawnPoints[randomIndex] = temp;
            }

            if (GameHelper.GetGameController().m_currentTurnNumber >= (gameOpponent.m_eliteSpawnWaveModifier + Constants.SpawnEliteTurn))
            {
                GameEnemyUnit gameEnemyUnit = GameUnitFactory.GetRandomEliteEnemy(gameOpponent);
                for (int i = 0; i < gameOpponent.m_spawnPoints.Count; i++)
                {
                    if (!gameOpponent.m_spawnPoints[i].m_tile.IsPassable(gameEnemyUnit, false))
                    {
                        continue;
                    }

                    if (gameOpponent.TryForceSpawnAtSpawnPoint(gameEnemyUnit, gameOpponent.m_spawnPoints[i]))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public override bool TrySpawnBoss(List<GameTile> tilesAtFogEdge)
    {
        GameOpponent gameOpponent = GameHelper.GetOpponent();

        if (GetFogSpawningActive())
        {
            if (GameHelper.GetGameController().m_currentTurnNumber <= Constants.SpawnBossTurn && GameHelper.GetCurrentWaveNum() == Constants.FinalWaveNum)
            {
                GameEnemyUnit gameEnemyUnit = GameUnitFactory.GetRandomBossEnemy(gameOpponent);
                gameOpponent.TryForceSpawnAtEdgeOfFog(gameEnemyUnit, tilesAtFogEdge);
                return true;
            }
        }
        else
        {
            for (int i = 0; i < gameOpponent.m_spawnPoints.Count; i++)
            {
                GameSpawnPoint temp = gameOpponent.m_spawnPoints[i];
                int randomIndex = UnityEngine.Random.Range(i, gameOpponent.m_spawnPoints.Count);
                gameOpponent.m_spawnPoints[i] = gameOpponent.m_spawnPoints[randomIndex];
                gameOpponent.m_spawnPoints[randomIndex] = temp;
            }

            if (GameHelper.GetGameController().m_currentTurnNumber <= Constants.SpawnBossTurn && GameHelper.GetCurrentWaveNum() == Constants.FinalWaveNum)
            {
                GameEnemyUnit gameEnemyUnit = GameUnitFactory.GetRandomBossEnemy(gameOpponent);
                for (int i = 0; i < gameOpponent.m_spawnPoints.Count; i++)
                {
                    if (!gameOpponent.m_spawnPoints[i].m_tile.IsPassable(gameEnemyUnit, false))
                    {
                        continue;
                    }

                    if (gameOpponent.TryForceSpawnAtSpawnPoint(gameEnemyUnit, gameOpponent.m_spawnPoints[i]))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    protected override void FillMapEvents()
    {
        //No events, left blank by default.  No Chaos on this map.
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
}
