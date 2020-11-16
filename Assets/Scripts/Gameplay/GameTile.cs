using Game.Util;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public int m_numAllowPlacement = 0;

    public GameWorldPerk m_gameWorldPerk;
    
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

            ContentLordOfShadowsEnemy lordOfShadowsEnemy = GameHelper.GetBoss<ContentLordOfShadowsEnemy>();
            if (lordOfShadowsEnemy != null)
            {
                WorldGridManager.Instance.EndIntermissionFogUpdate();
            }
            else
            {
                m_worldTile.ClearSurroundingFog(m_occupyingUnit.GetSightRange());
            }

            if (m_gameWorldPerk != null)
            {
                if (m_gameWorldPerk.IsGold())
                {
                    UIHelper.CreateWorldElementNotification("+" + m_gameWorldPerk.GetGoldVal() + " gold!", true, m_worldTile.gameObject);
                }

                m_gameWorldPerk.Trigger();

                m_gameWorldPerk = null;
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
            m_worldTile.ClearSurroundingFog(newBuilding.GetSightRange());
        }

        if (newBuilding.m_expandsPlaceRange)
        {
            m_worldTile.ExpandPlaceRange(2);
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

    public override string GetName()
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
        return (HasBuilding() && GetBuilding().GetName() == new ContentPowerCrystalBuilding().GetName());
    }

    public void SetTerrain(GameTerrainBase newTerrain, bool clearBuilding = false)
    {
        if (HasBuilding() && clearBuilding)
        {
            ClearBuilding();
        }

        m_terrain = newTerrain;

        if (!GameHelper.IsInLevelBuilder())
        {
            if (newTerrain.IsEventTerrain())
            {
                m_gameWorldPerk = new GameWorldPerk(GameEventFactory.GetRandomEvent(this));
                m_terrain = GameTerrainFactory.GetCompletedEventTerrainClone(m_terrain);
            }
        }
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
        int tileValue;
        bool buildingOverrideValue = true;

        if (checkerUnit != null)
        {
            bool canFly = checkerUnit.GetFlyingKeyword() != null;

            if (canFly)
            {
                return 1;
            }

            if (checkerUnit is ContentLordOfEruptionsEnemy)
            {
                return 1;
            }

            if (checkerUnit.GetTeam() == Team.Player && GameHelper.HasRelic<ContentNaturalProtectionRelic>())
            {
                tileValue = 1;
            }
            else if (GetTerrain().IsForest() && checkerUnit.GetForestwalkKeyword() != null)
            {
                tileValue = 1;
                buildingOverrideValue = false;
            }
            else if (GetTerrain().IsLava() && checkerUnit.GetLavawalkKeyword() != null)
            {
                tileValue = 1;
                buildingOverrideValue = false;
            }
            else if (GetTerrain().IsWater() && (checkerUnit.GetWaterwalkKeyword() != null || checkerUnit.GetWaterboundKeyword() != null))
            {
                if (checkerUnit.m_instantWaterMovement)
                {
                    tileValue = 0;
                }
                else
                {
                    tileValue = GetTerrain().GetCostToPass();
                }
                buildingOverrideValue = false;
            }
            else if (GetTerrain().IsMountain() && checkerUnit.GetMountainwalkKeyword() != null)
            {
                tileValue = GetTerrain().GetCostToPass();
                buildingOverrideValue = false;
            }
            else if (GetTerrain().IsHill() && checkerUnit.GetMountainwalkKeyword() != null)
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
            tileValue = 1;
        }

        ContentLordOfChaosEnemy lordOfChaosEnemy = GameHelper.GetBoss<ContentLordOfChaosEnemy>();
        if (lordOfChaosEnemy != null && lordOfChaosEnemy.m_currentChaosWarpAbility == ContentLordOfChaosEnemy.ChaosWarpAbility.StaminaCostAttackDecreaseMoveCostIncrease)
        {
            tileValue++;
        }

        return tileValue;
    }

    public bool IsPassable(GameUnit checkerUnit, bool letPassEnemies)
    {
        if (checkerUnit == null)
        {
            if (!m_terrain.IsPassable(checkerUnit))
            {
                return false;
            }
        }
        else
        {
            if (checkerUnit is ContentLordOfEruptionsEnemy && !GetTerrain().IsWater())
            {
                return true;
            }

            if (checkerUnit.GetFlyingKeyword() != null)
            {
                return true;
            }

            if (!m_terrain.IsPassable(checkerUnit))
            {
                return false;
            }

            if (HasBuilding() && checkerUnit.GetTeam() == GetBuilding().GetTeam())
            {
                return true;
            }

            if (HasBuilding() && checkerUnit.GetTeam() != GetBuilding().GetTeam())
            {
                return false;
            }

            if (IsOccupied() && checkerUnit.GetTeam() == m_occupyingUnit.GetTeam())
            {
                return true;
            }

            if (IsOccupied() && checkerUnit.GetTeam() != m_occupyingUnit.GetTeam())
            {
                if (letPassEnemies)
                {
                    return true;
                }

                if (checkerUnit.m_shouldAlwaysPassEnemies)
                {
                    return true;
                }

                bool isZombiePass = (checkerUnit is ContentZombie || checkerUnit is ContentZombieEnemy) && (m_occupyingUnit is ContentZombie || m_occupyingUnit is ContentZombieEnemy);

                if (isZombiePass)
                {
                    return true;
                }

                return false;
            }
        }

        return true;
    }

    public bool CanPlace()
    {
        return m_numAllowPlacement > 0;
    }

    //============================================================================================================//

    public JsonGameTileData SaveToJson()
    {
        JsonGameTileData jsonData = new JsonGameTileData
        {
            gridPositionX = m_gridPosition.x,
            gridPositionY = m_gridPosition.y,
            isFog = m_isFog,
            isSoftFog = m_isSoftFog,
            isFogBorder = m_isFogBorder
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
        //m_isFog = jsonData.isFog;
        //m_isSoftFog = jsonData.isSoftFog;
        //m_isFogBorder = jsonData.isFogBorder;

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
