using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGridTile : WorldElementBase
{
    public Vector2Int GridPosition;
    public GameObject m_entityPrefab;
    public GameTile m_gameTile { get; private set; }

    private SpriteRenderer m_renderer;
    private GameObject m_occupyingEntity;

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
            if (selectedCard.m_card.IsValidToPlay(m_gameTile))
            {
                selectedCard.m_card.PlayCard(m_gameTile);
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
}
