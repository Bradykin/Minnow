using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentZombieEnemy : GameEnemyEntity
{
    public ContentZombieEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        ContentZombie zombieOrigin = new ContentZombie();
        zombieOrigin.SetTeam(Team.Enemy);

        m_maxHealth = zombieOrigin.GetMaxHealth();
        m_maxAP = zombieOrigin.GetMaxAP();
        m_apRegen = zombieOrigin.GetAPRegen();
        m_power = zombieOrigin.GetPower();

        m_team = Team.Enemy;
        m_rarity = GameRarity.Rare;

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
        GameEntity newZombie;

        if (other.GetTeam() == Team.Enemy)
        {
            newZombie = new ContentZombieEnemy(GameHelper.GetOpponent());
            WorldController.Instance.m_gameController.m_gameOpponent.m_controlledEntities.Add((GameEnemyEntity)newZombie);
        }
        else
        {
            newZombie = new ContentZombie();
            GameHelper.GetPlayer().AddControlledEntity(newZombie);
        }

        int damageTaken = base.HitEntity(other);

        other.m_curTile.SwapEntity(newZombie);

        return damageTaken;
    }
}