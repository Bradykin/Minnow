using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameActionIntermission : GameElementBase
{
    public abstract void Activate();

    public string GetDesc()
    {
        return m_desc;
    }

    public Sprite GetIcon()
    {
        return m_icon;
    }
}

