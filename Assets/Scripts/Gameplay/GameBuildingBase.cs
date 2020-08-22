using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameBuildingBase : GameElementBase
{
    protected WorldTile m_tile;

    public void SetWorldTile(WorldTile worldTile)
    {
        m_tile = worldTile;
    }

    public virtual Sprite GetIcon()
    {
        return m_icon;
    }

    public virtual void EndTurn() { }
}
