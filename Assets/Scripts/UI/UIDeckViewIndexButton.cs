using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDeckViewIndexButton : MonoBehaviour
{
    public bool m_increase;
    public Image m_tintImage;
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
        m_tintImage.color = UIHelper.GetValidTintColor(true);
    }

    void OnMouseExit()
    {
        m_tintImage.color = UIHelper.GetDefaultTintColor();
    }
}
