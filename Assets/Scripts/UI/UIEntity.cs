using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEntity : WorldElementBase
{
    public SpriteRenderer m_tintRenderer;

    private SpriteRenderer m_renderer;

    private bool m_isHovered;

    void Start()
    {
        m_renderer = GetComponent<SpriteRenderer>();
        m_renderer.color = m_gameElement.GetColor();
        UIHelper.SetDefaultTintColor(m_tintRenderer);

        gameObject.AddComponent<UITooltipGenerator>();
    }

    public void Init(GameEntity entity)
    {
        m_gameElement = entity;
    }

    void Update()
    {
        if (!m_isHovered)
        {
            UIHelper.SetSelectTintColor(m_tintRenderer, Globals.m_selectedEntity == this);
        }
    }

    void OnMouseDown()
    {
        Globals.m_selectedEntity = this;
        Globals.m_selectedCard = null;

        UIHelper.SetSelectTintColor(m_tintRenderer, Globals.m_selectedEntity == this);
    }

    public bool CanReachWorldTileFromCurPosition(GameTile toReach)
    {
        return ((GameEntity)m_gameElement).CanMoveTo(toReach);
    }

    void OnMouseOver()
    {
        m_isHovered = true;
        if (Globals.m_selectedCard != null)
        {
            UIHelper.SetValidTintColor(m_tintRenderer, Globals.m_selectedCard.GetCard().IsValidToPlay(GetEntity()));
        }
    }

    void OnMouseExit()
    {
        m_isHovered = false;
        if (Globals.m_selectedEntity != this)
        {
            UIHelper.SetDefaultTintColor(m_tintRenderer);
        }
    }

    public GameEntity GetEntity()
    {
        return (GameEntity)m_gameElement;
    }
}
