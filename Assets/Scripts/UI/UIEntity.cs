﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIUnit : MonoBehaviour
{
    public SpriteRenderer m_tintRenderer;
    public SpriteRenderer m_renderer;
    public UIStaminaContainer m_staminaContainer;
    public Text m_healthText;
    public Text m_powerText;
    public Text m_titleText;
    private BoxCollider2D m_collider;

    public GameObject m_holder;
    public GameObject m_titleBlock;

    private bool m_isHovered;
    private bool m_isShowingTooltip;

    private Vector3 m_moveTarget = new Vector3();
    private float m_movementSpeed = 0.5f;

    public SpriteRenderer m_damageShieldIndicator;

    private GameUnit m_entity;

    public void Init(GameUnit entity)
    {
        m_moveTarget = gameObject.transform.position;

        m_entity = entity;
        entity.m_worldUnit = this;

        m_renderer.sprite = GetUnit().m_icon;
        m_tintRenderer.sprite = GetUnit().m_iconWhite;

        m_staminaContainer.Init(GetUnit().GetCurStamina(), GetUnit().GetMaxStamina(), GetUnit().GetTeam());

        if (GetUnit().GetTeam() == Team.Player)
        {
            UIHelper.SelectEntity(this);
        }

        m_collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (GetUnit() == null)
        {
            return;
        }

        m_damageShieldIndicator.gameObject.SetActive(GetUnit().GetKeyword<GameDamageShieldKeyword>() != null);

        if (m_moveTarget != gameObject.transform.position)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_moveTarget, m_movementSpeed);
        }

        if (this == Globals.m_selectedEntity || this == Globals.m_selectedEnemy || (m_isHovered && GetUnit().GetCurStamina() != 0 && Globals.m_canSelect && Globals.m_selectedCard == null && GetUnit().GetTeam() == Team.Player))
        {
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            m_collider.size = new Vector2(2.5f, 3.5f);
        }
        else
        {
            transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            m_collider.size = new Vector2(3f, 4.3f);
        }

        if (!m_isHovered)
        {
            if (this == Globals.m_selectedEntity || this == Globals.m_selectedEnemy)
            {
                m_tintRenderer.color = UIHelper.GetSelectTintColor(true);
            }
            else
            {
                m_tintRenderer.color = UIHelper.GetDefaultTintColor();
            }
        }

        m_titleBlock.SetActive(true);

        if (GetUnit().GetCurStamina() == 0 && Globals.m_selectedEntity == this)
        {
            UIHelper.UnselectEntity();
        }

        m_staminaContainer.DoUpdate(GetUnit().GetCurStamina(), GetUnit().GetMaxStamina(), GetUnit().GetTeam());
        if (GetUnit().HasCustomName())
        {
            m_titleText.text = GetUnit().GetCustomName();
        }
        else
        {
            m_titleText.text = GetUnit().GetName();
        }
        m_healthText.text = GetUnit().GetCurHealth() + "/" + GetUnit().GetMaxHealth();
        m_powerText.text = "" + GetUnit().GetPower();
    }

    void OnMouseDown()
    {
        if (UIHelper.UIShouldBlockClick())
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
        }
        else if (Globals.m_selectedEntity != null && Globals.m_selectedEntity.GetUnit().CanHitEntity(GetUnit()))
        {
            Globals.m_selectedEntity.GetUnit().HitUnit(GetUnit());
            Globals.m_selectedEntity.PlayHitAnim();
        }
        else if (CanSelect())
        {
            UIHelper.SelectEntity(this);

            m_tintRenderer.color = UIHelper.GetSelectTintColor(Globals.m_selectedEntity == this);
        }
        else if (GetUnit().GetTeam() == Team.Player) //This means that the target doesn't have enough Stamina to be selected (typically 0)
        {
            UIHelper.CreateWorldElementNotification(GetUnit().GetName() + " has no Stamina.", false, gameObject);
        }
        else if (GetUnit().GetTeam() == Team.Enemy)
        {
            if (Globals.m_selectedEntity != null)
            {
                if (!Globals.m_selectedEntity.GetUnit().IsInRangeOfUnit(GetUnit()))
                {
                    UIHelper.CreateWorldElementNotification("Out of range.", false, GetUnit().m_worldUnit.gameObject);
                }
                else if (!Globals.m_selectedEntity.GetUnit().HasStaminaToAttack())
                {
                    UIHelper.CreateWorldElementNotification("Requires " + GetUnit().GetStaminaToAttack() + " Stamina to attack.", false, GetUnit().m_worldUnit.gameObject);
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

    public void MoveTo(GameTile targetTile)
    {
        GetUnit().GetWorldTile().ClearEntity();
        targetTile.GetWorldTile().PlaceEntity(this);
        GetUnit().MoveTo(targetTile);

        m_moveTarget = targetTile.GetWorldTile().GetScreenPositionForUnit();
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

        if (!m_isShowingTooltip)
        {
            HandleTooltip();

            m_isShowingTooltip = true;
        }

        m_isHovered = true;
        if (Globals.m_selectedEntity != null)
        {
            bool canHit = Globals.m_selectedEntity.GetUnit().CanHitEntity(GetUnit());
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
                if (Globals.m_selectedEntity == this)
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
        else if (Globals.m_selectedEntity == null)
        {
            m_tintRenderer.color = UIHelper.GetValidTintColor(CanSelect());
        }
    }

    void OnMouseExit()
    {
        UITooltipController.Instance.ClearTooltipStack();

        m_isShowingTooltip = false;

        m_isHovered = false;
        if (Globals.m_selectedEntity != this)
        {
            m_tintRenderer.color = UIHelper.GetDefaultTintColorForTeam(GetUnit().GetTeam());
        }
    }

    public GameUnit GetUnit()
    {
        return m_entity;
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
            UIHelper.CreateEntityTooltip(GetUnit());
        }
    }
}
