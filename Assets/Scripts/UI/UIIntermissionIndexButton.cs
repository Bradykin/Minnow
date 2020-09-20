using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIIntermissionIndexButton : MonoBehaviour
{
    public bool m_increase;
    
    public Image m_tintImage;
    public GameObject m_holder;

    void Update()
    {
        if (m_increase)
        {
            m_holder.SetActive(UIIntermissionController.Instance.CanIndexIncrease());
        }
        else
        {
            m_holder.SetActive(UIIntermissionController.Instance.GetIndex() > 0);
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
        int indexVal = UIIntermissionController.Instance.GetIndex();
        if (m_increase)
        {
            UIIntermissionController.Instance.SetIndex(indexVal+1);
        }
        else
        {
            UIIntermissionController.Instance.SetIndex(indexVal-1);
        }
    }

    void OnMouseOver()
    {
        m_tintImage.color = UIHelper.GetValidTintColor(true);
        Globals.m_canScroll = false;
    }

    void OnMouseExit()
    {
        m_tintImage.color = UIHelper.GetDefaultTintColor();
        Globals.m_canScroll = true;
    }
}
