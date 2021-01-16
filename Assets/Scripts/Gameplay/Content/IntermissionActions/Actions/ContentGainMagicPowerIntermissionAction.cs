using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGainMagicPowerIntermissionAction : GameActionIntermission
{
    public ContentGainMagicPowerIntermissionAction()
    {
        m_name = "Gain Magic Power";
        m_desc = $"<b>Permanently</b> gain 1 <b>Magic Power</b>!";

        m_icon = UIHelper.GetIconIntermissionAction(m_name);
    }

    public override void Activate(Action action)
    {
        GameHelper.GetPlayer().AddMagicPower(1);
        action.Invoke();
    }
}