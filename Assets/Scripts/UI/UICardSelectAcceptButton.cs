using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICardSelectAcceptButton : MonoBehaviour
{
    public SpriteRenderer m_tintRenderer;
    public GameObject m_holder;

    void Update()
    {
        m_holder.SetActive(Globals.m_selectedCard != null);
    }

    void OnMouseDown()
    {
        if (!m_holder.activeSelf)
        {
            return;
        }

        UICardSelectController.Instance.AcceptCard(Globals.m_selectedCard.m_card);
        UIHelper.SetDefaultTintColor(m_tintRenderer);
        Globals.m_selectedCard = null;
    }

    void OnMouseOver()
    {
        UIHelper.SetValidTintColor(m_tintRenderer, true);
    }

    void OnMouseExit()
    {
        UIHelper.SetDefaultTintColor(m_tintRenderer);
    }
}
