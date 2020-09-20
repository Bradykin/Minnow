﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICard : WorldElementBase
{
    public enum CardDisplayType
    {
        Hand,
        Deck,
        Select,
        Tooltip
    }

    public Image m_tintImage;

    public Text m_nameText;
    public Text m_costText;
    public Image m_image;
    public Text m_typelineText;
    public Text m_descText;
    public Text m_powerText;
    public Text m_healthText;
    public UIAPContainer m_apContainer;

    public CardDisplayType m_displayType;

    public GameCard m_card { get; private set; }

    public bool m_isHovered;

    private UICardHand m_handCard;
    private UICardSelectButton m_cardSelect;
    private UICardDeckView m_cardDeck;
    private UITooltipCard m_cardTooltip;

    private bool m_hasSetDisplayType;

    void Start()
    {
        m_tintImage.color = UIHelper.GetDefaultTintColor();
    }

    public void Init(GameCard card, CardDisplayType displayType)
    {
        m_card = card;
        m_displayType = displayType;

        SetCardData();

        if (!m_hasSetDisplayType)
        {
            if (m_displayType == CardDisplayType.Hand)
            {
                m_handCard = gameObject.AddComponent<UICardHand>();
            }
            else if (m_displayType == CardDisplayType.Select)
            {
                m_cardSelect = gameObject.AddComponent<UICardSelectButton>();
            }
            else if (m_displayType == CardDisplayType.Deck)
            {
                m_cardDeck = gameObject.AddComponent<UICardDeckView>();
            }
            else if (m_displayType == CardDisplayType.Tooltip)
            {
                m_cardTooltip = gameObject.AddComponent<UITooltipCard>();
            }

            m_hasSetDisplayType = true;
        }
    }

    void Update() 
    {
        if (!m_isHovered)
        {
            bool isSelected = Globals.m_selectedCard == this;
            m_tintImage.color = UIHelper.GetSelectTintColor(Globals.m_selectedCard == this);
        }
    }

    public void SetCardData()
    {
        if (m_card == null)
        {
            return;
        }

        m_image.sprite = m_card.m_icon;
        m_nameText.text = m_card.GetName();
        m_costText.text = m_card.GetCost() + "";
        m_typelineText.text = m_card.m_typeline;
        m_descText.text = m_card.GetDesc();

        if (m_card is GameCardEntityBase)
        {
            GameCardEntityBase entityCard = (GameCardEntityBase)m_card;
            m_powerText.text = entityCard.m_entity.GetPower() + "";
            m_healthText.text = entityCard.m_entity.GetMaxHealth() + "";

            m_apContainer.gameObject.SetActive(true);
            m_apContainer.Init(entityCard.GetEntity().GetAPRegen(), entityCard.GetEntity().GetMaxAP(), Team.Player);
        }
        else
        {
            m_powerText.text = "";
            m_healthText.text = "";
            m_apContainer.gameObject.SetActive(false);
        }
    }

    public void CardPlayed(WorldElementBase target)
    {
        if (target != null)
        {
            //UIHelper.CreateWorldElementNotification(m_card.m_playDesc, true, target); nmartino - reassess how important this is (need shorter play descs)
        }
    }

    void OnMouseOver()
    {
        m_isHovered = true;

        if (Globals.m_selectedCard != this)
        {
            bool isValid = m_card.IsValidToPlay() || m_cardSelect != null;
            m_tintImage.color = UIHelper.GetValidTintColor(isValid);
        }
    }

    void OnMouseExit()
    {
        m_isHovered = false;

        if (Globals.m_selectedCard != this)
        {
            m_tintImage.color = UIHelper.GetDefaultTintColor();
        }
    }

    public bool GetIsBig()
    {
        if (m_handCard == null)
        {
            return false;
        }

        return m_handCard.m_isBig;
    }

    public override void HandleTooltip()
    {
        //Stub.  No tooltip on cards for now.
    }

    public void InitCardDeck(UIDeckViewController.DeckViewType viewType)
    {
        m_cardDeck.Init(viewType);
    }

    public UITooltipCard GetCardTooltip()
    {
        return m_cardTooltip;
    }
}
