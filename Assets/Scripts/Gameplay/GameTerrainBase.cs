using Game.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

//Checklist for setting up new terrain tiles:
//Add all image variants to Terrain file with naming scheme "Name1, Name2"
//Add tiny image variants to Terrain file with naming scheme "Name1W, Name2W"
//Create class inheriting from GameTerrainBase with naming scheme "ContentNameTerrain". In this class, do the following:
//Set m_name value to "Name"
//Configure the following terrain statistics: m_damageReduction, m_rangeModifier, m_costToPass
//Add an m_desc. Describe the effects of the above statistics
//Configure the follow icon choosing stats: m_maxTerrainImageNumber(the number of variants of the terrain image), m_terrainImageNumber(Set to Random.Range(0, m_maxTerrainImageNumber + 1)
//set m_icon = UIHelper.GetIconTerrain(m_name, m_terrainImageNumber);
//Review the following list of "terrain attributes", and set any applicable to true:
//m_isPlains, m_isForest, m_isHill, m_isMountain, m_isWater, m_isHot, m_isCold, m_isEventTerrain, m_isCave, m_isVolcano, m_isBurned, m_canBurn;
//If applicable, add a type value for the type of terrain this should become in the following scenarios: m_burnedTerrainType, m_unburnedTerrainType, m_completedEventType, m_addedEventType
//Add new class to the appropriate lists in GameTerrainFactory.Init
public abstract class GameTerrainBase : GameElementBase, ISave<JsonGameTerrainData>, ILoad<JsonGameTerrainData>
{
    public enum CoverType
    {
        None,
        Cover
    }

    public enum TerrainMovementType
    {
        Normal,
        Difficult,
        Extreme
    }

    protected CoverType m_coverType;
    protected TerrainMovementType m_movementType;

    public int m_rangeModifier { get; protected set; }
    public int m_staminaRegenLoss { get; protected set; }

    protected int m_terrainImageNumber;
    protected int m_maxTerrainImageNumber;

    protected bool m_isPassable = true;
    protected bool m_isPlains;
    protected bool m_isForest;
    protected bool m_isHill;
    protected bool m_isMountain;
    protected bool m_isWater;

    protected bool m_isEventTerrain;
    protected bool m_isCave; // Not currently used
    protected bool m_isVolcano;
    protected bool m_isLava;
    protected bool m_isIce;
    protected bool m_isIceCracked;
    protected bool m_isDunes;
    protected bool m_isCorruption;
    protected bool m_isWaterSource; // Not currently used

    protected bool m_isHot; // Not currently used
    protected bool m_isCold;
    protected bool m_isBurned;
    protected bool m_canBurn;

    protected Type m_burnedTerrainType;
    protected Type m_unburnedTerrainType;
    protected Type m_completedEventTerrainType;
    protected Type m_addedEventTerrainType;
    protected Type m_iceCrackedTerrainType;
    protected Type m_volcanoEruptTerrainType;
    protected Type m_marshTideRiseTerrainType;
    protected Type m_marshTideLowerTerrainType;

    public Sprite m_iconWhite;

    //Only call these from the GameTile.  If you want these from outside, grab them from the GameTile functions instead of here.
    public bool IsPassable(GameUnit checkerUnit)
    {
        if (checkerUnit != null)
        {
            if (IsWater() && (checkerUnit.GetWaterwalkKeyword() != null || checkerUnit.GetWaterboundKeyword() != null || checkerUnit.GetFrostwalkKeyword() != null))
            {
                return true;
            }

            if (!IsWater() && checkerUnit.GetWaterboundKeyword() != null)
            {
                return false;
            }

            if (IsMountain() && checkerUnit.GetMountainwalkKeyword() != null)
            {
                return true;
            }
        }

        return m_isPassable;
    }

    public int GetCostToPass()
    {
        bool lordOfChaosDifficultTerrainSwapActive = false;
        ContentLordOfChaosEnemy lordOfChaosEnemy = GameHelper.GetBoss<ContentLordOfChaosEnemy>();
        if (lordOfChaosEnemy != null && lordOfChaosEnemy.m_currentChaosWarpAbility == ContentLordOfChaosEnemy.ChaosWarpAbility.NormalDifficultTerrainCostReversal)
        {
            lordOfChaosDifficultTerrainSwapActive = true;
        }

        if (m_movementType == TerrainMovementType.Normal)
        {
            if (lordOfChaosDifficultTerrainSwapActive)
            {
                return 2;
            }

            return 1;
        }
        else if (m_movementType == TerrainMovementType.Difficult)
        {
            if (lordOfChaosDifficultTerrainSwapActive)
            {
                return 1;
            }

            return 2;
        }
        else
        {
            return 3;
        }
    }

    public TerrainMovementType GetMovementType()
    {
        return m_movementType;
    }

    public CoverType GetCoverType()
    {
        return m_coverType;
    }

    public bool IsPlains()
    {
        return m_isPlains;
    }

    public bool IsForest()
    {
        return m_isForest;
    }

    public bool IsCold()
    {
        return m_isCold;
    }

    public bool IsMountain()
    {
        return m_isMountain;
    }

    public bool IsHill()
    {
        return m_isHill;
    }

    public bool IsWater()
    {
        return m_isWater;
    }

    public bool IsVolcano()
    {
        return m_isVolcano;
    }

    public bool IsLava()
    {
        return m_isLava;
    }

    public bool IsIce()
    {
        return m_isIce;
    }

    public bool IsIceCracked()
    {
        return m_isIceCracked;
    }

    public bool IsDunes()
    {
        return m_isDunes;
    }

    public bool IsCorruption()
    {
        return m_isCorruption;
    }

    public bool IsWaterSource()
    {
        return m_isWater || m_isWaterSource;
    }

    public bool IsEventTerrain()
    {
        return m_isEventTerrain;
    }

    public bool IsBurned()
    {
        return m_isBurned;
    }

    public bool CanBurn()
    {
        return m_canBurn && !m_isBurned && m_burnedTerrainType != null;
    }

    public bool CanUnburn()
    {
        return m_isBurned && m_unburnedTerrainType != null;
    }

    public Type GetBurnedTerrainType()
    {
        return m_burnedTerrainType;
    }

    public Type GetUnburnedTerrainType()
    {
        return m_unburnedTerrainType;
    }

    public Type GetCompletedEventTerrainType()
    {
        return m_completedEventTerrainType;
    }

    public Type GetIceCrackedTerrainType()
    {
        return m_iceCrackedTerrainType;
    }

    public Type GetVolcanoEruptTerrainType()
    {
        return m_volcanoEruptTerrainType;
    }

    public Type GetMarshTideRiseTerrainType()
    {
        return m_marshTideRiseTerrainType;
    }

    public Type GetMarshTideLowerTerrainType()
    {
        return m_marshTideLowerTerrainType;
    }

    protected void LateInit()
    {
        m_icon = UIHelper.GetIconTerrain(m_name + m_terrainImageNumber);
        m_iconWhite = UIHelper.GetIconTerrain(m_name + m_terrainImageNumber + "W");
    }

    public void SetSprite(int x)
    {
        if (x >= 0 && x <= 4)
            m_terrainImageNumber = x;

        m_icon = UIHelper.GetIconTerrain(m_name + m_terrainImageNumber);
        m_iconWhite = UIHelper.GetIconTerrain(m_name + m_terrainImageNumber + "W");
    }

    public void SetNextSprite()
    {
        if (m_terrainImageNumber == 4)
            m_terrainImageNumber = 1;
        else
            m_terrainImageNumber++;

        m_icon = UIHelper.GetIconTerrain(m_name + m_terrainImageNumber);
        m_iconWhite = UIHelper.GetIconTerrain(m_name + m_terrainImageNumber + "W");
    }

    public int GetTerrainImageNumber()
    {
        return m_terrainImageNumber;
    }

    public virtual string GetDesc()
    {
        string description = "";

        if (m_rangeModifier > 0)
        {
            description += "\n\nRanged units on this tile get +" + m_rangeModifier + " increased range.\n";
        }

        if (this is ContentIceTerrain)
        {
            description += "\n\nIf an adjacent cracked tile breaks, this tile will break into cracked ice.\n";
        }

        if (this is ContentIceCrackedTerrain)
        {
            description += "\n\nIf any unit dies while on this tile, this tile and all adjacent ice and cracked tiles will break.\nIf a unit is standing on this tile when it breaks, they die.\n";
        }

        if (this is ContentLavaFieldActiveTerrain)
        {
            description += $"\n\nUnits standing on this tile take {Constants.LavaFieldDamageDealt} damage at the end of their turn.";
        }

        if (IsDunes())
        {
            description += $"\n\n<b>Spell Cover</b> - Units standing on this tile take {Constants.SandDuneMagicDamageReductionPercentage}% less damage from spells.";
        }
        
        return description;
    }

    //============================================================================================================//

    public JsonGameTerrainData SaveToJson()
    {
        JsonGameTerrainData jsonData = new JsonGameTerrainData
        {
            name = m_name,
            terrainImageNumber = m_terrainImageNumber
        };

        return jsonData;
    }

    public void LoadFromJson(JsonGameTerrainData jsonData)
    {
        m_terrainImageNumber = jsonData.terrainImageNumber;
        
        SetSprite(m_terrainImageNumber);
    }
}