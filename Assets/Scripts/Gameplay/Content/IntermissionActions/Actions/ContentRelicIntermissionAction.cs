using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRelicIntermissionAction : GameActionIntermission
{
    public ContentRelicIntermissionAction()
    {
        m_actionCost = 3;
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

        GameRarity rarity = GameRelicFactory.GetRandomRarity();

        GameRelic relicOne = GameRelicFactory.GetRandomRelicAtRarity(rarity);
        GameRelic relicTwo = GameRelicFactory.GetRandomRelicAtRarity(rarity, relicOne);

        UIRelicSelectController.Instance.Init(relicOne, relicTwo);

        SpendCost();
    }
}
