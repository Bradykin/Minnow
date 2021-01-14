using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTransformUnitIntermissionAction : GameActionIntermission
{
    public ContentTransformUnitIntermissionAction()
    {
        m_name = "Transform Unit";
        m_desc = "Transform a unit card in your deck into a random one!";

        m_icon = UIHelper.GetIconIntermissionAction(m_name);
    }

    public override void Activate()
    {
        UIDeckViewController.Instance.Init(GameHelper.GetPlayerBaseDeckOfUnits(), UIDeckViewController.DeckViewType.Transform, "Transform a Unit");
    }
}