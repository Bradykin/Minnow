using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLordOfShadowsEnemy : GameEnemyUnit
{
    public int m_brightnessLevel = 0;
    public int m_visionReductionAmount = 1;
    public int m_brightnessExplosionAmount = 20;
    //private int m_brightnessExplosionBreakpoint = 5;
    
    public ContentLordOfShadowsEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.BossStrength))
        {
            m_maxHealth = 900;
            m_maxStamina = 9;
            m_staminaRegen = 9;
            m_power = 50;
        }
        else
        {
            m_maxHealth = 600;
            m_maxStamina = 8;
            m_staminaRegen = 8;
            m_power = 30;
        }

        m_team = Team.Enemy;
        m_rarity = GameRarity.Special;
        m_isBoss = true;

        m_name = "Lord Of Shadows";
        m_desc = $"The final boss. Kill it, and win.\nWhile this boss is alive, all units have -{m_visionReductionAmount} sight radius and fog will not stay revealed.\nGlows brighter for each time this unit attacks or is attacked, and explodes at the end of its turn damaging all enemies in the brightness area for {m_brightnessExplosionAmount}\n";
        //m_desc = $"The final boss. Kill it, and win.\nWhile this boss is alive, all units have -{m_visionReductionAmount} sight radius and fog will not stay revealed.\nGlows brighter for each time this unit attacks or is attacked. When it reaches {m_brightnessExplosionBreakpoint} brightness, it explodes damaging all enemies in the brightness area for {m_brightnessExplosionAmount}\n";

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
        WorldGridManager.Instance.EndIntermissionFogUpdate();

        UIHelper.CreateHUDNotification("Boss Arrived", "The Lord of Shadows has arrived and plunged the world into darkness!");
    }

    public override int HitUnit(GameUnit other, int damageAmount, bool spendStamina = true, bool shouldThorns = true)
    {
        int hitAmount = base.HitUnit(other, damageAmount, spendStamina, shouldThorns);

        if (hitAmount > 0)
        {
            m_brightnessLevel += 1;
            WorldGridManager.Instance.EndIntermissionFogUpdate();

            //TryExplodeBrightness();
        }

        return hitAmount;
    }

    public override int HitBuilding(GameBuildingBase other, bool spendStamina = true)
    {
        int hitAmount = base.HitBuilding(other, spendStamina);

        if (hitAmount > 0)
        {
            m_brightnessLevel += 1;
            WorldGridManager.Instance.EndIntermissionFogUpdate();

            //TryExplodeBrightness();
        }

        return hitAmount;
    }

    protected override int GetHitImpl(int damage, DamageType damageType)
    {
        int hitAmount = base.GetHitImpl(damage, damageType);

        if (hitAmount > 0)
        {
            m_brightnessLevel += 1;
            WorldGridManager.Instance.EndIntermissionFogUpdate();

            //TryExplodeBrightness();
        }

        return hitAmount;
    }

    public override void EndTurn()
    {
        TryExplodeBrightness();
        
        base.EndTurn();
    }

    private void TryExplodeBrightness()
    {
        if (m_brightnessLevel > 0)
        //if (m_brightnessLevel >= m_brightnessExplosionBreakpoint)
        {
            List<GameTile> tilesInBrightnessRange = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), m_brightnessLevel);

            for (int i = 0; i < tilesInBrightnessRange.Count; i++)
            {
                if (tilesInBrightnessRange[i].IsOccupied() && tilesInBrightnessRange[i].m_occupyingUnit.GetTeam() == Team.Player)
                {
                    tilesInBrightnessRange[i].m_occupyingUnit.GetHitByAbility(m_brightnessExplosionAmount);
                }

                if (tilesInBrightnessRange[i].HasBuilding() && tilesInBrightnessRange[i].GetBuilding().GetTeam() == Team.Player)
                {
                    tilesInBrightnessRange[i].GetBuilding().GetHit(m_brightnessExplosionAmount);
                }
            }

            m_brightnessLevel = 0;
            WorldGridManager.Instance.EndIntermissionFogUpdate();
        }
    }

    public override string GetDesc()
    {
        string descString = m_desc;

        descString += $"<b>Brightness Level:</b> {m_brightnessLevel}\n";

        if (!WorldController.Instance.m_gameController.m_map.AllCrystalsDestroyed())
        {
            descString = $"<b>Invulnerable:</b> Crystals still remain.\n{descString}";
        }

        return descString;
    }

    public override void Die(bool canRevive = true)
    {
        base.Die(canRevive);

        GameHelper.GetGameController().m_activeBossUnits.Remove(this);

        GameHelper.EndLevel(RunEndType.Win);
    }

    public override bool IsInvulnerable()
    {
        return !WorldController.Instance.m_gameController.m_map.AllCrystalsDestroyed();
    }
}