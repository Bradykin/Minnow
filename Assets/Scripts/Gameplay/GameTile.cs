using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTile : GameElementBase
{
    public GameEntity m_occupyingEntity { get; private set; } //Always set this with PlaceEntity() or ClearEntity() to ensure proper data setup
    public Vector2Int m_gridPosition;
    private GameTerrainBase m_terrain;
    public GameEvent m_event { get; private set; }
    private GameBuildingBase m_building;

    public WorldTile m_curTile;
    public bool m_isFog;
    public bool m_canPlace;

    public GameTile(WorldTile curTile)
    {
        m_curTile = curTile;

        if (Constants.FogOfWar)
        {
            m_isFog = true;
        }

        HandleTerrain();

        if (m_terrain is ContentRuinsTerrain)
        {
            m_event = GameEventFactory.GetRandomEvent(this);
        }
    }

    public void PlaceEntity(GameEntity newEntity)
    {
        if (IsOccupied())
        {
            Debug.LogWarning("Placing new entity " + newEntity.m_name + " over existing entity " + m_occupyingEntity.m_name + ".");
        }

        m_occupyingEntity = newEntity;
        newEntity.m_curTile = this;

        if (m_occupyingEntity.GetTeam() == Team.Player)
        {
            m_curTile.ClearSurroundingFog(m_occupyingEntity.GetSightRange());

            if (m_event != null && !m_event.m_isComplete)
            {
                UIEventController.Instance.Init(m_event);
            }
        }
    }

    public void PlaceBuilding(GameBuildingBase newBuilding)
    {
        if (HasBuilding())
        {
            Debug.LogWarning("Placing new building " + newBuilding.m_name + " over existing building " + m_building.m_name + ".");
        }

        m_curTile.ClearSurroundingFog(newBuilding.m_sightRange);

        if (newBuilding.m_expandsPlaceRange)
        {
            m_curTile.ExpandPlaceRange(newBuilding.m_sightRange);
        }

        m_building = newBuilding;
    }

    public void ClearEntity()
    {
        if (!IsOccupied())
        {
            Debug.LogWarning("Clearing entity on a tile, but no entity currently exists on this tile.");
        }

        m_occupyingEntity = null;
    }

    public bool IsOccupied()
    {
        return m_occupyingEntity != null;
    }

    public bool HasAvailableEvent()
    {
        return m_event != null;
    }

    public bool HasBuilding()
    {
        return m_building != null;
    }

    private void HandleTerrain()
    {
        int terrainVal = Random.Range(1, 101);
        if (terrainVal <= Constants.PercentChanceForTerrainGrasslands)
        {
            m_terrain = new ContentGrassTerrain();
        }
        else if (terrainVal <= Constants.PercentChanceForTerrainGrasslands + Constants.PercentChanceForTerrainForest)
        {
            m_terrain = new ContentForestTerrain();
        }
        else if (terrainVal <= Constants.PercentChanceForTerrainGrasslands + Constants.PercentChanceForTerrainForest + Constants.PercentChanceForTerrainMountain)
        {
            m_terrain = new ContentMountainTerrain();
        }
        else if (terrainVal <= Constants.PercentChanceForTerrainGrasslands + Constants.PercentChanceForTerrainForest + Constants.PercentChanceForTerrainMountain + Constants.PercentChanceForTerrainWater)
        {
            m_terrain = new ContentWaterTerrain();
        }
        else if (terrainVal <= Constants.PercentChanceForTerrainGrasslands + Constants.PercentChanceForTerrainForest + Constants.PercentChanceForTerrainMountain + Constants.PercentChanceForTerrainWater + Constants.PercentChanceForTerrainRuins)
        {
            m_terrain = new ContentRuinsTerrain();
        }
    }

    public void ClearEvent()
    {
        m_event = null;
    }

    public void ClearBuilding()
    {
        m_building = null;
    }

    public Sprite GetIcon()
    {
        if (HasBuilding())
        {
            return m_building.GetIcon();
        }
        else if (HasAvailableEvent())
        {
            if (m_event.m_isComplete)
            {
                return m_event.m_iconComplete;
            }
            else
            {
                return m_event.m_icon;
            }
        }
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

    public GameBuildingBase GetBuilding()
    {
        return m_building;
    }

    public int GetCostToPass(GameEntity checkerEntity)
    {
        bool canFly = checkerEntity.GetKeywordHolder().GetKeyword<GameFlyingKeyword>() != null;

        if (canFly)
        {
            return 1;
        }

        if (HasBuilding())
        {
            return 2;
        }
        else
        {
            return m_terrain.GetCostToPass();
        }
    }

    public bool IsPassable(GameEntity checkerEntity)
    {
        if (checkerEntity == null)
        {
            return false;
        }

        bool canFly = checkerEntity.GetKeywordHolder().GetKeyword<GameFlyingKeyword>() != null;

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
        if (hasNotDestroyedBuilding)
        {
            hasNotDestroyedBuilding = m_building.m_isDestroyed;
        }

        bool terrainImpassable = !m_terrain.IsPassable();

        if (isOccupiedOpposingTeam)
        {
            return false;
        }

        if (hasNotDestroyedBuilding)
        {
            return false;
        }

        if (terrainImpassable)
        {
            return false;
        }

        return true;
    }

    public int GetDamageReduction()
    {
        if (HasBuilding())
        {
            return 4;
        }
        else
        {
            return m_terrain.m_damageReduction;
        }
    }
}
