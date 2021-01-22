using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContentLordOfFrostEnemy : GameEnemyUnit
{
    private int m_numRespawnsAllowed;
    private int m_numRespawnsDone = 0;

    private int m_respawnDamageRadius = 2;
    private int m_respawnDamageAmount = 30;

    public ContentLordOfFrostEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.BossStrength))
        {
            m_maxHealth = 175;
            m_maxStamina = 5;
            m_staminaRegen = 5;
            m_attack = 30;
            m_numRespawnsAllowed = 3;
        }
        else
        {
            m_maxHealth = 125;
            m_maxStamina = 5;
            m_staminaRegen = 5;
            m_attack = 20;
            m_numRespawnsAllowed = 2;
        }

        m_team = Team.Enemy;
        m_rarity = GameRarity.Special;
        m_isBoss = true;
        m_attackSFX = AudioHelper.SlamHeavy;

        AddKeyword(new GameFlyingKeyword(), true, false);
        m_name = "Lord of Frost";
        m_desc = $"The final boss. Kill it, and win.\nThe first {m_numRespawnsAllowed} times this unit dies, it instead will revive to full health, gain 50% more attack <b>permanently</b>.\nWhen this unit revives, it damages <b>all</b> units in range {m_respawnDamageRadius} for {m_respawnDamageAmount} and roots them until end of turn.\n";

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override string GetDesc()
    {
        return base.GetDesc() + $"Revives remaining: {m_numRespawnsAllowed - m_numRespawnsDone}\n";
    }

    public override void OnSummon()
    {
        base.OnSummon();

        GameHelper.GetGameController().m_activeBossUnits.Add(this);

        UIHelper.CreateHUDNotification("Boss Arrived", "The Lord of Frost has arrived!");
    }

    public override void Die(bool canRevive = true, DamageType damageType = DamageType.None)
    {
        if (m_numRespawnsDone < m_numRespawnsAllowed)
        {
            m_numRespawnsDone++;

            UIHelper.CreateWorldElementNotification("The Lord of Frost revives and gains power!", false, GetWorldTile().gameObject);

            Heal(GetMaxHealth(), false);
            AddStats(GetAttack() / 2, 0, true, false);
            List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), m_respawnDamageRadius);
            for (int i = 0; i < surroundingTiles.Count; i++)
            {
                if (surroundingTiles[i].IsOccupied())
                {
                    GameUnit other = surroundingTiles[i].GetOccupyingUnit();
                    other.GetHitByAbility(m_respawnDamageAmount);
                    other.AddKeyword(new GameRootedKeyword(), false, false);

                    if (other.GetTeam() == Team.Player)
                    {
                        GameHelper.GetPlayer().AddScheduledAction(ScheduledActionTime.EndOfTurn, new GameLoseKeywordAction(other, new GameRootedKeyword()));
                    }
                    else
                    {
                        GameHelper.GetPlayer().AddScheduledAction(ScheduledActionTime.StartOfTurn, new GameLoseKeywordAction(other, new GameRootedKeyword()));
                    }
                }
            }

            return;
        }
        
        base.Die(canRevive, damageType);

        GameHelper.GetGameController().m_activeBossUnits.Remove(this);

        GameHelper.EndLevel(RunEndType.Win);
    }
}