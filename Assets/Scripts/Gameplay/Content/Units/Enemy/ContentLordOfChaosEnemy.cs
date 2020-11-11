using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLordOfChaosEnemy : GameEnemyUnit
{
    public enum ChaosWarpAbility : int
    {
        CoverTakesMoreDamage, // 0
        NormalDifficultTerrainCostReversal, // 1
        StaminaCostAttackIncreaseMoveDecrease, // 2
        RangedNotRangedSwap, // 3
        AllUnitsDeathExplode, // 4
        DamageAppliesBleeds, // 5
        NobodyCanDealDamage, // 6
        AllRooted, // 7
        None // 8
    }

    public List<ChaosWarpAbility> m_possibleChaosWarpAbilities = new List<ChaosWarpAbility>();

    public ChaosWarpAbility m_currentChaosWarpAbility;

    public ContentLordOfChaosEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.BossStrength))
        {
            m_maxHealth = 1000;
            m_maxStamina = 5;
            m_staminaRegen = 5;
            m_power = 120;
        }
        else
        {
            m_maxHealth = 700;
            m_maxStamina = 4;
            m_staminaRegen = 4;
            m_power = 80;
        }

        m_team = Team.Enemy;
        m_rarity = GameRarity.Special;
        m_isBoss = true;

        m_name = "Lord Of Chaos";
        m_desc = $"The final boss. Kill it, and win.\n";

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override void OnSummon()
    {
        base.OnSummon();

        GameHelper.GetGameController().m_activeBossUnits.Add(this);

        m_currentChaosWarpAbility = (ChaosWarpAbility)Random.Range(0, 8);
    }

    public override string GetDesc()
    {
        string descString = m_desc;

        descString += GetChaosWarpString();

        if (!WorldController.Instance.m_gameController.m_map.AllCrystalsDestroyed())
        {
            descString = $"<b>Invulnerable:</b> Crystals still remain.\n{descString}";
        }

        return descString;
    }

    private string GetChaosWarpString()
    {
        string chaosWarpString = "";

        switch(m_currentChaosWarpAbility)
        {
            case ChaosWarpAbility.CoverTakesMoreDamage:
                chaosWarpString = "Units on cover take 50% more damage instead of 50% less.\n";
                break;
            case ChaosWarpAbility.NormalDifficultTerrainCostReversal:
                chaosWarpString = "Normal terrain costs 2 to move through, and Difficult terrain costs 1 to move through.\n";
                break;
            case ChaosWarpAbility.StaminaCostAttackIncreaseMoveDecrease:
                chaosWarpString = "Attacking costs 1 less stamina (minimum of 1), and movement costs 1 more stamina.\n";
                break;
            case ChaosWarpAbility.RangedNotRangedSwap:
                chaosWarpString = "All ranged units only have one range. All non-ranged units have ranged 2.\n";
                break;
            case ChaosWarpAbility.AllUnitsDeathExplode:
                chaosWarpString = "All units explode on death for damage equal to their power.\n";
                break;
            case ChaosWarpAbility.DamageAppliesBleeds:
                chaosWarpString = "Attacks apply bleeds instead of damage.\n";
                break;
            case ChaosWarpAbility.NobodyCanDealDamage:
                chaosWarpString = "Attacks deal no damage this turn.\n";
                break;
            case ChaosWarpAbility.AllRooted:
                chaosWarpString = "All units are rooted for this turn.\n";
                break;
            case ChaosWarpAbility.None:
                chaosWarpString = "";
                break;
        }

        return chaosWarpString;
    }

    public override void Die(bool canRevive = true)
    {
        base.Die(canRevive);

        GameHelper.GetGameController().m_activeBossUnits.Remove(this);

        GameHelper.EndLevel(RunEndType.Win);
    }

    public override bool IsInvulnerable()
    {
        return !WorldController.Instance.m_gameController.m_map.AllCrystalsDestroyed();
    }
}