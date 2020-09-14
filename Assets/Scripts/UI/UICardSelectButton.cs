using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICardSelectButton : MonoBehaviour
{
    private UICard m_uiCard;

    void Start()
    {
        m_uiCard = GetComponent<UICard>();
    }

    void OnMouseDown()
    {
        UICardSelectController.Instance.AcceptCard(m_uiCard.m_card);
    }
}
