using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGainMagicPowerIntermissionAction : GameActionIntermission
{
    public ContentGainMagicPowerIntermissionAction()
    {
        m_actionCost = 2;
        m_name = "Gain <b>Magic Power</b>";
        m_desc = $"<b>Permanently</b> gain 1 <b>Magic Power</b>!";

        m_icon = UIHelper.GetIconIntermissionAction(m_name);
    }

    public override void Activate()
    {
        GameHelper.GetPlayer().AddMagicPower(1);

        SpendCost();
    }
}