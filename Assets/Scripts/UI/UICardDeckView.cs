using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICardDeckView : WorldElementBase
{
    public SpriteRenderer m_tintRenderer;

    public Text m_nameText;
    public Text m_costText;
    public SpriteRenderer m_imageRenderer;
    public Text m_typelineText;
    public Text m_descText;
    public Text m_powerText;
    public Text m_healthText;
    public UIAPContainer m_apContainer;

    public GameCard m_card { get; private set; }

    private UIDeckViewController.DeckViewType m_viewType;

    public void Init(GameCard card, UIDeckViewController.DeckViewType viewType)
    {
        m_card = card;
        m_viewType = viewType;

        SetCardData();
    }

    void OnMouseDown()
    {
        if (m_card == null)
        {
            return;
        }

        if (m_viewType == UIDeckViewController.DeckViewType.View)
        {
            return;
        }

        GamePlayer player = GameHelper.GetPlayer();
        if (player == null)
        {
            return;
        }

        if (m_viewType == UIDeckViewController.DeckViewType.Remove)
        {
            player.m_deckBase.RemoveCard(m_card);

            if (player.m_hand.Contains(m_card))
            {
                player.m_hand.Remove(m_card);
            }
            else
            {
                player.m_curDeck.RemoveCard(m_card);
            }
        }
        else if (m_viewType == UIDeckViewController.DeckViewType.Duplicate)
        {
            GameCard dupCard = GameCardFactory.GetCardDup(m_card);
            player.m_deckBase.AddCard(dupCard);
            player.m_curDeck.AddToDiscard(dupCard);
        }
        else if (m_viewType == UIDeckViewController.DeckViewType.Transform)
        {
            GameCard newCard = GameCardFactory.GetRandomStandardCard();

            if (player.m_hand.Contains(m_card))
            {
                player.m_hand.Remove(m_card);
                player.m_hand.Add(newCard);
            }
            else
            {
                player.m_curDeck.AddToDiscard(newCard);
                player.m_curDeck.RemoveCard(m_card);
            }

            player.m_deckBase.RemoveCard(m_card);
            player.m_deckBase.AddCard(newCard);
        }

        UIDeckViewController.Instance.UpdateDeck(UIDeckViewController.DeckViewType.View);

        UIHelper.SetDefaultTintColor(m_tintRenderer);
        GetComponent<UITooltipGenerator>().ClearTooltip();
    }

    void OnMouseOver()
    {
        UIHelper.SetValidTintColor(m_tintRenderer, true);
    }

    void OnMouseExit()
    {
        UIHelper.SetDefaultTintColor(m_tintRenderer);
    }

    public override void HandleTooltip()
    {
        if (m_card == null)
        {
            return;
        }

        if (m_card is GameCardEntityBase)
        {
            GameEntity entity = ((GameCardEntityBase)m_card).GetEntity();

            //UIHelper.CreateEntityTooltip(entity, false);
        }
    }

    private void SetCardData()
    {
        m_imageRenderer.sprite = m_card.m_icon;
        m_nameText.text = m_card.GetName();
        m_costText.text = m_card.GetCost() + "";
        m_typelineText.text = m_card.m_typeline;
        m_descText.text = m_card.GetDesc();

        if (m_card is GameCardEntityBase)
        {
            m_gameElement = ((GameCardEntityBase)m_card).m_entity;

            GameCardEntityBase entityCard = (GameCardEntityBase)m_card;
            m_powerText.text = entityCard.m_entity.GetPower() + " ";
            m_healthText.text = entityCard.m_entity.GetMaxHealth() + " ";

            m_apContainer.gameObject.SetActive(true);
            m_apContainer.Init(entityCard.GetEntity().GetAPRegen(), entityCard.GetEntity().GetMaxAP(), Team.Player);
        }
        else
        {
            m_powerText.text = "";
            m_healthText.text = "";

            m_apContainer.gameObject.SetActive(false);

            m_showTooltip = false;
        }
    }
}
