using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIIntermissionIndexButton : UIElementBase
    , IPointerClickHandler
{
    public bool m_increase;
    
    public GameObject m_holder;

    void Start()
    {
        m_stopScrolling = true;
    }

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

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioSFXController.Instance.PlaySFX(AudioHelper.UIClick);

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
