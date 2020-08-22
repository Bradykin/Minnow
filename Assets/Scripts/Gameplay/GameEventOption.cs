﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEventOption
{
    public bool m_hasTooltip;
    public string m_message { get; protected set; }

    public abstract void AcceptOption();
    public virtual bool IsOptionValid() { return true; }

    public virtual void EndEvent()
    {
        UIEventController.Instance.EndEvent();
    }

    public virtual void BuildTooltip() { }
}
