using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGridTile : WorldElementBase
{
    public Vector2Int GridPosition;
    public GameObject m_entityPrefab;
    public SpriteRenderer m_tintRenderer;

    private GameTile m_gameTile;
    private SpriteRenderer m_renderer;
    private GameObject m_occupyingEntity;

    public bool IsPassable => true;
    public int CostToPass => 1;

    void Start()
    {
        m_gameTile = new GameTile();
        m_renderer = GetComponent<SpriteRenderer>();

        m_gameElement = new GameGrassTerrain();

        m_renderer.color = m_gameElement.GetColor();

        gameObject.AddComponent<UITooltipGenerator>();
    }

    void Update()
    {
        if (m_gameTile.IsOccupied() && m_occupyingEntity == null)
        {
            m_occupyingEntity = Instantiate(m_entityPrefab, UIHelper.GetScreenPositionForWorldGridElement(GridPosition.x, GridPosition.y), Quaternion.identity);

            GameObject uiParent = GameObject.Find("UI");
            if (uiParent != null)
            {
                m_occupyingEntity.transform.parent = uiParent.transform;
            }

            m_occupyingEntity.GetComponent<UIEntity>().Init(m_gameTile.m_occupyingEntity);
        }
        else if (!m_gameTile.IsOccupied() && m_occupyingEntity != null)
        {
            Destroy(m_occupyingEntity);
        }
    }

    public void Init(int x, int y)
    {
        GridPosition = new Vector2Int(x, y);
    }

    void OnMouseDown()
    {
        UIPlayerCard selectedCard = Globals.m_selectedCard;
        if (selectedCard != null)
        {
            if (selectedCard.GetCard().IsValidToPlay(m_gameTile))
            {
                selectedCard.GetCard().PlayCard(m_gameTile);
                EngineLog.LogInfo("Placing on tile: " + GridPosition);
                return;
            }
        }

        UIEntity selectedEntity = Globals.m_selectedEntity;
        if (selectedEntity != null)
        {
            GameEntityBase gameEntity = (GameEntityBase)(selectedEntity.GetElement());
            if (gameEntity.CanMoveTo(this))
            {
                gameEntity.MoveTo(this);
            }
        }
    }

    void OnMouseOver()
    {
        if (Globals.m_selectedEntity != null)
        {
           UIHelper.SetValidGameobjectColor(m_tintRenderer, Globals.m_selectedEntity.CanReachWorldTileFromCurPosition(this));
        }
    }

    void OnMouseExit()
    {
        if (Globals.m_selectedEntity != null)
        {
            m_tintRenderer.color = Color.white;
        }
    }

    public bool IsOccupied()
    {
        return m_gameTile.IsOccupied();
    }

    public void PlaceEntity(GameEntityBase newEntity)
    {
        m_gameTile.PlaceEntity(newEntity);

        newEntity.m_curTile = this;
    }

    public void ClearEntity()
    {
        m_gameTile.ClearEntity();
    }
}
