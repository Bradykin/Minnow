﻿using Game.Util;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTile : GameElementBase, ISave<JsonGameTileData>, ILoad<JsonGameTileData>, ICustomRecycle
{
    public Vector2Int m_gridPosition;

    private GameUnit m_occupyingUnit; //Always set this with PlaceUnit() or ClearUnit() to ensure proper data setup
    private GameBuildingBase m_building;
    private GameTerrainBase m_terrain;
    private GameSpawnPoint m_spawnPoint;
    private List<int> m_gameEventMarkers = new List<int>();

    private WorldTile m_worldTile;

    public bool m_isFog;
    public bool m_isSoftFog;
    public bool m_isFogBorder;

    public int m_numAllowPlacement = 0;
    public int m_numCauseStorm = 0;

    public GameWorldPerk m_gameWorldPerk;
    public GameBuildingBase m_destroyedBuilding;
    
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

            if (HasBuilding() && GetBuilding() is ContentRingOfProtectionBuilding && newUnit.GetDamageShieldKeyword() == null)
            {
                newUnit.AddKeyword(new GameDamageShieldKeyword(), false, false);
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

            List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(this, newUnit.GetSightRange(), 0);
            for (int i = 0; i < surroundingTiles.Count; i++)
            {
                List<GameTile> neighbourTiles = WorldGridManager.Instance.GetSurroundingGameTiles(surroundingTiles[i], Constants.WinterStormVisionRange, 0);
                bool keepRevealed = neighbourTiles.Any(t => (t.IsOccupied() && t.GetOccupyingUnit().GetTeam() == Team.Player) || 
                                                            (t.HasBuilding() && t.GetBuilding().GetTeam() == Team.Player) ||
                                                            !t.IsStorm());
                if (!keepRevealed)
                {
                    surroundingTiles[i].m_isFog = true;
                    surroundingTiles[i].m_isSoftFog = true;
                }
            }

            if (m_occupyingUnit is ContentRoyalCaravan)
            {
                m_worldTile.ExpandPlaceRange(2);
            }
        }

        GetWorldTile().TryAddOccupyingUnit();
    }

    public void SetDestroyedBuilding(GameBuildingBase prevBuilding)
    {
        m_destroyedBuilding = prevBuilding;
    }

    public void RestoreBuilding()
    {
        GameHelper.MakePlayerBuilding(this, m_destroyedBuilding);
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
            return;
        }

        if (m_occupyingUnit is ContentRoyalCaravan)
        {
            m_worldTile.ReducePlaceRange(2);
        }

        m_occupyingUnit = null;
    }

    public void ClearBuilding()
    {
        if (!HasBuilding())
        {
            Debug.LogWarning("Clearing building on a tile, but no building currently exists on this tile.");
            return;
        }

        if (GameHelper.GetPlayer() != null)
        {
            GameHelper.GetPlayer().RemoveControlledBuilding(m_building);
        }

        if (m_building.m_expandsPlaceRange)
        {
            m_worldTile.ReducePlaceRange(2);
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

    public bool HasCoverBuilding()
    {
        if (m_building == null)
        {
            return false;
        }

        if (m_building is ContentFarmlandBuilding)
        {
            return false;
        }

        return true;
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

        if (m_terrain.IsWater() && IsOccupied() && GetOccupyingUnit().GetFrostwalkKeyword() != null)
        {
            SetTerrain(new ContentIceTerrain(), clearBuilding);
            return;
        }
    }

    public GameSpawnPoint GetSpawnPoint()
    {
        return m_spawnPoint;
    }

    public bool HasSpawnPoint()
    {
        return m_spawnPoint != null;
    }

    public void SetSpawnPoint(GameSpawnPoint newSpawnPoint)
    {
        m_spawnPoint = newSpawnPoint;
        m_spawnPoint.m_tile = this;
    }

    public bool HasEventMarker(int toCheck)
    {
        return m_gameEventMarkers.Contains(toCheck);
    }

    public bool HasEventMarker()
    {
        return m_gameEventMarkers.Count > 0;
    }

    public List<int> GetEventMarkers()
    {
        return m_gameEventMarkers;
    }

    public void AddEventMarker(int toAdd)
    {
        m_gameEventMarkers.Add(toAdd);
    }

    public void ClearEventMarkers()
    {
        m_gameEventMarkers.Clear();
    }

    public GameUnit GetOccupyingUnit()
    {
        return m_occupyingUnit;
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
            if (GetTerrain().IsForest() && checkerUnit.m_instantForestMovement)
            {
                return 0;
            }

            if (GetTerrain().IsWater() && checkerUnit.m_instantWaterMovement)
            {
                return 0;
            }

            if (GetTerrain().IsDunes() && checkerUnit.m_instantDunesMovement)
            {
                return 0;
            }

            bool canFly = checkerUnit.GetFlyingKeyword() != null;

            if (canFly)
            {
                return 1;
            }

            if (checkerUnit.m_alwaysIgnoreDifficultTerrain)
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
            else if (GetTerrain().IsWater() && (checkerUnit.GetWaterwalkKeyword() != null || checkerUnit.GetWaterboundKeyword() != null || checkerUnit.GetFrostwalkKeyword() != null))
            {
                tileValue = GetTerrain().GetCostToPass();
                buildingOverrideValue = false;
            }
            else if (GetTerrain().IsMountain() && checkerUnit.GetMountainwalkKeyword() != null)
            {
                tileValue = 1;
                buildingOverrideValue = false;
            }
            else if (GetTerrain().IsHill() && checkerUnit.GetMountainwalkKeyword() != null)
            {
                tileValue =  1;
                buildingOverrideValue = false;
            }
            else if (GetTerrain().IsDunes() && checkerUnit.GetDuneswalkKeyword() != null)
            {
                tileValue = 1;
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

        if (checkerUnit != null)
        {
            if (checkerUnit is ContentRiverlurkerEnemy && !GetTerrain().IsWater())
            {
                tileValue *= 2;
            }
        }
        
        return tileValue;
    }

    public bool IsPassable(GameUnit checkerUnit, bool letPassEnemies)
    {
        if (checkerUnit == null)
        {
            if (HasBuilding() && GetBuilding() is ContentMountainGatewayBuilding)
            {
                return true;
            }

            if (!m_terrain.IsPassable(checkerUnit))
            {
                return false;
            }
        }
        else
        {
            if (checkerUnit.GetTeam() == Team.Player && GetTerrain().IsCorruption())
            {
                return false;
            }
            
            if (checkerUnit is ContentLordOfEruptionsEnemy && !GetTerrain().IsWater())
            {
                return true;
            }

            if (HasBuilding() && checkerUnit.GetTeam() != GetBuilding().GetTeam())
            {
                if (!letPassEnemies)
                {
                    return false;
                }
            }

            if (checkerUnit.GetFlyingKeyword() != null)
            {
                return true;
            }

            if (HasBuilding() && checkerUnit.GetTeam() == GetBuilding().GetTeam() && GetBuilding() is ContentMountainGatewayBuilding)
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

                return false;
            }
        }

        return true;
    }

    public bool CanPlace()
    {
        return m_numAllowPlacement > 0;
    }

    public bool IsStorm()
    {
        return m_numCauseStorm > 0;
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

        if (GameHelper.IsInLevelBuilder())
        {
            jsonData.isFog = true;
            jsonData.isSoftFog = false;
            jsonData.isFogBorder = false;
        }

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
        if (m_gameWorldPerk != null)
        {
            jsonData.gameWorldPerkData = m_gameWorldPerk.SaveToJson();
        }

        return jsonData;
    }

    public void LoadFromJson(JsonGameTileData jsonData)
    {
        m_gridPosition = new Vector2Int(jsonData.gridPositionX, jsonData.gridPositionY);

        if (Globals.loadingRun)
        {
            m_isFog = jsonData.isFog;
            m_isSoftFog = jsonData.isSoftFog;
            m_isFogBorder = jsonData.isFogBorder;
        }

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

        if (jsonData.gameWorldPerkData != null)
        {
            m_gameWorldPerk = new GameWorldPerk(this);
            m_gameWorldPerk.LoadFromJson(jsonData.gameWorldPerkData);
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
