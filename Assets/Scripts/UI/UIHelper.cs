using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIHelper
{
    public static void SelectGameobject(SpriteRenderer renderer, bool isSelected)
    {
        if (isSelected)
        {
            renderer.color = Color.yellow;
        }
        else
        {
            renderer.color = Color.white;
        }
    }

    public static void SetValidGameobjectColor(SpriteRenderer renderer, bool isValid)
    {
        if (isValid)
        {
            renderer.color = Color.green;
        }
        else
        {
            renderer.color = Color.red;
        }
    }

    public static Vector3 GetScreenPositionForWorldGridElement(int x, int y)
    {
        return new Vector3((x + y * 0.5f - y / 2) * Constants.HexagonInnerRadius * 2.0f, y * Constants.HexagonOuterRadius * 1.5f, 0.0f);
    }
}
