using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentVolcanoCrabEnemy : GameEnemyUnit
{
    int m_powerIncrease = 3;
    int m_staminaRegenIncrease = 1;
    int m_damageReductionDecrease = 2;

    int m_maxDamageReduction = 12;
    
    public ContentVolcanoCrabEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 20;
        m_maxStamina = 8;
        m_staminaRegen = 1;
        m_power = 2;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Uncommon;

        m_minWave = 3;
        m_maxWave = 4;

        m_name = "Volcano Crab";
        m_desc = $"At the start of each turn, this unit gets +{m_powerIncrease} Power, +{m_staminaRegenIncrease} Stamina Regen, and -{m_damageReductionDecrease} Damage Reduction.";

        AddKeyword(new GameDamageReductionKeyword(m_maxDamageReduction), false);
        AddKeyword(new GameLavawalkKeyword(), false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            m_desc += " When this unit steps on a lava tile, it regains all Damage Reduction.";
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override void OnSummon()
    {
        base.OnSummon();

        SpendStamina(GetCurStamina() - 1);
    }

    public override void StartTurn()
    {
        m_staminaRegen += m_staminaRegenIncrease;
        m_maxStamina += m_staminaRegenIncrease;
        m_power += m_powerIncrease;
        SubtractKeyword(new GameDamageReductionKeyword(m_damageReductionDecrease));

        base.StartTurn();
    }

    public override void OnMoveEnd()
    {
        base.OnMoveEnd();

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility) && GetGameTile().GetTerrain().IsLava())
        {
            GameDamageReductionKeyword gameDamageReductionKeyword = GetDamageReductionKeyword();

            if (gameDamageReductionKeyword == null)
            {
                AddKeyword(new GameDamageReductionKeyword(m_maxDamageReduction));
            }
            else
            {
                AddKeyword(new GameDamageReductionKeyword(m_maxDamageReduction - gameDamageReductionKeyword.m_damageReduction));
            }
        }
    }

    public override void EndTurn()
    {
        base.EndTurn();

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility) && GetGameTile().GetTerrain().IsLava())
        {
            GameDamageReductionKeyword gameDamageReductionKeyword = GetDamageReductionKeyword();

            if (gameDamageReductionKeyword == null)
            {
                AddKeyword(new GameDamageReductionKeyword(m_maxDamageReduction));
            }
            else
            {
                AddKeyword(new GameDamageReductionKeyword(m_maxDamageReduction - gameDamageReductionKeyword.m_damageReduction));
            }
        }
    }
}