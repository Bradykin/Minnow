using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICardFrameController : MonoBehaviour
{
    public UICard m_card;
    public UICardSelectButton m_cardSelect;

    public Sprite m_spellCardFrame;
    public Sprite m_entityCardFrame;

    private Image m_image;

    void Start()
    {
        m_image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        GameCard card = null;

        if (m_card != null)
        {
            card = m_card.m_card;
        }

        if (card is GameCardEntityBase)
        {
            m_image.sprite = m_entityCardFrame;
        }
        else
        {
            m_image.sprite = m_spellCardFrame;
        }
    }
}
