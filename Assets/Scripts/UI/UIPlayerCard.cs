using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerCard : WorldElementBase
{
    public Text m_nameText;
    public Text m_costText;
    public Text m_typelineText;
    public Text m_descText;

    private GameCard m_card;

    void Start()
    {
        m_card = new GameGoblinCard();
        if (m_card is GameCardEntityBase)
        {
            m_gameElement = ((GameCardEntityBase)m_card).GetEntity();
        }

        SetCardData();
    }

    private void SetCardData()
    {
        m_nameText.text = m_card.m_name;
        m_costText.text = m_card.m_cost + "";
        m_typelineText.text = m_card.m_typeline;
        m_descText.text = m_card.m_desc;
    }
}
