using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICardSelectButton : WorldElementBase
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
    public UIAPContainer m_apContainer;

    public GameCard m_card { get; private set; }

    public void Init(GameCard card)
    {
        m_card = card;

        SetCardData();
    }

    void OnMouseDown()
    {
        if (m_card == null)
        {
            return;
        }

        UICardSelectController.Instance.AcceptCard(m_card);

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

            UIHelper.CreateEntityTooltip(entity, false);
        }
    }

    private void SetCardData()
    {
        m_imageRenderer.sprite = m_card.m_icon;
        m_nameText.text = m_card.m_name;
        m_costText.text = m_card.GetCost() + "";
        m_typelineText.text = m_card.m_typeline;
        m_descText.text = m_card.m_desc;

        if (m_card is GameCardEntityBase)
        {
            m_gameElement = ((GameCardEntityBase)m_card).m_entity;

            GameCardEntityBase entityCard = (GameCardEntityBase)m_card;
            m_keywordText.text = entityCard.m_entity.GetKeywordHolder().GetDesc();
            m_powerText.text = entityCard.m_entity.GetPower() + " ";
            m_healthText.text = entityCard.m_entity.GetMaxHealth() + " ";

            m_apContainer.gameObject.SetActive(true);
            m_apContainer.Init(entityCard.GetEntity().GetAPRegen(), entityCard.GetEntity().GetMaxAP(), Team.Player);
        }
        else
        {
            m_keywordText.text = "";
            m_powerText.text = "";
            m_healthText.text = "";

            m_apContainer.gameObject.SetActive(false);

            m_showTooltip = false;
        }
    }
}
