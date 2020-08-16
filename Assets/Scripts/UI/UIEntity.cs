using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEntity : WorldElementBase
{
    private SpriteRenderer m_renderer;

    void Start()
    {
        m_gameElement = new GameGoblinEntity();

        m_renderer = GetComponent<SpriteRenderer>();

        gameObject.AddComponent<UITooltipGenerator>();
    }

    void Update()
    {
        UIHelper.SelectGameobject(m_renderer, Globals.m_selectedEntity == this);
    }

    void OnMouseDown()
    {
        Globals.m_selectedEntity = this;
        Globals.m_selectedCard = null;
    }
}
