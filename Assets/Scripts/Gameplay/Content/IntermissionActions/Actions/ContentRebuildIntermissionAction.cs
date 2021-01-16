using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRebuildIntermissionAction : GameActionIntermission
{
    public ContentRebuildIntermissionAction()
    {
        m_name = "Rebuild";
        m_desc = "Rebuild a destroyed building.";

        m_icon = UIHelper.GetIconIntermissionAction(m_name);
    }

    public override void Activate()
    {
        UIHelper.SelectAction(this);
    }

    public override bool IsValidToSpawn()
    {
        if (!base.IsValidToSpawn())
        {
            return false;
        }

        WorldTile[] worldTiles = WorldGridManager.Instance.m_gridArray;

        for (int i = 0; i < worldTiles.Length; i++)
        {
            if (worldTiles[i].GetGameTile().m_destroyedBuilding != null)
            {
                return true;
            }
        }

        return false;
    }
}