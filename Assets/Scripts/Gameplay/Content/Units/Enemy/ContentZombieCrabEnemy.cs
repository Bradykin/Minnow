using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContentZombieCrabEnemy : GameEnemyUnit
{
    private int m_shipBombardDamage = 3;
    private int m_shipChaosBombardRange = 1;
    
    public ContentZombieCrabEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 8 + GetHealthModByWave();
        m_maxStamina = 6;
        m_staminaRegen = 3 + GetStaminaRegenModByWave();
        m_power = 2 + GetPowerModByWave();
        m_shipBombardDamage += GetBombardModByWave();

        m_team = Team.Enemy;
        m_rarity = GameRarity.Special;
        m_isElite = true;

        m_name = "Zombie Crab";
        m_desc = $"An elite foe. Defeat it and gain a relic!\nThis unit is part of the pirate crew.\nWhen this unit attacks, all zombie ships bombard the target for {m_shipBombardDamage} damage.\n";

        AddKeyword(new GameDamageReductionKeyword(3), true, false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.BossStrength))
        {
            m_desc = $"An elite foe. Defeat it and gain a relic!\nThis unit is part of the pirate crew.\nWhen this unit attacks, all zombie ships bombard the target and all other player units in range {m_shipChaosBombardRange} for {m_shipBombardDamage} damage. Any members of the crew hit by the bombard are instead healed for that amount.\n";
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        m_experienceAmount = 50;

        LateInit();
    }

    public override int HitUnit(GameUnit other, int damageAmount, bool spendStamina = true, bool isThornsAttack = false, bool canCleave = true)
    {
        GameTile targetTile = other.GetGameTile();
        
        int hitAmount = base.HitUnit(other, damageAmount, spendStamina, isThornsAttack, canCleave);

        TriggerBombard(targetTile);

        return hitAmount;
    }

    public override int HitBuilding(GameBuildingBase other, bool spendStamina = true, bool canCleave = true)
    {
        GameTile targetTile = other.GetGameTile();

        int hitAmount = base.HitBuilding(other, spendStamina, canCleave);

        TriggerBombard(targetTile);

        return hitAmount;
    }

    private void TriggerBombard(GameTile targetTile)
    {
        List<GameTile> tilesInBombardArea;
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.BossStrength))
        {
            tilesInBombardArea = WorldGridManager.Instance.GetSurroundingGameTiles(targetTile, m_shipChaosBombardRange, 0);
        }
        else
        {
            tilesInBombardArea = new List<GameTile>();
            tilesInBombardArea.Add(targetTile);
        }

        List<GameEnemyUnit> activeZombieShips = GameHelper.GetOpponent().m_controlledUnits.Where(u => u is ContentZombieShipEnemy).ToList();
        for (int i = 0; i < activeZombieShips.Count; i++)
        {
            for (int k = 0; k < tilesInBombardArea.Count; k++)
            {
                if (!tilesInBombardArea[k].IsOccupied())
                {
                    continue;
                }

                GameUnit unit = tilesInBombardArea[k].GetOccupyingUnit();

                if (unit is ContentSkeletalPirateEnemy || unit is ContentSkeletalCaptainEnemy || unit is ContentZombieCrabEnemy)
                {
                    unit.Heal(m_shipBombardDamage);
                }
                else if (unit != null && tilesInBombardArea[k].GetOccupyingUnit().GetTeam() == Team.Player)
                {
                    unit.GetHitByAbility(m_shipBombardDamage);
                }
            }
        }
    }

    public override void Die(bool canRevive = true, DamageType damageType = DamageType.None)
    {
        GamePlayer player = GameHelper.GetPlayer();

        UIHelper.TriggerRelicSelect();

        if (GameHelper.HasRelic<ContentHeroicTrophyRelic>())
        {
            for (int i = 0; i < GameHelper.GetPlayer().m_controlledUnits.Count; i++)
            {
                player.m_controlledUnits[i].AddStats(5, 5, true, true);
            }
        }

        if (GameHelper.HasRelic<ContentAncientCoinsRelic>())
        {
            player.GainGold(75);
        }

        base.Die(canRevive, damageType);
    }

    private int GetHealthModByWave()
    {
        int waveNum = GameHelper.GetCurrentWaveNum();

        int scalingValue = waveNum;
        if (waveNum >= 3)
        {
            scalingValue += (waveNum - 2);
        }

        return scalingValue * 7;
    }

    private int GetStaminaRegenModByWave()
    {
        int waveNum = GameHelper.GetCurrentWaveNum();

        return Mathf.FloorToInt((float)waveNum * 0.5f);
    }

    private int GetPowerModByWave()
    {
        int waveNum = GameHelper.GetCurrentWaveNum();

        return waveNum * 4;
    }

    private int GetBombardModByWave()
    {
        int waveNum = GameHelper.GetCurrentWaveNum();

        return waveNum;
    }
}