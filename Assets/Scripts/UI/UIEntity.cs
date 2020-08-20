using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public class UIEntity : WorldElementBase
{
    public SpriteRenderer m_tintRenderer;
    public SpriteRenderer m_renderer;

    private bool m_isHovered;

    void Start()
    {
        UIHelper.SetDefaultTintColor(m_tintRenderer);

        gameObject.AddComponent<UITooltipGenerator>();
    }

    public void Init(GameEntity entity)
    {
        m_gameElement = entity;

        m_renderer.sprite = m_gameElement.m_icon;
    }

    void Update()
    {
        if (!m_isHovered)
        {
            UIHelper.SetSelectTintColor(m_tintRenderer, Globals.m_selectedEntity == this);
        }

        if (GetEntity().GetCurAP() == 0 && Globals.m_selectedEntity == this)
        {
            Globals.m_selectedEntity = null;
        }

        if (GetEntity().m_isDead)
        {
            GetEntity().m_curTile.ClearEntity();
        }
    }

    void OnMouseDown()
    {
        if (CanSelect())
        {
            UIHelper.SelectEntity(this);

            UIHelper.SetSelectTintColor(m_tintRenderer, Globals.m_selectedEntity == this);
        }
        else if (Globals.m_selectedCard != null)
        {
            if (Globals.m_selectedCard.m_card.IsValidToPlay(GetEntity()))
            {
                Globals.m_selectedCard.m_card.PlayCard(GetEntity());
                Globals.m_selectedCard.CardPlayed(this);
                Destroy(Globals.m_selectedCard.gameObject);
                Globals.m_selectedCard = null;
                UIHelper.SetDefaultTintColor(m_tintRenderer);
            }
        }
        else if (GetEntity().GetTeam() == Team.Player) //This means that the target doesn't have enough AP to be selected (typically 0)
        {
            UIHelper.CreateWorldElementNotification(GetEntity().m_name + " can't move any more this turn.", false, this);
        }
        else if (Globals.m_selectedEntity != null)
        {
            if (Globals.m_selectedEntity.GetEntity().CanHitEntity(GetEntity()))
            {
                int damageDealt = Globals.m_selectedEntity.GetEntity().HitEntity(GetEntity());

                if (GetEntity().m_isDead)
                {
                    UIHelper.CreateWorldElementNotification("With a mighty blow, " + Globals.m_selectedEntity.GetEntity().m_name + " slays the " + GetEntity().m_name + "!", true, this);
                }
                else
                {
                    UIHelper.CreateWorldElementNotification(Globals.m_selectedEntity.GetEntity().m_name + " hits " + GetEntity().m_name + " for " + damageDealt + " damage!", true, this);
                }
            }
            else
            {
                if (!Globals.m_selectedEntity.GetEntity().IsInRangeOfEntity(GetEntity()))
                {
                    UIHelper.CreateWorldElementNotification("Out of range", false, this);
                }
                else if (!Globals.m_selectedEntity.GetEntity().HasAPToAttack())
                {
                    UIHelper.CreateWorldElementNotification("No AP to attack", false, this);
                }
            }
        }
    }

    public bool CanMoveToWorldTileFromCurPosition(GameTile toMoveTo)
    {
        return GetEntity().CanMoveTo(toMoveTo);
    }

    void OnMouseOver()
    {
        m_isHovered = true;
        if (GetEntity().GetTeam() == Team.Enemy && Globals.m_selectedEntity != null)
        {
            UIHelper.SetValidTintColor(m_tintRenderer, Globals.m_selectedEntity.GetEntity().CanHitEntity(GetEntity()));
        }
        else if (Globals.m_selectedCard != null)
        {
            UIHelper.SetValidTintColor(m_tintRenderer, Globals.m_selectedCard.m_card.IsValidToPlay(GetEntity()));
        } 
        else
        {
            UIHelper.SetValidTintColor(m_tintRenderer, CanSelect());
        }
    }

    void OnMouseExit()
    {
        m_isHovered = false;
        if (Globals.m_selectedEntity != this)
        {
            UIHelper.SetDefaultTintColor(m_tintRenderer);
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
}
