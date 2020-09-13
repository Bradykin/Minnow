using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using UnityEngine.UI;

public class UIEntity : WorldElementBase
{
    public SpriteRenderer m_tintRenderer;
    public SpriteRenderer m_renderer;
    public UIAPContainer m_apContainer;
    public Text m_healthText;
    public Text m_powerText;
    public Text m_titleText;
    private BoxCollider2D m_collider;

    public GameObject m_titleBlock;

    private bool m_isHovered;

    public void Init(GameEntity entity)
    {
        m_gameElement = entity;
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
        if (this == Globals.m_selectedEntity || (m_isHovered && GetEntity().GetCurAP() != 0 && Globals.m_canSelect && Globals.m_selectedCard == null))
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            m_collider.size = new Vector2(2f, 2.9f);
        }
        else
        {
            transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            m_collider.size = new Vector2(3f, 4.3f);
        }

        if (!m_isHovered)
        {
            if (this == Globals.m_selectedEntity)
            {
                UIHelper.SetSelectTintColor(m_tintRenderer, true);
            }
            else
            {
                UIHelper.SetDefaultTintColor(m_tintRenderer);
            }
        }

        //if (m_isHovered || GetEntity().GetTeam() == Team.Player)
        {
            m_titleBlock.SetActive(true);
        }
        //else
        {
            //m_titleBlock.SetActive(false);
        }

        if (GetEntity().GetCurAP() == 0 && Globals.m_selectedEntity == this)
        {
            UIHelper.UnselectEntity();
        }

        m_apContainer.DoUpdate(GetEntity().GetCurAP(), GetEntity().GetMaxAP(), GetEntity().GetTeam());
        m_titleText.text = GetEntity().GetName();
        m_healthText.text = GetEntity().GetCurHealth() + "/" + GetEntity().GetMaxHealth();
        m_powerText.text = "" + GetEntity().GetPower();

        if (GetEntity().m_isDead)
        {
            WorldGridManager.Instance.ClearAllTilesMovementRange();
            GetEntity().m_curTile.ClearEntity();
        }
    }

    void OnMouseDown()
    {
        if (Globals.m_selectedCard != null)
        {
            if (Globals.m_selectedCard.m_card.IsValidToPlay(GetEntity()))
            {
                Globals.m_selectedCard.m_card.PlayCard(GetEntity());
                WorldController.Instance.PlayCard(Globals.m_selectedCard, this);
                UIHelper.SetDefaultTintColorForTeam(m_tintRenderer, GetEntity().GetTeam());
            }
        }
        else if (Globals.m_selectedEntity != null && Globals.m_selectedEntity.GetEntity().CanHitEntity(GetEntity()))
        {
            int damageDealt = Globals.m_selectedEntity.GetEntity().HitEntity(GetEntity());

            if (GetEntity().m_isDead)
            {
                UIHelper.CreateWorldElementNotification("With a mighty blow, " + Globals.m_selectedEntity.GetEntity().GetName() + " slays the " + GetEntity().GetName() + "!", true, Globals.m_selectedEntity);
                UIHelper.CreateWorldElementNotification(GetEntity().GetName() + " dies.", false, this);
            }
            else if (damageDealt == 0)
            {
                UIHelper.CreateWorldElementNotification(Globals.m_selectedEntity.GetEntity().GetName() + " hits " + GetEntity().GetName() + " but it glances right off, dealing " + damageDealt + " damage.", true, Globals.m_selectedEntity);
            }
            else
            {
                UIHelper.CreateWorldElementNotification(Globals.m_selectedEntity.GetEntity().GetName() + " hits " + GetEntity().GetName() + " for " + damageDealt + ".", true, Globals.m_selectedEntity);
                UIHelper.CreateWorldElementNotification(GetEntity().GetName() + " takes " + damageDealt + " damage.", false, this);
            }
        }
        else if (CanSelect())
        {
            UIHelper.SelectEntity(this);

            UIHelper.SetSelectTintColor(m_tintRenderer, Globals.m_selectedEntity == this);
        }
        else if (GetEntity().GetTeam() == Team.Player) //This means that the target doesn't have enough AP to be selected (typically 0)
        {
            UIHelper.CreateWorldElementNotification(GetEntity().GetName() + " can't move any more this turn.", false, this);
        }
    }

    public bool CanMoveToWorldTileFromCurPosition(GameTile toMoveTo)
    {
        return GetEntity().CanMoveTo(toMoveTo);
    }

    void OnMouseOver()
    {
        m_isHovered = true;
        if (Globals.m_selectedEntity != null)
        {
            bool canHit = Globals.m_selectedEntity.GetEntity().CanHitEntity(GetEntity());
            if (GetEntity().GetTeam() == Team.Player && canHit)
            {
                UIHelper.SetValidTintColor(m_tintRenderer, true);
            }
            else if (GetEntity().GetTeam() == Team.Enemy)
            {
                UIHelper.SetValidTintColor(m_tintRenderer, canHit);
            }
            else if (GetEntity().GetTeam() == Team.Player && !canHit)
            {
                if (Globals.m_selectedEntity == this)
                {
                    UIHelper.SetSelectTintColor(m_tintRenderer, CanSelect());
                }
                else
                {
                    UIHelper.SetValidTintColor(m_tintRenderer, CanSelect());
                }
            }
        }
        else if (Globals.m_selectedCard != null)
        {
            UIHelper.SetValidTintColor(m_tintRenderer, Globals.m_selectedCard.m_card.IsValidToPlay(GetEntity()));
        } 
        else if (Globals.m_selectedEntity == null)
        {
            UIHelper.SetValidTintColor(m_tintRenderer, CanSelect());
        }
    }

    void OnMouseExit()
    {
        m_isHovered = false;
        if (Globals.m_selectedEntity != this)
        {
            UIHelper.SetDefaultTintColorForTeam(m_tintRenderer, GetEntity().GetTeam());
        }
    }

    public GameEntity GetEntity()
    {
        return (GameEntity)m_gameElement;
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

    public override void HandleTooltip()
    {
        UIHelper.CreateEntityTooltip(GetEntity());
        UIHelper.CreateTerrainTooltip(GetEntity().m_curTile.GetTerrain());
    }
}
