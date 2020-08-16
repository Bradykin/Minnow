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

    public GameCard m_card { get; private set; }

    private SpriteRenderer m_renderer;

    void Start()
    {
        m_card = new GameGoblinCard();
        if (m_card is GameCardEntityBase)
        {
            m_gameElement = ((GameCardEntityBase)m_card).GetEntity();
        }

        SetCardData();

        m_renderer = GetComponent<SpriteRenderer>();

        gameObject.AddComponent<UITooltipGenerator>();
    }

    void Update() 
    {
        UIHelper.SelectGameobject(m_renderer, Globals.m_selectedCard == this);
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
        Globals.m_selectedCard = this;
        Globals.m_selectedEntity = null;
    }
}
