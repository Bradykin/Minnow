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
        UICard selectedCard = Globals.m_selectedCard;
        if (selectedCard != null)
        {
            if (selectedCard.m_card.IsValidToPlay(m_gameTile))
            {
                selectedCard.m_card.PlayCard(m_gameTile);
                Destroy(selectedCard.gameObject);
                Globals.m_selectedCard = null;
                return;
            }
        }

        UIEntity selectedEntity = Globals.m_selectedEntity;
        if (selectedEntity != null)
        {
            GameEntity gameEntity = (GameEntity)(selectedEntity.GetElement());
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

        if (Globals.m_selectedCard != null)
        {
            UIHelper.SetValidTintColor(m_tintRenderer, Globals.m_selectedCard.m_card.IsValidToPlay(m_gameTile));
        }
    }

    void OnMouseExit()
    {
        UIHelper.SetDefaultTintColor(m_tintRenderer);
    }
}
