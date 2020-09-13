using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using UnityEngine.SceneManagement;

public class WorldTile : WorldElementBase, ICustomRecycle
{
    public SpriteRenderer m_renderer;
    public SpriteRenderer m_tintRenderer;
    public SpriteRenderer m_frameRenderer;
    public SpriteRenderer m_fogRenderer;
    public GameObject m_fogOfWar;
    public GameObject m_spawnIndicator;

    private UIEntity m_occupyingEntityObj;

    bool m_isHovered;
    bool m_isMoveable;

    void Start()
    {
        if (!Constants.FogOfWar)
        {
            ClearFog();
        }
    }

    void Update()
    {
        HandleFogUpdate();

        m_renderer.sprite = GetGameTile().GetIcon();
        m_tintRenderer.sprite = GetGameTile().GetIcon();
        m_fogRenderer.sprite = GetGameTile().GetIcon();

        if (GameHelper.IsInLevelBuilder())
        {
            m_spawnIndicator.SetActive(GetGameTile().m_spawnPoint != null);
        }
        else
        {
            m_spawnIndicator.SetActive(false);
        }

        bool entityMovedIntoTile = false;
        if (m_occupyingEntityObj != null && GetGameTile().IsOccupied())
        {
            entityMovedIntoTile = GetGameTile().m_occupyingEntity != m_occupyingEntityObj.GetEntity();
        }

        if (entityMovedIntoTile)
        {
            Recycler.Recycle<UIEntity>(m_occupyingEntityObj);
            m_occupyingEntityObj = FactoryManager.Instance.GetFactory<UIEntityFactory>().CreateObject<UIEntity>(this);
        }
        else if ((GetGameTile().IsOccupied() && m_occupyingEntityObj == null))
        {
            m_occupyingEntityObj = FactoryManager.Instance.GetFactory<UIEntityFactory>().CreateObject<UIEntity>(this);
        }
        else if (!GetGameTile().IsOccupied() && m_occupyingEntityObj != null)
        {
            Recycler.Recycle<UIEntity>(m_occupyingEntityObj);
            m_occupyingEntityObj = null;
        }

        if (GetGameTile().HasBuilding() && GetGameTile().GetBuilding().m_curTile != this)
        {
            GetGameTile().GetBuilding().SetWorldTile(this);
        }

        //Handle Tint Color
        if (m_isHovered)
        {
            if (Globals.m_testSpawnEnemyEntity != null && GetGameTile().m_occupyingEntity == null)
            {
                GameEnemyEntity newEnemyEntity = GameEntityFactory.GetEnemyEntityClone(Globals.m_testSpawnEnemyEntity, WorldController.Instance.m_gameController.m_gameOpponent);
                GetGameTile().PlaceEntity(newEnemyEntity);
                WorldController.Instance.m_gameController.m_gameOpponent.AddControlledEntity(newEnemyEntity);
                Globals.m_testSpawnEnemyEntity = null;
            }
            
            if (Globals.m_selectedEntity != null)
            {
                UIHelper.SetSelectValidTintColor(m_tintRenderer, Globals.m_selectedEntity.CanMoveToWorldTileFromCurPosition(GetGameTile()));
            }

            if (Globals.m_selectedCard != null)
            {
                UIHelper.SetSelectValidTintColor(m_tintRenderer, Globals.m_selectedCard.m_card.IsValidToPlay(GetGameTile()));
            }

            if (Globals.m_selectedIntermissionBuilding != null)
            {
                UIHelper.SetSelectValidTintColor(m_tintRenderer, Globals.m_selectedIntermissionBuilding.IsValidToPlay(GetGameTile()));
            }
        }
        else
        {
            if (Globals.m_selectedCard != null && Globals.m_selectedCard.m_card.IsValidToPlay(GetGameTile()))
            {
                UIHelper.SetValidTintColor(m_tintRenderer, true);
            }
            else if (Globals.m_selectedEntity != null && m_isMoveable)
            {
                UIHelper.SetValidTintColor(m_tintRenderer, true);
            }
            else
            {
                UIHelper.SetDefaultTintColor(m_tintRenderer);
            }
        }

        //Handle Frame Color
        if (GetGameTile().m_isFog)
        {
            m_frameRenderer.color = Color.black;
        }
        else
        {
            if (Globals.m_selectedCard != null && Globals.m_selectedCard.m_card.IsValidToPlay(GetGameTile()))
            {
                UIHelper.SetValidColor(m_frameRenderer, true);
            }
            else if (Globals.m_selectedEntity != null && m_isMoveable)
            {
                UIHelper.SetValidColor(m_frameRenderer, true);
            }
            else
            {
                m_frameRenderer.color = Color.black;
            }
        }
    }

    public void Init(int x, int y)
    {
        m_gameElement = new GameTile(this);
        GetGameTile().m_gridPosition = new Vector2Int(x, y);
    }

    void OnMouseDown()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LevelCreatorScene"))
        {
            if (Globals.m_levelCreatorEraserMode)
            {
                GameTile gameTile = GetGameTile();
                if (gameTile.GetTerrain() != null)
                    gameTile.ClearTerrain();
                if (gameTile.GetBuilding() != null)
                    gameTile.ClearBuilding();
                if (gameTile.m_event != null)
                    gameTile.ClearEvent();
                if (gameTile.m_spawnPoint != null)
                    gameTile.ClearSpawnPoint();
            }
            else
            {
                if (Globals.m_currentlyPaintingType == typeof(GameTerrainBase) && Globals.m_currentlyPaintingTerrain != null)
                {
                    GetGameTile().SetTerrain(GameTerrainFactory.GetTerrainClone(Globals.m_currentlyPaintingTerrain));
                    print(GetGameTile().GetTerrain().m_name);
                    if (Globals.m_currentlyPaintingTerrain.IsEventTerrain())
                    {
                        GetGameTile().SetEvent();
                    }
                }
                else if (Globals.m_currentlyPaintingType == typeof(GameBuildingBase) && Globals.m_currentlyPaintingBuilding != null)
                {
                    GetGameTile().PlaceBuilding(GameBuildingFactory.GetBuildingClone(Globals.m_currentlyPaintingBuilding));
                }
                else if (Globals.m_currentlyPaintingType == typeof(GameSpawnPoint))
                {
                    GameSpawnPoint gameSpawnPoint = new GameSpawnPoint();
                    gameSpawnPoint.SetSpawnPointRandom();
                    GetGameTile().SetSpawnPoint(gameSpawnPoint);
                }
            }
        }

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

        GameBuildingIntermission selectedBuilding = Globals.m_selectedIntermissionBuilding;
        if (selectedBuilding != null)
        {
            if (selectedBuilding.IsValidToPlay(GetGameTile()))
            {
                GameHelper.MakePlayerBuilding(GetGameTile(), selectedBuilding.m_building);
                selectedBuilding.Place();
            }
        }
    }

    void OnMouseOver()
    {
        m_isHovered = true;
    }

    void OnMouseExit()
    {
        m_isHovered = false;
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

        if (Globals.m_selectedCard != null)
        {
            if (!Globals.m_selectedCard.m_card.IsValidToPlay(GetGameTile()))
            {
                string titleText = "Can't Place";
                GameCardEntityBase entityCard = null;
                if (Globals.m_selectedCard.m_card is GameCardEntityBase)
                {
                    entityCard = (GameCardEntityBase)Globals.m_selectedCard.m_card;
                }

                if (Globals.m_selectedCard.m_card is GameCardEntityBase && !GetGameTile().m_canPlace && GetGameTile().IsPassable(entityCard.GetEntity(), false))
                {
                     UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip(titleText, "Placement is too far away from buildings that extend range.", false));
                }
                else if (GetGameTile().m_canPlace && entityCard != null && !GetGameTile().IsPassable(entityCard.GetEntity(), false))
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
        List<WorldTile> toExpand = WorldGridManager.Instance.GetSurroundingTiles(this, distance, 0);

        for (int i = 0; i < toExpand.Count; i++)
        {
            toExpand[i].GetGameTile().m_canPlace = true;
        }
    }

    public void ReducePlaceRange(int distance)
    {
        List<WorldTile> toReduce = WorldGridManager.Instance.GetSurroundingTiles(this, distance, 0);

        for (int i = 0; i < toReduce.Count; i++)
        {
            toReduce[i].GetGameTile().m_canPlace = false;
        }
    }

    public void HandleFogUpdate()
    {
        if (GetGameTile().m_isFog && !GameHelper.IsInLevelBuilder())
        {
            if (m_occupyingEntityObj != null)
            {
                m_occupyingEntityObj.gameObject.SetActive(false);
            }
            m_fogOfWar.SetActive(true);

            if (GetGameTile().m_isSoftFog)
            {
                m_fogRenderer.color = new Color(m_fogRenderer.color.r, m_fogRenderer.color.g, m_fogRenderer.color.b, 0.35f);
            }
            else
            {
                m_fogRenderer.color = new Color(m_fogRenderer.color.r, m_fogRenderer.color.g, m_fogRenderer.color.b, 1f);
            }
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

    public void SetMoveable(bool isMoveable)
    {
        m_isMoveable = isMoveable;
    }

    public void CustomRecycle(params object[] args)
    {
        m_gameElement = null;
        m_renderer.sprite = null;
        m_tintRenderer.sprite = null;
        m_fogRenderer.sprite = null;

        m_fogOfWar.SetActive(true);

        m_occupyingEntityObj = null;

        m_isHovered = false;
        m_isMoveable = false;
    }
}
