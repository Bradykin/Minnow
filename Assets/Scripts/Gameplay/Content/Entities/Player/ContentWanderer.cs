using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWanderer : GameUnit
{
    public ContentWanderer()
    {
        m_maxHealth = 20;
        m_maxStamina = 5;
        m_staminaRegen = 4;
        m_power = 9;

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_name = "Wanderer";
        m_desc = "At beginning of each turn, add a shiv to your hand.";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    public override void StartTurn()
    {
        base.StartTurn();

        GameHelper.GetPlayer().AddCardToHand(GameCardFactory.GetCardClone(new ContentShivCard()), false);
    }
}
