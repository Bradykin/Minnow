using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldElementBase : MonoBehaviour
{
    protected GameElementBase m_gameElement;

    public virtual GameElementBase GetElement()
    {
        return m_gameElement;
    }
}
