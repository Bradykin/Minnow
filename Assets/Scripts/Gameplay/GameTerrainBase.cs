using Game.Util;
using UnityEngine;

public abstract class GameTerrainBase : GameElementBase, ISave, ILoad<JsonGameTerrainData>
{
    public int m_damageReduction { get; protected set; }

    protected bool m_isPassable = true;
    protected int m_costToPass;
    protected int m_terrainImageNumber;

    //Only call these from the GameTile.  If you want these from outside, grab them from the GameTile functions instead of here.
    public bool IsPassable()
    {
        return m_isPassable;
    }

    public int GetCostToPass()
    {
        return m_costToPass;
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