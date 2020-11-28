using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContentLordOfWinterEnemy : GameEnemyUnit
{
    public ContentLordOfWinterEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.BossStrength))
        {
            m_maxHealth = 900;
            m_maxStamina = 9;
            m_staminaRegen = 9;
            m_power = 40;
        }
        else
        {
            m_maxHealth = 600;
            m_maxStamina = 8;
            m_staminaRegen = 8;
            m_power = 20;
        }

        m_team = Team.Enemy;
        m_rarity = GameRarity.Special;
        m_isBoss = true;

        m_name = "Lord of Shadows";
        m_desc = $"The final boss. Kill it, and win.\n";

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override void OnSummon()
    {
        base.OnSummon();

        GameHelper.GetGameController().m_activeBossUnits.Add(this);
        WorldGridManager.Instance.EndIntermissionFogUpdate();

        UIHelper.CreateHUDNotification("Boss Arrived", "The Lord of Winter has arrived and summoned the endless winter!");
    }

    public override string GetDesc()
    {
        string descString = m_desc;

        if (!WorldController.Instance.m_gameController.m_map.AllCrystalsDestroyed())
        {
            descString = $"<b>Invulnerable:</b> Crystals still remain.\n{descString}";
        }

        return descString;
    }

    public override void Die(bool canRevive = true, DamageType damageType = DamageType.None)
    {
        base.Die(canRevive, damageType);

        GameHelper.GetGameController().m_activeBossUnits.Remove(this);

        GameHelper.EndLevel(RunEndType.Win);
    }

    public override bool IsInvulnerable()
    {
        return !WorldController.Instance.m_gameController.m_map.AllCrystalsDestroyed();
    }
}