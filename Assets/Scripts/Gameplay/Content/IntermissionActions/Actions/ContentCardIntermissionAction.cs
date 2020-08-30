using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCardIntermissionAction : GameActionIntermission
{
    public ContentCardIntermissionAction()
    {
        m_actionCost = 1;
        m_name = "Gain a card";
        m_desc = "Gain a card from a random set of 3.";

        m_icon = UIHelper.GetIconIntermissionAction(m_name);
    }

    public override void Activate()
    {
        UICardSelectController.Instance.Init(GameCardFactory.GetRandomNonEventCard(), GameCardFactory.GetRandomNonEventCard(), GameCardFactory.GetRandomNonEventCard());

        SpendCost();
    }
}
