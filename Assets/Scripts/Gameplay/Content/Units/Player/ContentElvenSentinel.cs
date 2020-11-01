using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentElvenSentinel : GameUnit
{
    public ContentElvenSentinel()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.4f, 0);

        m_maxHealth = 6;
        m_maxStamina = 5;
        m_staminaRegen = 3;
        m_power = 1;

        m_team = Team.Player;
        m_rarity = GameRarity.Rare;

        AddKeyword(new GameRangeKeyword(3), false);
        AddKeyword(new GameVictoriousKeyword(new GameGainRangeAction(this, 1)), false);

        m_name = "Elven Sentinel";
        m_desc = "Deal an extra point of damage per tile between " + m_name + " and the target unit.\n";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    protected override int GetDamageToDealTo(GameUnit other)
    {
        int toDeal = base.GetDamageToDealTo(other);

        toDeal += WorldGridManager.Instance.GetPathLength(m_gameTile, other.GetGameTile(), true, false, true);

        return toDeal;
    }
}
