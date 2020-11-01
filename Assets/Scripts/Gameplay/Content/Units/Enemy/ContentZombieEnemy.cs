using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: ashulman
//For AI:
//Can't attack other zombies, but can move through them regardless of team.
//Highly prefers units to buildings.
//Doesn't go for the castle, goes towards nearest player non-zombie unit instead (no matter sight)
public class ContentZombieEnemy : GameEnemyUnit
{
    public ContentZombieEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.5f, 0);

        ContentZombie zombieOrigin = new ContentZombie();
        zombieOrigin.SetTeam(Team.Enemy);

        m_maxHealth = zombieOrigin.GetMaxHealth();
        m_maxStamina = zombieOrigin.GetMaxStamina();
        m_staminaRegen = zombieOrigin.GetStaminaRegen();
        m_power = zombieOrigin.GetPower();

        m_team = Team.Enemy;
        m_rarity = GameRarity.Rare;

        m_minWave = 5;
        m_maxWave = 6;

        m_name = "Zombie";
        m_desc = "On hit, turns the other unit into a zombie!\nZombies can't attack zombies.\n";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconUnit(m_name);

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameDamageShieldKeyword(2), false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackOnceStandardStep(m_AIGameEnemyUnit), false);

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

    public override int HitUnit(GameUnit other, int damageAmount, bool spendStamina = true, bool shouldThorns = true)
    {
        int damageTaken = base.HitUnit(other, damageAmount, spendStamina);

        if (damageTaken > 0 && !other.m_isDead)
        {
            GameUnit newZombie = new ContentZombie();
            other.m_worldUnit.Init(newZombie);
            other.m_worldUnit.SetMoveTarget(other.m_worldUnit.gameObject.transform.position);
            GameHelper.GetPlayer().RemoveControlledUnit(other);
            GameHelper.GetPlayer().AddControlledUnit(newZombie);

            other.GetGameTile().SwapUnit(newZombie);
        }

        return damageTaken;
    }
}