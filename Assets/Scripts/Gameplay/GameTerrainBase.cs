using Game.Util;
using System;
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
public abstract class GameTerrainBase : GameElementBase, ISave, ILoad<JsonGameTerrainData>
{
    public int m_damageReduction { get; protected set; }
    public int m_rangeModifier { get; protected set; }

    protected int m_costToPass;
    protected int m_terrainImageNumber;
    protected int m_maxTerrainImageNumber;
    protected string m_focusPanelText;

    protected bool m_isPassable = true;
    protected bool m_isPlains;
    protected bool m_isForest;
    protected bool m_isHill;
    protected bool m_isMountain;
    protected bool m_isWater;

    protected bool m_isEventTerrain;
    protected bool m_isCave;
    protected bool m_isVolcano;

    protected bool m_isHot;
    protected bool m_isCold;
    protected bool m_isBurned;
    protected bool m_canBurn;

    protected Type m_burnedTerrainType;
    protected Type m_unburnedTerrainType;
    protected Type m_completedEventType;
    protected Type m_addedEventType;

    public Sprite m_iconWhite;

    //Only call these from the GameTile.  If you want these from outside, grab them from the GameTile functions instead of here.
    public bool IsPassable(GameEntity checkerEntity)
    {
        if (checkerEntity != null)
        {
            if (IsWater() && checkerEntity.GetKeyword<GameWaterwalkKeyword>() != null)
            {
                return true;
            }

            if (IsMountain() && checkerEntity.GetKeyword<GameMountainwalkKeyword>() != null)
            {
                return true;
            }
        }

        return m_isPassable;
    }

    public int GetCostToPass()
    {
        return m_costToPass;
    }

    public bool IsPlains()
    {
        return m_isPlains;
    }

    public bool IsForest()
    {
        return m_isForest;
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

    public bool IsFlatTerrain()
    {
        if (m_isForest)
        {
            return false;
        }

        if (m_isWater)
        {
            return false;
        }

        if (m_isMountain)
        {
            return false;
        }

        if (m_isHill)
        {
            return false;
        }

        return true;
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

    public Type GetBurnedTerrainType()
    {
        return m_burnedTerrainType;
    }

    public Type GetCompletedEventTerrainType()
    {
        return m_completedEventType;
    }

    public string GetFocusPanelText()
    {
        return m_focusPanelText;
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

    public virtual string GenerateDescription()
    {
        string description = "";

        if (m_isPassable)
        {
            description += m_costToPass + " AP movement.";
        }
        else
        {
            description += "Impassable.";
        }

        if (m_damageReduction > 0)
        {
            description += "\nEntities on this tile take " + m_damageReduction + " less damage.";
        }

        if (m_rangeModifier > 0)
        {
            description += "\nRanged entities on this tile get +" + m_rangeModifier + " increased range.";
        }

        return description;
    }

    public virtual string GenerateFocusText()
    {
        string description = "";

        if (m_rangeModifier > 0)
        {
            description += "Ranged entities on this tile get +" + m_rangeModifier + " increased range.";
        }

        return description;
    }

    //============================================================================================================//

    public string SaveToJsonAsString()
    {
        JsonGameTerrainData jsonData = new JsonGameTerrainData
        {
            name = m_name,
            terrainImageNumber = m_terrainImageNumber
        };
        
        var export = JsonUtility.ToJson(jsonData);

        return export;
    }

    public void LoadFromJson(JsonGameTerrainData jsonData)
    {
        m_terrainImageNumber = jsonData.terrainImageNumber;
        
        SetSprite(m_terrainImageNumber);
    }
}