using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUnitDeathAnimation : UIAnimationBase
{
    private WorldUnit m_targetUnit;
    public SpriteRenderer m_image;

    private int m_flickerCount = 0;

    public override void PlayAnim(WorldUnit targetUnit)
    {
        m_shouldAnimate = true;
        m_targetUnit = targetUnit;
    }

    void Update()
    {
        if (!m_shouldAnimate)
        {
            return;
        }

        if (m_flickerCount == 0)
        {
            m_image.color = new Color(m_image.color.r, m_image.color.g, m_image.color.b, m_image.color.a - 0.2f);
            if (m_image.color.a <= 0.0f)
            {
                m_flickerCount++;
            }
        }
        else if (m_flickerCount == 1)
        {
            m_image.color = new Color(m_image.color.r, m_image.color.g, m_image.color.b, m_image.color.a + 0.35f);
            if (m_image.color.a >= 1.0f)
            {
                m_flickerCount++;
            }
        }
        else if (m_flickerCount == 2)
        {
            m_image.color = new Color(m_image.color.r, m_image.color.g, m_image.color.b, m_image.color.a - 0.1f);
            if (m_image.color.a <= 0.0f)
            {
                m_flickerCount++;
            }
        }
        else if (m_flickerCount == 3)
        {
            m_shouldAnimate = false;

            m_targetUnit.GetUnit().GetWorldTile().RecycleUnit(m_targetUnit.GetUnit());
        }
    }
}
