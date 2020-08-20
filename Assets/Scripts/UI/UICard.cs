﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICard : WorldElementBase
{
    public SpriteRenderer m_tintRenderer;

    public Text m_nameText;
    public Text m_costText;
    public SpriteRenderer m_imageRenderer;
    public Text m_typelineText;
    public Text m_descText;

    public GameCard m_card { get; private set; }

    public bool m_isSpellCard; //This is a testing variable and can be removed once we have a real hand being drawn

    void Start()
    {
        if (m_isSpellCard)
        {
            m_card = new GameCureLightWoundsCard();
        }
        else
        {
            m_card = new GameGoblinCard();
        }
        m_imageRenderer.sprite = m_card.m_icon;
        
        if (m_card is GameCardEntityBase)
        {
            m_gameElement = ((GameCardEntityBase)m_card).m_entity;
        }
        else
        {
            m_showTooltip = false;
        }

        SetCardData();

        UIHelper.SetDefaultTintColor(m_tintRenderer);

        gameObject.AddComponent<UITooltipGenerator>();
    }

    void Update() 
    {
        UIHelper.SetSelectTintColor(m_tintRenderer, Globals.m_selectedCard == this);
    }

    private void SetCardData()
    {
        m_nameText.text = m_card.m_name;
        m_costText.text = m_card.m_cost + "";
        m_typelineText.text = m_card.m_typeline;
        m_descText.text = m_card.m_desc;
    }

    void OnMouseDown()
    {
        UIHelper.SelectCard(this);
    }

    public void CardPlayed(WorldElementBase target)
    {
        UIHelper.CreateWorldElementNotification(m_card.m_playDesc, true, target);
    }
}
