using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameActionIntermission : GameElementBase
{
    public abstract void Activate(Action action);

    public string GetDesc()
    {
        return m_desc;
    }

    public Sprite GetIcon()
    {
        return m_icon;
    }

    public virtual bool IsValidToSpawn()
    {
        return true;
    }
}

