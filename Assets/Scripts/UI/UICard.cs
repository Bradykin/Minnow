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
    public Text m_powerText;
    public Text m_healthText;
    public UIAPContainer m_apContainer;

    public GameCard m_card { get; private set; }

    public bool m_isBig;

    private bool m_isHovered;

    void Start()
    {
        UIHelper.SetDefaultTintColor(m_tintRenderer);
    }

    public void Init(GameCard card)
    {
        m_card = card;

        SetCardData();
    }

    void Update() 
    {
        if ((m_isHovered && Globals.m_selectedCard == null) || Globals.m_selectedCard == this)
        {
            m_isBig = true;
        }
        else
        {
            m_isBig = false;
        }
        
        if (!m_isHovered)
        {
            UIHelper.SetSelectTintColor(m_tintRenderer, Globals.m_selectedCard == this);
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

            m_apContainer.Init(entityCard.GetEntity().GetAPRegen(), entityCard.GetEntity().GetMaxAP(), Team.Player);
        }
        else
        {
            m_showTooltip = false;
        }
    }

    void OnMouseDown()
    {
        if (m_card.IsValidToPlay())
        {
            if (m_card.m_targetType == GameCard.Target.None)
            {
                WorldController.Instance.PlayCard(this, this);
                m_card.PlayCard();
            }
            else
            {
                UIHelper.SelectCard(this);
                UIHelper.SetSelectTintColor(m_tintRenderer, Globals.m_selectedCard == this);
            }
        }
        else
        {
            if (Globals.m_canSelect)
            {
                UIHelper.CreateWorldElementNotification("Not enough energy.", false, this);
            }
        }
    }

    public void CardPlayed(WorldElementBase target)
    {
        if (target != null)
        {
            UIHelper.CreateWorldElementNotification(m_card.m_playDesc, true, target);
        }
    }

    public override void HandleTooltip()
    {
        if (m_card is GameCardEntityBase)
        {
            GameEntity entity = ((GameCardEntityBase)m_card).GetEntity();

            //UIHelper.CreateEntityTooltip(entity, false);
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
