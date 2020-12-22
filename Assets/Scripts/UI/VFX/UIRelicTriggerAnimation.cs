using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRelicTriggerAnimation : UIAnimationBase
{
    private bool m_gettingBig;

    public override void PlayAnim()
    {
        m_shouldAnimate = true;
        m_gettingBig = true;
    }

    void Update()
    {
        if (!m_shouldAnimate)
        {
            return;
        }

        if (m_gettingBig)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(2, 2, 2), 0.1f);

            if (transform.localScale == new Vector3(2, 2, 2))
            {
                m_gettingBig = false;
            }
        }
        else
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(1, 1, 1), 0.1f);

            if (transform.localScale == new Vector3(1, 1, 1))
            {
                m_shouldAnimate = false;
            }
        }
    }
}
