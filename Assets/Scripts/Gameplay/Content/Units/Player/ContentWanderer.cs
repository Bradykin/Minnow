using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWanderer : GameUnit
{
    public ContentWanderer()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.5f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        m_name = "Wanderer";
        m_desc = "At beginning of each turn, add 2 <b>Shivs</b> to your hand.\n";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        AddKeyword(new GameShivKeyword(), true, false);

        LateInit();
    }

    public override void StartTurn()
    {
        base.StartTurn();

        GameHelper.GetPlayer().AddCardToHand(GameCardFactory.GetCardClone(new ContentShivCard()), false);
        GameHelper.GetPlayer().AddCardToHand(GameCardFactory.GetCardClone(new ContentShivCard()), false);
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 30;
        m_maxStamina = 5;
        m_staminaRegen = 4;
        m_power = 15;
    }
}
