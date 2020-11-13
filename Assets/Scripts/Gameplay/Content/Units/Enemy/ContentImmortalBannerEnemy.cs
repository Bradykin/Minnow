using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentImmortalBannerEnemy : GameEnemyUnit
{
    private int m_powerIncreaseAmount = 5;
    private int m_damageReductionIncrease = 3;
    public int m_auraRange = 3;

    public ContentImmortalBannerEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.BossStrength))
        {
            m_maxHealth = 350;
            m_maxStamina = 7;
            m_staminaRegen = 7;
            m_power = 25;
        }
        else
        {
            m_maxHealth = 150;
            m_maxStamina = 5;
            m_staminaRegen = 5;
            m_power = 15;
        }

        m_team = Team.Enemy;
        m_rarity = GameRarity.Special;
        m_isBoss = true;

        m_name = "Immortal Banner";
        m_desc = $"One of the final bosses. If all three Immortals die, you win. If any are alive at the start of their turn, the others will respawn.\nOther enemies in range 2 get +{m_powerIncreaseAmount} Power and {m_damageReductionIncrease} Damage Reduction.\n";


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

    public override void Die(bool canRevive = true)
    {
        base.Die(canRevive);

        if (m_isDead)
        {
            GameController gameController = GameHelper.GetGameController();
            gameController.m_activeBossUnits.Remove(this);

            List<GameEnemyUnit> activeBossUnits = gameController.m_activeBossUnits;
            for (int i = 0; i < activeBossUnits.Count; i++)
            {
                if (activeBossUnits[i] is ContentImmortalSpearEnemy || activeBossUnits[i] is ContentImmortalBowEnemy)
                {
                    //At least one other immortal is alive, the game is not over
                    return;
                }
            }

            //The other members of the Immortals are dead, player has won
            GameHelper.EndLevel(RunEndType.Win);
        }
    }
}