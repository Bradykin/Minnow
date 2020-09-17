using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWanderer : GameEntity
{
    public ContentWanderer()
    {
        m_maxHealth = 10;
        m_maxAP = 5;
        m_apRegen = 3;
        m_power = 5;

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_name = "Wanderer";
        m_desc = "At beginning of each turn, add a shiv to your hand.";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }

    public override void StartTurn()
    {
        base.StartTurn();

        GameHelper.GetPlayer().AddCardToHand(GameCardFactory.GetCardClone(new ContentShivCard()), false);
    }
}
