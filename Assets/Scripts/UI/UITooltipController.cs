using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITooltipController : Singleton<UITooltipController>
{
    private List<UITooltipBase> m_tooltipStack;
    private List<UITooltipBase> m_secondTooltipStack;

    void Start()
    {
        m_tooltipStack = new List<UITooltipBase>();
        m_secondTooltipStack = new List<UITooltipBase>();
    }

    void Update()
    {
        //Update the stack to position vertically
        float curY = 0.0f;
        for (int i = 0; i < m_tooltipStack.Count; i++)
        {
            m_tooltipStack[i].m_yVal = curY;
            curY -= m_tooltipStack[i].m_height;
        }

        //Update the second stack
        float secondStackCurY = 0.0f;
        for (int i = 0; i < m_secondTooltipStack.Count; i++)
        {
            m_secondTooltipStack[i].m_yVal = secondStackCurY;
            secondStackCurY -= m_secondTooltipStack[i].m_height;
        }

        //Update the controller position
        UpdatePosition();
    }

    public void AddTooltipToStack(UITooltipBase newTooltip)
    {
        newTooltip.m_horizontalIndex = 0;
        m_tooltipStack.Add(newTooltip);
    }

    public void AddTooltipToSecondStack(UITooltipBase newTooltip)
    {
        newTooltip.m_horizontalIndex = 1;
        m_secondTooltipStack.Add(newTooltip);
    }

    public void ClearTooltipStack()
    {
        for (int i = m_tooltipStack.Count - 1; i >= 0; i--)
        {
            Recycler.Recycle<UITooltipBase>(m_tooltipStack[i]);
        }

        for (int i = m_secondTooltipStack.Count - 1; i >= 0; i--)
        {
            Recycler.Recycle<UITooltipBase>(m_secondTooltipStack[i]);
        }

        m_tooltipStack = new List<UITooltipBase>();
        m_secondTooltipStack = new List<UITooltipBase>();
    }

    private void UpdatePosition()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = transform.position.z - Camera.main.transform.position.z;

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(pos);
        worldPoint.x = worldPoint.x + 2.5f;
        worldPoint.y = worldPoint.y - 0.6f;

        transform.position = worldPoint;
    }
}
