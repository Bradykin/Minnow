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
        if (GameHelper.IsInGame())
        {
            m_countText.text = $"{GameHelper.GetGameController().m_curRage}";
        }
    }

    public override void HandleTooltip()
    {
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Rage", "After the first few turns of a wave, rage will begin to build each turn, empowering the enemies. It resets each Wave."));
    }
}
