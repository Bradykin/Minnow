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
}
