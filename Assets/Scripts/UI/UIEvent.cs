using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvent : WorldElementBase
{
    public SpriteRenderer m_tintRenderer;
    public SpriteRenderer m_renderer;
    public SpriteMask m_mask;

    void Start()
    {
        UIHelper.SetDefaultTintColor(m_tintRenderer);

        gameObject.AddComponent<UITooltipGenerator>();
    }

    public void Init(GameEvent gameEvent)
    {
        m_gameElement = gameEvent;

        m_renderer.sprite = GetEvent().m_icon;
        m_mask.sprite = GetEvent().m_icon;
    }

    void OnMouseDown()
    {
        if (IsValidToUse())
        {
            UIHelper.CreateWorldElementNotification("WIP - The event has been activated!", true, this);
            GetEvent().m_tile.m_occupyingEntity.SpendAP(GetEvent().m_APCost);
        }
        else
        {
            if (!HasValidOccupant())
            {
                UIHelper.CreateWorldElementNotification("You need a unit on this tile!", false, this);
            }
            else if (!OccupantHasValidAP())
            {
                UIHelper.CreateWorldElementNotification("The unit on this tile needs " + GetEvent().m_APCost + " AP to activate this event!", false, this);
            }
        }
    }

    void OnMouseOver()
    {
        UIHelper.SetValidTintColor(m_tintRenderer, IsValidToUse());
    }

    void OnMouseExit()
    {
        UIHelper.SetDefaultTintColor(m_tintRenderer);
    }

    public GameEvent GetEvent()
    {
        return (GameEvent)m_gameElement;
    }

    private bool IsValidToUse()
    {
        if (!HasValidOccupant())
        {
            return false;
        }

        if (!OccupantHasValidAP())
        {
            return false;
        }

        return true;
    }

    private bool HasValidOccupant()
    {
        return GetEvent().m_tile.IsOccupied() && GetEvent().m_tile.m_occupyingEntity.GetTeam() == Team.Player;
    }

    private bool OccupantHasValidAP()
    {
        return GetEvent().m_tile.m_occupyingEntity.GetCurAP() >= GetEvent().m_APCost;
    }
}
