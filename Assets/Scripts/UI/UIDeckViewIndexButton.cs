using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIDeckViewIndexButton : WorldElementBase
    , IPointerClickHandler
{
    public bool m_increase;
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!m_holder.activeSelf)
        {
            return;
        }

        AdjustIndex();
    }

    public override void HandleTooltip()
    {
        //Left as stub
    }
}
