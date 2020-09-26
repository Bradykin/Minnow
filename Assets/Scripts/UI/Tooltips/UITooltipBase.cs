using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UITooltipBase : MonoBehaviour
{
    public float m_yVal;
    public float m_horizontalIndex;
    public float m_height { get; protected set; }
    public float m_widthLeft { get; protected set; }
    public float m_widthRight { get; protected set; }

    protected void FrameUpdate()
    {
        float multiplier = 1.0f;
        if (UITooltipController.Instance.m_flipStackHorizontal)
        {
            multiplier = -1.0f * multiplier;
        }

        gameObject.transform.localPosition = new Vector3(m_horizontalIndex * Constants.TooltipWidth * multiplier, m_yVal, -4f);
    }
}
