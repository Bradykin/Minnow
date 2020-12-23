using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIAnimationBase : MonoBehaviour
{
    protected bool m_shouldAnimate;

    public virtual void PlayAnim() { }
    public virtual void PlayAnim(WorldUnit targetUnit) { }
}
