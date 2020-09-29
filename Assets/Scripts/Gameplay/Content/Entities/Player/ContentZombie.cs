using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentZombie : GameEntity
{
    public ContentZombie()
    {
        m_maxHealth = 35;
        m_maxAP = 6;
        m_apRegen = 3;
        m_power = 4;

        m_team = Team.Player;
        m_rarity = GameRarity.Event;

        m_name = "Zombie";
        m_desc = "On hit, turn them into a zombie.";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }

    public override bool CanHitEntity(GameEntity other, bool checkRange = true)
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

    public override int HitEntity(GameEntity other, bool spendAP = true)
    {
        int damageTaken = 0;

        if (!(other is ContentZombieEnemy))
        {
            GameEnemyEntity newZombie = new ContentZombieEnemy(GameHelper.GetOpponent());

            GameHelper.GetOpponent().m_controlledEntities.Remove((GameEnemyEntity)other);
            GameHelper.GetOpponent().m_controlledEntities.Add((GameEnemyEntity)newZombie);

            other.GetGameTile().SwapEntity(newZombie);

            damageTaken = base.HitEntity(newZombie, spendAP);
        }
        else
        {
            damageTaken = base.HitEntity(other, spendAP);
        }

        return damageTaken;
    }
}