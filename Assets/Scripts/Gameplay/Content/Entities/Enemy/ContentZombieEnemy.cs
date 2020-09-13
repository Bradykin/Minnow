using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: ashulman
//For AI:
//Can't attack other zombies, but can move through them regardless of team.
//Highly prefers units to buildings.
//Doesn't go for the castle, goes towards nearest player non-zombie unit instead (no matter sight)
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

        m_minWave = 6;

        m_name = "Zombie";
        m_desc = "When this entity hits another entity, turn it into a zombie.";
        m_typeline = Typeline.Construct;
        m_icon = UIHelper.GetIconEntity(m_name);

        m_AIGameEnemyEntity.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIChooseTargetToAttackStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIAttackUntilOutOfAPStep(m_AIGameEnemyEntity));

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

    public override int HitEntity(GameEntity other, bool spendAP = true)
    {
        GameEntity newZombie = new ContentZombie();
        GameHelper.GetPlayer().RemoveControlledEntity(other);
        GameHelper.GetPlayer().AddControlledEntity(newZombie);

        int damageTaken = base.HitEntity(other, spendAP);

        other.m_curTile.SwapEntity(newZombie);

        return damageTaken;
    }
}