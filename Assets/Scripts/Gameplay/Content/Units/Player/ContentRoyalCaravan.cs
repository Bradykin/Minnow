﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRoyalCaravan : GameUnit
{
    public ContentRoyalCaravan() : base()
    {
        m_team = Team.Player;
        m_rarity = GameRarity.Special;

        AddKeyword(new GameRangeKeyword(2), true, false);

        m_name = "Royal Caravan";
        m_desc = "With the castle gone, this caravan holds the royal court. Lose this, and it's game over!\n";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);
        m_attackSFX = AudioHelper.BowLight;

        LateInit();
    }

    public override void Die(bool canRevive = true, DamageType damageType = DamageType.None)
    {
        base.Die(canRevive, damageType);

        GameHelper.EndLevel(RunEndType.Loss);
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 50;
        m_maxStamina = 5;
        m_staminaRegen = 3;
        m_attack = 5;

        m_curStamina = m_staminaRegen;
    }
}