using System.Collections;
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
    public Text m_keywordText;
    public Text m_powerText;
    public Text m_healthText;

    public GameCard m_card { get; private set; }

    private bool m_isHovered;

    void Start()
    {
        UIHelper.SetDefaultTintColor(m_tintRenderer);
    }

    public void Init(GameCard card)
    {
        m_card = card;

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

        m_card = card;
    }

    void Update() 
    {
        if (!m_isHovered)
        {
            UIHelper.SetSelectTintColor(m_tintRenderer, Globals.m_selectedCard == this);
        }
    }

    private void SetCardData()
    {
        m_nameText.text = m_card.m_name;
        m_costText.text = m_card.m_cost + "";
        m_typelineText.text = m_card.m_typeline;
        m_descText.text = m_card.m_desc;

        if (m_card is GameCardEntityBase)
        {
            GameCardEntityBase entityCard = (GameCardEntityBase)m_card;
            m_keywordText.text = entityCard.m_entity.GetKeywordHolder().GetDesc();
            m_powerText.text = entityCard.m_entity.GetPower() + " ";
            m_healthText.text = entityCard.m_entity.GetMaxHealth() + " ";
        }
    }

    void OnMouseDown()
    {
        if (m_card.IsValidToPlay())
        {
            UIHelper.SelectCard(this);
            UIHelper.SetSelectTintColor(m_tintRenderer, Globals.m_selectedCard == this);
        }
        else
        {
            UIHelper.CreateWorldElementNotification("Not energy energy.", false, this);
        }
    }

    public void CardPlayed(WorldElementBase target)
    {
        UIHelper.CreateWorldElementNotification(m_card.m_playDesc, true, target);
    }

    public override void HandleTooltip()
    {
        if (m_card is GameCardEntityBase)
        {
            GameEntity entity = ((GameCardEntityBase)m_card).GetEntity();

            UIHelper.CreateEntityTooltip(entity);
        }
    }

    void OnMouseOver()
    {
        m_isHovered = true;

        if (Globals.m_selectedCard != this)
        {
            UIHelper.SetValidTintColor(m_tintRenderer, m_card.IsValidToPlay());
        }
    }

    void OnMouseExit()
    {
        m_isHovered = false;

        if (Globals.m_selectedCard != this)
        {
            UIHelper.SetDefaultTintColor(m_tintRenderer);
        }
    }
}
