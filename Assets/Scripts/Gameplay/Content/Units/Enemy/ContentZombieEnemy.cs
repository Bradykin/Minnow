using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        m_name = "Zombie";
        m_desc = "On hit, turns the other unit into a zombie!\nZombies can't attack zombies.\n";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconUnit(m_name);

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameDamageShieldKeyword(2), false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
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

    public override int HitUnit(GameUnit other, int damageAmount, bool spendStamina = true, bool shouldThorns = true, bool canCleave = true)
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