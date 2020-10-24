using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLichEnemy : GameEnemyUnit
{
    public ContentLichEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.BossStrength))
        {
            m_maxHealth = 700;
            m_maxStamina = 8;
            m_staminaRegen = 8;
            m_power = 30;
        }
        else
        {
            m_maxHealth = 350;
            m_maxStamina = 8;
            m_staminaRegen = 6;
            m_power = 15;
        }

        m_team = Team.Enemy;
        m_rarity = GameRarity.Event;
        m_isBoss = true;

        m_minWave = Constants.FinalWaveNum;
        m_maxWave = Constants.FinalWaveNum;

        m_name = "Lich";
        m_desc = "The final boss.  Kill it, and win.\n";

        AddKeyword(new GameRangeKeyword(3), false);
        AddKeyword(new GameFlyingKeyword(), false);

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override string GetDesc()
    {
        string descString = base.GetDesc();

        if (!WorldController.Instance.m_gameController.m_map.AllCrystalsDestroyed())
        {
            descString = "<b>Invulnerable:</b> Crystals still remain.\n" + descString;
        }

        return descString;
    }

    public override void Die(bool canRevive = true)
    {
        WorldController.Instance.WinGame();

        base.Die(canRevive);
    }

    public override bool IsInvulnerable()
    {
        return !WorldController.Instance.m_gameController.m_map.AllCrystalsDestroyed();
    }
}