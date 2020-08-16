using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEntity : WorldElementBase
{
    public SpriteRenderer m_tintRenderer;

    private SpriteRenderer m_renderer;

    void Start()
    {
        m_renderer = GetComponent<SpriteRenderer>();
        UIHelper.SetDefaultTintColor(m_tintRenderer);

        gameObject.AddComponent<UITooltipGenerator>();
    }

    public void Init(GameEntityBase entity)
    {
        m_gameElement = entity;
    }

    void Update()
    {
        UIHelper.SetSelectTintColor(m_tintRenderer, Globals.m_selectedEntity == this);
    }

    void OnMouseDown()
    {
        Globals.m_selectedEntity = this;
        Globals.m_selectedCard = null;
    }

    public bool CanReachWorldTileFromCurPosition(GameTile toReach)
    {
        return ((GameEntityBase)m_gameElement).CanMoveTo(toReach);
    }
}
