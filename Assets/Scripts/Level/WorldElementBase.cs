using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WorldElementBase : MonoBehaviour
{
    protected GameElementBase m_gameElement;
    public bool m_showTooltip { get; protected set; } = true;

    public virtual GameElementBase GetElement()
    {
        return m_gameElement;
    }

    public abstract void HandleTooltip();
}
