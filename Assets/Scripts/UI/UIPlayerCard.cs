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

    void Start()
    {
        m_gameElement = new GameGoblinCard();

        SetCardData((GameCard)m_gameElement);
    }

    private void SetCardData(GameCard card)
    {
        m_nameText.text = card.m_name;
        m_costText.text = card.m_cost + "";
        m_typelineText.text = card.m_typeline;
        m_descText.text = card.m_desc;
    }
}
