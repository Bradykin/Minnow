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

    private UIEntity m_occupyingEntityObj;

    bool m_isHovered;

    void Start()
    {
        UIHelper.SetDefaultTintColorCanPlace(m_tintRenderer, GetGameTile().m_canPlace);

        if (!Constants.FogOfWar)
        {
            ClearFog();
        }
    }

    void Update()
    {
        HandleFogUpdate();

        m_renderer.gameObject.SetActive(!m_fogOfWar.activeSelf);

        m_renderer.sprite = GetGameTile().GetIcon();
        m_tintRenderer.sprite = GetGameTile().GetIcon();

        if (GetGameTile().IsOccupied() && m_occupyingEntityObj == null)
        {
            m_occupyingEntityObj = FactoryManager.Instance.GetFactory<UIEntityFactory>().CreateObject<UIEntity>(this);
        }
        else if (!GetGameTile().IsOccupied() && m_occupyingEntityObj != null)
        {
            Recycler.Recycle<UIEntity>(m_occupyingEntityObj);
            m_occupyingEntityObj = null;
            GetGameTile().ClearEntity();
        }

        if (GetGameTile().HasBuilding() && GetGameTile().GetBuilding().m_curTile != this)
        {
            GetGameTile().GetBuilding().SetWorldTile(this);
        }

        if (Globals.m_selectedCard == null || (Globals.m_selectedCard != null && !Globals.m_selectedCard.m_card.IsValidToPlay(GetGameTile())))
        {
            m_frameRenderer.color = Color.black;
        }
        else if (Globals.m_selectedCard.m_card.IsValidToPlay(GetGameTile()))
        {
            UIHelper.SetValidColor(m_frameRenderer, GetGameTile().m_canPlace);
            if (!m_isHovered)
            {
                UIHelper.SetSelectTintColor(m_tintRenderer, GetGameTile().m_canPlace);
            }
        }

        if (!m_isHovered && Globals.m_selectedCard == null)
        {
            UIHelper.SetSelectTintColor(m_tintRenderer, false);
        }
    }

    public void Init(int x, int y)
    {
        m_gameElement = new GameTile(this);
        GetGameTile().m_gridPosition = new Vector2Int(x, y);
    }

    void OnMouseDown()
    {
        UICard selectedCard = Globals.m_selectedCard;
        if (selectedCard != null)
        {
            if (selectedCard.m_card.IsValidToPlay(GetGameTile()))
            {
                selectedCard.m_card.PlayCard(GetGameTile());
                WorldController.Instance.PlayCard(selectedCard, this);
                return;
            }
        }

        UIEntity selectedEntity = Globals.m_selectedEntity;
        if (selectedEntity != null)
        {
            GameEntity gameEntity = (GameEntity)(selectedEntity.GetElement());
            if (gameEntity.CanMoveTo(GetGameTile()))
            {
                gameEntity.MoveTo(GetGameTile());
            }
        }
    }

    void OnMouseOver()
    {
        m_isHovered = true;

        if (Globals.m_selectedEntity != null)
        {
           UIHelper.SetValidTintColor(m_tintRenderer, Globals.m_selectedEntity.CanMoveToWorldTileFromCurPosition(GetGameTile()));
        }

        if (Globals.m_selectedCard != null)
        {
            UIHelper.SetValidTintColor(m_tintRenderer, Globals.m_selectedCard.m_card.IsValidToPlay(GetGameTile()));
        }
    }

    void OnMouseExit()
    {
        m_isHovered = false;

        UIHelper.SetDefaultTintColorCanPlace(m_tintRenderer, GetGameTile().m_canPlace);
    }

    public override void HandleTooltip()
    {
        if (GetGameTile().HasBuilding())
        {
            UIHelper.CreateBuildingTooltip(GetGameTile().GetBuilding());
        }
        else if (GetGameTile().HasAvailableEvent())
        {
            UIHelper.CreateEventTooltip(GetGameTile().m_event);
        }
        else
        {
            UIHelper.CreateTerrainTooltip(GetGameTile().GetTerrain());
        }

        if (!GetGameTile().IsOccupied() && Globals.m_selectedEntity != null)
        {
            UIHelper.CreateAPTooltip(GetGameTile());
        }

        if (Globals.m_selectedCard != null)
        {
            if (!Globals.m_selectedCard.m_card.IsValidToPlay(GetGameTile()))
            {
                string titleText = "Can't Place";
                if (!GetGameTile().m_canPlace && GetGameTile().IsPassable())
                {
                     UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip(titleText, "Placement is too far away from buildings that extend range.", false));
                }
                else if (GetGameTile().m_canPlace && !GetGameTile().IsPassable())
                {
                    UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip(titleText, "Impassable terrain.", false));
                }
                else if (GetGameTile().IsOccupied())
                {
                    UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip(titleText, "Tile already occupied.", false));
                }
            }
        }
    }

    public void ClearFog()
    {
        GetGameTile().m_isFog = false;
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
            toReveal[i].GetGameTile().m_canPlace = true;
        }
    }

    public void HandleFogUpdate()
    {
        if (GetGameTile().m_isFog)
        {
            if (m_occupyingEntityObj != null)
            {
                m_occupyingEntityObj.gameObject.SetActive(false);
            }
            m_fogOfWar.SetActive(true);
        }
        else
        {
            if (m_occupyingEntityObj != null)
            {
                m_occupyingEntityObj.gameObject.SetActive(true);
            }
            m_fogOfWar.SetActive(false);
        }
    }

    public GameTile GetGameTile()
    {
        return (GameTile)m_gameElement;
    }
}
