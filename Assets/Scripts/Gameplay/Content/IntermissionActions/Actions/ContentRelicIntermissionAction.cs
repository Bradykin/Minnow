using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRelicIntermissionAction : GameActionIntermission
{
    public ContentRelicIntermissionAction()
    {
        m_name = "Find Relic";
        m_desc = "Gain a random relic!";

        m_icon = UIHelper.GetIconIntermissionAction(m_name);
    }

    public override void Activate(Action action)
    {
        UIHelper.TriggerRelicSelect();
        action.Invoke();
    }
}
