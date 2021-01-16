using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class WorldUnit : MonoBehaviour, IRecycled, ICustomRecycle
{
    public UIUnitAttackAnimation m_attackAnimation;
    public UIUnitDeathAnimation m_deathAnimation;

    public SpriteRenderer m_tintRenderer;
    public SpriteRenderer m_renderer;
    public UIStaminaContainer m_staminaContainer;
    public TMP_Text m_titleText;
    public TMP_Text m_statsText;
    private BoxCollider2D m_collider;

    public GameObject m_holder;
    public GameObject m_titleBlock;
    public RectTransform m_titleHolderRectTransform;

    private bool m_isHovered;
    private bool m_isShowingTooltip;

    private List<WorldTile> m_movePath = new List<WorldTile>();
    private Vector3 m_moveTarget = new Vector3();
    private float m_movementSpeed = 25.0f;

    public SpriteMask m_indicatorMask;
    public SpriteRenderer m_damageShieldIndicator;
    public SpriteRenderer m_rootedIndicator;
    public SpriteRenderer m_brittleIndicator;
    public SpriteRenderer m_bleedingIndicator;
    
    private GameUnit m_unit;

    public bool IsMoving => m_movePath.Count > 0;

    public bool IsRecycled { get => m_isRecycled; set => m_isRecycled = value; }
    private bool m_isRecycled;

    public void Init(GameUnit unit)
    {
        m_unit = unit;
        unit.m_worldUnit = this;

        m_renderer.sprite = GetUnit().m_icon;
        m_indicatorMask.sprite = GetUnit().m_icon;
        m_tintRenderer.sprite = GetUnit().m_iconWhite;

        m_staminaContainer.Init(GetUnit().GetCurStamina(), GetUnit().GetMaxStamina(), GetUnit().GetTeam());

        if (GetUnit().GetTeam() == Team.Player && !Globals.loadingRun)
        {
            //Not ideal - this is calling SetHealthValues twice in init. This is done to make sure that the stamina value is correct before selecting tiles
            m_unit.SetHealthStaminaValues();

            UIHelper.SelectUnit(this);
        }

        m_collider = GetComponent<BoxCollider2D>();

        m_indicatorMask.gameObject.transform.localPosition = new Vector3(
            m_indicatorMask.gameObject.transform.localPosition.x - GetUnit().GetWorldTilePositionAdjustment().x,
            m_indicatorMask.gameObject.transform.localPosition.y - GetUnit().GetWorldTilePositionAdjustment().y,
            m_indicatorMask.gameObject.transform.localPosition.z - GetUnit().GetWorldTilePositionAdjustment().z);
    }

    public void SetMoveTarget(Vector3 moveTarget)
    {
        m_moveTarget = moveTarget;
    }

    void Update()
    {
        if (GetUnit() == null)
        {
            return;
        }

        if (m_titleHolderRectTransform != null)
        {
            if (this == Globals.m_selectedUnit)
            {
                m_titleHolderRectTransform.localPosition = new Vector3(m_titleHolderRectTransform.localPosition.x, m_titleHolderRectTransform.localPosition.y, -2);
            }
            else if (m_isHovered)
            {
                m_titleHolderRectTransform.localPosition = new Vector3(m_titleHolderRectTransform.localPosition.x, m_titleHolderRectTransform.localPosition.y, -1);
            }
            else
            {
                m_titleHolderRectTransform.localPosition = new Vector3(m_titleHolderRectTransform.localPosition.x, m_titleHolderRectTransform.localPosition.y, 0);
            }
        }

        m_damageShieldIndicator.gameObject.SetActive(GetUnit().GetDamageShieldKeyword() != null);
        m_brittleIndicator.gameObject.SetActive(GetUnit().GetBrittleKeyword() != null);
        m_bleedingIndicator.gameObject.SetActive(GetUnit().GetBleedKeyword() != null);
        m_rootedIndicator.gameObject.SetActive(GetUnit().GetRootedKeyword() != null);

        if (m_movePath.Count > 0)
        {
            float movementSpeed = m_movementSpeed;
            if (!PlayerDataManager.PlayerAccountData.m_followEnemy)
            {
                movementSpeed *= 1.5f;
            }
            
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_moveTarget, movementSpeed * Time.deltaTime);
        
            if (gameObject.transform.position == m_moveTarget)
            {
                m_movePath.RemoveAt(0);
                if (m_movePath.Count > 0)
                {
                    m_moveTarget = m_movePath[0].GetScreenPositionForUnit(this);
                    AudioHelper.PlaySFXForWalkOnTile(m_movePath[0].GetGameTile(), GetUnit());
                }
            }
        }

        if (this == Globals.m_selectedUnit || this == Globals.m_selectedEnemy || (m_isHovered && GetUnit().GetCurStamina() != 0 && Globals.m_canSelect && Globals.m_selectedCard == null && GetUnit().GetTeam() == Team.Player))
        {
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        }
        else
        {
            transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        }

        if (!m_isHovered)
        {
            if (this == Globals.m_selectedUnit || this == Globals.m_selectedEnemy)
            {
                m_tintRenderer.color = UIHelper.GetSelectTintColor(true);
            }
            else
            {
                if (Globals.m_selectedCard != null && Globals.m_selectedCard.m_card is GameCardSpellBase)
                {
                    m_tintRenderer.color = UIHelper.GetValidTintColor(Globals.m_selectedCard.m_card.IsValidToPlay(GetUnit()));
                }
                else
                {
                    m_tintRenderer.color = UIHelper.GetDefaultTintColor();
                }
            }
        }

        m_titleBlock.SetActive(!GetUnit().m_isDead);

        if (GetUnit().GetCurStamina() == 0 && Globals.m_selectedUnit == this)
        {
            UIHelper.UnselectUnit();
        }

        m_staminaContainer.DoUpdate(GetUnit().GetCurStamina(), GetUnit().GetMaxStamina(), GetUnit().GetTeam());
        if (GetUnit().HasCustomName())
        {
            m_titleText.text = GetUnit().GetName();
        }
        else
        {
            m_titleText.text = GetUnit().GetName();
        }
        m_statsText.text = GetUnit().GetPower() + "/" + GetUnit().GetCurHealth();
    }

    void OnMouseDown()
    {
        OnMouseDownExt();
    }

    public void OnMouseDownExt()
    {
        if (UIHelper.UIShouldBlockClick())
        {
            return;
        }

        if (GetUnit().m_isDead)
        {
            return;
        }

        GameController gameController = GameHelper.GetGameController();
        if (gameController != null && gameController.m_runStateType == RunStateType.Intermission)
        {
            return;
        }

        if (Globals.m_selectedCard != null)
        {
            if (Globals.m_selectedCard.m_card.IsValidToPlay(GetUnit()))
            {
                GameHelper.PlayCardOnUnit(Globals.m_selectedCard, GetUnit());
                m_tintRenderer.color = UIHelper.GetDefaultTintColorForTeam(GetUnit().GetTeam());
            }
            else
            {
                if (Globals.m_selectedCard.m_card is GameCardSpellBase)
                {
                    AudioHelper.PlaySFX(AudioHelper.UIError);
                    UIHelper.CreateWorldElementNotification("Invalid target for " + Globals.m_selectedCard.m_card.GetBaseName(), false, gameObject);
                }
            }
        }
        else if (GetUnit().GetTeam() == Team.Enemy && GetUnit().GetFadeKeyword() != null)
        {
            AudioHelper.PlaySFX(AudioHelper.UIError);
            UIHelper.CreateWorldElementNotification(GetUnit().GetName() + " has Fade and cannot be targeted.", false, gameObject);
        }
        else if (Globals.m_selectedUnit != null && Globals.m_selectedUnit.GetUnit().CanHitUnit(GetUnit()))
        {
            Globals.m_selectedUnit.GetUnit().HitUnit(GetUnit(), Globals.m_selectedUnit.GetUnit().GetDamageToDealTo(GetUnit()));
        }
        else if (CanSelect())
        {
            UIHelper.SelectUnit(this);

            m_tintRenderer.color = UIHelper.GetSelectTintColor(Globals.m_selectedUnit == this);
        }
        else if (GetUnit().GetTeam() == Team.Player) //This means that the target doesn't have enough Stamina to be selected (typically 0)
        {
            AudioHelper.PlaySFX(AudioHelper.UIError);
            UIHelper.CreateWorldElementNotification(GetUnit().GetName() + " has no Stamina.", false, gameObject);
        }
        else if (GetUnit().GetTeam() == Team.Enemy)
        {
            if (Globals.m_selectedUnit != null)
            {
                if (!Globals.m_selectedUnit.GetUnit().IsInRangeOfUnit(GetUnit()))
                {
                    /*if (GetUnit().GetWorldTile().IsAttackable())
                    {*/
                        if (WorldGridManager.Instance.GetTilesInRangeToMoveAndAttack(Globals.m_selectedUnit.GetUnit().GetGameTile(), false, false).Contains(GetUnit().GetGameTile()))
                        {
                            GameTile tileToMoveTo = Globals.m_selectedUnit.GetUnit().GetTileMoveInRangeToAttack(GetUnit().GetGameTile());
                            if (tileToMoveTo != null)
                            {
                                Globals.m_selectedUnit.MoveTo(tileToMoveTo);
                                Globals.m_selectedUnit.GetUnit().HitUnit(GetUnit(), Globals.m_selectedUnit.GetUnit().GetDamageToDealTo(GetUnit()));
                            }
                            else
                            {
                                AudioHelper.PlaySFX(AudioHelper.UIError);
                                UIHelper.CreateWorldElementNotification("Out of range to attack.", false, GetUnit().m_worldUnit.gameObject);
                            }
                        }
                        else
                        {
                            AudioHelper.PlaySFX(AudioHelper.UIError);
                            UIHelper.CreateWorldElementNotification("Out of range to attack.", false, GetUnit().m_worldUnit.gameObject);
                        }
                    /*}
                    else
                    {
                        AudioHelper.PlaySFX(AudioHelper.UIError);
                        UIHelper.CreateWorldElementNotification("Out of range.", false, GetUnit().m_worldUnit.gameObject);
                    }*/
                }
                else if (!Globals.m_selectedUnit.GetUnit().HasStaminaToAttack(GetUnit()))
                {
                    AudioHelper.PlaySFX(AudioHelper.UIError);
                    UIHelper.CreateWorldElementNotification("Requires " + Globals.m_selectedUnit.GetUnit().GetStaminaToAttack(GetUnit()) + " Stamina to attack.", false, GetUnit().m_worldUnit.gameObject);
                }
            }
            else
            {
                UIHelper.SelectEnemy(this);
            }
        }
    }

    public bool CanMoveToWorldTileFromCurPosition(GameTile toMoveTo)
    {
        return GetUnit().CanMoveTo(toMoveTo);
    }

    public void MoveTo(GameTile targetTile, bool spendStamina = true)
    {
        List<GameTile> path = WorldGridManager.Instance.CalculateAStarPath(GetUnit().GetGameTile(), targetTile, false, false, false, false);

        for (int i = 0; i < path.Count; i++)
        {
            m_movePath.Add(path[i].GetWorldTile());
        }

        GetUnit().OnMoveBegin();
        GetUnit().GetWorldTile().ClearUnit();
        targetTile.GetWorldTile().PlaceUnit(this);
        GetUnit().MoveTo(targetTile, spendStamina);
        
        GetUnit().OnMoveEnd();

        m_movePath.RemoveAt(0); //Remove current tile
        m_moveTarget = m_movePath[0].GetScreenPositionForUnit(this);
        AudioHelper.PlaySFXForWalkOnTile(GetUnit().GetGameTile(), GetUnit());

        m_renderer.sortingOrder = targetTile.GetWorldTile().m_renderer.sortingOrder;
        m_tintRenderer.sortingOrder = targetTile.GetWorldTile().m_renderer.sortingOrder;
    }

    public void SetVisible(bool isVisible)
    {
        m_holder.SetActive(isVisible);
    }

    void OnMouseOver()
    {
        if (UIHelper.UIShouldBlockClick())
        {
            return;
        }

        if (!m_isShowingTooltip && !GetUnit().GetGameTile().m_isFog)
        {
            HandleTooltip();

            m_isShowingTooltip = true;
        }

        m_isHovered = true;
        UIHelper.SetAoeTiles(GetUnit());

        if (Globals.m_selectedUnit != null)
        {
            bool canHit = Globals.m_selectedUnit.GetUnit().CanHitUnit(GetUnit());
            if (GetUnit().GetTeam() == Team.Player && canHit)
            {
                m_tintRenderer.color = UIHelper.GetValidTintColor(true);
            }
            else if (GetUnit().GetTeam() == Team.Enemy)
            {
                m_tintRenderer.color = UIHelper.GetValidTintColor(canHit);
            }
            else if (GetUnit().GetTeam() == Team.Player && !canHit)
            {
                if (Globals.m_selectedUnit == this)
                {
                    m_tintRenderer.color = UIHelper.GetSelectTintColor(CanSelect());
                }
                else
                {
                    m_tintRenderer.color = UIHelper.GetValidTintColor(CanSelect());
                }
            }
        }
        else if (Globals.m_selectedCard != null)
        {
            m_tintRenderer.color = UIHelper.GetValidTintColor(Globals.m_selectedCard.m_card.IsValidToPlay(GetUnit()));
        } 
        else if (Globals.m_selectedUnit == null)
        {
            m_tintRenderer.color = UIHelper.GetValidTintColor(CanSelect());
        }
    }

    void OnMouseExit()
    {
        UITooltipController.Instance.ClearTooltipStack();

        m_isShowingTooltip = false;

        m_isHovered = false;
        UIHelper.ClearAoeTiles();
        if (Globals.m_selectedUnit != this)
        {
            m_tintRenderer.color = UIHelper.GetDefaultTintColorForTeam(GetUnit().GetTeam());
        }
    }

    public GameUnit GetUnit()
    {
        return m_unit;
    }

    public void PlayHitAnim(WorldUnit targetUnit)
    {
        m_attackAnimation.PlayAnim(targetUnit);
    }

    public void PlayHitAnim(WorldTile targetTile)
    {
        m_attackAnimation.PlayAnim(targetTile);
    }

    public void PlayDeathAnim()
    {
        m_titleBlock.SetActive(false);
        m_deathAnimation.PlayAnim(this);
    }

    private bool CanSelect()
    {
        if (Globals.m_selectedCard != null)
        {
            return false;
        }

        if (GetUnit().GetCurStamina() <= 0)
        {
            return false;
        }

        if (GetUnit().GetTeam() == Team.Enemy)
        {
            return false;
        }

        if (GetUnit() is ContentWildwoodExplorer)
        {
            return true;
        }

        return true;
    }

    public void HandleTooltip()
    {
        if (Globals.m_canSelect)
        {
            if (GetUnit().UsesBigTooltip())
            {
                UIHelper.CreateBigUnitTooltip(GetUnit());
            }
            else
            {
                UIHelper.CreateUnitTooltip(GetUnit());
            }
        }
    }

    public void CustomRecycle(params object[] args)
    {
        m_isRecycled = true;
        m_unit = null;
    }
}
