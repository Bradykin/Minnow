using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRelicPickupAnimation : UIAnimationBase
{
    public override void PlayAnim()
    {
        transform.localScale = new Vector3(2, 2, 2);

        m_shouldAnimate = true;
    }

    void Update()
    {
        if (!m_shouldAnimate)
        {
            return;
        }

        transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(1, 1, 1), 0.1f);

        if (transform.localScale == new Vector3(1,1,1))
        {
            m_shouldAnimate = false;
        }
    }
}
