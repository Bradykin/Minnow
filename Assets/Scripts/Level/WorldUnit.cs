﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class WorldUnit : MonoBehaviour
{
    public SpriteRenderer m_tintRenderer;
    public SpriteRenderer m_renderer;
    public UIStaminaContainer m_staminaContainer;
    public TMP_Text m_titleText;
    public TMP_Text m_statsText;
    private BoxCollider2D m_collider;

    public GameObject m_holder;
    public GameObject m_titleBlock;

    private bool m_isHovered;
    private bool m_isShowingTooltip;

    private Vector3 m_moveTarget = new Vector3();
    private float m_movementSpeed = 25.0f;

    public SpriteRenderer m_damageShieldIndicator;

    private GameUnit m_unit;

    public void Init(GameUnit unit)
    {
        m_unit = unit;
        unit.m_worldUnit = this;

        m_renderer.sprite = GetUnit().m_icon;
        m_tintRenderer.sprite = GetUnit().m_iconWhite;

        m_staminaContainer.Init(GetUnit().GetCurStamina(), GetUnit().GetMaxStamina(), GetUnit().GetTeam());

        if (GetUnit().GetTeam() == Team.Player && !Globals.loadingRun)
        {
            //Not ideal - this is calling SetHealthValues twice in init. This is done to make sure that the stamina value is correct before selecting tiles
            m_unit.SetHealthStaminaValues();

            UIHelper.SelectUnit(this);
        }

        m_collider = GetComponent<BoxCollider2D>();

        m_damageShieldIndicator.gameObject.transform.localPosition = new Vector3(
            m_damageShieldIndicator.gameObject.transform.localPosition.x - GetUnit().GetWorldTilePositionAdjustment().x,
            m_damageShieldIndicator.gameObject.transform.localPosition.y - GetUnit().GetWorldTilePositionAdjustment().y,
            m_damageShieldIndicator.gameObject.transform.localPosition.z - GetUnit().GetWorldTilePositionAdjustment().z);
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

        m_damageShieldIndicator.gameObject.SetActive(GetUnit().GetDamageShieldKeyword() != null);

        if (m_moveTarget != gameObject.transform.position)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_moveTarget, m_movementSpeed * Time.deltaTime);
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

        m_titleBlock.SetActive(true);

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

        GameController gameController = GameHelper.GetGameController();
        if (gameController != null && gameController.m_runStateType == RunStateType.Intermission)
        {
            return;
        }

        if (Globals.m_selectedCard != null)
        {
            if (Globals.m_selectedCard.m_card.IsValidToPlay(GetUnit()))
            {
                UICard card = Globals.m_selectedCard;
                WorldController.Instance.PlayCard(Globals.m_selectedCard);
                card.m_card.PlayCard(GetUnit());
                m_tintRenderer.color = UIHelper.GetDefaultTintColorForTeam(GetUnit().GetTeam());
                WorldController.Instance.PostPlayCard();
            }
            else
            {
                if (Globals.m_selectedCard.m_card is GameCardSpellBase)
                {
                    UIHelper.CreateWorldElementNotification("Invalid target for " + Globals.m_selectedCard.m_card.GetBaseName(), false, gameObject);
                }
            }
        }
        else if (GetUnit().GetTeam() == Team.Enemy && GetUnit().GetFadeKeyword() != null)
        {
            UIHelper.CreateWorldElementNotification(GetUnit().GetName() + " has Fade and cannot be targeted.", false, gameObject);
        }
        else if (Globals.m_selectedUnit != null && Globals.m_selectedUnit.GetUnit().CanHitUnit(GetUnit()))
        {
            Globals.m_selectedUnit.PlayHitAnim();
            Globals.m_selectedUnit.GetUnit().HitUnit(GetUnit(), Globals.m_selectedUnit.GetUnit().GetDamageToDealTo(GetUnit()));
        }
        else if (CanSelect())
        {
            UIHelper.SelectUnit(this);

            m_tintRenderer.color = UIHelper.GetSelectTintColor(Globals.m_selectedUnit == this);
        }
        else if (GetUnit().GetTeam() == Team.Player) //This means that the target doesn't have enough Stamina to be selected (typically 0)
        {
            UIHelper.CreateWorldElementNotification(GetUnit().GetName() + " has no Stamina.", false, gameObject);
        }
        else if (GetUnit().GetTeam() == Team.Enemy)
        {
            if (Globals.m_selectedUnit != null)
            {
                if (!Globals.m_selectedUnit.GetUnit().IsInRangeOfUnit(GetUnit()))
                {
                    if (GetUnit().GetWorldTile().IsAttackable())
                    {
                        UIHelper.CreateWorldElementNotification("Move into range to attack.", false, GetUnit().m_worldUnit.gameObject);
                    }
                    else
                    {
                        UIHelper.CreateWorldElementNotification("Out of range.", false, GetUnit().m_worldUnit.gameObject);
                    }
                }
                else if (!Globals.m_selectedUnit.GetUnit().HasStaminaToAttack(GetUnit()))
                {
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
        GetUnit().OnMoveBegin();
        GetUnit().GetWorldTile().ClearUnit();
        targetTile.GetWorldTile().PlaceUnit(this);
        GetUnit().MoveTo(targetTile, spendStamina);
        
        GetUnit().OnMoveEnd();

        m_moveTarget = targetTile.GetWorldTile().GetScreenPositionForUnit(this);

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
        if (Globals.m_selectedUnit != this)
        {
            m_tintRenderer.color = UIHelper.GetDefaultTintColorForTeam(GetUnit().GetTeam());
        }
    }

    public GameUnit GetUnit()
    {
        return m_unit;
    }

    public void PlayHitAnim()
    {
        //nmartino - Implement this
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
}
