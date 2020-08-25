using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITooltipController : Singleton<UITooltipController>
{
    private List<UITooltipBase> m_tooltipStack;
    private List<UITooltipBase> m_secondTooltipStack;

    public bool m_flipStackHorizontal;
    public bool m_flipStackVertical;

    void Start()
    {
        m_tooltipStack = new List<UITooltipBase>();
        m_secondTooltipStack = new List<UITooltipBase>();
    }

    void Update()
    {
        if (Input.mousePosition.x >= Screen.width * 0.7f)
        {
            m_flipStackHorizontal = true;
        }
        else
        {
            m_flipStackHorizontal = false;
        }

        if (Input.mousePosition.y <= Screen.height * 0.5f)
        {
            m_flipStackVertical = true;
        }
        else
        {
            m_flipStackVertical = false;
        }



        //Update the stack to position vertically
        float curY = 0.0f;
        for (int i = 0; i < m_tooltipStack.Count; i++)
        {
            m_tooltipStack[i].m_yVal = curY;
            if (m_flipStackVertical)
            {
                curY += m_tooltipStack[i].m_height;
            }
            else
            {
                curY -= m_tooltipStack[i].m_height;
            }
        }

        //Update the second stack
        float secondStackCurY = 0.0f;
        for (int i = 0; i < m_secondTooltipStack.Count; i++)
        {
            m_secondTooltipStack[i].m_yVal = secondStackCurY;
            if (m_flipStackVertical)
            {
                secondStackCurY += m_secondTooltipStack[i].m_height;
            }
            else
            {
                secondStackCurY -= m_secondTooltipStack[i].m_height;
            }
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
        if (m_flipStackHorizontal)
        {
            worldPoint.x = worldPoint.x - 2.5f;
        }
        else
        {
            worldPoint.x = worldPoint.x + 2.5f;
        }

        if (m_flipStackVertical)
        {
            float flipSize = 3.0f;
            if (m_tooltipStack.Count > 0)
            {
                flipSize = m_tooltipStack[0].m_height - 1.5f;
            }

            worldPoint.y = worldPoint.y + flipSize;
        }
        else
        {
            worldPoint.y = worldPoint.y - 0.6f;
        }

        transform.position = worldPoint;
    }
}
