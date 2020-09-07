using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentZombie : GameEntity
{
    public ContentZombie()
    {
        m_maxHealth = 8;
        m_maxAP = 3;
        m_apRegen = 2;
        m_power = 2;

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        m_name = "Zombie";
        m_desc = "When this entity hits another entity, turn it into a zombie.";
        m_typeline = Typeline.Construct;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }

    public override bool CanHitEntity(GameEntity other)
    {
        if (other is ContentZombie)
        {
            return false;
        }

        if (other is ContentZombieEnemy)
        {
            return false;
        }

        return base.CanHitEntity(other);
    }

    public override int HitEntity(GameEntity other)
    {
        GameEnemyEntity newZombie = new ContentZombieEnemy(GameHelper.GetOpponent());

        GameHelper.GetOpponent().m_controlledEntities.Remove((GameEnemyEntity)other);
        GameHelper.GetOpponent().m_controlledEntities.Add((GameEnemyEntity)newZombie);

        int damageTaken = base.HitEntity(other);

        other.m_curTile.SwapEntity(newZombie);

        return damageTaken;
    }
}