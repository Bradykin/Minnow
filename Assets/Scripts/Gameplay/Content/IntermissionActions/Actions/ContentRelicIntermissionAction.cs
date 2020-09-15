using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRelicIntermissionAction : GameActionIntermission
{
    public ContentRelicIntermissionAction()
    {
        m_actionCost = 2;
        m_name = "Find Relic";
        m_desc = "Gain a random relic!";

        m_icon = UIHelper.GetIconIntermissionAction(m_name);
    }

    public override void Activate()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        UIRelicSelectController.Instance.Init(GameRelicFactory.GetRandomRelic(), GameRelicFactory.GetRandomRelic());

        SpendCost();
    }
}
