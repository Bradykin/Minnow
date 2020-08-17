using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEntity : WorldElementBase
{
    public SpriteRenderer m_tintRenderer;
    public SpriteRenderer m_renderer;

    private bool m_isHovered;

    void Start()
    {
        UIHelper.SetDefaultTintColor(m_tintRenderer);

        gameObject.AddComponent<UITooltipGenerator>();
    }

    public void Init(GameEntity entity)
    {
        m_gameElement = entity;

        m_renderer.color = m_gameElement.GetColor();
        m_renderer.sprite = m_gameElement.m_icon;
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
        if (Globals.m_selectedCard == null)
        {
            UIHelper.SelectEntity(this);

            UIHelper.SetSelectTintColor(m_tintRenderer, Globals.m_selectedEntity == this);
        }
        else
        {
            if (Globals.m_selectedCard.m_card.IsValidToPlay(GetEntity()))
            {
                Globals.m_selectedCard.m_card.PlayCard(GetEntity());
                Destroy(Globals.m_selectedCard.gameObject);
                Globals.m_selectedCard = null;
            }
        }
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
            UIHelper.SetValidTintColor(m_tintRenderer, Globals.m_selectedCard.m_card.IsValidToPlay(GetEntity()));
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
