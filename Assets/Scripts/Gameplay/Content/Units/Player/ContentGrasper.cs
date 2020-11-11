﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGrasper : GameUnit
{
    public ContentGrasper()
    {
        m_maxHealth = 9;
        m_maxStamina = 3;
        m_staminaRegen = 2;
        m_power = 1;

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        m_name = "Grasper";
        m_desc = "When this hits a unit, it drains all Stamina from it.\n";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    public override int HitUnit(GameUnit other, int damageAmount, bool spendStamina = true, bool shouldThorns = true)
    {
        int staminaToDrain = other.GetCurStamina();

        int damageTaken = base.HitUnit(other, damageAmount, spendStamina);

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            if (damageTaken > 0)
            {
                if (!other.m_isDead)
                {
                    GainStamina(other.GetCurStamina());
                    other.EmptyStamina();
                }
                else if (staminaToDrain >= 0)
                {
                    GainStamina(staminaToDrain);
                }
            }
        }

        return damageTaken;
    }
}