using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class WorldTile : MonoBehaviour, ICustomRecycle
{
    public SpriteRenderer m_renderer;
    public SpriteRenderer m_tintRenderer;
    public SpriteRenderer m_frameRenderer;
    public SpriteRenderer m_fogRenderer;
    public GameObject m_fogOfWar;

    public GameObject m_spawnIndicator;
    public GameObject m_specialTileIndicator;
    public GameObject m_worldPerkIndicator;
    public Text m_spawnText;
    public Text m_specialTileText;

    private WorldUnit m_occupyingUnitObj;

    public GameObject m_titleHolder;
    public RectTransform m_titleHolderRectTransform;
    public TMP_Text m_nameText;
    public TMP_Text m_healthText;

    private bool m_isHovered;
    private bool m_isMoveable;
    private bool m_isAttackable;
    private int m_inSpellcraftRange;
    private int m_inBuildingRange;
    private int m_aoeRangeCount;

    public bool m_shouldAlertTint;

    private GameTile m_gameTile;

    private bool m_isShowingTooltip;

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

        if (m_titleHolderRectTransform != null)
        {
            if (this == Globals.m_selectedTile)
            {
                m_titleHolderRectTransform.localPosition = new Vector3(m_titleHolderRectTransform.localPosition.x, m_titleHolderRectTransform.localPosition.y, -3);
            }
            else
            {
                m_titleHolderRectTransform.localPosition = new Vector3(m_titleHolderRectTransform.localPosition.x, m_titleHolderRectTransform.localPosition.y, 0);
            }
        }

        if (GetGameTile().m_gameWorldPerk != null)
        {
            if (m_worldPerkIndicator.activeSelf == false)
            {
                if (GetGameTile().m_gameWorldPerk.IsGold() && (GetGameTile().m_isFog && !GetGameTile().m_isSoftFog))
                {
                    m_worldPerkIndicator.SetActive(false);
                }
                else
                {
                    m_worldPerkIndicator.SetActive(true);
                }
            }
        }
        else
        {
            m_worldPerkIndicator.SetActive(false);
        }

        if (GetGameTile().HasBuilding())
        {
            GameBuildingBase building = GetGameTile().GetBuilding();

            m_titleHolder.SetActive(true);
            m_nameText.text = building.GetName();
            m_healthText.text = "" + building.m_curHealth;
        }
        else
        {
            m_titleHolder.SetActive(false);
        }

        m_renderer.sprite = GetGameTile().GetIcon();
        m_tintRenderer.sprite = GetGameTile().GetIconWhite();
        m_fogRenderer.sprite = GetGameTile().GetIcon();

        if (GameHelper.IsInLevelBuilder())
        {
            bool hasSpawnPoint = GetGameTile().HasSpawnPoint();
            int numEventMarkers = GetGameTile().GetEventMarkers().Count;
            bool hasEventMarker = GetGameTile().HasEventMarker();

            m_spawnIndicator.SetActive(hasSpawnPoint);
            m_spawnText.gameObject.SetActive(hasSpawnPoint);
            m_specialTileIndicator.SetActive(hasEventMarker);
            m_specialTileText.gameObject.SetActive(hasEventMarker);

            if (hasSpawnPoint)
            {
                int numSpawnPoints = GetGameTile().GetSpawnPoint().m_spawnPointMarkers.Count;
                string desc = "";
                for (int i = 0; i < numSpawnPoints; i++)
                {
                    desc += GetGameTile().GetSpawnPoint().m_spawnPointMarkers[i];
                    if (i < numSpawnPoints - 1)
                    {
                        desc += ",";
                    }
                }

                m_spawnText.text = desc;
            }

            if (hasEventMarker)
            {
                string desc = "";
                for (int i = 0; i < numEventMarkers; i++)
                {
                    desc += GetGameTile().GetEventMarkers()[i];
                    if (i < numEventMarkers - 1)
                    {
                        desc += ",";
                    }
                    m_specialTileText.text = desc;
                }
            }
        }
        else
        {
            m_spawnIndicator.SetActive(false);
            m_specialTileIndicator.SetActive(false);
            m_specialTileText.gameObject.SetActive(false);
            m_spawnText.gameObject.SetActive(false);
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
                if (Globals.m_testSpawnEnemyUnit != null && !GetGameTile().IsOccupied())
                {
                    GameEnemyUnit newEnemyUnit = GameUnitFactory.GetEnemyUnitClone(Globals.m_testSpawnEnemyUnit, WorldController.Instance.m_gameController.m_gameOpponent);
                    GetGameTile().PlaceUnit(newEnemyUnit);
                    newEnemyUnit.OnSummon();
                    WorldController.Instance.m_gameController.m_gameOpponent.AddControlledUnit(newEnemyUnit);
                    Globals.m_testSpawnEnemyUnit = null;
                }
                
                if (Globals.m_selectedUnit != null && !Globals.m_selectedUnit.GetUnit().m_isDead)
                {
                    m_tintRenderer.color = UIHelper.GetSelectValidTintColor(Globals.m_selectedUnit.CanMoveToWorldTileFromCurPosition(GetGameTile()));
                }
                else if (Globals.m_selectedCard != null)
                {
                    if (GameHelper.GetGameController().m_runStateType != RunStateType.Intermission)
                    {
                        m_tintRenderer.color = UIHelper.GetSelectValidTintColor(Globals.m_selectedCard.m_card.IsValidToPlay(GetGameTile()));
                    }
                }
                else if (Globals.m_selectedIntermissionBuilding != null)
                {
                    m_tintRenderer.color = UIHelper.GetSelectValidTintColor(Globals.m_selectedIntermissionBuilding.IsValidToPlay(GetGameTile()));
                }
                else if (m_gameTile.IsStorm())
                {
                    m_tintRenderer.color = UIHelper.GetStormTintColor();
                }
                else
                {
                    m_tintRenderer.color = UIHelper.GetValidTintColor(true);
                }
            }
            else
            {
                if (Globals.m_selectedAction != null &&
                    Globals.m_selectedAction.GetName() == "Rebuild" &&
                    GetGameTile().GetTerrain() is ContentRubbleTerrain)
                {
                    m_tintRenderer.color = UIHelper.GetValidTintColor(true);
                }
                else if (m_shouldAlertTint)
                {
                    m_tintRenderer.color = UIHelper.GetValidTintColor(false);
                }
                else if (Globals.m_selectedCard != null && Globals.m_selectedCard.m_card.IsValidToPlay(GetGameTile()) && GameHelper.GetGameController().m_runStateType != RunStateType.Intermission)
                {
                    m_tintRenderer.color = UIHelper.GetValidTintColor(true);
                }
                else if (Globals.m_selectedCard != null && GameHelper.GetGameController().m_runStateType != RunStateType.Intermission && m_inSpellcraftRange > 0)
                {
                    m_tintRenderer.color = UIHelper.GetSpellcraftTint(m_inSpellcraftRange);
                }
                else if (m_aoeRangeCount > 0)
                {
                    m_tintRenderer.color = UIHelper.GetAoeRangeTint(m_aoeRangeCount);
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
                    !GetGameTile().m_isFog)
                {
                    m_tintRenderer.color = UIHelper.GetValidTintColor(true);
                }
                else if (Globals.m_selectedTile != null &&
                    Globals.m_selectedTile.m_gameTile.HasBuilding() &&
                    m_inBuildingRange > 0)
                {
                    m_tintRenderer.color = UIHelper.GetBuildingRangeTint(m_inBuildingRange);
                }
                else if (Globals.m_hoveredTile != null &&
                    Globals.m_hoveredTile.m_gameTile.HasBuilding() &&
                    m_inBuildingRange > 0)
                {
                    m_tintRenderer.color = UIHelper.GetBuildingRangeTint(m_inBuildingRange);
                }
                else if (m_gameTile.IsStorm())
                {
                    m_tintRenderer.color = UIHelper.GetStormTintColor();
                }
                else
                {
                    m_tintRenderer.color = UIHelper.GetDefaultTintColor();
                }
            }
        }

        //Handle Frame Color
        if (GetGameTile().m_isFog || (GameHelper.GetGameController() != null && GameHelper.GetGameController().m_runStateType == RunStateType.Intermission))
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

    public void OnMouseDownExt()
    {
        if (!GetGameTile().m_isFog)
        {
            OnMouseDownImpl(false);
        }
    }

    void OnMouseDown()
    {
        OnMouseDownImpl(true);
    }

    private void OnMouseDownImpl(bool uiShouldBlock)
    {
        if (!Globals.m_canSelect)
        {
            return;
        }

        if (UIHelper.UIShouldBlockClick() && uiShouldBlock)
        {
            return;
        }

        if (GameHelper.IsInLevelBuilder())
        {
            HandleLevelBuilderMouseDown();
            return;
        }

        GameActionIntermission action = Globals.m_selectedAction;
        if (action != null && GameHelper.GetGameController().m_runStateType == RunStateType.Intermission)
        {
            if (action.GetName() == "Rebuild" && GetGameTile().GetTerrain() is ContentRubbleTerrain)
            {
                GetGameTile().RestoreBuilding();
                UIHelper.SelectAction(action);
                return;
            }
        }

        UICard selectedCard = Globals.m_selectedCard;
        if (selectedCard != null && GameHelper.GetGameController().m_runStateType != RunStateType.Intermission)
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
                    if (!GetGameTile().CanPlace())
                    {
                        AudioHelper.PlaySFX(AudioHelper.UIError);
                        UIHelper.CreateWorldElementNotification("Out of placement range.", false, gameObject);
                    }
                    else if (GetGameTile().IsOccupied())
                    {
                        AudioHelper.PlaySFX(AudioHelper.UIError);
                        UIHelper.CreateWorldElementNotification("Tile already occupied.", false, gameObject);
                    }
                    else if (!GetGameTile().IsPassable(selectedCard.m_unitCard.GetUnit(), false))
                    {
                        AudioHelper.PlaySFX(AudioHelper.UIError);
                        UIHelper.CreateWorldElementNotification("Unit cannot stand on tile.", false, gameObject);
                    }
                }
                else
                {
                    if (GetGameTile().IsOccupied())
                    {
                        if (selectedCard.m_card.IsValidToPlay(GetGameTile().GetOccupyingUnit()))
                        {
                            GameHelper.PlayCardOnUnit(Globals.m_selectedCard, GetGameTile().GetOccupyingUnit());
                        }
                    }
                    else
                    {
                        AudioHelper.PlaySFX(AudioHelper.UIError);
                        UIHelper.CreateWorldElementNotification("This doesn't target tiles.", false, gameObject);
                    }
                }
            }
        }

        WorldUnit selectedUnit = Globals.m_selectedUnit;
        if (selectedUnit != null && GameHelper.GetGameController().m_runStateType != RunStateType.Intermission)
        {
            if (GetGameTile().HasBuilding() &&
                GetGameTile().GetBuilding().GetTeam() == Team.Enemy &&
                !GetGameTile().GetBuilding().m_isDestroyed &&
                selectedUnit.GetUnit().CanHitBuilding(GetGameTile().GetBuilding()))
            {
                selectedUnit.GetUnit().HitBuilding(GetGameTile().GetBuilding());
            }
            else if (selectedUnit.GetUnit().GetRootedKeyword() != null)
            {
                AudioHelper.PlaySFX(AudioHelper.UIError);
                UIHelper.CreateWorldElementNotification("Unit is Rooted.", false, gameObject);
            }
            else if (selectedUnit.GetUnit().CanMoveTo(GetGameTile()))
            {
                selectedUnit.MoveTo(GetGameTile());
            }
            else
            {
                if (GetGameTile().IsPassable(selectedUnit.GetUnit(), false))
                {
                    int pathLength = WorldGridManager.Instance.GetPathLength(selectedUnit.GetUnit().GetGameTile(), GetGameTile(), true, false, true);
                    if (pathLength == 1 && !GetGameTile().IsOccupied())
                    {
                        AudioHelper.PlaySFX(AudioHelper.UIError);
                        UIHelper.CreateWorldElementNotification("Can't move, difficult terrain requires " + GetGameTile().GetCostToPass(selectedUnit.GetUnit()) + " Stamina.", false, gameObject);
                    }
                    else
                    {
                        if (GetGameTile().GetOccupyingUnit() == Globals.m_selectedUnit.GetUnit())
                        {
                            AudioHelper.PlaySFX(AudioHelper.UIError);
                            UIHelper.CreateWorldElementNotification("Already here.", false, gameObject);
                        }
                        else if (GetGameTile().IsOccupied())
                        {
                            AudioHelper.PlaySFX(AudioHelper.UIError);
                            UIHelper.CreateWorldElementNotification("Already occupied.", false, gameObject);
                        }
                        else
                        {
                            AudioHelper.PlaySFX(AudioHelper.UIError);
                            UIHelper.CreateWorldElementNotification("Out of movement range.", false, gameObject);
                        }
                    }
                }
                else
                {
                    if (!GetGameTile().IsOccupied())
                    {
                        AudioHelper.PlaySFX(AudioHelper.UIError);
                        UIHelper.CreateWorldElementNotification(GetGameTile().GetName() + " is impassable.", false, gameObject);
                    }
                    else
                    {
                        AudioHelper.PlaySFX(AudioHelper.UIError);
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

        if ((selectedUnit == null && selectedBuilding == null && selectedCard == null) || GameHelper.GetGameController().m_runStateType == RunStateType.Intermission)
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

        if (!m_isShowingTooltip && GetGameTile().HasBuilding())
        {
            HandleTooltip();

            m_isShowingTooltip = true;

            UIHelper.SetBuildingTiles();
        }

        m_isHovered = true;

        Globals.m_hoveredTile = this;
    }

    void OnMouseExit()
    {
        if (m_isShowingTooltip)
        {
            UITooltipController.Instance.ClearTooltipStack();

            m_isShowingTooltip = false;
        }

        if (GetGameTile().HasBuilding())
        {
            UIHelper.ClearBuildingTiles();
        }

        m_isHovered = false;

        Globals.m_hoveredTile = null;
    }

    public void TryAddOccupyingUnit()
    {
        if (GetGameTile().IsOccupied() && m_occupyingUnitObj == null)
        {
            m_occupyingUnitObj = FactoryManager.Instance.GetFactory<UIUnitFactory>().CreateObject<WorldUnit>(this);
        }
    }

    public bool ClearFog()
    {
        if (!GetGameTile().m_isFog)
        {
            return false;
        }

        if (GetGameTile().GetTerrain() != null && GetGameTile().GetTerrain().IsCorruption())
        {
            GetGameTile().m_isFog = true;
            GetGameTile().m_isSoftFog = true;
            return true;
        }

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

        return true;
    }

    public int ClearSurroundingFog(int distance)
    {
        int numFogCleared = 0;

        List<WorldTile> toReveal = WorldGridManager.Instance.GetSurroundingWorldTiles(this, distance, 0);

        for (int i = 0; i < toReveal.Count; i++)
        {
            bool didReveal = toReveal[i].ClearFog();

            if (didReveal)
            {
                numFogCleared++;
            }
        }

        return numFogCleared;
    }

    public void ExpandPlaceRange(int distance)
    {
        List<WorldTile> toExpand = WorldGridManager.Instance.GetSurroundingWorldTiles(this, distance, 0);

        for (int i = 0; i < toExpand.Count; i++)
        {
            toExpand[i].GetGameTile().m_numAllowPlacement++;
        }
    }

    public void ReducePlaceRange(int distance)
    {
        List<WorldTile> toReduce = WorldGridManager.Instance.GetSurroundingWorldTiles(this, distance, 0);

        for (int i = 0; i < toReduce.Count; i++)
        {
            toReduce[i].GetGameTile().m_numAllowPlacement--;
        }
    }

    public void HandleFogUpdate()
    {
        if (GetGameTile().m_isFog && !GameHelper.IsInLevelBuilder() && !Constants.DebugSeeAllThroughFog)
        {
            if (m_occupyingUnitObj != null)
            {
                m_occupyingUnitObj.SetVisible(false);
            }
            m_fogOfWar.SetActive(true);

            if (GetGameTile().m_isSoftFog || GetGameTile().IsSpecialSoftFogTile())
            {
                m_fogRenderer.color = new Color(m_fogRenderer.color.r, m_fogRenderer.color.g, m_fogRenderer.color.b, 0.35f);
                m_fogRenderer.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
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

    public void RecycleUnit(GameUnit unitReference)
    {
        if (m_occupyingUnitObj == null)
        {
            Debug.LogError("Trying to recycle unit that is not on the tile.");
        }
        
        if (unitReference != null && unitReference != GetGameTile().GetOccupyingUnit())
        {
            Recycler.Recycle<WorldUnit>(m_occupyingUnitObj);
            m_occupyingUnitObj = null;
            TryAddOccupyingUnit();
        }
        else
        {
            m_occupyingUnitObj.GetUnit().SetGameTile(null);
            GetGameTile().ClearUnit();
            Recycler.Recycle<WorldUnit>(m_occupyingUnitObj);
            m_occupyingUnitObj = null;
        }
    }

    public void PlaceUnit(WorldUnit newUnit)
    {
        if (m_occupyingUnitObj != null)
        {
            Recycler.Recycle<WorldUnit>(m_occupyingUnitObj);
        }
        
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

    public void AddInBuildingRangeCount()
    {
        m_inBuildingRange++;
    }

    public void AddAoeRangeCount()
    {
        m_aoeRangeCount++;
    }

    public void ClearInBuildingRangeCount()
    {
        m_inBuildingRange = 0;
    }

    public void ClearAoeRangeCount()
    {
        m_aoeRangeCount = 0;
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

    public void HandleTooltip()
    {
        GameBuildingBase building = GetGameTile().GetBuilding();

        if (building == null)
        {
            return;
        }

        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip(building.GetName(), building.GetDesc()));
    }

    private void HandleLevelBuilderMouseDown()
    {
        if (Globals.m_levelCreatorEraserMode)
        {
            GameTile gameTile = GetGameTile();
            if (gameTile.m_gameWorldPerk != null)
            {
                gameTile.m_gameWorldPerk = null;
            }
            else if (gameTile.HasSpawnPoint())
            {
                gameTile.ClearSpawnPoint();
            }
            else if (gameTile.HasEventMarker())
            {
                gameTile.ClearEventMarkers();
            }
            else if (gameTile.HasBuilding())
            {
                gameTile.ClearBuilding();
            }
            else if (gameTile.GetTerrain() != null)
            {
                gameTile.ClearTerrain();
            }
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
                if (!GetGameTile().HasSpawnPoint())
                {
                    GameSpawnPoint gameSpawnPoint = new GameSpawnPoint();
                    GetGameTile().SetSpawnPoint(gameSpawnPoint);
                }

                if (!GetGameTile().GetSpawnPoint().m_spawnPointMarkers.Contains(Globals.m_currentlyPaintingNumberIndex))
                {
                    GetGameTile().GetSpawnPoint().m_spawnPointMarkers.Add(Globals.m_currentlyPaintingNumberIndex);
                    Debug.Log("Add Spawn point index" + Globals.m_currentlyPaintingNumberIndex);
                }
            }
            else if (Globals.m_currentlyPaintingType == typeof(int))
            {
                if (!GetGameTile().HasEventMarker(Globals.m_currentlyPaintingNumberIndex))
                {
                    GetGameTile().AddEventMarker(Globals.m_currentlyPaintingNumberIndex);
                    Debug.Log("Add Event Tile index" + Globals.m_currentlyPaintingNumberIndex);
                }
            }
            else if (Globals.m_currentlyPaintingType == typeof(GameWorldPerk))
            {
                if (GetGameTile().m_gameWorldPerk == null)
                {
                    GetGameTile().m_gameWorldPerk = new GameWorldPerk(GetGameTile(), GameEventFactory.GetRandomEvent(GetGameTile()));
                }
            }
        }
    }
}
