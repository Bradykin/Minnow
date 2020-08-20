using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public static class UIHelper
{
    public static Color m_defaultTint = new Color(Color.white.r, Color.white.g, Color.white.b, 0f);
    public static Color m_selectedTint = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 0.3f);
    public static Color m_validTint = new Color(Color.cyan.r, Color.cyan.g, Color.cyan.b, 0.3f);
    public static Color m_invalidTint = new Color(Color.red.r, Color.red.g, Color.red.b, 0.5f);

    public static Color m_valid = new Color(Color.blue.r, Color.blue.g, Color.blue.b, 1.0f);
    public static Color m_invalid = new Color(Color.red.r, Color.red.g, Color.red.b, 1.0f);

    public static Color m_playerColor = new Color(Color.cyan.r, Color.cyan.g, Color.cyan.b, 1f);
    public static Color m_enemyColor = new Color(Color.red.r, Color.red.g, Color.red.b, 1f);

    public static void SetSelectTintColor(SpriteRenderer renderer, bool isSelected)
    {
        if (isSelected)
        {
            renderer.color = m_selectedTint;
        }
        else
        {
            SetDefaultTintColor(renderer);
        }
    }

    public static void SetValidTintColor(SpriteRenderer renderer, bool isValid)
    {
        if (isValid)
        {
            renderer.color = m_validTint;
        }
        else
        {
            renderer.color = m_invalidTint;
        }
    }

    public static void SetDefaultTintColor(SpriteRenderer renderer)
    {
        renderer.color = m_defaultTint;
    }

    public static Sprite GetIconCard(string cardName)
    {
        return Resources.Load<Sprite>("Cards/" + cardName) as Sprite;
    }

    public static Sprite GetIconEntity(string entityName)
    {
        return Resources.Load<Sprite>("Entities/" + entityName) as Sprite;
    }

    public static Sprite GetIconEvent(string eventName)
    {
        return Resources.Load<Sprite>("Events/" + eventName) as Sprite;
    }

    public static void SelectEntity(UIEntity entity)
    {
        if (Globals.m_selectedEntity == entity)
        {
            Globals.m_selectedEntity = null;
        }
        else
        {
            Globals.m_selectedEntity = entity;
            Globals.m_selectedCard = null;
        }
    }

    public static void SelectCard(UICard card)
    {
        if (Globals.m_selectedCard == card)
        {
            Globals.m_selectedCard = null;
        }
        else
        {
            Globals.m_selectedEntity = null;
            Globals.m_selectedCard = card;
        }
    }

    private static void CreateWorldElementNotificationImpl(string message, Color color, WorldElementBase worldElement)
    {
        FactoryManager.Instance.GetFactory<UIWorldElementNotificationFactory>().CreateObject<UIWorldElementNotification>(message, color, worldElement);
    }

    public static void CreateWorldElementNotification(string message, bool isPositive, WorldElementBase worldElement)
    {
        Color color = Color.black;

        if (isPositive)
        {
            color = m_valid;
        }
        else
        {
            color = m_invalid;
        }

        CreateWorldElementNotificationImpl(message, color, worldElement);
    }
}
