﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIEntity : MonoBehaviour
{
    public SpriteRenderer m_tintRenderer;
    public SpriteRenderer m_renderer;
    public UIAPContainer m_apContainer;
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

    private GameEntity m_entity;

    public void Init(GameEntity entity)
    {
        m_moveTarget = gameObject.transform.position;

        m_entity = entity;
        entity.m_uiEntity = this;

        m_renderer.sprite = GetEntity().m_icon;
        m_tintRenderer.sprite = GetEntity().m_iconWhite;

        m_apContainer.Init(GetEntity().GetCurAP(), GetEntity().GetMaxAP(), GetEntity().GetTeam());

        if (GetEntity().GetTeam() == Team.Player)
        {
            UIHelper.SelectEntity(this);
        }

        m_collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (GetEntity() == null)
        {
            return;
        }

        m_damageShieldIndicator.gameObject.SetActive(GetEntity().GetKeyword<GameDamageShieldKeyword>() != null);

        if (m_moveTarget != gameObject.transform.position)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_moveTarget, m_movementSpeed);
        }

        if (this == Globals.m_selectedEntity || this == Globals.m_selectedEnemy || (m_isHovered && GetEntity().GetCurAP() != 0 && Globals.m_canSelect && Globals.m_selectedCard == null && GetEntity().GetTeam() == Team.Player))
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

        if (GetEntity().GetCurAP() == 0 && Globals.m_selectedEntity == this)
        {
            UIHelper.UnselectEntity();
        }

        m_apContainer.DoUpdate(GetEntity().GetCurAP(), GetEntity().GetMaxAP(), GetEntity().GetTeam());
        if (GetEntity().HasCustomName())
        {
            m_titleText.text = GetEntity().GetCustomName();
        }
        else
        {
            m_titleText.text = GetEntity().GetName();
        }
        m_healthText.text = GetEntity().GetCurHealth() + "/" + GetEntity().GetMaxHealth();
        m_powerText.text = "" + GetEntity().GetPower();
    }

    void OnMouseDown()
    {
        if (Globals.m_selectedCard != null)
        {
            if (Globals.m_selectedCard.m_card.IsValidToPlay(GetEntity()))
            {
                //Keep an eye on this if playing cards on units, particularly ones that draw cards like blood sac, do some weird behaviour .I modified this from:
                //                  Globals.m_selectedCard.m_card.PlayCard(GetEntity());
                //                  WorldController.Instance.PlayCard(Globals.m_selectedCard, this);
                UICard card = Globals.m_selectedCard;
                WorldController.Instance.PlayCard(Globals.m_selectedCard);
                card.m_card.PlayCard(GetEntity());
                m_tintRenderer.color = UIHelper.GetDefaultTintColorForTeam(GetEntity().GetTeam());
            }
        }
        else if (Globals.m_selectedEntity != null && Globals.m_selectedEntity.GetEntity().CanHitEntity(GetEntity()))
        {
            Globals.m_selectedEntity.GetEntity().HitEntity(GetEntity());
            Globals.m_selectedEntity.PlayHitAnim();
        }
        else if (CanSelect())
        {
            UIHelper.SelectEntity(this);

            m_tintRenderer.color = UIHelper.GetSelectTintColor(Globals.m_selectedEntity == this);
        }
        else if (GetEntity().GetTeam() == Team.Player) //This means that the target doesn't have enough AP to be selected (typically 0)
        {
            UIHelper.CreateWorldElementNotification(GetEntity().GetName() + " has no AP.", false, gameObject);
        }
        else if (GetEntity().GetTeam() == Team.Enemy)
        {
            UIHelper.SelectEnemy(this);
        }
    }

    public bool CanMoveToWorldTileFromCurPosition(GameTile toMoveTo)
    {
        return GetEntity().CanMoveTo(toMoveTo);
    }

    public void MoveTo(GameTile targetTile)
    {
        GetEntity().GetWorldTile().ClearEntity();
        targetTile.GetWorldTile().PlaceEntity(this);
        GetEntity().MoveTo(targetTile);

        m_moveTarget = targetTile.GetWorldTile().GetScreenPositionForEntity();
    }

    public void SetVisible(bool isVisible)
    {
        m_holder.SetActive(isVisible);
    }

    void OnMouseOver()
    {
        if (!m_isShowingTooltip)
        {
            HandleTooltip();

            m_isShowingTooltip = true;
        }

        m_isHovered = true;
        if (Globals.m_selectedEntity != null)
        {
            bool canHit = Globals.m_selectedEntity.GetEntity().CanHitEntity(GetEntity());
            if (GetEntity().GetTeam() == Team.Player && canHit)
            {
                m_tintRenderer.color = UIHelper.GetValidTintColor(true);
            }
            else if (GetEntity().GetTeam() == Team.Enemy)
            {
                m_tintRenderer.color = UIHelper.GetValidTintColor(canHit);
            }
            else if (GetEntity().GetTeam() == Team.Player && !canHit)
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
            m_tintRenderer.color = UIHelper.GetValidTintColor(Globals.m_selectedCard.m_card.IsValidToPlay(GetEntity()));
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
            m_tintRenderer.color = UIHelper.GetDefaultTintColorForTeam(GetEntity().GetTeam());
        }
    }

    public GameEntity GetEntity()
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

        if (GetEntity().GetCurAP() <= 0)
        {
            return false;
        }

        if (GetEntity().GetTeam() == Team.Enemy)
        {
            return false;
        }

        return true;
    }

    public void HandleTooltip()
    {
        if (Globals.m_canSelect)
        {
            UIHelper.CreateEntityTooltip(GetEntity());
        }
    }
}
