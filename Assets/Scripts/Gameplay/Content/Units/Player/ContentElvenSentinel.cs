﻿using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentElvenSentinel : GameUnit
{
    public ContentElvenSentinel() : base()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.4f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Rare;

        AddKeyword(new GameRangeKeyword(3), true, false);
        AddKeyword(new GameVictoriousKeyword(new GameGainRangeAction(this, 2)), true, false);

        m_name = "Elven Sentinel";
        m_desc = "Multiply damage dealt by the distance between " + m_name + " and the target unit.\n";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);
        m_attackSFX = AudioHelper.BowHeavy;

        LateInit();
    }

    public override int GetDamageToDealTo(GameUnit other)
    {
        int toDeal = base.GetDamageToDealTo(other);

        toDeal = toDeal * WorldGridManager.Instance.GetPathLength(m_gameTile, other.GetGameTile(), true, false, true);

        return toDeal;
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 6;
        m_maxStamina = 5;
        m_staminaRegen = 3;
        m_attack = 1;
    }
}
