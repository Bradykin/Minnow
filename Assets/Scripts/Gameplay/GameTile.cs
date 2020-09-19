using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTile : GameElementBase, ISave, ILoad<JsonGameTileData>, ICustomRecycle
{
    public Vector2Int m_gridPosition;

    public GameEntity m_occupyingEntity { get; private set; } //Always set this with PlaceEntity() or ClearEntity() to ensure proper data setup
    private GameBuildingBase m_building;
    private GameTerrainBase m_terrain;
    public bool m_event { get; set; }
    public GameSpawnPoint m_spawnPoint { get; private set; }
    private WorldTile m_worldTile;

    public bool m_isFog;
    public bool m_isSoftFog;
    public bool m_canPlace;

    public GameTile(WorldTile worldTile)
    {
        m_worldTile = worldTile;

        if (Constants.FogOfWar)
        {
            m_isFog = true;
        }
    }

    public void SwapEntity(GameEntity newEntity)
    {
        m_occupyingEntity = newEntity;
        newEntity.SetGameTile(this);
        newEntity.OnSummon();
    }

    public void PlaceEntity(GameEntity newEntity)
    {
        if (IsOccupied())
        {
            Debug.LogWarning("Placing new entity " + newEntity.m_name + " over existing entity " + m_occupyingEntity.m_name + ".");
        }

        m_occupyingEntity = newEntity;
        newEntity.SetGameTile(this);

        if (m_occupyingEntity.GetTeam() == Team.Player)
        {
            m_worldTile.ClearSurroundingFog(m_occupyingEntity.GetSightRange());

            if (m_event)
            {
                UIEventController.Instance.Init(GameEventFactory.GetRandomEvent(this));
                SetTerrain(GameTerrainFactory.GetCompletedEventTerrainClone(GetTerrain()));
                m_event = false;
            }
        }
    }

    public void PlaceBuilding(GameBuildingBase newBuilding)
    {
        if (HasBuilding())
        {
            Debug.LogWarning("Placing new building " + newBuilding.m_name + " over existing building " + m_building.m_name + ".");
        }

        m_worldTile.ClearSurroundingFog(newBuilding.m_sightRange);

        if (newBuilding.m_expandsPlaceRange)
        {
            m_worldTile.ExpandPlaceRange(newBuilding.m_sightRange);
        }

        m_building = newBuilding;
        newBuilding.SetGameTile(this)   ;
    }

    public void ClearEntity()
    {
        if (!IsOccupied())
        {
            Debug.LogWarning("Clearing entity on a tile, but no entity currently exists on this tile.");
        }

        m_occupyingEntity = null;
    }

    public void ClearBuilding()
    {
        if (!HasBuilding())
        {
            Debug.LogWarning("Clearing building on a tile, but no building currently exists on this tile.");
        }

        m_building = null;
    }

    public void ClearTerrain()
    {
        m_terrain = null;
    }

    public void ClearSpawnPoint()
    {
        m_spawnPoint = null;
    }

    public WorldTile GetWorldTile()
    {
        return m_worldTile;
    }

    public bool IsOccupied()
    {
        return m_occupyingEntity != null;
    }

    public bool HasAvailableEvent()
    {
        return m_event;
    }

    public bool HasBuilding()
    {
        return m_building != null;
    }

    public string GetName()
    {
        if (HasBuilding())
        {
            return m_building.m_name;
        }
        else
        {
            return GetTerrain().m_name;
        }
    }

    public string GetFocusPanelText()
    {
        if (HasBuilding())
        {
            return m_building.m_desc;
        }
        else
        {
            return GetTerrain().GetFocusPanelText();
        }
    }

    public Sprite GetIcon()
    {
        if (m_terrain == null)
            return null;
        
        if (HasBuilding())
        {
            return m_building.GetIcon();
        }

        /*else if (HasAvailableEvent())
        {
            if (m_event.m_isComplete)
            {
                return m_event.m_iconComplete;
            }
            else
            {
                return m_event.m_icon;
            }
        }*/

        else
        {
            return m_terrain.m_icon;
        }
    }

    public GameTerrainBase GetTerrain()
    {
        return m_terrain;
    }

    public void SetTerrain(GameTerrainBase newTerrain)
    {
        m_terrain = newTerrain;
    }

    public void SetSpawnPoint(GameSpawnPoint newSpawnPoint)
    {
        m_spawnPoint = newSpawnPoint;
        m_spawnPoint.m_tile = this;
    }

    public GameBuildingBase GetBuilding()
    {
        return m_building;
    }

    public int GetCostToPass(GameEntity checkerEntity)
    {
        if (checkerEntity != null)
        {
            bool canFly = checkerEntity.GetKeyword<GameFlyingKeyword>() != null;

            if (canFly)
            {
                return 1;
            }
        }

        if (HasBuilding())
        {
            return 2;
        }
        else
        {
            return m_terrain.GetCostToPass(checkerEntity);
        }
    }

    public bool IsPassable(GameEntity checkerEntity, bool letPassEnemies)
    {
        bool terrainImpassable = !m_terrain.IsPassable(checkerEntity);

        if (checkerEntity != null)
        {
            bool canFly = checkerEntity.GetKeyword<GameFlyingKeyword>() != null;

            if (canFly)
            {
                return true;
            }

            bool isOccupiedOpposingTeam = IsOccupied();
            if (isOccupiedOpposingTeam)
            {
                isOccupiedOpposingTeam = checkerEntity.GetTeam() != m_occupyingEntity.GetTeam();
            }

            bool hasNotDestroyedBuilding = HasBuilding();
            bool hasCastleBuilding = false;
            if (hasNotDestroyedBuilding)
            {
                hasNotDestroyedBuilding = m_building.m_isDestroyed;
                hasCastleBuilding = m_building is ContentCastleBuilding;
            }


            if (!letPassEnemies && isOccupiedOpposingTeam)
            {
                return false;
            }


            if (hasNotDestroyedBuilding || hasCastleBuilding)
            {
                return false;
            }
        }

        if (terrainImpassable)
        {
            return false;
        }

        return true;
    }

    public int GetDamageReduction(GameEntity checkerEntity)
    {
        if (checkerEntity != null)
        {
            if (checkerEntity.GetKeyword<GameFlyingKeyword>() != null)
            {
                return 0;
            }
        }

        if (HasBuilding())
        {
            return 0;
        }
        else
        {
            return m_terrain.m_damageReduction;
        }
    }

    //============================================================================================================//

    public string SaveToJson()
    {
        JsonGameTileData jsonData = new JsonGameTileData
        {
            gridPosition = m_gridPosition,
        };

        if (m_occupyingEntity != null)
        {
            jsonData.gameEntityData = m_occupyingEntity.SaveToJson();
        }
        if (m_building != null)
        {
            jsonData.gameBuildingData = m_building.SaveToJson();
        }
        if (m_terrain != null)
        {
            jsonData.gameTerrainData = m_terrain.SaveToJson();
        }
        if (m_event)
        {
            jsonData.gameEventData = "True";
        }
        if (m_spawnPoint != null)
        {
            jsonData.gameSpawnPointData = m_spawnPoint.SaveToJson();
        }

        var export = JsonUtility.ToJson(jsonData);

        return export;
    }

    public void LoadFromJson(JsonGameTileData jsonData)
    {
        m_gridPosition = jsonData.gridPosition;

        if (jsonData.gameEntityData != string.Empty)
        {
            JsonGameEntityData jsonGameEntityData = JsonUtility.FromJson<JsonGameEntityData>(jsonData.gameEntityData);
            if (jsonGameEntityData.team == (int)Team.Player)
                PlaceEntity(GameEntityFactory.GetEntityFromJson(jsonGameEntityData));
            else
                PlaceEntity(GameEntityFactory.GetEnemyFromJson(jsonGameEntityData, WorldController.Instance.m_gameController.m_gameOpponent));
        }

        if (jsonData.gameBuildingData != string.Empty)
        {
            JsonGameBuildingData jsonGameBuildingData = JsonUtility.FromJson<JsonGameBuildingData>(jsonData.gameBuildingData);
            PlaceBuilding(GameBuildingFactory.GetBuildingFromJson(jsonGameBuildingData));
        }

        if (jsonData.gameTerrainData != string.Empty)
        {
            JsonGameTerrainData jsonGameTerrainData = JsonUtility.FromJson<JsonGameTerrainData>(jsonData.gameTerrainData);
            SetTerrain(GameTerrainFactory.GetTerrainFromJson(jsonGameTerrainData));
        }

        if (jsonData.gameEventData != string.Empty)
        {
            JsonGameEventData jsonGameEventData = JsonUtility.FromJson<JsonGameEventData>(jsonData.gameEventData);
            m_event = true;
        }

        if (jsonData.gameSpawnPointData != string.Empty)
        {
            GameSpawnPoint gameSpawnPoint = new GameSpawnPoint();
            gameSpawnPoint.LoadFromJson(JsonUtility.FromJson<JsonGameSpawnPointData>(jsonData.gameSpawnPointData));
            SetSpawnPoint(gameSpawnPoint);
        }
    }

    public void CustomRecycle(params object[] args)
    {
        m_occupyingEntity = null;
        m_gridPosition = Vector2Int.zero;
        m_terrain = null;
        m_event = false;
        m_spawnPoint = null;
        m_building = null;
        m_worldTile = null;
    }
}
