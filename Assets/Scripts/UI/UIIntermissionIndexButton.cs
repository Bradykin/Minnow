using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIIntermissionIndexButton : MonoBehaviour
{
    public bool m_increase;
    
    public SpriteRenderer m_tintRenderer;
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
        UIHelper.SetValidTintColor(m_tintRenderer, true);
        Globals.m_canScroll = false;
    }

    void OnMouseExit()
    {
        UIHelper.SetDefaultTintColor(m_tintRenderer);
        Globals.m_canScroll = true;
    }
}
