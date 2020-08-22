using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISimpleTooltip : UITooltipBase
{
    public Text m_titleText;
    public Text m_descText;

    public void Init(string title, string desc)
    {
        m_height = 2.0f;

        m_titleText.text = title;
        m_descText.text = desc;
    }

    void Update()
    {
        base.FrameUpdate();
    }
}
