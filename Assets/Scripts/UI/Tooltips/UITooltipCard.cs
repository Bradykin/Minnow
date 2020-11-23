using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITooltipCard : UITooltipBase
{
    public void Init(bool isLarge)
    {
        if (isLarge)
        {
            m_height = 4.9f;
            m_widthLeft = 12.0f;
            m_widthRight = 2.0f;
        }
        else
        {
            m_height = 5.0f;
            m_widthLeft = 4.0f;
            m_widthRight = 4.0f;
        }
    }

    void Update()
    {
        base.FrameUpdate();
    }
}