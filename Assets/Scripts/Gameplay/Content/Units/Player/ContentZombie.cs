using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentZombie : GameUnit
{
    public ContentZombie()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.5f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Special;

        m_name = "Zombie";
        m_desc = "On hit, turn them into a zombie.\nZombies can't attack zombies.\n";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    public override bool CanHitUnit(GameUnit other, bool checkRange = true)
    {
        if (other is ContentZombie)
        {
            return false;
        }

        if (other is ContentZombieEnemy)
        {
            return false;
        }

        return base.CanHitUnit(other, checkRange);
    }

    public override int HitUnit(GameUnit other, int damageAmount, bool spendStamina = true, bool shouldThorns = true, bool canCleave = true)
    {
        int damageTaken = base.HitUnit(other, damageAmount, spendStamina);

        if (damageTaken > 0 && !other.m_isDead)
        {
            GameEnemyUnit newZombie = new ContentZombieEnemy(GameHelper.GetOpponent());
            other.m_worldUnit.Init(newZombie);
            other.m_worldUnit.SetMoveTarget(other.m_worldUnit.gameObject.transform.position);
            GameHelper.GetOpponent().m_controlledUnits.Remove((GameEnemyUnit)other);
            GameHelper.GetOpponent().m_controlledUnits.Add(newZombie);

            other.GetGameTile().SwapUnit(newZombie);
        }

        return damageTaken;
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 35;
        m_maxStamina = 5;
        m_staminaRegen = 3;
        m_power = 3;
    }
}