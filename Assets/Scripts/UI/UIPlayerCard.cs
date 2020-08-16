using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerCard : WorldElementBase
{
    public SpriteRenderer m_tintRenderer;

    public Text m_nameText;
    public Text m_costText;
    public Text m_typelineText;
    public Text m_descText;

    private SpriteRenderer m_renderer;

    void Start()
    {
        m_gameElement = new GameGoblinCard();
        m_showTooltip = false;

        SetCardData();

        m_renderer = GetComponent<SpriteRenderer>();

        gameObject.AddComponent<UITooltipGenerator>();
    }

    void Update() 
    {
        UIHelper.SelectGameobject(m_tintRenderer, Globals.m_selectedCard == this);
    }

    private void SetCardData()
    {
        m_nameText.text = GetCard().m_name;
        m_costText.text = GetCard().m_cost + "";
        m_typelineText.text = GetCard().m_typeline;
        m_descText.text = GetCard().m_desc;
    }

    void OnMouseDown()
    {
        Globals.m_selectedCard = this;
        Globals.m_selectedEntity = null;
    }

    public GameCard GetCard()
    {
        return (GameCard)m_gameElement;
    }
}
