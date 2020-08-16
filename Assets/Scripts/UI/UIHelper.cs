using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIHelper
{
    public static Color m_default = new Color(Color.white.r, Color.white.g, Color.white.b, 0f);
    public static Color m_selected = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 0.3f);
    public static Color m_valid = new Color(Color.green.r, Color.green.g, Color.green.b, 0.3f);
    public static Color m_invalid = new Color(Color.red.r, Color.red.g, Color.red.b, 0.5f);

    public static Color m_playerColor = new Color(Color.cyan.r, Color.cyan.g, Color.cyan.b, 1f);
    public static Color m_enemyColor = new Color(Color.red.r, Color.red.g, Color.red.b, 1f);

    public static void SetSelectTintColor(SpriteRenderer renderer, bool isSelected)
    {
        if (isSelected)
        {
            renderer.color = m_selected;
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
            renderer.color = m_valid;
        }
        else
        {
            renderer.color = m_invalid;
        }
    }

    public static void SetDefaultTintColor(SpriteRenderer renderer)
    {
        renderer.color = m_default;
    }

    public static Sprite GetIconCard(string cardName)
    {
        return Resources.Load<Sprite>("Cards/" + cardName) as Sprite;
    }

    public static Sprite GetIconEntity(string cardName)
    {
        return Resources.Load<Sprite>("Entities/" + cardName) as Sprite;
    }
}
