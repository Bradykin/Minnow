using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAPBubble : MonoBehaviour
{
    public SpriteRenderer m_renderer;

    public void Init(bool active, Team team)
    {
        UIHelper.SetValidColorAltByTeam(m_renderer, active, team);
    }
}
