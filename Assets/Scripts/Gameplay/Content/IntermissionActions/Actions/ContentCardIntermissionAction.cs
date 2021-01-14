using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCardIntermissionAction : GameActionIntermission
{
    public ContentCardIntermissionAction()
    {
        m_name = "Gain a card";
        m_desc = "Gain a spell card from a random set of 3.";

        m_icon = UIHelper.GetIconIntermissionAction(m_name);
    }

    public override void Activate()
    {
        UIHelper.TriggerSpellCardSelection();
    }
}
