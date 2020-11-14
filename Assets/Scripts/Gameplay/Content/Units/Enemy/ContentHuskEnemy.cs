using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHuskEnemy : GameEnemyUnit
{
    public ContentHuskEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 4;
        m_maxStamina = 4;
        m_staminaRegen = 2;
        m_power = 4;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Special;

        m_name = "Husk";
        m_desc = "This unit is damaged by healing effects.";

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override int Heal(int toHeal)
    {
        UIHelper.CreateWorldElementNotification("The Husk is burned by healing power!", true, m_worldUnit.gameObject);
        GetHitByAbility(toHeal);

        return 0;
    }

    public void SetStatsEqualToUnit(GameUnit deadUnit)
    {
        m_maxHealth = deadUnit.GetMaxHealth();
        m_maxStamina = deadUnit.GetMaxStamina();
        m_staminaRegen = deadUnit.GetStaminaRegen();
        m_power = deadUnit.GetPower();

        IReadOnlyList<GameKeywordBase> deadUnitKeywords = deadUnit.GetKeywordHolderForRead().GetKeywordsForRead();
        for (int i = 0; i < deadUnitKeywords.Count; i++)
        {
            AddKeyword(deadUnitKeywords[i]);
        }
    }
}