using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public class WorldTile : WorldElementBase
{
    public SpriteRenderer m_renderer;
    public SpriteRenderer m_tintRenderer;
    public SpriteRenderer m_frameRenderer;
    public GameObject m_fogOfWar;
    public GameTile m_gameTile { get; private set; }

    private UIEntity m_occupyingEntityObj;
    private UIEvent m_occupyingEventObj;
    private UIBuilding m_occupyingBuildingObj;

    void Start()
    {
        m_gameElement = m_gameTile.m_terrain;
        m_renderer.sprite = m_gameElement.m_icon;
        UIHelper.SetDefaultTintColorCanPlace(m_tintRenderer, m_gameTile.m_canPlace);

        if (!Constants.FogOfWar)
        {
            ClearFog();
        }
    }

    void Update()
    {
        HandleFogUpdate();

        if (m_gameTile.IsOccupied() && m_occupyingEntityObj == null)
        {
            m_occupyingEntityObj = FactoryManager.Instance.GetFactory<UIEntityFactory>().CreateObject<UIEntity>(this);
        }
        else if (!m_gameTile.IsOccupied() && m_occupyingEntityObj != null)
        {
            Recycler.Recycle<UIEntity>(m_occupyingEntityObj);
            m_occupyingEntityObj = null;
            m_gameTile.ClearEntity();
        }

        if (m_gameTile.HasAvailableEvent() && m_occupyingEventObj == null)
        {
            m_occupyingEventObj = FactoryManager.Instance.GetFactory<UIEventFactory>().CreateObject<UIEvent>(this);
        }
        else if (!m_gameTile.HasAvailableEvent() && m_occupyingEventObj != null)
        {
            Recycler.Recycle<UIEvent>(m_occupyingEventObj);
            m_occupyingEventObj = null;
            m_gameTile.ClearEvent();
        }
        else if (m_gameTile.HasAvailableEvent() && m_gameTile.m_event.m_isComplete)
        {
            Recycler.Recycle<UIEvent>(m_occupyingEventObj);
            m_occupyingEventObj = null;
            m_gameTile.ClearEvent();
        }

        if (m_gameTile.HasBuilding() && m_occupyingBuildingObj == null)
        {
            m_occupyingBuildingObj = FactoryManager.Instance.GetFactory<UIBuildingFactory>().CreateObject<UIBuilding>(m_gameTile.m_building, this);
        }
        else if (!m_gameTile.HasBuilding() && m_occupyingBuildingObj != null)
        {
            Recycler.Recycle<UIBuilding>(m_occupyingBuildingObj);
            m_occupyingBuildingObj = null;
            m_gameTile.ClearBuilding();
        }

        if (Globals.m_selectedCard == null || (Globals.m_selectedCard != null && !Globals.m_selectedCard.m_card.IsValidToPlay(m_gameTile)))
        {
            m_frameRenderer.color = Color.black;
        }
        else if (Globals.m_selectedCard.m_card.IsValidToPlay(m_gameTile))
        {
            UIHelper.SetValidColor(m_frameRenderer, m_gameTile.m_canPlace);
        }
    }

    public void Init(int x, int y)
    {
        m_gameTile = new GameTile(this);
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
                WorldController.Instance.PlayCard(selectedCard, this);
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
           UIHelper.SetValidTintColor(m_tintRenderer, Globals.m_selectedEntity.CanMoveToWorldTileFromCurPosition(m_gameTile));
        }

        if (Globals.m_selectedCard != null)
        {
            UIHelper.SetValidTintColor(m_tintRenderer, Globals.m_selectedCard.m_card.IsValidToPlay(m_gameTile));
        }
    }

    void OnMouseExit()
    {
        UIHelper.SetDefaultTintColorCanPlace(m_tintRenderer, m_gameTile.m_canPlace);
    }

    public override void HandleTooltip()
    {
        UIHelper.CreateTerrainTooltip(m_gameTile.m_terrain);
        if (!m_gameTile.IsOccupied() && Globals.m_selectedEntity != null)
        {
            UIHelper.CreateAPTooltip(m_gameTile);
        }

        if (Globals.m_selectedCard != null)
        {
            if (!Globals.m_selectedCard.m_card.IsValidToPlay(m_gameTile))
            {
                string titleText = "Can't Place";
                if (!m_gameTile.m_canPlace && m_gameTile.m_terrain.m_isPassable)
                {
                     UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip(titleText, "Placement is too far away from buildings that extend range.", false));
                }
                else if (m_gameTile.m_canPlace && !m_gameTile.m_terrain.m_isPassable)
                {
                    UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip(titleText, "Impassable terrain.", false));
                }
                else if (m_gameTile.IsOccupied())
                {
                    UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip(titleText, "Tile already occupied.", false));
                }
            }
        }
    }

    public void ClearFog()
    {
        m_gameTile.m_isFog = false;
    }

    public void ClearSurroundingFog(int distance)
    {
        List<WorldTile> toReveal = WorldGridManager.Instance.GetSurroundingTiles(this, distance, 0);

        for (int i = 0; i < toReveal.Count; i++)
        {
            toReveal[i].ClearFog();
        }
    }

    public void ExpandPlaceRange(int distance)
    {
        List<WorldTile> toReveal = WorldGridManager.Instance.GetSurroundingTiles(this, distance, 0);

        for (int i = 0; i < toReveal.Count; i++)
        {
            toReveal[i].m_gameTile.m_canPlace = true;
        }
    }

    public void HandleFogUpdate()
    {
        if (m_gameTile.m_isFog)
        {
            if (m_occupyingBuildingObj != null)
            {
                m_occupyingBuildingObj.gameObject.SetActive(false);
            }
            if (m_occupyingEntityObj != null)
            {
                m_occupyingEntityObj.gameObject.SetActive(false);
            }
            if (m_occupyingEventObj != null)
            {
                m_occupyingEventObj.gameObject.SetActive(false);
            }
            m_fogOfWar.SetActive(true);
        }
        else
        {
            if (m_occupyingBuildingObj != null)
            {
                m_occupyingBuildingObj.gameObject.SetActive(true);
            }
            if (m_occupyingEntityObj != null)
            {
                m_occupyingEntityObj.gameObject.SetActive(true);
            }
            if (m_occupyingEventObj != null)
            {
                m_occupyingEventObj.gameObject.SetActive(true);
            }
            m_fogOfWar.SetActive(false);
        }
    }
}
