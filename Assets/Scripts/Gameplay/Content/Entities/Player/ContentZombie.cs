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

    public override int HitEntity(GameEntity other)
    {
        GameEntity newZombie = new ContentZombie();

        Team otherTeam;
        if (other.GetTeam() == Team.Enemy)
        {
            //TODO: ashulman.  How can we do this?  Do we need a different enemy for zombies?
            //WorldController.Instance.m_gameController.m_gameOpponent.m_controlledEntities.Add((GameEnemyEntity)newZombie);
            otherTeam = Team.Enemy;
        }
        else
        {
            GameHelper.GetPlayer().AddControlledEntity(newZombie);
            otherTeam = Team.Player;
        }

        int damageTaken = base.HitEntity(other);

        newZombie.SetTeam(otherTeam);
        other.m_curTile.SwapEntity(newZombie);

        return damageTaken;
    }
}