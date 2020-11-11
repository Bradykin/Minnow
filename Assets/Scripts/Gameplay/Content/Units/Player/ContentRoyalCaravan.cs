using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRoyalCaravan : GameUnit
{
    public ContentRoyalCaravan()
    {
        m_maxHealth = 50;
        m_maxStamina = 5;
        m_staminaRegen = 3;
        m_power = 5;

        m_team = Team.Player;
        m_rarity = GameRarity.Special;

        AddKeyword(new GameRangeKeyword(2), false);

        m_name = "Royal Caravan";
        m_desc = "With the castle gone, this caravan holds the royal court. Lose this, and it's game over!\n";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    public override void Die(bool canRevive = true)
    {
        base.Die(canRevive);

        GameHelper.EndLevel(RunEndType.Loss);
    }

    public override void OnMoveBegin()
    {
        base.OnMoveBegin();

        GetWorldTile().ReducePlaceRange(2);
    }

    public override void OnMoveEnd()
    {
        base.OnMoveEnd();

        GetWorldTile().ExpandPlaceRange(2);
    }
}