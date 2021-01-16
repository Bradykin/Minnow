using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBuffUnitIntermissionAction : GameActionIntermission
{
    private int m_buffValue = Constants.IntermissionBuffValue;

    public ContentBuffUnitIntermissionAction()
    {
        m_name = "Buff Unit";
        m_desc = $"<b>Permanently</b> give a unit +{m_buffValue}/+{m_buffValue}!";

        m_icon = UIHelper.GetIconIntermissionAction(m_name);
    }

    public override void Activate(Action action)
    {
        UIDeckViewController.Instance.Init(GameHelper.GetPlayerBaseDeckOfUnits(), UIDeckViewController.DeckViewType.Buff, $"+{m_buffValue}/+{m_buffValue} to a Unit", action);
    }
}