using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITooltipCard : UITooltipBase
{
    void Awake()
    {
        m_height = 5.0f;
        m_widthLeft = 4.0f;
        m_widthRight = 4.0f;
    }

    void Update()
    {
        base.FrameUpdate();
    }
}