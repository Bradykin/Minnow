using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICardFrameController : MonoBehaviour
{
    public UICard m_card;

    public Sprite m_spellCardFrame;
    public Sprite m_entityCardFrame;

    private SpriteRenderer m_renderer;

    void Start()
    {
        m_renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_card.m_card is GameCardEntityBase)
        {
            m_renderer.sprite = m_entityCardFrame;
        }
        else
        {
            m_renderer.sprite = m_spellCardFrame;
        }
    }
}
