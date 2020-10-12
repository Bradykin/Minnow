using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEventOption
{
    public bool m_hasTooltip;
    protected string m_message;

    public abstract void AcceptOption();
    public virtual bool IsOptionValid() { return true; }

    public int m_experienceAmount = 5;

    public virtual void EndEvent()
    {
        UIEventController.Instance.EndEvent();
        GameHelper.GetGameController().AddPlaythroughExperience(m_experienceAmount);
    }

    public virtual string GetMessage()
    {
        return m_message;
    }

    public virtual void BuildTooltip() { }
    public virtual void Init() { }
}
