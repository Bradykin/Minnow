using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUnitAttackAnimation : UIAnimationBase
{
    private bool m_movingOut;

    private Vector3 m_targetPos;
    private Vector3 m_initPos;

    public override void PlayAnim(WorldUnit targetUnit)
    {
        if (!m_shouldAnimate)
        {
            m_shouldAnimate = true;
            m_movingOut = true;

            m_initPos = transform.localPosition;

            Vector3 dir = (transform.position - targetUnit.transform.position).normalized * 1.25f;

            m_targetPos = new Vector3(transform.localPosition.x - dir.x, transform.localPosition.y - dir.y, transform.localPosition.z - dir.z);
        }
    }

    void Update()
    {
        if (!m_shouldAnimate)
        {
            return;
        }

        if (m_movingOut)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, m_targetPos, 0.1f);

            if (transform.localPosition == m_targetPos)
            {
                m_movingOut = false;
            }
        }
        else
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, m_initPos, 0.1f);

            if (transform.localPosition == m_initPos)
            {
                m_shouldAnimate = false;
            }
        }
    }
}
