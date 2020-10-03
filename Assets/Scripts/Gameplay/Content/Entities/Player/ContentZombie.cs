using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentZombie : GameUnit
{
    public ContentZombie()
    {
        m_maxHealth = 35;
        m_maxStamina = 6;
        m_staminaRegen = 3;
        m_power = 4;

        m_team = Team.Player;
        m_rarity = GameRarity.Event;

        m_name = "Zombie";
        m_desc = "On hit, turn them into a zombie.\nZombies can't attack zombies.";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }

    public override bool CanHitEntity(GameUnit other, bool checkRange = true)
    {
        if (other is ContentZombie)
        {
            return false;
        }

        if (other is ContentZombieEnemy)
        {
            return false;
        }

        return base.CanHitEntity(other, checkRange);
    }

    public override int HitUnit(GameUnit other, bool spendStamina = true)
    {
        int damageTaken = 0;

        if (!(other is ContentZombieEnemy))
        {
            GameEnemyEntity newZombie = new ContentZombieEnemy(GameHelper.GetOpponent());
            other.m_worldUnit.Init(newZombie);
            GameHelper.GetOpponent().m_controlledEntities.Remove((GameEnemyEntity)other);
            GameHelper.GetOpponent().m_controlledEntities.Add(newZombie);

            other.GetGameTile().SwapEntity(newZombie);

            damageTaken = base.HitUnit(newZombie, spendStamina);
        }
        else
        {
            damageTaken = base.HitUnit(other, spendStamina);
        }

        return damageTaken;
    }
}