using Game.Util;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTile : GameElementBase, ISave<JsonGameTileData>, ILoad<JsonGameTileData>, ICustomRecycle
{
    public Vector2Int m_gridPosition;

    public GameUnit m_occupyingUnit { get; private set; } //Always set this with PlaceUnit() or ClearUnit() to ensure proper data setup
    private GameBuildingBase m_building;
    private GameTerrainBase m_terrain;
    public GameSpawnPoint m_spawnPoint { get; private set; }
    private WorldTile m_worldTile;

    public List<int> m_gameEventMarkers = new List<int>();

    public bool m_isFog;
    public bool m_isSoftFog;
    public bool m_isFogBorder;
    public bool m_canPlace;

    public GameTile(WorldTile worldTile)
    {
        m_worldTile = worldTile;

        if (Constants.FogOfWar)
        {
            m_isFog = true;
        }
    }

    public void SwapUnit(GameUnit newUnit)
    {
        m_occupyingUnit = newUnit;
        newUnit.SetGameTile(this);
        newUnit.SetHealthStaminaValues();
    }

    public void PlaceUnit(GameUnit newUnit)
    {
        if (IsOccupied())
        {
            Debug.LogWarning("Placing new unit " + newUnit.GetName() + " over existing unit " + m_occupyingUnit.GetName() + ".");
        }

        m_occupyingUnit = newUnit;
        newUnit.SetGameTile(this);

        if (m_occupyingUnit.GetTeam() == Team.Player)
        {
            m_worldTile.ClearSurroundingFog(m_occupyingUnit.GetSightRange());

            if (GetTerrain().IsEventTerrain())
            {
                UIEventController.Instance.Init(GameEventFactory.GetRandomEvent(this));
                SetTerrain(GameTerrainFactory.GetCompletedEventTerrainClone(GetTerrain()));
            }
        }
    }

    public void PlaceBuilding(GameBuildingBase newBuilding)
    {
        if (HasBuilding())
        {
            Debug.LogWarning("Placing new building " + newBuilding.GetName() + " over existing building " + m_building.GetName() + ".");
        }

        if (newBuilding.GetTeam() == Team.Player)
        {
            m_worldTile.ClearSurroundingFog(newBuilding.m_sightRange);
        }

        if (newBuilding.m_expandsPlaceRange)
        {
            m_worldTile.ExpandPlaceRange(newBuilding.m_sightRange-1);
        }

        m_building = newBuilding;
        newBuilding.SetGameTile(this);
    }

    public void ClearUnit()
    {
        if (!IsOccupied())
        {
            Debug.LogWarning("Clearing unit on a tile, but no unit currently exists on this tile.");
        }

        m_occupyingUnit = null;
    }

    public void ClearBuilding()
    {
        if (!HasBuilding())
        {
            Debug.LogWarning("Clearing building on a tile, but no building currently exists on this tile.");
        }

        if (GameHelper.GetPlayer() != null)
        {
            GameHelper.GetPlayer().RemoveControlledBuilding(m_building);
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
        return m_occupyingUnit != null;
    }

    public bool HasBuilding()
    {
        return m_building != null;
    }

    public string GetName()
    {
        if (HasBuilding())
        {
            return m_building.GetName();
        }
        else
        {
            return GetTerrain().GetName();
        }
    }

    public string GetFocusPanelText()
    {
        if (HasBuilding())
        {
            return m_building.GetDesc();
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

    public bool IsSpecialSoftFogTile()
    {
        return GetTerrain().IsEventTerrain() || (HasBuilding() && GetBuilding().GetName() == new ContentPowerCrystalBuilding().GetName());
    }

    public void SetTerrain(GameTerrainBase newTerrain, bool clearBuilding = false)
    {
        if (HasBuilding() && clearBuilding)
        {
            ClearBuilding();
        }

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

    public int GetCostToPass(GameUnit checkerUnit)
    {
        if (checkerUnit != null)
        {
            bool canFly = checkerUnit.GetKeyword<GameFlyingKeyword>() != null;

            if (canFly)
            {
                return 1;
            }
        }

        int tileValue;
        bool buildingOverrideValue = true;

        if (checkerUnit != null)
        {
            if (GetTerrain().IsForest() && checkerUnit.GetKeyword<GameForestwalkKeyword>() != null)
            {
                tileValue = 1;
                buildingOverrideValue = false;
            }
            else if (GetTerrain().IsWater() && checkerUnit.GetKeyword<GameWaterwalkKeyword>() != null)
            {
                if (checkerUnit.m_instantWaterMovement)
                {
                    tileValue = 0;
                }
                else
                {
                    tileValue = 1;
                }
                buildingOverrideValue = false;
            }
            else if (GetTerrain().IsMountain() && checkerUnit.GetKeyword<GameMountainwalkKeyword>() != null)
            {
                tileValue =  2;
                buildingOverrideValue = false;
            }
            else if (GetTerrain().IsHill() && checkerUnit.GetKeyword<GameMountainwalkKeyword>() != null)
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

    public bool IsPassable(GameUnit checkerUnit, bool letPassEnemies)
    {
        if (checkerUnit != null && checkerUnit.m_shouldAlwaysPassEnemies)
        {
            letPassEnemies = true;
        }
        
        bool terrainImpassable = !m_terrain.IsPassable(checkerUnit);

        if (checkerUnit != null)
        {
            bool canFly = checkerUnit.GetKeyword<GameFlyingKeyword>() != null;

            if (canFly)
            {
                return true;
            }

            bool isOccupiedOpposingTeam = IsOccupied();
            if (isOccupiedOpposingTeam)
            {
                isOccupiedOpposingTeam = checkerUnit.GetTeam() != m_occupyingUnit.GetTeam();
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

    public int GetDamageReduction(GameUnit checkerUnit)
    {
        if (checkerUnit != null)
        {
            if (checkerUnit.GetKeyword<GameFlyingKeyword>() != null)
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

            if (damageReduction > 0 && checkerUnit != null && checkerUnit.GetTeam() == Team.Player && GameHelper.RelicCount<ContentNaturalProtectionRelic>() > 0)
            {
                damageReduction += damageReduction * GameHelper.RelicCount<ContentNaturalProtectionRelic>();
            }
            
            return damageReduction;
        }
    }

    //============================================================================================================//

    public JsonGameTileData SaveToJson()
    {
        JsonGameTileData jsonData = new JsonGameTileData
        {
            gridPositionX = m_gridPosition.x,
            gridPositionY = m_gridPosition.y
        };

        if (m_occupyingUnit != null)
        {
            jsonData.gameUnitData = m_occupyingUnit.SaveToJson();
        }
        if (m_building != null)
        {
            jsonData.gameBuildingData = m_building.SaveToJson();
        }
        if (m_terrain != null)
        {
            jsonData.gameTerrainData = m_terrain.SaveToJson();
        }
        if (m_spawnPoint != null)
        {
            jsonData.gameSpawnPointData = m_spawnPoint.SaveToJson();
        }
        if (m_gameEventMarkers != null)
        {
            jsonData.gameEventMarkers = m_gameEventMarkers;
        }

        return jsonData;
    }

    public void LoadFromJson(JsonGameTileData jsonData)
    {
        m_gridPosition = new Vector2Int(jsonData.gridPositionX, jsonData.gridPositionY);

        if (jsonData.gameTerrainData != null)
        {
            SetTerrain(GameTerrainFactory.GetTerrainFromJson(jsonData.gameTerrainData));
        }

        if (jsonData.gameUnitData != null)
        {
            if (jsonData.gameUnitData.team == (int)Team.Player)
                PlaceUnit(GameUnitFactory.GetUnitFromJson(jsonData.gameUnitData));
            else
                PlaceUnit(GameUnitFactory.GetEnemyFromJson(jsonData.gameUnitData, WorldController.Instance.m_gameController.m_gameOpponent));
        }

        if (jsonData.gameBuildingData != null)
        {
            PlaceBuilding(GameBuildingFactory.GetBuildingFromJson(jsonData.gameBuildingData));
        }

        if (jsonData.gameSpawnPointData != null)
        {
            GameSpawnPoint gameSpawnPoint = new GameSpawnPoint();
            gameSpawnPoint.LoadFromJson(jsonData.gameSpawnPointData);
            SetSpawnPoint(gameSpawnPoint);
        }

        if (jsonData.gameEventMarkers != null)
        {
            m_gameEventMarkers = jsonData.gameEventMarkers;
        }
        else
        {
            m_gameEventMarkers = new List<int>();
        }
    }

    public void CustomRecycle(params object[] args)
    {
        m_occupyingUnit = null;
        m_gridPosition = Vector2Int.zero;
        m_terrain = null;
        m_spawnPoint = null;
        m_building = null;
        m_worldTile = null;
    }
}
