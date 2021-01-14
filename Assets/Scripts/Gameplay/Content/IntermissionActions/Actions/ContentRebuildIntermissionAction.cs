using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRebuildIntermissionAction : GameActionIntermission
{
    public ContentRebuildIntermissionAction()
    {
        m_name = "Rebuild";
        m_desc = "Rebuild a destroyed building.";

        m_icon = UIHelper.GetIconIntermissionAction(m_name);
    }

    public override void Activate()
    {
        UIHelper.SelectAction(this);
    }
}