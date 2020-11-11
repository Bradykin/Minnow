using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLichEnemy : GameEnemyUnit
{
    public int m_auraRange = 4;
    
    public ContentLichEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.BossStrength))
        {
            m_maxHealth = 700;
            m_maxStamina = 8;
            m_staminaRegen = 8;
            m_power = 40;
        }
        else
        {
            m_maxHealth = 350;
            m_maxStamina = 5;
            m_staminaRegen = 5;
            m_power = 15;
        }

        m_team = Team.Enemy;
        m_rarity = GameRarity.Special;
        m_isBoss = true;

        m_name = "Lich";
        m_desc = $"The final boss. Kill it, and win.\nAll healing done to player units within range {m_auraRange} is instead converted into damage.\n";

        AddKeyword(new GameRangeKeyword(2), false);
        AddKeyword(new GameFlyingKeyword(), false);

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
    }

    public override string GetDesc()
    {
        string descString = m_desc;

        /*if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.BossStrength))
        {*/
            descString += $"Any player units that die within range {m_auraRange} are reanimated as a <b>Husk</b> that gains their stats and <b>keywords</b>.";
        /*}
        else
        {
            descString += $"Any player units that die within range {m_auraRange} are reanimated as a <b>Husk</b> that gains their stats.";
        }*/

        if (!WorldController.Instance.m_gameController.m_map.AllCrystalsDestroyed())
        {
            descString = "<b>Invulnerable:</b> Crystals still remain.\n" + descString;
        }

        return descString;
    }

    public override void Die(bool canRevive = true)
    {
        base.Die(canRevive);

        GameHelper.EndLevel(RunEndType.Win);
    }

    public override bool IsInvulnerable()
    {
        return !WorldController.Instance.m_gameController.m_map.AllCrystalsDestroyed();
    }
}