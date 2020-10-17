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

    private WorldUnit m_occupyingUnitObj;

    public GameObject m_titleHolder;
    public Text m_nameText;
    public Text m_healthText;

    private bool m_isHovered;
    private bool m_isMoveable;
    private bool m_isAttackable;
    private int m_inSpellcraftRange;
    private int m_inDefensiveBuildingRange;

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

        if ((GetGameTile().IsOccupied() && m_occupyingUnitObj == null))
        {
            m_occupyingUnitObj = FactoryManager.Instance.GetFactory<UIUnitFactory>().CreateObject<WorldUnit>(this);
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
                if (Globals.m_testSpawnEnemyUnit != null && GetGameTile().m_occupyingUnit == null)
                {
                    GameEnemyUnit newEnemyUnit = GameUnitFactory.GetEnemyUnitClone(Globals.m_testSpawnEnemyUnit, WorldController.Instance.m_gameController.m_gameOpponent);
                    GetGameTile().PlaceUnit(newEnemyUnit);
                    WorldController.Instance.m_gameController.m_gameOpponent.AddControlledUnit(newEnemyUnit);
                    Globals.m_testSpawnEnemyUnit = null;
                }
                else if (Globals.m_selectedUnit != null)
                {
                    m_tintRenderer.color = UIHelper.GetSelectValidTintColor(Globals.m_selectedUnit.CanMoveToWorldTileFromCurPosition(GetGameTile()));
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
                else if (Globals.m_selectedCard != null && !Globals.m_inIntermission && m_inSpellcraftRange > 0)
                {
                    m_tintRenderer.color = UIHelper.GetSpellcraftTint(m_inSpellcraftRange);
                }
                else if ((Globals.m_selectedUnit != null || Globals.m_selectedEnemy != null) && m_isMoveable)
                {
                    m_tintRenderer.color = UIHelper.GetValidTintColor(true);
                }
                else if ((Globals.m_selectedUnit != null || Globals.m_selectedEnemy != null) && m_isAttackable)
                {
                    m_tintRenderer.color = UIHelper.GetAttackTintColor();
                }
                else if (Globals.m_selectedIntermissionBuilding != null &&
                    Globals.m_selectedIntermissionBuilding.m_building.IsValidTerrainToPlace(GetGameTile().GetTerrain(), GetGameTile()) &&
                    !GetGameTile().GetTerrain().IsEventTerrain() &&
                    !GetGameTile().m_isFog)
                {
                    m_tintRenderer.color = UIHelper.GetValidTintColor(true);
                }
                else if (Globals.m_selectedTile != null && 
                    Globals.m_selectedTile.m_gameTile.HasBuilding() && 
                    Globals.m_selectedTile.m_gameTile.GetBuilding().m_buildingType == BuildingType.Defensive &&
                    m_inDefensiveBuildingRange > 0)
                {
                    m_tintRenderer.color = UIHelper.GetDefensiveBuildingTint(m_inDefensiveBuildingRange);
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
            else if (Globals.m_selectedUnit != null && m_isMoveable)
            {
                m_frameRenderer.color = UIHelper.GetValidColor(true);
            }
            else if (Globals.m_selectedUnit != null && m_isAttackable)
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
                if (gameTile.m_spawnPoint != null)
                    gameTile.ClearSpawnPoint();
            }
            else
            {
                if (Globals.m_currentlyPaintingType == typeof(GameTerrainBase) && Globals.m_currentlyPaintingTerrain != null)
                {
                    GetGameTile().SetTerrain(GameTerrainFactory.GetTerrainClone(Globals.m_currentlyPaintingTerrain));
                }
                else if (Globals.m_currentlyPaintingType == typeof(GameBuildingBase) && Globals.m_currentlyPaintingBuilding != null)
                {
                    GetGameTile().PlaceBuilding(GameBuildingFactory.GetBuildingClone(Globals.m_currentlyPaintingBuilding));
                }
                else if (Globals.m_currentlyPaintingType == typeof(GameSpawnPoint))
                {
                    GameSpawnPoint gameSpawnPoint = new GameSpawnPoint();
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
            else
            {
                if (selectedCard.m_unitCard != null)
                {
                    if (!GetGameTile().m_canPlace)
                    {
                        UIHelper.CreateWorldElementNotification("Out of placement range.", false, gameObject);
                    }
                    else if (GetGameTile().IsOccupied())
                    {
                        UIHelper.CreateWorldElementNotification("Tile already occupied.", false, gameObject);
                    }
                    else if (!GetGameTile().IsPassable(selectedCard.m_unitCard.GetUnit(), false))
                    {
                        UIHelper.CreateWorldElementNotification("Unit cannot stand on tile.", false, gameObject);
                    }
                }
                else
                {
                    UIHelper.CreateWorldElementNotification("Invalid target.", false, gameObject);
                }
            }
        }

        WorldUnit selectedUnit = Globals.m_selectedUnit;
        if (selectedUnit != null && !Globals.m_inIntermission)
        {
            if (selectedUnit.GetUnit().CanMoveTo(GetGameTile()))
            {
                selectedUnit.MoveTo(GetGameTile());
            }
            else
            {
                if (GetGameTile().IsPassable(selectedUnit.GetUnit(), false))
                {
                    int pathLength = WorldGridManager.Instance.GetPathLength(selectedUnit.GetUnit().GetGameTile(), GetGameTile(), true, false, true);
                    if (pathLength == 1 && GetGameTile().m_occupyingUnit == null)
                    {
                        UIHelper.CreateWorldElementNotification("Can't move, " + GetGameTile().GetName() + " requires " + GetGameTile().GetCostToPass(selectedUnit.GetUnit()) + " Stamina.", false, gameObject);
                    }
                    else
                    {
                        if (GetGameTile().m_occupyingUnit == Globals.m_selectedUnit.GetUnit())
                        {
                            UIHelper.CreateWorldElementNotification("Already here.", false, gameObject);
                        }
                        else if (GetGameTile().m_occupyingUnit != null)
                        {
                            UIHelper.CreateWorldElementNotification("Already occupied.", false, gameObject);
                        }
                        else
                        {
                            UIHelper.CreateWorldElementNotification("Out of movement range.", false, gameObject);
                        }
                    }
                }
                else
                {
                    if (!GetGameTile().IsOccupied())
                    {
                        UIHelper.CreateWorldElementNotification(GetGameTile().GetName() + " is impassable.", false, gameObject);
                    }
                    else
                    {
                        UIHelper.CreateWorldElementNotification("Can't move to tile with enemy unit.", false, gameObject);
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

        if ((selectedUnit == null && selectedBuilding == null && selectedCard == null) || Globals.m_inIntermission)
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

        if (m_gameTile.GetTerrain().IsEventTerrain())
        {
            Globals.m_hoveredTile = this;
        }
        else
        {
            Globals.m_hoveredTile = null;
        }

        m_isHovered = true;
    }

    void OnMouseExit()
    {
        m_isHovered = false;

        Globals.m_hoveredTile = null;
    }

    public void ClearFog()
    {
        GetGameTile().m_isFog = false;
        GetGameTile().m_isFogBorder = false;

        List<GameTile> adjacentTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), 1);
        for (int i = 0; i < adjacentTiles.Count; i++)
        {
            if (adjacentTiles[i].m_isFog)
            {
                adjacentTiles[i].m_isFogBorder = true;
            }
        }
    }

    public void ClearSurroundingFog(int distance)
    {
        List<WorldTile> toReveal = WorldGridManager.Instance.GetSurroundingWorldTiles(this, distance, 0);

        for (int i = 0; i < toReveal.Count; i++)
        {
            toReveal[i].ClearFog();
        }
    }

    public void ExpandPlaceRange(int distance)
    {
        List<WorldTile> toExpand = WorldGridManager.Instance.GetSurroundingWorldTiles(this, distance, 0);

        for (int i = 0; i < toExpand.Count; i++)
        {
            toExpand[i].GetGameTile().m_canPlace = true;
        }
    }

    public void ReducePlaceRange(int distance)
    {
        List<WorldTile> toReduce = WorldGridManager.Instance.GetSurroundingWorldTiles(this, distance, 0);

        for (int i = 0; i < toReduce.Count; i++)
        {
            toReduce[i].GetGameTile().m_canPlace = false;
        }
    }

    public void HandleFogUpdate()
    {
        m_eventIndicator.SetActive(GetGameTile().GetTerrain().IsEventTerrain() && GetGameTile().m_isFog && Constants.DebugEventsVisibleInFog);

        if (GetGameTile().m_isFog && !GameHelper.IsInLevelBuilder())
        {
            if (m_occupyingUnitObj != null)
            {
                m_occupyingUnitObj.SetVisible(false);
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
            if (m_occupyingUnitObj != null)
            {
                m_occupyingUnitObj.SetVisible(true);
            }
            m_fogOfWar.SetActive(false);
        }
    }

    public void ClearUnit()
    {
        m_occupyingUnitObj = null;
    }

    public void RecycleUnit()
    {
        if (m_occupyingUnitObj != null)
        {
            GetGameTile().ClearUnit();
            Recycler.Recycle<WorldUnit>(m_occupyingUnitObj);
            m_occupyingUnitObj = null;
        }
    }

    public void PlaceUnit(WorldUnit newUnit)
    {
        m_occupyingUnitObj = newUnit;
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

    public bool IsAttackable()
    {
        return m_isAttackable;
    }

    public void AddInSpellcraftRangeCount()
    {
        m_inSpellcraftRange++;
    }

    public void ClearSpellcraftRangeCount()
    {
        m_inSpellcraftRange = 0;
    }

    public void AddInDefensiveBuildingRangeCount()
    {
        m_inDefensiveBuildingRange++;
    }

    public void ClearInDefensiveBuildingRangeCount()
    {
        m_inDefensiveBuildingRange = 0;
    }

    public void CustomRecycle(params object[] args)
    {
        m_gameTile = null;
        m_renderer.sprite = null;
        m_tintRenderer.sprite = null;
        m_fogRenderer.sprite = null;

        m_fogOfWar.SetActive(true);

        if (m_occupyingUnitObj != null)
        {
            Recycler.Recycle<WorldUnit>(m_occupyingUnitObj);
            m_occupyingUnitObj = null;
        }

        m_isHovered = false;
        m_isMoveable = false;
        m_isAttackable = false;
    }
}
