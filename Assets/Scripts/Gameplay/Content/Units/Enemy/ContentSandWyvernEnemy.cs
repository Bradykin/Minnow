using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSandWyvernEnemy : GameEnemyUnit
{
    public ContentSandWyvernEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 55;
        m_maxStamina = 6;
        m_staminaRegen = 4;
        m_power = 20;
        m_attackSFX = AudioHelper.BirdFlap;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Uncommon;

        m_name = "Sand Wyvern";
        m_desc = "";

        AddKeyword(new GameFlyingKeyword(), true, false);
        AddKeyword(new GameVictoriousKeyword(new GameGainStaminaAction(this, m_maxStamina)), true, false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            m_aoeRange = 3;
            m_desc += $"Whenever any unit dies within range {m_aoeRange} of this unit, this unit heals to full health.";
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override void OnOtherDie(GameUnit other, GameTile deathLocation)
    {
        base.OnOtherDie(other, deathLocation);

        if (!GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            return;
        }

        if (WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(GetGameTile(), deathLocation) <= m_aoeRange)
        {
            Heal(GetMaxHealth());
        }
    }
}