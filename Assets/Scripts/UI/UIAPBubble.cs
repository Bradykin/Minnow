using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAPBubble : MonoBehaviour
{
    public SpriteRenderer m_renderer;

    public void Init(bool active)
    {
        UIHelper.SetValidColorAlt(m_renderer, active);
    }
}
