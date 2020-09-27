using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WorldTile : MonoBehaviour, ICustomRecycle
{
    public SpriteRenderer m_renderer;
    public SpriteRenderer m_tintRenderer;
    public SpriteRenderer m_frameRenderer;
    public SpriteRenderer m_fogRenderer;
    public GameObject m_fogOfWar;
    public GameObject m_spawnIndicator;
    public GameObject m_eventIndicator;

    private UIEntity m_occupyingEntityObj;

    public GameObject m_titleHolder;
    public Text m_nameText;
    public Text m_healthText;

    private bool m_isHovered;
    private bool m_isMoveable;
    private bool m_isAttackable;

    private GameTile m_gameTile;

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

        if (GetGameTile().HasBuilding())
        {
            GameBuildingBase building = GetGameTile().GetBuilding();

            m_titleHolder.SetActive(true);
            m_nameText.text = building.m_name;
            m_healthText.text = building.m_curHealth + "/" + building.m_maxHealth;
        }
        else
        {
            m_titleHolder.SetActive(false);
        }

        m_renderer.sprite = GetGameTile().GetIcon();
        m_tintRenderer.sprite = GetGameTile().GetIconWhite();
        m_fogRenderer.sprite = GetGameTile().GetIcon();
        m_eventIndicator.GetComponent<SpriteRenderer>().sprite = GetGameTile().GetIcon();

        if (GameHelper.IsInLevelBuilder())
        {
            m_spawnIndicator.SetActive(GetGameTile().m_spawnPoint != null);
        }
        else
        {
            m_spawnIndicator.SetActive(false);
        }

        if ((GetGameTile().IsOccupied() && m_occupyingEntityObj == null))
        {
            m_occupyingEntityObj = FactoryManager.Instance.GetFactory<UIEntityFactory>().CreateObject<UIEntity>(this);
        }

        if (GetGameTile().HasBuilding() && GetGameTile().GetBuilding().GetWorldTile() != this)
        {
            GetGameTile().GetBuilding().SetGameTile(this.GetGameTile());
        }

        //Handle Tint Color
        if (Globals.m_selectedTile == this)
        {
            m_tintRenderer.color = UIHelper.GetSelectTintColor(true);
        }
        else
        {
            if (m_isHovered)
            {
                if (Globals.m_testSpawnEnemyEntity != null && GetGameTile().m_occupyingEntity == null)
                {
                    GameEnemyEntity newEnemyEntity = GameEntityFactory.GetEnemyEntityClone(Globals.m_testSpawnEnemyEntity, WorldController.Instance.m_gameController.m_gameOpponent);
                    GetGameTile().PlaceEntity(newEnemyEntity);
                    WorldController.Instance.m_gameController.m_gameOpponent.AddControlledEntity(newEnemyEntity);
                    Globals.m_testSpawnEnemyEntity = null;
                }
                else if (Globals.m_selectedEntity != null)
                {
                    m_tintRenderer.color = UIHelper.GetSelectValidTintColor(Globals.m_selectedEntity.CanMoveToWorldTileFromCurPosition(GetGameTile()));
                } 
                else if (Globals.m_selectedCard != null)
                {
                    if (!Globals.m_inIntermission)
                    {
                        m_tintRenderer.color = UIHelper.GetSelectValidTintColor(Globals.m_selectedCard.m_card.IsValidToPlay(GetGameTile()));
                    }
                }
                else if (Globals.m_selectedIntermissionBuilding != null)
                {
                    m_tintRenderer.color = UIHelper.GetSelectValidTintColor(Globals.m_selectedIntermissionBuilding.IsValidToPlay(GetGameTile()));
                }
                else
                {
                    m_tintRenderer.color = UIHelper.GetValidTintColor(true);
                }
            }
            else
            {
                if (Globals.m_selectedCard != null && Globals.m_selectedCard.m_card.IsValidToPlay(GetGameTile()) && !Globals.m_inIntermission)
                {
                    m_tintRenderer.color = UIHelper.GetValidTintColor(true);
                }
                else if ((Globals.m_selectedEntity != null || Globals.m_selectedEnemy != null) && m_isMoveable)
                {
                    m_tintRenderer.color = UIHelper.GetValidTintColor(true);
                }
                else if ((Globals.m_selectedEntity != null || Globals.m_selectedEnemy != null) && m_isAttackable)
                {
                    m_tintRenderer.color = UIHelper.GetAttackTintColor();
                }
                else if (Globals.m_selectedIntermissionBuilding != null && 
                    Globals.m_selectedIntermissionBuilding.m_building.IsValidTerrainToPlace(GetGameTile().GetTerrain()) && 
                    !GetGameTile().HasAvailableEvent() &&
                    !GetGameTile().m_isFog)
                {
                    m_tintRenderer.color = UIHelper.GetValidTintColor(true);
                }
                else
                {
                    m_tintRenderer.color = UIHelper.GetDefaultTintColor();
                }
            }
        }

        //Handle Frame Color
        if (GetGameTile().m_isFog || Globals.m_inIntermission)
        {
            m_frameRenderer.color = Color.black;
        }
        else
        {
            if (Globals.m_selectedCard != null && Globals.m_selectedCard.m_card.IsValidToPlay(GetGameTile()))
            {
                m_frameRenderer.color = UIHelper.GetValidColor(true);
            }
            else if (Globals.m_selectedEntity != null && m_isMoveable)
            {
                m_frameRenderer.color = UIHelper.GetValidColor(true);
            }
            else if (Globals.m_selectedEntity != null && m_isAttackable)
            {
                m_frameRenderer.color = UIHelper.GetAttackColor();
            }
            else
            {
                m_frameRenderer.color = Color.black;
            }
        }
    }

    public void Init(int x, int y)
    {
        m_gameTile = new GameTile(this);
        GetGameTile().m_gridPosition = new Vector2Int(x, y);
    }

    void OnMouseDown()
    {
        if (!Globals.m_canSelect)
        {
            return;
        }

        if (UIHelper.UIShouldBlockClick())
        {
            return;
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LevelCreatorScene"))
        {
            if (Globals.m_levelCreatorEraserMode)
            {
                GameTile gameTile = GetGameTile();
                if (gameTile.GetTerrain() != null)
                    gameTile.ClearTerrain();
                if (gameTile.GetBuilding() != null)
                    gameTile.ClearBuilding();
                if (gameTile.m_event)
                    gameTile.m_event = false;
                if (gameTile.m_spawnPoint != null)
                    gameTile.ClearSpawnPoint();
            }
            else
            {
                if (Globals.m_currentlyPaintingType == typeof(GameTerrainBase) && Globals.m_currentlyPaintingTerrain != null)
                {
                    GetGameTile().SetTerrain(GameTerrainFactory.GetTerrainClone(Globals.m_currentlyPaintingTerrain));
                    if (Globals.m_currentlyPaintingTerrain.IsEventTerrain())
                    {
                        GetGameTile().m_event = true;
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

            return;
        }

        UICard selectedCard = Globals.m_selectedCard;
        if (selectedCard != null && !Globals.m_inIntermission)
        {
            if (selectedCard.m_card.IsValidToPlay(GetGameTile()))
            {
                WorldController.Instance.PlayCard(selectedCard);
                selectedCard.m_card.PlayCard(GetGameTile());
                WorldController.Instance.PostPlayCard();
                return;
            }
        }

        UIEntity selectedEntity = Globals.m_selectedEntity;
        if (selectedEntity != null && !Globals.m_inIntermission)
        {
            if (selectedEntity.GetEntity().CanMoveTo(GetGameTile()))
            {
                selectedEntity.MoveTo(GetGameTile());
            }
            else
            {
                if (GetGameTile().IsPassable(selectedEntity.GetEntity(), false))
                {
                    int pathLength = WorldGridManager.Instance.GetPathLength(selectedEntity.GetEntity().GetGameTile(), GetGameTile(), true, false, true);
                    if (pathLength == 1)
                    {
                        UIHelper.CreateWorldElementNotification("Can't move, requires " + GetGameTile().GetCostToPass(selectedEntity.GetEntity()) + " AP.", false, gameObject);
                    }
                    else
                    {
                        UIHelper.CreateWorldElementNotification("Out of movement range.", false, gameObject);
                    }
                }
                else
                {
                    if (!GetGameTile().IsOccupied())
                    {
                        UIHelper.CreateWorldElementNotification(GetGameTile().GetName() + " is impassable.", false, gameObject);
                    }
                }
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

        if ((selectedEntity == null && selectedBuilding == null && selectedCard == null) || Globals.m_inIntermission)
        {
            UIHelper.SelectTile(this);
        }
    }

    void OnMouseOver()
    {
        if (UIHelper.UIShouldBlockClick())
        {
            return;
        }

        m_isHovered = true;
    }

    void OnMouseExit()
    {
        m_isHovered = false;
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
        m_eventIndicator.SetActive(GetGameTile().HasAvailableEvent() && GetGameTile().m_isFog && Constants.DebugEventsVisibleInFog);

        if (GetGameTile().m_isFog && !GameHelper.IsInLevelBuilder())
        {
            if (m_occupyingEntityObj != null)
            {
                m_occupyingEntityObj.SetVisible(false);
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
                m_occupyingEntityObj.SetVisible(true);
            }
            m_fogOfWar.SetActive(false);
        }
    }

    public void ClearEntity()
    {
        m_occupyingEntityObj = null;
    }

    public void RecycleEntity()
    {
        if (m_occupyingEntityObj != null)
        {
            GetGameTile().ClearEntity();
            Recycler.Recycle<UIEntity>(m_occupyingEntityObj);
            m_occupyingEntityObj = null;
        }
    }

    public void PlaceEntity(UIEntity newEntity)
    {
        m_occupyingEntityObj = newEntity;
    }

    public GameTile GetGameTile()
    {
        return m_gameTile;
    }

    public void SetMoveable(bool isMoveable)
    {
        m_isMoveable = isMoveable;
    }

    public void SetAttackable(bool isAttackable)
    {
        m_isAttackable = isAttackable;
    }

    public void CustomRecycle(params object[] args)
    {
        m_gameTile = null;
        m_renderer.sprite = null;
        m_tintRenderer.sprite = null;
        m_fogRenderer.sprite = null;

        m_fogOfWar.SetActive(true);

        m_occupyingEntityObj = null;

        m_isHovered = false;
        m_isMoveable = false;
        m_isAttackable = false;
    }
}
