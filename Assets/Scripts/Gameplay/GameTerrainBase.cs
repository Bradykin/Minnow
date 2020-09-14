using Game.Util;
using UnityEngine;

public abstract class GameTerrainBase : GameElementBase, ISave, ILoad<JsonGameTerrainData>
{
    public int m_damageReduction { get; protected set; }
    public int m_rangeModifier { get; protected set; }

    protected bool m_isPassable = true;
    protected int m_costToPass;
    protected int m_terrainImageNumber;
    protected string m_focusPanelText;

    protected bool m_isForest;
    protected bool m_isHill;
    protected bool m_isMountain;
    protected bool m_isWater;

    protected bool m_isHot;
    protected bool m_isCold;

    protected bool m_isEventTerrain;
    protected bool m_isCave;
    protected bool m_isVolcano;

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

    public int GetCostToPass(GameEntity checkerEntity)
    {
        if (checkerEntity != null)
        {
            if (IsWater() && checkerEntity.GetKeyword<GameWaterwalkKeyword>() != null)
            {
                return 0;
            }

            if (IsMountain() && checkerEntity.GetKeyword<GameMountainwalkKeyword>() != null)
            {
                return 2;
            }

            if (IsHill() && checkerEntity.GetKeyword<GameMountainwalkKeyword>() != null)
            {
                return 1;
            }
        }

        return m_costToPass;
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

    public string GetFocusPanelText()
    {
        return m_focusPanelText;
    }

    public void SetSprite(int x)
    {
        if (x >= 0 && x <= 4)
            m_terrainImageNumber = x;

        m_icon = UIHelper.GetIconTerrain(m_name, m_terrainImageNumber);
    }

    public void SetNextSprite()
    {
        if (m_terrainImageNumber == 4)
            m_terrainImageNumber = 1;
        else
            m_terrainImageNumber++;

        m_icon = UIHelper.GetIconTerrain(m_name, m_terrainImageNumber);
    }

    //============================================================================================================//

    public string SaveToJson()
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