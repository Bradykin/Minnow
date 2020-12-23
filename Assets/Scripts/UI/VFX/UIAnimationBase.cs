using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIAnimationBase : MonoBehaviour
{
    protected bool m_shouldAnimate;
    protected bool m_isComplete;

    public virtual void PlayAnim() { }
    public virtual void PlayAnim(WorldUnit targetUnit) { }
    public virtual void PlayAnim(WorldTile targetTile) { }

    public bool IsComplete()
    {
        return m_isComplete;
    }
}
