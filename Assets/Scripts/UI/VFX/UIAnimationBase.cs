using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIAnimationBase : MonoBehaviour
{
    protected bool m_shouldAnimate;

    public abstract void PlayAnim();
}
