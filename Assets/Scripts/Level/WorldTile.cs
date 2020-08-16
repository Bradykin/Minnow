using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTile : WorldElementBase
{
    public GameObject m_entityPrefab;
    public SpriteRenderer m_tintRenderer;
    public GameTile m_gameTile { get; private set; } = new GameTile();

    private SpriteRenderer m_renderer;
    private GameObject m_occupyingEntityObj;

    void Start()
    {
        m_renderer = GetComponent<SpriteRenderer>();

        m_gameElement = m_gameTile.m_terrain;
        m_renderer.color = m_gameElement.GetColor();

        gameObject.AddComponent<UITooltipGenerator>();
    }

    void Update()
    {
        if (m_gameTile.IsOccupied() && m_occupyingEntityObj == null)
        {
            m_occupyingEntityObj = Instantiate(m_entityPrefab, UIHelper.GetScreenPositionForWorldGridElement(m_gameTile.m_gridPosition.x, m_gameTile.m_gridPosition.y), Quaternion.identity);

            GameObject uiParent = GameObject.Find("UI");
            if (uiParent != null)
            {
                m_occupyingEntityObj.transform.parent = uiParent.transform;
            }

            m_occupyingEntityObj.GetComponent<UIEntity>().Init(m_gameTile.m_occupyingEntity);
        }
        else if (!m_gameTile.IsOccupied() && m_occupyingEntityObj != null)
        {
            Destroy(m_occupyingEntityObj);
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
           UIHelper.SetValidGameobjectColor(m_tintRenderer, Globals.m_selectedEntity.CanReachWorldTileFromCurPosition(m_gameTile));
        }
    }

    void OnMouseExit()
    {
        if (Globals.m_selectedEntity != null)
        {
            m_tintRenderer.color = Color.white;
        }
    }
}
