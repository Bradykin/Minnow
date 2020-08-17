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
            print("Play event: " + GetEvent().m_name);
            GetEvent().m_tile.m_occupyingEntity.SpendAP(GetEvent().m_APCost);
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
        if (!GetEvent().m_tile.IsOccupied())
        {
            return false;
        }

        return GetEvent().m_tile.m_occupyingEntity.GetCurAP() >= GetEvent().m_APCost;
    }
}
