using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLordOfEruptionsEnemy : GameEnemyUnit
{
    public int m_teleportRange = 3;
    
    public ContentLordOfEruptionsEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.BossStrength))
        {
            m_maxHealth = 900;
            m_maxStamina = 6;
            m_staminaRegen = 6;
            m_power = 25;
        }
        else
        {
            m_maxHealth = 600;
            m_maxStamina = 6;
            m_staminaRegen = 6;
            m_power = 12;
        }

        m_team = Team.Enemy;
        m_rarity = GameRarity.Special;
        m_isBoss = true;

        m_name = "Lord of Eruptions";
        m_desc = $"The final boss.  Kill it, and win.\nThis boss moves up to {m_teleportRange} tiles per turn, but uses no stamina to move. This unit can traverse any terrain type except for water.\nThis unit can use its full turn to ignite an adjacent volcano.";

        AddKeyword(new GameRangeKeyword(3), false);
        AddKeyword(new GameLavawalkKeyword(), false);

        m_AIGameEnemyUnit.AddAIStep(new AILordOfEruptionsScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AILordOfEruptionsChooseTargetToAttackStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AILordOfEruptionsTryIgniteVolcanoStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AILordOfEruptionsMoveToTargetStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override string GetDesc()
    {
        string descString = m_desc;

        if (!WorldController.Instance.m_gameController.m_map.AllCrystalsDestroyed())
        {
            descString = "<b>Invulnerable:</b> Crystals still remain.\n" + descString;
        }

        return descString;
    }

    public override void Die(bool canRevive = true, DamageType damageType = DamageType.None)
    {
        base.Die(canRevive, damageType);

        GameHelper.EndLevel(RunEndType.Win);
    }

    public override bool IsInvulnerable()
    {
        return !WorldController.Instance.m_gameController.m_map.AllCrystalsDestroyed();
    }
}