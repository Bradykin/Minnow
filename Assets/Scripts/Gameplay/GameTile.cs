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
        newEntity.SetHealthStaminaValues();
    }

    public void PlaceEntity(GameEntity newEntity)
    {
        if (IsOccupied())
        {
            Debug.LogWarning("Placing new unit " + newEntity.m_name + " over existing unit " + m_occupyingEntity.m_name + ".");
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
            m_worldTile.ExpandPlaceRange(newBuilding.m_sightRange-1);
        }

        m_building = newBuilding;
        newBuilding.SetGameTile(this)   ;
    }

    public void ClearEntity()
    {
        if (!IsOccupied())
        {
            Debug.LogWarning("Clearing unit on a tile, but no entity currently exists on this tile.");
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
        else
        {
            return m_terrain.m_icon;
        }
    }

    public Sprite GetIconWhite()
    {
        if (m_terrain == null)
            return null;

        if (HasBuilding())
        {
            return m_building.GetIconWhite();
        }
        else
        {
            return m_terrain.m_iconWhite;
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

        int tileValue;
        bool buildingOverrideValue = true;

        if (checkerEntity != null)
        {
            if (GetTerrain().IsForest() && checkerEntity.GetKeyword<GameForestwalkKeyword>() != null)
            {
                tileValue = 1;
                buildingOverrideValue = false;
            }
            else if (GetTerrain().IsWater() && checkerEntity.GetKeyword<GameWaterwalkKeyword>() != null)
            {
                tileValue = 0;
                buildingOverrideValue = false;
            }
            else if (GetTerrain().IsMountain() && checkerEntity.GetKeyword<GameMountainwalkKeyword>() != null)
            {
                tileValue =  2;
                buildingOverrideValue = false;
            }
            else if (GetTerrain().IsHill() && checkerEntity.GetKeyword<GameMountainwalkKeyword>() != null)
            {
                tileValue =  1;
                buildingOverrideValue = false;
            }
            else
            {
                tileValue = GetTerrain().GetCostToPass();
            }
        }
        else
        {
            tileValue = GetTerrain().GetCostToPass();
        }

        if (HasBuilding() && buildingOverrideValue)
        {
            tileValue = Mathf.Max(tileValue, 2);
        }

        return tileValue;
    }

    public bool IsPassable(GameEntity checkerEntity, bool letPassEnemies)
    {
        if (checkerEntity != null && checkerEntity.m_shouldAlwaysPassEnemies)
        {
            letPassEnemies = true;
        }
        
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
            int damageReduction = m_terrain.m_damageReduction;

            if (damageReduction > 0 && checkerEntity != null && checkerEntity.GetTeam() == Team.Player && GameHelper.RelicCount<ContentNaturalProtectionRelic>() > 0)
            {
                damageReduction += damageReduction * GameHelper.RelicCount<ContentNaturalProtectionRelic>();
            }
            
            return damageReduction;
        }
    }

    public bool IsInFog()
    {
        return m_isFog || m_isSoftFog;
    }

    //============================================================================================================//

    public string SaveToJsonAsString()
    {
        JsonGameTileData jsonData = new JsonGameTileData
        {
            gridPosition = m_gridPosition,
        };

        if (m_occupyingEntity != null)
        {
            jsonData.gameEntityData = m_occupyingEntity.SaveToJsonAsString();
        }
        if (m_building != null)
        {
            jsonData.gameBuildingData = m_building.SaveToJsonAsString();
        }
        if (m_terrain != null)
        {
            jsonData.gameTerrainData = m_terrain.SaveToJsonAsString();
        }
        if (m_event)
        {
            jsonData.gameEventData = "True";
        }
        if (m_spawnPoint != null)
        {
            jsonData.gameSpawnPointData = m_spawnPoint.SaveToJsonAsString();
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
            //JsonGameEventData jsonGameEventData = JsonUtility.FromJson<JsonGameEventData>(jsonData.gameEventData);
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
