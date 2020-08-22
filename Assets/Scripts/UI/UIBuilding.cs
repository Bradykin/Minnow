﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public class UIBuilding : WorldElementBase
{
    public SpriteRenderer m_tintRenderer;
    public SpriteRenderer m_renderer;
    public SpriteMask m_mask;

    void Start()
    {
        UIHelper.SetDefaultTintColor(m_tintRenderer);
    }

    void Update()
    {
    }

    public void Init(GameBuildingBase building, WorldTile tile)
    {
        m_gameElement = building;
        building.SetWorldTile(tile);

        m_renderer.sprite = GetBuilding().GetIcon();
    }

    void OnMouseOver()
    {
        UIHelper.SetValidTintColor(m_tintRenderer, true);
    }

    void OnMouseExit()
    {
        UIHelper.SetDefaultTintColor(m_tintRenderer);
    }

    public GameBuildingBase GetBuilding()
    {
        return (GameBuildingBase)m_gameElement;
    }

    public override void HandleTooltip()
    {
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip(m_gameElement.m_name, m_gameElement.m_desc));
    }
}
