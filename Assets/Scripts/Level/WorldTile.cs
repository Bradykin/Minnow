using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public class WorldTile : WorldElementBase
{
    public SpriteRenderer m_tintRenderer;
    public GameTile m_gameTile { get; private set; } = new GameTile();

    private SpriteRenderer m_renderer;
    private UIEntity m_occupyingEntityObj;

    void Start()
    {
        m_renderer = GetComponent<SpriteRenderer>();

        m_gameElement = m_gameTile.m_terrain;
        m_renderer.color = m_gameElement.GetColor();
        UIHelper.SetDefaultTintColor(m_tintRenderer);

        gameObject.AddComponent<UITooltipGenerator>();
    }

    void Update()
    {
        if (m_gameTile.IsOccupied() && m_occupyingEntityObj == null)
        {
            m_occupyingEntityObj = FactoryManager.Instance.GetFactory<UIEntityFactory>().CreateObject<UIEntity>(this);
        }
        else if (!m_gameTile.IsOccupied() && m_occupyingEntityObj != null)
        {
            Recycler.Recycle<UIEntity>(m_occupyingEntityObj);
            m_occupyingEntityObj = null;
        }
    }

    public void Init(int x, int y)
    {
        m_gameTile.m_gridPosition = new Vector2Int(x, y);
    }

    void OnMouseDown()
    {
        UIPlayerCard selectedCard = Globals.m_selectedCard;
        if (selectedCard != null)
        {
            if (selectedCard.GetCard().IsValidToPlay(m_gameTile))
            {
                selectedCard.GetCard().PlayCard(m_gameTile);
                return;
            }
        }

        UIEntity selectedEntity = Globals.m_selectedEntity;
        if (selectedEntity != null)
        {
            GameEntityBase gameEntity = (GameEntityBase)(selectedEntity.GetElement());
            if (gameEntity.CanMoveTo(m_gameTile))
            {
                gameEntity.MoveTo(m_gameTile);
            }
        }
    }

    void OnMouseOver()
    {
        if (Globals.m_selectedEntity != null)
        {
           UIHelper.SetValidTintColor(m_tintRenderer, Globals.m_selectedEntity.CanReachWorldTileFromCurPosition(m_gameTile));
        }
    }

    void OnMouseExit()
    {
        if (Globals.m_selectedEntity != null)
        {
            UIHelper.SetDefaultTintColor(m_tintRenderer);
        }
    }
}
