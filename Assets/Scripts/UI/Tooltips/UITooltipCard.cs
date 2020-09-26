using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITooltipCard : UITooltipBase
{
    void Start()
    {
        m_height = 5.0f;
        m_widthLeft = 6.0f;
        m_widthRight = 1.74f;
    }

    void Update()
    {
        base.FrameUpdate();
    }
}