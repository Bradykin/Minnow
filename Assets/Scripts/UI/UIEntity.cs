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

        gameObject.AddComponent<UITooltipGenerator>();
    }

    public void Init(GameEntityBase entity)
    {
        m_gameElement = entity;
    }

    void Update()
    {
        UIHelper.SelectGameobject(m_tintRenderer, Globals.m_selectedEntity == this);
    }

    void OnMouseDown()
    {
        Globals.m_selectedEntity = this;
        Globals.m_selectedCard = null;
    }

    public bool CanReachWorldTileFromCurPosition(WorldGridTile toReach)
    {
        return ((GameEntityBase)m_gameElement).CanMoveTo(toReach);
    }
}
