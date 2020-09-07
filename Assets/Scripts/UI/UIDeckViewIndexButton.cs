using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDeckViewIndexButton : MonoBehaviour
{
    public bool m_increase;
    public SpriteRenderer m_tintRenderer;
    public GameObject m_holder;

    void Update()
    {
        if (m_increase)
        {
            m_holder.SetActive(UIDeckViewController.Instance.CanIndexIncrease());
        }
        else
        {
            m_holder.SetActive(UIDeckViewController.Instance.m_index > 0);
        }
    }

    void OnMouseDown()
    {
        if (!m_holder.activeSelf)
        {
            return;
        }

        AdjustIndex();
    }

    private void AdjustIndex()
    {
        int indexVal = UIDeckViewController.Instance.m_index;
        if (m_increase)
        {
            UIDeckViewController.Instance.SetIndex(indexVal + 1);
        }
        else
        {
            UIDeckViewController.Instance.SetIndex(indexVal - 1);
        }
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
