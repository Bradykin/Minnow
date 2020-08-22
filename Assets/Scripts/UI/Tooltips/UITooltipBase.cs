using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UITooltipBase : MonoBehaviour
{
    public float m_yVal;
    public float m_horizontalIndex;
    public float m_height { get; protected set; }

    protected void FrameUpdate()
    {
        gameObject.transform.localPosition = new Vector3(m_horizontalIndex * Constants.TooltipWidth, m_yVal, 0);
    }
}
