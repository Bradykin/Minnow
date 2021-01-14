using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIBeginWaveButton : UIElementBase
    , IPointerClickHandler
{
    public GameObject m_holder;

    void Start()
    {
        m_stopScrolling = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioSFXController.Instance.PlaySFX(AudioHelper.UIClick);

        BeginWave();
    }

    private void BeginWave()
    {
        if (!Globals.m_canSelect)
        {
            return;
        }

        m_tintImage.color = UIHelper.GetDefaultTintColor();

        WorldController.Instance.EndIntermission();
    }

    public override void HandleTooltip()
    {
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Begin Wave", "Start the next wave of enemies!", true));
    }
}
