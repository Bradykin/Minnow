using System.Collections;
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
        bool isValid = true;
        if (Globals.m_selectedEntity != null)
        {
            isValid = false;
        }

        if (Globals.m_selectedCard != null)
        {
            isValid = Globals.m_selectedCard.m_card.IsValidToPlay(GetBuilding());
        }

        UIHelper.SetValidTintColor(m_tintRenderer, isValid);
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
        string desc = m_gameElement.m_desc + "\n" + "Health: " + GetBuilding().m_curHealth + "/" + GetBuilding().m_maxHealth;
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip(m_gameElement.m_name, desc));
        UIHelper.CreateTerrainTooltip(GetBuilding().m_curTile.m_gameTile.m_terrain);
    }
}
