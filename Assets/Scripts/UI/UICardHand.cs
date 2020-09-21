using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UICardHand : MonoBehaviour
    , IPointerClickHandler
{
    private UICard m_uiCard;

    public bool m_isBig;

    void Start()
    {
        m_uiCard = GetComponent<UICard>();
    }

    void Update()
    {
        if ((m_uiCard.m_isHovered && Globals.m_selectedCard == null) || Globals.m_selectedCard == m_uiCard)
        {
            m_isBig = true;
        }
        else
        {
            m_isBig = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (m_uiCard.m_card.IsValidToPlay())
        {
            if (m_uiCard.m_card.m_targetType == GameCard.Target.None)
            {
                WorldController.Instance.PlayCard(m_uiCard);
                m_uiCard.m_card.PlayCard();
            }
            else
            {
                UIHelper.SelectCard(m_uiCard);
                m_uiCard.m_tintImage.color = UIHelper.GetSelectTintColor(Globals.m_selectedCard == m_uiCard);
            }
        }
        else
        {
            if (Globals.m_canSelect)
            {
                UIHelper.CreateWorldElementNotification("Not enough energy.", false, m_uiCard.gameObject);
            }
        }
    }
}
