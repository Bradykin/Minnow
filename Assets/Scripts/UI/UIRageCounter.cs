using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIRageCounter : UIElementBase
{
    public TMP_Text m_countText;

    void Start()
    {
        m_stopScrolling = true;
    }

    void Update()
    {
        m_countText.text = $"{Globals.m_curRage}";
    }

    public override void HandleTooltip()
    {
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Rage", "Rage increases over turns, empowering the enemies. It resets each Wave."));
    }
}
