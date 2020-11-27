using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRiverlordEnemy : GameEnemyUnit
{
    public int m_auraRange = 3;
    public bool m_hasReanimated = false;

    public ContentRiverlordEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.BossStrength))
        {
            m_maxHealth = 600;
            m_maxStamina = 7;
            m_staminaRegen = 7;
            m_power = 30;
        }
        else
        {
            m_maxHealth = 300;
            m_maxStamina = 5;
            m_staminaRegen = 5;
            m_power = 15;
        }

        m_team = Team.Enemy;
        m_rarity = GameRarity.Special;
        m_isBoss = true;

        m_name = "Riverlord";
        m_desc = $"The final boss. Kill it, and win.\n";

        AddKeyword(new GameRangeKeyword(2), false);
        AddKeyword(new GameFlyingKeyword(), false);

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

        GetWorldTile().ClearSurroundingFog(2);

        if (!m_hasReanimated)
        {
            UIHelper.CreateHUDNotification("Boss Arrived", "The Lich has arrived and brought death to the world!");
        }
    }

    public override string GetDesc()
    {
        string descString = m_desc;

        descString += $"Any player units that die within range {m_auraRange} are reanimated as a <b>Husk</b> that gains their stats and <b>keywords</b>.";

        if (!WorldController.Instance.m_gameController.m_map.AllCrystalsDestroyed())
        {
            descString = "<b>Invulnerable:</b> Crystals still remain.\n" + descString;
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