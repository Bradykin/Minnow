using Game.Util;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public enum Team : int
{
    Player,
    Enemy
}

public enum Typeline : int
{
    Humanoid,
    Monster,
    Creation,
    Count // = 3
}

public enum DamageType : int
{
    None,
    Unit,
    Spell,
    Ability
}

public abstract class GameUnit : GameElementBase, ITurns, ISave<JsonGameUnitData>, ILoad<JsonGameUnitData>
{
    //General data.  This should be set for every unit
    protected Team m_team;
    protected int m_curHealth;
    protected int m_maxHealth;
    protected int m_permMaxHealth;
    protected int m_curStamina;
    protected int m_staminaRegen;
    protected int m_permStaminaRegen;
    protected int m_maxStamina;
    protected int m_permMaxStamina;
    protected int m_power;
    protected int m_permPower;
    protected Typeline m_typeline;

    //Specific data.  Only set if it varies from the default.  Be sure to add to the descrip so it shows up in the UI.
    private GameKeywordHolder m_keywordHolder = new GameKeywordHolder();
    protected int m_staminaToAttack = 2;
    protected int m_sightRange = 3;
    public bool m_shouldAlwaysPassEnemies;
    public bool m_alwaysIgnoreDifficultTerrain;

    //Functionality
    protected GameTile m_gameTile;
    public bool m_isDead;
    public bool m_returnedToDeckDeath;
    public WorldUnit m_worldUnit;
    public Sprite m_iconWhite;
    protected string m_customName;
    protected Vector3 m_worldTilePositionAdjustment = new Vector3(0,0,0);
    protected bool m_usesBigTooltip;

    //Special functionality
    public bool m_instantWaterMovement;
    public bool m_instantForestMovement;
    public bool m_startWithMaxStamina;
    public bool m_takesLavaFieldDamage = true;
    public bool m_incrementsKillCounter = true;

    protected AudioClip m_attackSFX;

    //Unique guid per unit, to use to link together like gameunits in save data
    private string m_guid = System.Guid.NewGuid().ToString();

    public void CopyOff(GameUnit other)
    {
        m_maxHealth = other.m_maxHealth;
        m_permMaxHealth = other.m_permMaxHealth;
        m_staminaRegen = other.m_staminaRegen;
        m_permStaminaRegen = other.m_permStaminaRegen;
        m_maxStamina = other.m_maxStamina;
        m_permMaxStamina = other.m_permMaxStamina;
        m_power = other.m_power;
        m_permPower = other.m_permPower;
        m_typeline = other.m_typeline;
        m_attackSFX = other.m_attackSFX;

        m_keywordHolder = other.m_keywordHolder.Clone(other, this);

        if (other.HasCustomName())
        {
            SetCustomName();
        }
    }

    protected virtual void LateInit()
    {
        m_icon = UIHelper.GetIconUnit(m_name);
        m_iconWhite = UIHelper.GetIconUnit(m_name + "W");

        ResetToBase();
        SetHealthStaminaValues();
    }

    public void SetHealthStaminaValues()
    {
        m_curHealth = GetMaxHealth();

        if (m_startWithMaxStamina)
        {
            m_curStamina = GetMaxStamina();
        }
        else
        {
            m_curStamina = GetStaminaRegen();
        }

        if (m_curStamina > GetMaxStamina())
        {
            m_curStamina = GetMaxStamina();
        }
    }

    public virtual void OnSummon()
    {
        m_isDead = false;
        m_returnedToDeckDeath = false;
        SetHealthStaminaValues();

        TriggerSummonRelics();

        GameSummonKeyword summonKeyword = GetSummonKeyword();
        if (summonKeyword != null)
        {
            summonKeyword.DoAction();
        }

        if (GetTeam() == Team.Player)
        {
            GameHelper.GetPlayer().InformWasSummoned(this);
        }
    }

    public virtual void OnOtherSummon(GameUnit other)
    {

    }

    public virtual void OnMoveBegin()
    {

    }

    public virtual void OnMoveEnd()
    {
        
    }

    public virtual void OnOtherMove(GameUnit other, GameTile startingTIle, GameTile endingTile, List<GameTile> pathBetweenTiles)
    {

    }

    public virtual void EndWave()
    {
        GameFadeKeyword fadeKeyword = GetFadeKeyword(true);
        if (fadeKeyword != null && !fadeKeyword.m_isActive)
        {
            fadeKeyword.m_isActive = true;
        }
        
        ResetToBase();
    }

    protected abstract void ResetToBase();

    public GameTile GetGameTile()
    {
        return m_gameTile;
    }

    public WorldTile GetWorldTile()
    {
        return m_gameTile.GetWorldTile();
    }

    public void SetGameTile(GameTile gameTile)
    {
        m_gameTile = gameTile;
    }

    public void SetWorldTile(WorldTile worldTile)
    {
        m_gameTile = worldTile.GetGameTile();
    }

    public virtual int GetHitByUnit(int damage, GameUnit gameUnit, bool canReturnThorns)
    {
        GameThornsKeyword thornsKeyword = GetThornsKeyword();
        if (canReturnThorns && thornsKeyword != null)
        {
            HitUnit(gameUnit, thornsKeyword.m_thornsDamage, spendStamina: false, isThornsAttack: true);
        }

        return GetHitImpl(damage, DamageType.Unit);
    }

    public virtual int GetHitBySpell(int damage, GameCardSpellBase gameCardSpellBase)
    {
        return GetHitImpl(damage, DamageType.Spell);
    }

    public virtual int GetHitByAbility(int damage)
    {
        return GetHitImpl(damage, DamageType.Ability);
    }

    protected virtual int GetHitImpl(int damage, DamageType damageType)
    {
        if (m_isDead)
        {
            return 0;
        }

        if (IsInvulnerable())
        {
            if (!GetGameTile().m_isFog)
            {
                UIHelper.CreateWorldElementNotification(GetName() + " is invulnerable and takes no damage!", false, m_gameTile.GetWorldTile().gameObject);
            }
            return 0;
        }

        damage = CalculateDamageAmount(damage, damageType);

        if (damage <= 0)
        {
            if (!GetGameTile().m_isFog)
            {
                UIHelper.CreateWorldElementNotification(GetName() + " takes no damage from the hit!", false, m_gameTile.GetWorldTile().gameObject);
            }
            return 0;
        }

        GameDamageShieldKeyword damageShieldKeyword = GetDamageShieldKeyword();
        if (damageShieldKeyword != null)
        {
            if (!GetGameTile().m_isFog)
            {
                UIHelper.CreateWorldElementNotification("Damage Shield Broken!", true, m_worldUnit.gameObject);
            }
            RemoveKeyword(damageShieldKeyword);

            return 0;
        }

        if (GameHelper.HasRelic<ContentTalonOfTheCruelRelic>() && GetTeam() == Team.Enemy && GetFlyingKeyword() != null)
        {
            damage = damage * 2;
        }

        if (GameHelper.HasRelic<ContentHistoryInBloodRelic>())
        {
            damage = damage * 2;
        }

        bool lordOfChaosDamageApplyBleedsActive = false;
        ContentLordOfChaosEnemy lordOfChaosEnemy = GameHelper.GetBoss<ContentLordOfChaosEnemy>();
        if (lordOfChaosEnemy != null && lordOfChaosEnemy.m_currentChaosWarpAbility == ContentLordOfChaosEnemy.ChaosWarpAbility.DamageAppliesBleeds)
        {
            lordOfChaosDamageApplyBleedsActive = true;
        }

        if (lordOfChaosDamageApplyBleedsActive)
        {
            AddKeyword(new GameBleedKeyword(), false, false);
        }

        m_curHealth -= damage;

        if (GetTeam() == Team.Player)
        {
            if (GameHelper.HasRelic<ContentAngelicFeatherRelic>() && m_curHealth > 0 && m_curHealth <= 10)
            {
                AddKeyword(new GameDamageShieldKeyword(), false, false);
            }

            if (GameHelper.HasRelic<ContentBloodFeatherRelic>() && m_curHealth > 0 && m_curHealth <= 3)
            {
                AddStats(10, 0, false, true);
            }

            if (GameHelper.HasRelic<ContentGoldenFeatherRelic>() && m_curHealth > 0 && m_curHealth <= 1)
            {
                GameHelper.GetPlayer().GainGold(15, true);
            }
        }

        GameEnrageKeyword enrageKeyword = GetEnrageKeyword();

        if (enrageKeyword != null)
        {
            enrageKeyword.DoAction(damage);

            //Trigger again if the player has Bestial Wrath
            if (GetTypeline() == Typeline.Monster && GetTeam() == Team.Player)
            {
                if (GameHelper.HasRelic<ContentBestialWrathRelic>())
                {
                    enrageKeyword.DoAction(damage);
                }
            }
        }

        string coverReducDesc = "";
        if (IsInCover())
        {
            coverReducDesc += $" ({Constants.CoverProtectionPercent}% Cover)";
        }

        if (damageType == DamageType.Spell && GetGameTile().GetTerrain().IsDunes())
        {
            if (IsInCover())
            {
                coverReducDesc += ",";
            }
            coverReducDesc += $" ({Constants.SandDuneMagicDamageReductionPercentage}% Magic Cover";
        }

        string damageReducDesc = "";
        GameDamageReductionKeyword damageReductionKeyword = GetDamageReductionKeyword();
        if (damageReductionKeyword != null)
        {
            damageReducDesc = "\n(" + damageReductionKeyword.m_damageReduction + " <b>Damage Reduction</b>)";
        }

        if (m_curHealth <= 0)
        {
            Die(true, damageType);
        }
        else
        {
            if (!GetGameTile().m_isFog)
            {
                UIHelper.CreateWorldElementNotification(damage + " damage!" + damageReducDesc + coverReducDesc, false, m_gameTile.GetWorldTile().gameObject);
            }
        }

        return damage;
    }

    public virtual int CalculateDamageAmount(int damage, DamageType damageType)
    {
        ContentLordOfChaosEnemy lordOfChaosEnemy = GameHelper.GetBoss<ContentLordOfChaosEnemy>();
        if (lordOfChaosEnemy != null && lordOfChaosEnemy.m_currentChaosWarpAbility == ContentLordOfChaosEnemy.ChaosWarpAbility.NobodyCanDealDamage)
        {
            damage = 0;
        }

        GameDamageReductionKeyword damageReductionKeyword = GetDamageReductionKeyword();
        if (damageReductionKeyword != null)
        {
            damage -= damageReductionKeyword.m_damageReduction;
        }

        if (IsInCover())
        {
            if (lordOfChaosEnemy != null && lordOfChaosEnemy.m_currentChaosWarpAbility == ContentLordOfChaosEnemy.ChaosWarpAbility.CoverTakesMoreDamage)
            {
                damage += Mathf.FloorToInt((float)damage / (100.0f / (100.0f - Constants.CoverProtectionPercent)));
            }
            else
            {
                damage = Mathf.FloorToInt((float)damage / (100.0f / Constants.CoverProtectionPercent));
            }
        }

        if (damageType == DamageType.Spell && GetGameTile().GetTerrain().IsDunes())
        {
            damage = Mathf.FloorToInt((float)damage / (100.0f / Constants.CoverProtectionPercent));
        }

        GameBrittleKeyword brittleKeyword = GetBrittleKeyword();
        if (brittleKeyword != null)
        {
            damage *= 2;
        }

        if (damage < 0)
        {
            damage = 0;
        }

        return damage;
    }

    protected virtual bool ShouldRevive(out int healthSurviveAt)
    {
        bool shouldRevive = false;
        healthSurviveAt = 1;

        if (GetTeam() == Team.Player)
        {
            if (GameHelper.HasRelic<ContentDestinyRelic>())
            {
                shouldRevive = shouldRevive || GameHelper.PercentChanceRoll(33);
            }
        }

        return shouldRevive;
    }

    public virtual void Die(bool canRevive = true, DamageType damageType = DamageType.None)
    {
        if (m_isDead)
        {
            return;
        }

        m_isDead = true;

        bool willSetDead = true;

        bool shouldRevive = ShouldRevive(out int healthSurviveAt) && canRevive;

        if (shouldRevive)
        {
            m_isDead = false;
            m_curHealth = healthSurviveAt;
            UIHelper.CreateWorldElementNotification(GetName() + " resists death.", true, m_gameTile.GetWorldTile().gameObject);
            return;
        }

        if (GetTeam() == Team.Player)
        {
            if (GameHelper.HasRelic<ContentSachelOfDeceptionRelic>())
            {
                if (m_keywordHolder.GetNumVisibleKeywords() == 0)
                {
                    AddKeyword(new GameDeathKeyword(new GameGainStatsAction(this, 3, 3)), false, false);
                    m_isDead = false;
                    m_curHealth = GetMaxHealth();
                    UIHelper.CreateWorldElementNotification(GetName() + " deceives the foe and survives.", true, m_gameTile.GetWorldTile().gameObject);
                    return;
                }
            }
        }

        m_curHealth = 0;

        GameDeathKeyword deathKeyword = GetDeathKeyword();
        if (deathKeyword != null)
        {
            deathKeyword.DoAction();
        }

        TriggerDeathRelics(damageType);

        if (GetTeam() == Team.Player)
        {
            GameHelper.GetPlayer().m_controlledUnits.Remove(this);
        }

        UIHelper.CreateWorldElementNotification(GetName() + " dies.", false, m_gameTile.GetWorldTile().gameObject);

        if (GetGameTile().GetTerrain().IsIceCracked())
        {
            GetGameTile().SetTerrain(GameTerrainFactory.GetIceCrackedTerrainClone(GetGameTile().GetTerrain()));
            List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), 1);
            for (int i = 0; i < surroundingTiles.Count; i++)
            {
                if (surroundingTiles[i].GetTerrain().IsIceCracked() && surroundingTiles[i].IsOccupied() && 
                    surroundingTiles[i].GetOccupyingUnit().GetFlyingKeyword() == null && 
                    surroundingTiles[i].GetOccupyingUnit().GetWaterwalkKeyword() == null && 
                    surroundingTiles[i].GetOccupyingUnit().GetFrostwalkKeyword() == null && 
                    surroundingTiles[i].GetOccupyingUnit().GetWaterboundKeyword() == null)
                {
                    surroundingTiles[i].GetOccupyingUnit().Die();
                }
                else if (surroundingTiles[i].GetTerrain().IsIce())
                {                    
                    surroundingTiles[i].SetTerrain(GameTerrainFactory.GetIceCrackedTerrainClone(surroundingTiles[i].GetTerrain()));
                }
            }
        }

        if (GetBleedKeyword() != null)
        {
            SubtractKeyword(GetBleedKeyword());
        }

        if (m_worldUnit == Globals.m_selectedUnit)
        {
            WorldGridManager.Instance.ClearAllTilesMovementRange();
        }
        m_gameTile.GetWorldTile().RecycleUnit();
        UITooltipController.Instance.ClearTooltipStack();

        m_isDead = willSetDead;

        if (GetTeam() == Team.Player)
        {
            bool lichAuraInRange = false;
            ContentLichEnemy lichEnemy = GameHelper.GetBoss<ContentLichEnemy>();
            if (lichEnemy != null && WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(GetGameTile(), lichEnemy.GetGameTile()) <= lichEnemy.m_auraRange)
            {
                lichAuraInRange = true;
            }

            if (lichAuraInRange)
            {
                UIHelper.CreateWorldElementNotification("The Lich reanimates the fallen unit!", true, m_worldUnit.gameObject);
                GameEnemyUnit newEnemyUnit = GameUnitFactory.GetEnemyUnitClone(new ContentHuskEnemy(null), WorldController.Instance.m_gameController.m_gameOpponent);
                GetGameTile().PlaceUnit(newEnemyUnit);
                ((ContentHuskEnemy)newEnemyUnit).SetStatsEqualToUnit(this);
                newEnemyUnit.OnSummon();
                WorldController.Instance.m_gameController.m_gameOpponent.AddControlledUnit(newEnemyUnit);
            }
        }

        GameHelper.GetPlayer().InformHasDied(this, this.GetGameTile());
        GameHelper.GetOpponent().InformHasDied(this, this.GetGameTile());

        if (GetTeam() == Team.Enemy)
        {
            GameHelper.GetGameController().KillEnemy(m_incrementsKillCounter);
        }

        SetGameTile(null);
    }

    public virtual void OnOtherDie(GameUnit other, GameTile deathLocation)
    {

    }

    //Returns the amount actually healed
    public virtual int Heal(int toHeal)
    {
        ContentLichEnemy lichEnemy = GameHelper.GetBoss<ContentLichEnemy>();
        if (lichEnemy != null && WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(GetGameTile(), lichEnemy.GetGameTile()) <= lichEnemy.m_auraRange)
        {
            UIHelper.CreateWorldElementNotification("The Lich converts healing into damage!", true, m_worldUnit.gameObject);
            GetHitByAbility(toHeal);
            return 0;
        }

        int maxHealth = GetMaxHealth();

        int realHealVal = toHeal;

        if (!GameHelper.HasRelic<ContentPrimeRibRelic>())
        {
            if (m_curHealth + toHeal > maxHealth)
            {
                realHealVal = maxHealth - m_curHealth;
            }
        }

        m_curHealth += realHealVal;

        if (!GameHelper.HasRelic<ContentPrimeRibRelic>() || !(GetTeam() == Team.Player))
        {
            if (m_curHealth >= maxHealth)
            {
                m_curHealth = maxHealth;
            }
        }

        if (realHealVal > 0)
        {
            UIHelper.CreateWorldElementNotification(GetName() + " heals " + realHealVal, true, m_worldUnit.gameObject);

            if (GameHelper.HasRelic<ContentLifebringerRelic>())
            {
                GainStamina(1);
            }
        }

        if (m_curHealth == maxHealth)
        {
            if (GetBleedKeyword() != null)
            {
                m_keywordHolder.SubtractKeyword(GetBleedKeyword());
                UIHelper.CreateWorldElementNotification($"{GetName()}'s Bleed is cured!", true, m_worldUnit.gameObject);
            }
        }

        return realHealVal;
    }

    public virtual bool CanHitUnit(GameUnit other, bool checkRange = true)
    {
        if (GetTeam() == other.GetTeam()) //Can't attack your own team
        {
            return false;
        }

        if (m_isDead || other.m_isDead)
        {
            return false;
        }

        if (!HasStaminaToAttack(other))
        {
            return false;
        }

        if (checkRange && !IsInRangeOfUnit(other))
        {
            return false;
        }

        return true;
    }

    public virtual bool CanHitBuilding(GameBuildingBase building, bool checkRange = true)
    {
        if (GetTeam() == building.GetTeam()) //Can't attack your own team
        {
            return false;
        }

        if (!HasStaminaToAttack(building))
        {
            return false;
        }

        if (checkRange && !IsInRangeOfBuilding(building))
        {
            return false;
        }

        return true;
    }

    public virtual bool IsInRangeOfGameElement(GameElementBase other)
    {
        switch (other)
        {
            case GameUnit gameUnit:
                return IsInRangeOfUnit(gameUnit);
            case GameBuildingBase gameBuildingBase:
                return IsInRangeOfBuilding(gameBuildingBase);
        }
        return false;
    }

    public virtual bool IsInRangeOfUnit(GameUnit other)
    {
        if (GetRange() > 1 && other is ContentSerpentineConstructEnemy)
        {
            return false;
        }
        
        return IsInRangeOfTile(other.GetGameTile());
    }

    public virtual bool IsInRangeOfBuilding(GameBuildingBase other)
    {
        return IsInRangeOfTile(other.GetGameTile());
    }

    public virtual bool IsInRangeOfTile(GameTile tile)
    {
        int distance = WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_gameTile, tile);

        if (distance > GetRange())
        {
            return false;
        }

        return true;
    }

    public virtual void SpellCast(GameCard.Target targetType, GameTile targetTile)
    {
        GameSpellcraftKeyword spellcraftKeyword = GetSpellcraftKeyword();

        if (spellcraftKeyword == null)
        {
            return;
        }

        if (Constants.UseLocationalSpellcraft)
        {
            if (targetType != GameCard.Target.None)
            {
                if (targetTile == null)
                {
                    Debug.LogError("Spellcast that isn't target none received null target tile");
                    return;
                }

                int distanceBetween = WorldGridManager.Instance.GetPathLength(GetGameTile(), targetTile, true, false, true);
                if (distanceBetween > GameSpellcraftKeyword.m_spellcraftRange)
                {
                    return;
                }
            }
        }

        spellcraftKeyword.DoAction();
    }

    public void TriggerKnowledgeable()
    {
        GameKnowledgeableKeyword knowledgeableKeyword = GetKnowledgeableKeyword();
        if (knowledgeableKeyword != null)
        {
            knowledgeableKeyword.DoAction();

            if (GameHelper.HasRelic<ContentForbiddenKnowledge>())
            {
                GameHelper.GetPlayer().AddEnergy(1);
            }
        }
    }

    public virtual bool HasStaminaToAttack(GameElementBase targetToAttack)
    {
        if (m_curStamina < GetStaminaToAttack(targetToAttack))
        {
            return false;
        }

        return true;
    }

    public virtual void SetStaminaToAttack(int newVal)
    {
        m_staminaToAttack = newVal;
    }

    public void AddStaminaRegen(int toAdd, bool permanent)
    {
        if (m_gameTile != null)
        {
            UIHelper.CreateWorldElementNotification(GetName() + " gains +" + toAdd + " stamina regen.", true, m_gameTile.GetWorldTile().gameObject);
        }

        if (permanent)
        {
            m_permStaminaRegen += toAdd;

            if (!HasCustomName())
            {
                SetCustomName();
            }
        }
        else
        {
            m_staminaRegen += toAdd;
        }
    }

    public void AddMaxStamina(int toAdd, bool permanent)
    {
        if (toAdd == 0)
        {
            return;
        }

        if (GetMaxStamina() >= Constants.MaxTotalStamina)
        {
            return;
        }

        if (GameHelper.IsUnitInWorld(this))
        {
            if (GameHelper.HasRelic<ContentSecretTiesRelic>() && GetTypeline() == Typeline.Creation)
            {
                List<GameTile> adjacentTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_gameTile, 3);
                for (int i = 0; i < adjacentTiles.Count; i++)
                {
                    if (adjacentTiles[i].IsOccupied() &&
                        adjacentTiles[i].GetOccupyingUnit().GetTeam() == Team.Player &&
                        !adjacentTiles[i].GetOccupyingUnit().m_isDead &&
                        adjacentTiles[i].GetOccupyingUnit().GetTypeline() == Typeline.Monster)
                    {
                        adjacentTiles[i].GetOccupyingUnit().AddKeyword(new GameVictoriousKeyword(new GameGainStatsAction(adjacentTiles[i].GetOccupyingUnit(), 3, 3)), false, false);
                    }
                }
            }
        }

        if (permanent)
        {
            m_permMaxStamina += toAdd;

            if (!HasCustomName())
            {
                SetCustomName();
            }
        }
        else
        {
            m_maxStamina += toAdd;
        }
    }

    public void RemoveMaxStamina(int toRemove, bool permanent)
    {
        if (toRemove == 0)
        {
            return;
        }

        if (GetMaxStamina() == 0)
        {
            return;
        }

        if (permanent)
        {
            m_permMaxStamina = Mathf.Max(0, GetMaxStamina() - toRemove);

            if (!HasCustomName())
            {
                SetCustomName();
            }
        }
        else
        {
            m_maxStamina = Mathf.Max(0, GetMaxStamina() - toRemove);
        }
    }

    public int GetSightRange()
    {
        int toReturn = m_sightRange;

        if (GetTeam() == Team.Player && GameHelper.HasRelic<ContentTheGreatestGiftRelic>())
        {
            toReturn += 1;
        }

        if (GetTeam() == Team.Player && GameHelper.HasRelic<ContentFadingLightRelic>())
        {
            toReturn -= 2;
        }

        if (GetTeam() == Team.Player && GameHelper.HasRelic<ContentBeaconOfSanityRelic>())
        {
            toReturn -= 1;
        }

        ContentLordOfShadowsEnemy lordOfShadowsEnemy = GameHelper.GetBoss<ContentLordOfShadowsEnemy>();
        if (lordOfShadowsEnemy != null)
        {
            toReturn -= lordOfShadowsEnemy.m_visionReductionAmount;
        }

        if (toReturn < 0)
        {
            toReturn = 0;
        }

        return toReturn;
    }

    public virtual int HitUnit(GameUnit other, int damageAmount, bool spendStamina = true, bool isThornsAttack = false, bool canCleave = true)
    {
        GameHelper.GetGameController().AddIntermissionLock();

        AudioHelper.PlaySFX(GetAttackSFX());
        
        if (spendStamina)
        {
            SpendStamina(GetStaminaToAttack(other));
        }

        List<GameTile> tilesToCleave = new List<GameTile>();
        if (canCleave && GetRangeKeyword() == null && GetCleaveKeyword() != null)
        {
            tilesToCleave = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), 1);
        }

        int damageTaken = other.GetHitByUnit(damageAmount, this, !isThornsAttack);

        if (!isThornsAttack)
        {
            GameMomentumKeyword momentumKeyword = GetMomentumKeyword();

            if (momentumKeyword != null)
            {
                momentumKeyword.DoAction(other);

                //If the player has Bestial Wrath relic, repeat the action
                if (GetTypeline() == Typeline.Monster && GetTeam() == Team.Player)
                {
                    if (GameHelper.HasRelic<ContentBestialWrathRelic>())
                    {
                        momentumKeyword.DoAction();
                    }
                }
            }
        }

        if (other.m_isDead && !m_isDead)
        {
            GameVictoriousKeyword victoriousKeyword = GetVictoriousKeyword();

            if (victoriousKeyword != null)
            {
                victoriousKeyword.DoAction();

                //If the player has Bestial Wrath relic, repeat the action
                if (GetTypeline() == Typeline.Monster && GetTeam() == Team.Player)
                {
                    if (GameHelper.HasRelic<ContentBestialWrathRelic>())
                    {
                        victoriousKeyword.DoAction();
                    }
                }
            }

            UIHelper.ReselectUnit();
        }

        if (canCleave && GetRangeKeyword() == null && GetCleaveKeyword() != null)
        {
            for (int i = 0; i < tilesToCleave.Count; i++)
            {
                if (tilesToCleave[i].IsOccupied() && tilesToCleave[i].GetOccupyingUnit().GetTeam() != GetTeam())
                {
                    damageTaken += HitUnit(tilesToCleave[i].GetOccupyingUnit(), GetDamageToDealTo(tilesToCleave[i].GetOccupyingUnit()), spendStamina: false, canCleave: false);
                }
                else if (tilesToCleave[i].HasBuilding() && tilesToCleave[i].GetBuilding().GetTeam() != GetTeam())
                {
                    damageTaken += HitBuilding(tilesToCleave[i].GetBuilding(), spendStamina: false, canCleave: false);
                }
            }
        }

        if (GetFadeKeyword() != null)
        {
            GetFadeKeyword().m_isActive = false;
        }

        if (GameHelper.IsInGame())
        {
            GameHelper.GetGameController().RemoveIntermissionLock();
        }

        return damageTaken;
    }

    public virtual int HitBuilding(GameBuildingBase other, bool spendStamina = true, bool canCleave = true)
    {
        GameHelper.GetGameController().AddIntermissionLock();

        if (spendStamina)
        {
            SpendStamina(GetStaminaToAttack(other));
        }

        List<GameTile> tilesToCleave = new List<GameTile>();
        if (canCleave && GetRangeKeyword() == null && GetCleaveKeyword() != null)
        {
            tilesToCleave = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), 1);
        }

        int damageTaken = other.GetHit(GetDamageToDealTo(other));

        GameMomentumKeyword momentumKeyword = GetMomentumKeyword();

        if (momentumKeyword != null)
        {
            momentumKeyword.DoAction(null);

            //Repeat the action if the player has the Bestial Wrath Relic
            if (GetTypeline() == Typeline.Monster && GetTeam() == Team.Player)
            {
                if (GameHelper.HasRelic<ContentBestialWrathRelic>())
                {
                    momentumKeyword.DoAction();
                }
            }
        }

        if (other.m_isDestroyed)
        {
            GameVictoriousKeyword victoriousKeyword = GetVictoriousKeyword();

            if (victoriousKeyword != null)
            {
                victoriousKeyword.DoAction();

                //Repeat the action if the player has the Bestial Wrath Relic
                if (GetTypeline() == Typeline.Monster && GetTeam() == Team.Player)
                {
                    if (GameHelper.HasRelic<ContentBestialWrathRelic>())
                    {
                        victoriousKeyword.DoAction();
                    }
                }
            }
        }

        if (canCleave && GetRangeKeyword() == null && GetCleaveKeyword() != null)
        {
            for (int i = 0; i < tilesToCleave.Count; i++)
            {
                if (tilesToCleave[i].IsOccupied() && tilesToCleave[i].GetOccupyingUnit().GetTeam() != GetTeam())
                {
                    damageTaken += HitUnit(tilesToCleave[i].GetOccupyingUnit(), GetDamageToDealTo(tilesToCleave[i].GetOccupyingUnit()), spendStamina: false, canCleave: false);
                }
                else if (tilesToCleave[i].HasBuilding() && tilesToCleave[i].GetBuilding().GetTeam() != GetTeam())
                {
                    damageTaken += HitBuilding(tilesToCleave[i].GetBuilding(), spendStamina: false, canCleave: false);
                }
            }
        }

        if (GetFadeKeyword() != null)
        {
            GetFadeKeyword().m_isActive = false;
        }

        if (GameHelper.IsInGame())
        {
            GameHelper.GetGameController().RemoveIntermissionLock();
        }

        return damageTaken;
    }

    public virtual int GetDamageToDealTo(GameUnit target)
    {
        return GetPower();
    }

    public virtual int GetDamageToDealTo(GameBuildingBase target)
    {
        return GetPower();
    }

    public Team GetTeam()
    {
        return m_team;
    }

    public void SetTeam(Team newTeam)
    {
        m_team = newTeam;
    }

    public int GetCurStamina()
    {
        return m_curStamina;
    }

    public virtual int GetStaminaToAttack(GameElementBase targetToAttack)
    {
        int staminaToAttack = m_staminaToAttack;

        if (GetTeam() == Team.Player)
        {
            if (GameHelper.HasRelic<ContentUrbanTacticsRelic>())
            {
                staminaToAttack--;
            }

            if (GameHelper.HasRelic<ContentNamelessFlaskRelic>())
            {
                if (GetCurStamina() == 1)
                {
                    staminaToAttack = 1;
                }
            }

            if (GameHelper.HasRelic<ContentAncientRitualRelic>())
            {
                if (GetTypeline() == Typeline.Monster)
                {
                    staminaToAttack--;
                }
            }
        }

        ContentLordOfChaosEnemy lordOfChaosEnemy = GameHelper.GetBoss<ContentLordOfChaosEnemy>();
        if (lordOfChaosEnemy != null && lordOfChaosEnemy.m_currentChaosWarpAbility == ContentLordOfChaosEnemy.ChaosWarpAbility.StaminaCostAttackDecreaseMoveCostIncrease)
        {
            staminaToAttack--;
        }

        return Mathf.Max(1, staminaToAttack);
    }

    public void FillStamina()
    {
        GainStamina(GetMaxStamina());
    }

    public void EmptyStamina()
    {
        SpendStamina(GetMaxStamina());
    }

    public virtual void GainStamina(int toGain, bool isRegen = false)
    {
        int staminaGained = toGain;

        m_curStamina += toGain;

        if (m_curStamina > GetMaxStamina())
        {
            staminaGained = staminaGained - (m_curStamina - GetMaxStamina());

            m_curStamina = GetMaxStamina();
        }

        if (GetTeam() == Team.Player && staminaGained > 0 && !isRegen)
        {
            UIHelper.CreateWorldElementNotification(GetName() + " gains " + staminaGained + " Stamina.", true, m_gameTile.GetWorldTile().gameObject);
        }

        UIHelper.ReselectUnit();
    }

    public void DrainStamina(int toDrain)
    {
        int staminaToDrain = toDrain;

        if (staminaToDrain > m_curStamina)
        {
            staminaToDrain = m_curStamina;
        }

        m_curStamina -= staminaToDrain;

        UIHelper.CreateWorldElementNotification(GetName() + " loses " + staminaToDrain + " Stamina.", true, m_gameTile.GetWorldTile().gameObject);
    }

    public void Reset()
    {
        m_isDead = false;
    }

    public void AddKeyword(GameKeywordBase newKeyword, bool isPermanent, bool canChangeName)
    {
        if (canChangeName)
        {
            UIHelper.CreateWorldElementNotification(GetName() + " gains " + newKeyword.GetName() + ".", true, m_gameTile.GetWorldTile().gameObject);
        }

        newKeyword.m_isPermanent = isPermanent;

        m_keywordHolder.AddKeyword(newKeyword);

        if (!HasCustomName() && canChangeName)
        {
            SetCustomName();
        }
    }

    public void ResetKeywords(bool ignorePerm)
    {
        m_keywordHolder.RemoveAllKeywords(ignorePerm);

        m_staminaToAttack = 2;
    }

    public void RemoveKeyword(GameKeywordBase toRemove)
    {
        m_keywordHolder.RemoveKeyword(toRemove);
    }

    public void SubtractKeyword(GameKeywordBase toRemove)
    {
        m_keywordHolder.SubtractKeyword(toRemove);
    }

    public virtual GameThornsKeyword GetThornsKeyword()
    {
        //Set the return keyword to a blank keyword
        GameThornsKeyword toReturn = new GameThornsKeyword(0);

        //Get the keyword from the holder, if it's not null, add it to the return keyword.
        GameThornsKeyword holderKeyword = m_keywordHolder.GetKeyword<GameThornsKeyword>();
        if (holderKeyword != null)
        {
            toReturn.AddKeyword((GameThornsKeyword)GameKeywordFactory.GetKeywordClone(holderKeyword, this));
        }

        //Check relics and other effects to see if anything needs to be added to the return keyword
        if (GameHelper.HasRelic<ContentThornsOfRayRelic>() && GetTeam() == Team.Player)
        {
            toReturn.AddKeyword(new GameThornsKeyword(2));
        }

        ContentLordOfChaosEnemy lordOfChaosEnemy = GameHelper.GetBoss<ContentLordOfChaosEnemy>();
        if (lordOfChaosEnemy != null && lordOfChaosEnemy.m_currentChaosWarpAbility == ContentLordOfChaosEnemy.ChaosWarpAbility.AllUnitsHaveThorns)
        {
            toReturn.AddKeyword(new GameThornsKeyword(10));
        }

        //If the return keyword is still blank, set it to null
        if (toReturn.m_thornsDamage == 0)
        {
            toReturn = null;
        }

        //Return it
        return toReturn;
    }

    public virtual GameCleaveKeyword GetCleaveKeyword()
    {
        return m_keywordHolder.GetKeyword<GameCleaveKeyword>();
    }

    public virtual GameFadeKeyword GetFadeKeyword(bool getInactiveFade = false)
    {
        GameFadeKeyword fadeKeyword = m_keywordHolder.GetKeyword<GameFadeKeyword>();
        
        if (fadeKeyword != null && !getInactiveFade && !fadeKeyword.m_isActive)
        {
            return null;
        }

        return m_keywordHolder.GetKeyword<GameFadeKeyword>();
    }

    public virtual GameFlyingKeyword GetFlyingKeyword()
    {
        return m_keywordHolder.GetKeyword<GameFlyingKeyword>();
    }

    public virtual GameMountainwalkKeyword GetMountainwalkKeyword()
    {
        if (GameHelper.HasRelic<ContentTokenOfFriendshipRelic>() && GetTeam() == Team.Player && GetTypeline() == Typeline.Humanoid)
        {
            return new GameMountainwalkKeyword();
        }

        return m_keywordHolder.GetKeyword<GameMountainwalkKeyword>();
    }

    public virtual GameWaterwalkKeyword GetWaterwalkKeyword()
    {
        if (GameHelper.HasRelic<ContentSecretOfTheDeepRelic>() && GetTeam() == Team.Player && GetTypeline() == Typeline.Humanoid)
        {
            return new GameWaterwalkKeyword();
        }

        return m_keywordHolder.GetKeyword<GameWaterwalkKeyword>();
    }

    public virtual GameDuneswalkKeyword GetDuneswalkKeyword()
    {
        return m_keywordHolder.GetKeyword<GameDuneswalkKeyword>();
    }

    public virtual GameFrostwalkKeyword GetFrostwalkKeyword()
    {
        return m_keywordHolder.GetKeyword<GameFrostwalkKeyword>();
    }

    public virtual GameWaterboundKeyword GetWaterboundKeyword()
    {
        return m_keywordHolder.GetKeyword<GameWaterboundKeyword>();
    }

    public virtual GameForestwalkKeyword GetForestwalkKeyword()
    {
        return m_keywordHolder.GetKeyword<GameForestwalkKeyword>();
    }

    public virtual GameRootedKeyword GetRootedKeyword()
    {
        return m_keywordHolder.GetKeyword<GameRootedKeyword>();
    }

    public virtual GameTauntKeyword GetTauntKeyword()
    {
        return m_keywordHolder.GetKeyword<GameTauntKeyword>();
    }

    public virtual GameLavawalkKeyword GetLavawalkKeyword()
    {
        return m_keywordHolder.GetKeyword<GameLavawalkKeyword>();
    }

    public virtual GameRegenerateKeyword GetRegenerateKeyword()
    {
        //Set the return keyword to a blank keyword
        GameRegenerateKeyword toReturn = new GameRegenerateKeyword(0);

        //Get the keyword from the holder, if it's not null, add it to the return keyword.
        GameRegenerateKeyword holderKeyword = m_keywordHolder.GetKeyword<GameRegenerateKeyword>();
        if (holderKeyword != null)
        {
            toReturn.AddKeyword((GameRegenerateKeyword)GameKeywordFactory.GetKeywordClone(holderKeyword, this));
        }

        //Check relics and other effects to see if anything needs to be added to the return keyword
        if (GameHelper.HasRelic<ContentPlagueMaskRelic>() && GetTeam() == Team.Player && GetTypeline() == Typeline.Monster)
        {
            toReturn.AddKeyword(new GameRegenerateKeyword(5));
        }
        if (GameHelper.IsUnitInWorld(this))
        {
            if (GetTeam() == Team.Player)
            {
                //Check relics and other effects to see if anything needs to be added to the return keyword
                if (GameHelper.HasRelic<ContentHealthFlaskRelic>() &&
                m_curHealth <= Mathf.FloorToInt((float)(GetMaxHealth() / 2.0f)))
                {
                    toReturn.AddKeyword(new GameRegenerateKeyword(5));
                }

                if (GameHelper.HasRelic<ContentCallOfTheSeaRelic>())
                {
                    List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), 1, 0);

                    bool isNearWater = false;
                    for (int i = 0; i < surroundingTiles.Count; i++)
                    {
                        if (surroundingTiles[i].GetTerrain().IsWater())
                        {
                            isNearWater = true;
                            break;
                        }
                    }

                    if (isNearWater)
                    {
                        toReturn.AddKeyword(new GameRegenerateKeyword(10));
                    }
                }
            }
        }

        //If the return keyword is still blank, set it to null
        if (toReturn.m_regenVal == 0)
        {
            toReturn = null;
        }

        //Return it
        return toReturn;
    }

    public virtual GameDamageShieldKeyword GetDamageShieldKeyword()
    {
        return m_keywordHolder.GetKeyword<GameDamageShieldKeyword>();
    }

    public virtual GameBleedKeyword GetBleedKeyword()
    {
        return m_keywordHolder.GetKeyword<GameBleedKeyword>();
    }

    public virtual GameBrittleKeyword GetBrittleKeyword()
    {
        return m_keywordHolder.GetKeyword<GameBrittleKeyword>();
    }

    public virtual GameEnrageKeyword GetEnrageKeyword()
    {
        return m_keywordHolder.GetKeyword<GameEnrageKeyword>();
    }

    public virtual GameMomentumKeyword GetMomentumKeyword()
    {
        //Set the return keyword to a blank keyword
        GameMomentumKeyword toReturn = new GameMomentumKeyword(null);

        //Get the keyword from the holder, if it's not null, add it to the return keyword.
        GameMomentumKeyword holderKeyword = m_keywordHolder.GetKeyword<GameMomentumKeyword>();
        if (holderKeyword != null)
        {
            toReturn.AddKeyword((GameMomentumKeyword)GameKeywordFactory.GetKeywordClone(holderKeyword, this));
        }

        //Check relics and other effects to see if anything needs to be added to the return keyword
        if (GameHelper.HasRelic<ContentChargingRingRelic>() && GetTeam() == Team.Player && GetTypeline() == Typeline.Monster)
        {
            toReturn.AddKeyword(new GameMomentumKeyword(new GameGainStatsAction(this, 1, 1)));
        }

        //If the return keyword is still blank, set it to null
        if (toReturn.IsEmpty())
        {
            toReturn = null;
        }

        //Return it
        return toReturn;
    }

    public virtual GameVictoriousKeyword GetVictoriousKeyword()
    {
        //Set the return keyword to a blank keyword
        GameVictoriousKeyword toReturn = new GameVictoriousKeyword(null);

        //Get the keyword from the holder, if it's not null, add it to the return keyword.
        GameVictoriousKeyword holderKeyword = m_keywordHolder.GetKeyword<GameVictoriousKeyword>();
        if (holderKeyword != null)
        {
            toReturn.AddKeyword((GameVictoriousKeyword)GameKeywordFactory.GetKeywordClone(holderKeyword, this));
        }

        //Check relics and other effects to see if anything needs to be added to the return keyword
        if (GameHelper.HasRelic<ContentBeadofJoyRelic>() && GetTeam() == Team.Player)
        {
            toReturn.AddKeyword(new GameVictoriousKeyword(new GameGainStatsAction(this, 1, 1)));
        }

        //If the return keyword is still blank, set it to null
        if (toReturn.IsEmpty())
        {
            toReturn = null;
        }

        //Return it
        return toReturn;
    }

    public virtual GameKnowledgeableKeyword GetKnowledgeableKeyword()
    {
        return m_keywordHolder.GetKeyword<GameKnowledgeableKeyword>();
    }

    public virtual GameSpellcraftKeyword GetSpellcraftKeyword()
    {
        return m_keywordHolder.GetKeyword<GameSpellcraftKeyword>();
    }

    public virtual GameRangeKeyword GetRangeKeyword()
    {
        //Set the return keyword to a blank keyword
        GameRangeKeyword toReturn = new GameRangeKeyword(0);

        //Get the keyword from the holder, if it's not null, add it to the return keyword.
        GameRangeKeyword holderKeyword = m_keywordHolder.GetKeyword<GameRangeKeyword>();
        if (holderKeyword != null)
        {
            toReturn.AddKeyword((GameRangeKeyword)GameKeywordFactory.GetKeywordClone(holderKeyword, this));
        }

        //Check relics and other effects to see if anything needs to be added to the return keyword
        if (GameHelper.HasRelic<ContentAdvancedWeaponryRelic>() && GetTeam() == Team.Player && toReturn.m_range >= 2)
        {
            toReturn.AddKeyword(new GameRangeKeyword(1));
        }

        if (GameHelper.IsUnitInWorld(this))
        {
            int terrainRange = m_gameTile.GetTerrain().m_rangeModifier;

            if (terrainRange > 0)
            {
                if (toReturn != null && toReturn.m_range > 0)
                {
                    GameRangeKeyword terrainRangeKeyword = new GameRangeKeyword(terrainRange);
                    terrainRangeKeyword.m_buffedByTerrain = true;

                    toReturn.AddKeyword(terrainRangeKeyword);
                }
            }
        }

        //If the return keyword is still blank, set it to null
        if (toReturn.m_range == 0)
        {
            toReturn = null;
        }

        //Return it
        return toReturn;
    }

    public virtual GameDeathKeyword GetDeathKeyword()
    {
        return m_keywordHolder.GetKeyword<GameDeathKeyword>();
    }

    public virtual GameSummonKeyword GetSummonKeyword()
    {
        return m_keywordHolder.GetKeyword<GameSummonKeyword>();
    }

    public virtual GameDamageReductionKeyword GetDamageReductionKeyword()
    {
        //Set the return keyword to a blank keyword
        GameDamageReductionKeyword toReturn = new GameDamageReductionKeyword(0);

        //Get the keyword from the holder, if it's not null, add it to the return keyword.
        GameDamageReductionKeyword holderKeyword = m_keywordHolder.GetKeyword<GameDamageReductionKeyword>();
        if (holderKeyword != null)
        {
            toReturn.AddKeyword((GameDamageReductionKeyword)GameKeywordFactory.GetKeywordClone(holderKeyword, this));
        }

        if (GameHelper.IsUnitInWorld(this))
        {
            //Check relics and other effects to see if anything needs to be added to the return keyword
            if (GameHelper.HasRelic<ContentEverflowingCanteenRelic>() && GetTeam() == Team.Player)
            {
                List<GameTile> adjacentTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_gameTile, 1);
                for (int i = 0; i < adjacentTiles.Count; i++)
                {
                    if (adjacentTiles[i].GetTerrain().IsWater())
                    {
                        toReturn.AddKeyword(new GameDamageReductionKeyword(2));
                        break;
                    }
                }
            }

            if (GetTeam() == Team.Enemy)
            {
                if (!(this is ContentImmortalShieldEnemy))
                {
                    ContentImmortalShieldEnemy immortalBannerEnemy = GameHelper.GetBoss<ContentImmortalShieldEnemy>();
                    if (immortalBannerEnemy != null && WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(GetGameTile(), immortalBannerEnemy.GetGameTile()) <= immortalBannerEnemy.m_auraRange)
                    {
                        toReturn.AddKeyword(new GameDamageReductionKeyword(3));
                    }
                }
            }
        }

        //If the return keyword is still blank, set it to null
        if (toReturn.m_damageReduction == 0)
        {
            toReturn = null;
        }

        //Return it
        return toReturn;
    }

    public bool IsInCover()
    {
        if (m_gameTile == null)
        {
            return false;
        }

        if (!GameHelper.IsUnitInWorld(this))
        {
            return false;
        }

        if (GetFlyingKeyword() != null)
        {
            return false;
        }

        if (m_gameTile.HasBuilding())
        {
            return true;
        }

        return m_gameTile.GetTerrain().GetCoverType() == GameTerrainBase.CoverType.Cover;
    }

    public bool IsInDunes()
    {
        if (m_gameTile == null)
        {
            return false;
        }

        if (!GameHelper.IsUnitInWorld(this))
        {
            return false;
        }

        if (GetFlyingKeyword() != null)
        {
            return false;
        }

        return m_gameTile.GetTerrain().IsDunes();
    }

    public GameKeywordHolder GetKeywordHolderForRead()
    {
        return m_keywordHolder;
    }

    public virtual int GetRange()
    {
        bool lordOfChaosRangeSwapActive = false;
        if (GameHelper.IsInGame())
        {
            ContentLordOfChaosEnemy lordOfChaosEnemy = GameHelper.GetBoss<ContentLordOfChaosEnemy>();
            if (lordOfChaosEnemy != null && lordOfChaosEnemy.m_currentChaosWarpAbility == ContentLordOfChaosEnemy.ChaosWarpAbility.RangedNotRangedSwap)
            {
                lordOfChaosRangeSwapActive = true;
            }
        }

        GameRangeKeyword rangeKeyword = GetRangeKeyword();
        if (rangeKeyword != null)
        {
            if (lordOfChaosRangeSwapActive)
            {
                return 1;
            }
            
            return rangeKeyword.m_range;
        }

        if (lordOfChaosRangeSwapActive)
        {
            return 2;
        }

        return 1;
    }

    public virtual int GetPower()
    {
        int toReturn = m_power;
        toReturn += m_permPower;

        if (GetTeam() == Team.Player && GameHelper.IsInGame())
        {
            if (GameHelper.HasRelic<ContentWolvenFangRelic>())
            {
                toReturn += 1;
            }

            if (GameHelper.HasRelic<ContentSigilOfTheSwordsmanRelic>() && GetTypeline() == Typeline.Humanoid)
            {
                toReturn += 8;
            }

            if (GameHelper.HasRelic<ContentMightOfSugoRelic>())
            {
                toReturn += 15;
            }

            if (GameHelper.IsUnitInWorld(this))
            {
                if (GameHelper.HasRelic<ContentNectarOfTheSeaGodRelic>())
                {
                    List<GameTile> adjacentTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_gameTile, 1);
                    for (int i = 0; i < adjacentTiles.Count; i++)
                    {
                        if (adjacentTiles[i].GetTerrain().IsWater())
                        {
                            toReturn += 3;
                            break;
                        }
                    }
                }

                if (GameHelper.HasRelic<ContentSecretsOfNatureRelic>() && m_gameTile.GetTerrain().IsForest())
                {
                    toReturn += 8;
                }

                if (GameHelper.HasRelic<ContentBondOfFamilyRelic>())
                {
                    if (GetGameTile() != null)
                    {
                        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), 3);

                        for (int i = 0; i < surroundingTiles.Count; i++)
                        {
                            if (surroundingTiles[i].IsOccupied() && !surroundingTiles[i].GetOccupyingUnit().m_isDead &&
                                surroundingTiles[i].GetOccupyingUnit().GetTeam() == Team.Player)
                            {
                                if (surroundingTiles[i].GetOccupyingUnit().GetTypeline() == Typeline.Humanoid)
                                {
                                    toReturn += 4;
                                }
                                else if (surroundingTiles[i].GetOccupyingUnit().GetTypeline() == Typeline.Creation)
                                {
                                    toReturn -= 4;
                                }
                            }
                        }
                    }
                }
            }

            if (GameHelper.HasRelic<ContentAncientEvilRelic>() && GetTypeline() == Typeline.Monster)
            {
                ContentAncientEvilRelic evilRelic = (ContentAncientEvilRelic)(GameHelper.GetPlayer().GetRelics().GetRelic<ContentAncientEvilRelic>());
                if (evilRelic.IsTransformed())
                {
                    toReturn += 10;
                }
            }

            if (GameHelper.HasRelic<ContentLegendaryFragmentRelic>())
            {
                toReturn -= 2;
            }

            if (GameHelper.HasRelic<ContentTalonOfTheMeradominRelic>())
            {
                toReturn += 5;
            }

            if (GetRange() > 1)
            {
                toReturn += GameHelper.GetPlayer().m_fletchingPowerIncrease;
            }
        }

        if (GameHelper.IsUnitInWorld(this))
        {
            if (GetTeam() == Team.Enemy)
            {
                if (!(this is ContentImmortalShieldEnemy))
                {
                    ContentImmortalShieldEnemy immortalBannerEnemy = GameHelper.GetBoss<ContentImmortalShieldEnemy>();
                    if (immortalBannerEnemy != null && WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(GetGameTile(), immortalBannerEnemy.GetGameTile()) <= immortalBannerEnemy.m_auraRange)
                    {
                        toReturn += 5;
                    }
                }
            }
        }

        if (toReturn < 0)
        {
            toReturn = 0;
        }

        return toReturn;
    }

    public Typeline GetTypeline()
    {
        if (GetTeam() == Team.Player)
        {
            if (GameHelper.HasRelic<ContentAncientEvilRelic>())
            {
                ContentAncientEvilRelic evilRelic = (ContentAncientEvilRelic)(GameHelper.GetPlayer().GetRelics().GetRelic<ContentAncientEvilRelic>());
                if (evilRelic.IsTransformed())
                {
                    return Typeline.Monster;
                }
            }
        }

        return m_typeline;
    }

    public int GetCurHealth()
    {
        return m_curHealth;
    }

    public bool UsesBigTooltip()
    {
        return m_usesBigTooltip;
    }

    public virtual int GetMaxHealth()
    {
        int toReturn = m_maxHealth;
        toReturn += m_permMaxHealth;

        if (GetTeam() == Team.Player)
        {
            if (GameHelper.HasRelic<ContentOrbOfHealthRelic>())
            {
                toReturn += 6;
            }

            if (GameHelper.HasRelic<ContentMightOfSugoRelic>())
            {
                toReturn += 15;
            }

            if (GameHelper.HasRelic<ContentAncientEvilRelic>() && GetTypeline() == Typeline.Monster)
            {
                ContentAncientEvilRelic evilRelic = (ContentAncientEvilRelic)(GameHelper.GetPlayer().GetRelics().GetRelic<ContentAncientEvilRelic>());
                if (evilRelic.IsTransformed())
                {
                    toReturn += 10;
                }
            }

            if (GameHelper.IsUnitInWorld(this))
            {
                if (GameHelper.HasRelic<ContentNectarOfTheSeaGodRelic>())
                {
                    List<GameTile> adjacentTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_gameTile, 1);
                    for (int i = 0; i < adjacentTiles.Count; i++)
                    {
                        if (adjacentTiles[i].GetTerrain().IsWater())
                        {
                            toReturn += 3;
                            break;
                        }
                    }
                }

                if (GameHelper.HasRelic<ContentSecretsOfNatureRelic>() && m_gameTile.GetTerrain().IsForest())
                {
                    toReturn += 8;
                }
            }
        }

        if (!GameHelper.HasRelic<ContentPrimeRibRelic>() || !(GetTeam() == Team.Player))
        {
            if (m_curHealth >= toReturn)
            {
                m_curHealth = toReturn;
            }
        }

        return toReturn;
    }

    public int GetMaxStamina()
    {
        int toReturn = m_maxStamina;
        toReturn += m_permMaxStamina;

        if (GetTeam() == Team.Player)
        {
            if (GameHelper.HasRelic<ContentHourglassOfSpeedRelic>())
            {
                toReturn += 1;
            }

            if (GameHelper.HasRelic<ContentBeaconOfSanityRelic>())
            {
                toReturn += 3;
            }
        }

        if (toReturn > 12)
        {
            toReturn = 12;
        }

        return toReturn;
    }

    public virtual int GetStaminaRegen()
    {
        int toReturn = m_staminaRegen;
        toReturn += m_permStaminaRegen;

        if (GetTeam() == Team.Player)
        {
            if (GameHelper.HasRelic<ContentLegendaryFragmentRelic>())
            {
                toReturn += 1;
            }

            if (GameHelper.HasRelic<ContentUrbanTacticsRelic>())
            {
                toReturn -= 2;
            }

            if (GameHelper.HasRelic<ContentCarapaceOfTutuiun>())
            {
                toReturn -= 1;
            }

            if (GameHelper.HasRelic<ContentTauntingPipeRelic>() && GetTypeline() == Typeline.Monster)
            {
                toReturn -= 1;
            }

            if (GameHelper.HasRelic<ContentGrandPactRelic>())
            {
                if (GameHelper.HasAllTypelines())
                {
                    toReturn += 1;
                }
            }

            if (GameHelper.HasRelic<ContentIotalRelic>())
            {
                int toAdd = Mathf.FloorToInt((float)(GameHelper.GetPlayer().GetGold()) / 150.0f);

                toReturn += toAdd;
            }

            if (m_rarity == GameRarity.Starter)
            {
                if (GameHelper.HasRelic<ContentTraditionalMethodsRelic>())
                {
                    toReturn += 1;
                }
            }
            
            if (m_typeline == Typeline.Monster)
            {
                if (GameHelper.HasRelic<ContentLegacyOfMonstersRelic>())
                {
                    toReturn += 1;
                }
            }
        }

        if (GameHelper.HasRelic<ContentSecretSoupRelic>())
        {
            toReturn += 1;
        }

        return toReturn;
    }

    public Vector3 GetWorldTilePositionAdjustment()
    {
        return m_worldTilePositionAdjustment;
    }

    public Color GetColor()
    {
        if (GetTeam() == Team.Player)
        {
            return UIHelper.m_playerColor;
        }
        else if (GetTeam() == Team.Enemy)
        {
            return UIHelper.m_enemyColor;
        }

        return Color.white;
    }

    public bool CanMoveTo(GameTile tile)
    {
        if (tile.IsOccupied())
        {
            return false;
        }

        if (!tile.IsPassable(this, false))
        {
            return false;
        }

        if (WorldGridManager.Instance.GetPathLength(m_gameTile, tile, false, false, false) > m_curStamina)
        {
            return false;
        }
        
        return true;
    }

    public void MoveTo(GameTile tile, bool spendStamina = true)
    {
        if (tile == m_gameTile)
        {
            return;
        }

        GameTile startingTile = m_gameTile;

        int pathCost = WorldGridManager.Instance.GetPathLength(m_gameTile, tile, false, false, false);
        List<GameTile> path = WorldGridManager.Instance.CalculateAStarPath(m_gameTile, tile, false, false, false, false);

        m_gameTile.ClearUnit();
        tile.PlaceUnit(this);

        if (spendStamina)
        {
            SpendStamina(pathCost);
        }

        if (GetFrostwalkKeyword() != null)
        {
            for (int i = 0; i < path.Count; i++)
            {
                if (path[i].GetTerrain().IsWater())
                {
                    path[i].SetTerrain(new ContentIceTerrain(), true);
                }
            }
        }

        if (GetTeam() == Team.Player)
        {
            if (GetGameTile().IsStorm())
            {
                GetHitByAbility(Constants.WinterStormDamage);
            }

            List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), GetSightRange(), 0);
            for (int i = 0; i < surroundingTiles.Count; i++)
            {
                List<GameTile> neighbourTiles = WorldGridManager.Instance.GetSurroundingGameTiles(surroundingTiles[i], Constants.WinterStormVisionRange, 0);
                bool keepRevealed = neighbourTiles.Any(t => (t.IsOccupied() && t.GetOccupyingUnit().GetTeam() == Team.Player) ||
                                                            (t.HasBuilding() && t.GetBuilding().GetTeam() == Team.Player) ||
                                                            !t.IsStorm());
                if (!keepRevealed)
                {
                    surroundingTiles[i].m_isFog = true;
                    surroundingTiles[i].m_isSoftFog = true;
                }
            }
        }

        GameHelper.GetPlayer().InformHasMoved(this, startingTile, GetGameTile(), path);
        GameHelper.GetOpponent().InformHasMoved(this, startingTile, GetGameTile(), path);
    }

    public GameTile GetMoveTowardsDestination(GameTile tile, int staminaToUse, bool ignoreTerrainDifference = false, bool letPassEnemies = true)
    {
        if (tile == m_gameTile || staminaToUse <= 0)
        {
            return m_gameTile;
        }

        List<GameTile> pathToTile = WorldGridManager.Instance.CalculateAStarPath(m_gameTile, tile, ignoreTerrainDifference, true, letPassEnemies, true);

        if (pathToTile == null || pathToTile.Count == 0)
        {
            return m_gameTile;
        }

        int staminaSpent = 0;
        GameTile destinationTile = m_gameTile;

        List<GameTile> path = new List<GameTile>();
        for (int i = 0; i < pathToTile.Count; i++)
        {
            if (pathToTile[i] == m_gameTile)
                continue;

            if (!pathToTile[i].IsPassable(this, false))
                break;

            int projectedStaminaSpent = staminaSpent + pathToTile[i].GetCostToPass(this);

            if (projectedStaminaSpent > GetCurStamina())
                break;

            staminaSpent += pathToTile[i].GetCostToPass(this);
            destinationTile = pathToTile[i];
            path.Add(destinationTile);

            if (staminaSpent >= staminaToUse)
                break;
        }

        if (destinationTile.IsOccupied())
        {
            path.Remove(destinationTile);
            for (int i = path.Count - 1; i >= 0; i--)
            {
                if (!path[i].IsOccupied())
                {
                    return path[i];
                }
            }

            if (letPassEnemies)
            {
                return GetMoveTowardsDestination(tile, staminaToUse, ignoreTerrainDifference, false);
            }
            else
            {
                return m_gameTile;
            }
        }
        else
        {
            return destinationTile;
        }
    }

    public virtual void SpendStamina(int toSpend)
    {
        m_curStamina -= toSpend;

        if (m_curStamina < 0)
        {
            m_curStamina = 0;
        }

        UIHelper.ReselectUnit();
    }

    public virtual string GetDesc()
    {
        return m_desc;
    }

    public virtual string GetKeywordDesc()
    {
        string returnDesc = "";

        GameThornsKeyword thornsKeyword = GetThornsKeyword();
        if (thornsKeyword != null)
        {
            returnDesc += thornsKeyword.GetDisplayString() + "\n";
        }

        GameFadeKeyword fadeKeyword = GetFadeKeyword();
        if (fadeKeyword != null)
        {
            returnDesc += fadeKeyword.GetDisplayString() + "\n";
        }

        GameFlyingKeyword flyingKeyword = GetFlyingKeyword();
        if (flyingKeyword != null)
        {
            returnDesc += flyingKeyword.GetDisplayString() + "\n";
        }

        GameMountainwalkKeyword mountainwalkKeyword = GetMountainwalkKeyword();
        if (mountainwalkKeyword != null)
        {
            returnDesc += mountainwalkKeyword.GetDisplayString() + "\n";
        }

        GameWaterwalkKeyword waterwalkKeyword = GetWaterwalkKeyword();
        if (waterwalkKeyword != null)
        {
            returnDesc += waterwalkKeyword.GetDisplayString() + "\n";
        }

        GameDuneswalkKeyword duneswalkKeyword = GetDuneswalkKeyword();
        if (duneswalkKeyword != null)
        {
            returnDesc += duneswalkKeyword.GetDisplayString() + "\n";
        }

        GameFrostwalkKeyword frostwalkKeyword = GetFrostwalkKeyword();
        if (frostwalkKeyword != null)
        {
            returnDesc += frostwalkKeyword.GetDisplayString() + "\n";
        }

        GameWaterboundKeyword waterboundKeyword = GetWaterboundKeyword();
        if (waterboundKeyword != null)
        {
            returnDesc += waterboundKeyword.GetDisplayString() + "\n";
        }

        GameForestwalkKeyword forestwalkKeyword = GetForestwalkKeyword();
        if (forestwalkKeyword != null)
        {
            returnDesc += forestwalkKeyword.GetDisplayString() + "\n";
        }

        GameRootedKeyword rootedKeyword = GetRootedKeyword();
        if (rootedKeyword != null)
        {
            returnDesc += rootedKeyword.GetDisplayString() + "\n";
        }

        GameTauntKeyword tauntKeyword = GetTauntKeyword();
        if (tauntKeyword != null)
        {
            returnDesc += tauntKeyword.GetDisplayString() + "\n";
        }

        GameCleaveKeyword cleaveKeyword = GetCleaveKeyword();
        if (cleaveKeyword != null)
        {
            returnDesc += cleaveKeyword.GetDisplayString() + "\n";
        }

        GameLavawalkKeyword lavawalkKeyword = GetLavawalkKeyword();
        if (lavawalkKeyword != null)
        {
            returnDesc += lavawalkKeyword.GetDisplayString() + "\n";
        }

        GameRegenerateKeyword regenKeyword = GetRegenerateKeyword();
        if (regenKeyword != null)
        {
            returnDesc += regenKeyword.GetDisplayString() + "\n";
        }

        GameDamageShieldKeyword damageShieldKeyword = GetDamageShieldKeyword();
        if (damageShieldKeyword != null)
        {
            returnDesc += damageShieldKeyword.GetDisplayString() + "\n";
        }

        GameBleedKeyword bleedKeyword = GetBleedKeyword();
        if (bleedKeyword != null)
        {
            returnDesc += bleedKeyword.GetDisplayString() + "\n";
        }

        GameBrittleKeyword brittleKeyword = GetBrittleKeyword();
        if (brittleKeyword != null)
        {
            returnDesc += brittleKeyword.GetDisplayString() + "\n";
        }

        GameEnrageKeyword enrageKeyword = GetEnrageKeyword();
        if (enrageKeyword != null)
        {
            returnDesc += enrageKeyword.GetDisplayString() + "\n";
        }

        GameMomentumKeyword momentumKeyword = GetMomentumKeyword();
        if (momentumKeyword != null)
        {
            returnDesc += momentumKeyword.GetDisplayString() + "\n";
        }

        GameVictoriousKeyword victoriousKeyword = GetVictoriousKeyword();
        if (victoriousKeyword != null)
        {
            returnDesc += victoriousKeyword.GetDisplayString() + "\n";
        }

        GameKnowledgeableKeyword knowledgeableKeyword = GetKnowledgeableKeyword();
        if (knowledgeableKeyword != null)
        {
            returnDesc += knowledgeableKeyword.GetDisplayString() + "\n";
        }

        GameSpellcraftKeyword spellcraftKeyword = GetSpellcraftKeyword();
        if (spellcraftKeyword != null)
        {
            returnDesc += spellcraftKeyword.GetDisplayString() + "\n";
        }

        GameRangeKeyword rangeKeyword = GetRangeKeyword();
        if (rangeKeyword != null)
        {
            returnDesc += rangeKeyword.GetDisplayString() + "\n";
        }

        GameDeathKeyword deathKeyword = GetDeathKeyword();
        if (deathKeyword != null)
        {
            returnDesc += deathKeyword.GetDisplayString() + "\n";
        }

        GameSummonKeyword summonKeyword = GetSummonKeyword();
        if (summonKeyword != null)
        {
            returnDesc += summonKeyword.GetDisplayString() + "\n";
        }

        if (IsInCover())
        {
            returnDesc += "<b>Cover</b>: 50% damage taken. (rounded down)\n";
        }

        if (IsInDunes())
        {
            returnDesc += $"<b>Spell Cover</b>: {Constants.SandDuneMagicDamageReductionPercentage}% less damage from spells.\n";
        }

        GameDamageReductionKeyword damageReductionKeyword = GetDamageReductionKeyword();
        if (damageReductionKeyword != null)
        {
            returnDesc += damageReductionKeyword.GetDisplayString() + "\n";
        }

        if(returnDesc.Length >= 2)
        {
            returnDesc = returnDesc.Substring(0, returnDesc.Length - 1);
        }

        return returnDesc;
    }

    private void RegenStamina()
    {
        int staminaToRegen = GetStaminaRegen();

        if (GetBleedKeyword() != null)
        {
            staminaToRegen -= 2;
        }

        if (staminaToRegen < 0)
        {
            staminaToRegen = 0;
        }

        GainStamina(staminaToRegen, true);
    }

    public void AddStats(int powerToAdd, int healthToAdd, bool permanent, bool showWorldNotification)
    {
        if (powerToAdd == 0 && healthToAdd == 0)
        {
            return;
        }

        if (showWorldNotification == true)
        {
            UIHelper.CreateWorldElementNotification(GetName() + " gets +" + powerToAdd + "/+" + healthToAdd + ".", true, m_gameTile.GetWorldTile().gameObject);
        }

        if (permanent)
        {
            m_permPower += powerToAdd;
            m_permMaxHealth += healthToAdd;

            if (!HasCustomName())
            {
                SetCustomName();
            }
        }
        else
        {
            m_power += powerToAdd;
            m_maxHealth += healthToAdd;
        }

        if (healthToAdd > 0)
        {
            m_curHealth += healthToAdd;
        }
    }

    public void RemoveStats(int powerToRemove, int healthToRemove, bool permanent)
    {
        if (!m_isDead)
        {
            UIHelper.CreateWorldElementNotification(GetName() + " gets -" + powerToRemove + "/-" + healthToRemove + ".", false, m_gameTile.GetWorldTile().gameObject);
        }

        if (permanent)
        {
            m_permPower -= powerToRemove;
            m_permMaxHealth -= healthToRemove;

            if (!HasCustomName())
            {
                SetCustomName();
            }
        }
        else
        {
            m_power -= powerToRemove;
            m_maxHealth -= healthToRemove;
        }

        if (GetMaxHealth() < 1)
        {
            if (!m_isDead)
            {
                UIHelper.CreateWorldElementNotification(GetName() + " can't be reduced below 1 Max Health.", GetTeam() == Team.Player, m_gameTile.GetWorldTile().gameObject);
            }
            m_maxHealth = 1;
        }

        if (m_curHealth > GetMaxHealth())
        {
            m_curHealth = GetMaxHealth();
        }
    }

    public bool HasCustomName()
    {
        if (m_customName == null || m_customName == "")
        {
            return false;
        }

        return true;
    }

    //Gets the custom name, without the base name attached
    public string GetCustomName()
    {
        return m_customName;
    }

    protected void SetCustomName()
    {
        if (GetTeam() == Team.Enemy)
        {
            return;
        }

        m_customName = GameNamesFactory.GetCustomUnitName(m_typeline);
    }

    //Gets the full name with custom + base or just base
    public override string GetName()
    {
        if (HasCustomName())
        {
            //return m_customName + ", the " + m_name;
            return m_customName;
        }
        else
        {
            return m_name;
        }
    }

    public virtual bool IsInvulnerable()
    {
        return false;
    }

    public virtual void TriggerSummonRelics()
    {
        if (GetTeam() == Team.Player)
        {
            if (GameHelper.HasRelic<ContentMarkOfTordrimRelic>())
            { 
                List<GameKeywordBase> tordrimKeywords = new List<GameKeywordBase>();
                tordrimKeywords.Add(new GameVictoriousKeyword(new GameFullHealAction(this)));
                tordrimKeywords.Add(new GameEnrageKeyword(new GameGainGoldAction(5)));
                tordrimKeywords.Add(new GameFlyingKeyword());
                tordrimKeywords.Add(new GameMomentumKeyword(new GameGainEnergyAction(1)));
                tordrimKeywords.Add(new GameDeathKeyword(new GameDrawCardAction(3)));
                tordrimKeywords.Add(new GameRangeKeyword(2));
                tordrimKeywords.Add(new GameRegenerateKeyword(10));
                tordrimKeywords.Add(new GameSpellcraftKeyword(new GameGainStaminaAction(this, 1)));

                int r = Random.Range(0, tordrimKeywords.Count);
                AddKeyword(tordrimKeywords[r], false, false);
            }

            if (GameHelper.HasRelic<ContentSporetechRelic>())
            {
                int r = Random.Range(0, 3);
                if (r == 0)
                {
                    m_typeline = Typeline.Humanoid;
                }
                else if (r == 1)
                {
                    m_typeline = Typeline.Monster;
                }
                else if (r == 2)
                {
                    m_typeline = Typeline.Creation;
                }
            }

            if (GameHelper.HasRelic<ContentToldiranMiracleRelic>())
            {
                if (GameHelper.HasAllTypelines())
                {
                    GameHelper.GetPlayer().DrawCards(2);
                    GameHelper.GetPlayer().AddEnergy(3);
                }
            }

            if (GameHelper.HasRelic<ContentSymbolOfTheAllianceRelic>())
            {
                if (GameHelper.HasAllTypelines())
                {
                    AddKeyword(new GameDamageReductionKeyword(3), false, false);
                }
            }

            if (GameHelper.HasRelic<ContentMemoryOfTheDefenderRelic>() && GetTypeline() == Typeline.Creation)
            {
                 GameHelper.GetPlayer().AddMagicPower(1);
            }

            if (GameHelper.HasRelic<ContentTauntingPipeRelic>() && GetTypeline() == Typeline.Humanoid)
            {
                AddKeyword(new GameTauntKeyword(), false, false);
            }

            if (GameHelper.HasRelic<ContentCarapaceOfTutuiun>())
            {
                AddKeyword(new GameDamageReductionKeyword(1), true, false);
            }

            if (GameHelper.HasRelic<ContentStarOfDenumainRelic>())
            {
                AddKeyword(new GameDamageShieldKeyword(), false, false);
            }

            if (GameHelper.HasRelic<ContentAlterOfTordrimRelic>())
            {
                int powerChange = Random.Range(-3, 8);
                int healthChange = Random.Range(-3, 8);

                if (powerChange >= 0 && healthChange >= 0)
                {
                    AddStats(powerChange, healthChange, false, true);
                }
                else if (powerChange < 0 && healthChange < 0)
                {
                    RemoveStats(-powerChange, -healthChange, false);
                }
                else if (powerChange >= 0 && healthChange < 0)
                {
                    AddStats(powerChange, 0, false, true);
                    RemoveStats(0, -healthChange, false);
                }
                else if (powerChange < 0 && healthChange >= 0)
                {
                    AddStats(0, healthChange, false, true);
                    RemoveStats(-powerChange, 0, false);
                }
            }

            if (GameHelper.HasRelic<ContentJugOfTordrimRelic>())
            {
                int tempPower = GetPower();
                m_power = GetMaxHealth();
                m_maxHealth = tempPower;
                m_curHealth = GetMaxHealth();
            }
        }
    }

    public virtual void TriggerDeathRelics(DamageType damageType = DamageType.None)
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (GameHelper.HasRelic<ContentSackOfSoulsRelic>())
        {
            player.GainGold(2);
        }

        if (GetTeam() == Team.Enemy)
        {
            if (GameHelper.HasRelic<ContentMorlemainsSkullRelic>())
            {
                player.AddEnergy(1);
            }

            if (GameHelper.HasRelic<ContentSpiritCatcherRelic>())
            {
                player.DrawCards(1);
            }

            if (GameHelper.HasRelic<ContentRelicOfVictoryRelic>() && GetPower() >= 20)
            {
                player.DrawCards(2);
                player.AddEnergy(2);
            }

            if (GameHelper.HasRelic<ContentAncientEvilRelic>())
            {
                ContentAncientEvilRelic evilRelic = (ContentAncientEvilRelic)(GameHelper.GetPlayer().GetRelics().GetRelic<ContentAncientEvilRelic>());
                evilRelic.AddKillCount();
            }

            if (GameHelper.IsUnitInWorld(this))
            {
                if (GameHelper.HasRelic<ContentCanvasOfHistoryRelic>())
                {
                    List<GameTile> adjacentTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_gameTile, 2);
                    for (int i = 0; i < adjacentTiles.Count; i++)
                    {
                        if (adjacentTiles[i].IsOccupied() &&
                            adjacentTiles[i].GetOccupyingUnit().GetTeam() == Team.Player &&
                            !adjacentTiles[i].GetOccupyingUnit().m_isDead)
                        {
                            adjacentTiles[i].GetOccupyingUnit().Heal(15);
                        }
                    }
                }

                if (GameHelper.HasRelic<ContentBeadsOfProphecyRelic>())
                {
                    List<GameTile> adjacentTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_gameTile, 1);
                    for (int i = 0; i < adjacentTiles.Count; i++)
                    {
                        if (adjacentTiles[i].IsOccupied() &&
                            adjacentTiles[i].GetOccupyingUnit().GetTeam() == Team.Player &&
                            !adjacentTiles[i].GetOccupyingUnit().m_isDead)
                        {
                            adjacentTiles[i].GetOccupyingUnit().GainStamina(1);
                        }
                    }
                }
            }

            if (GameHelper.HasRelic<ContentToolOfTheDeadmanRelic>())
            {
                if (damageType == DamageType.Unit)
                {
                    if (GameHelper.GetGameController().CurrentActor == player)
                    {
                        player.AddCardToHand(GameCardFactory.GetCardClone(new ContentShivCard()), false);
                    }
                    else
                    {
                        player.AddScheduledAction(ScheduledActionTime.StartOfTurn, new GameGainShivAction(1));
                    }
                }
            }
        }
        else if (GetTeam() == Team.Player)
        {
            if (GameHelper.HasRelic<ContentSoulTrapRelic>())
            {
                if (GameHelper.GetGameController().CurrentActor == player)
                {
                    player.DrawCards(3);
                }
                else
                {
                    player.AddScheduledAction(ScheduledActionTime.StartOfTurn, new GameDrawCardAction(3));
                }
            }

            if (GameHelper.HasRelic<ContentTombOfTheDefenderRelic>())
            {
                if (GameHelper.GetGameController().CurrentActor == player)
                {
                    player.AddCardToHand(GameCardFactory.GetCardClone(new ContentShivCard()), false);
                    player.AddCardToHand(GameCardFactory.GetCardClone(new ContentShivCard()), false);
                    player.AddCardToHand(GameCardFactory.GetCardClone(new ContentShivCard()), false);
                }
                else
                {
                    player.AddScheduledAction(ScheduledActionTime.StartOfTurn, new GameGainShivAction(3));
                }
            }

            if (GameHelper.IsUnitInWorld(this))
            {
                if (GameHelper.HasRelic<ContentCursedAmuletRelic>())
                {
                    List<GameTile> adjacentTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_gameTile, 1);
                    for (int i = 0; i < adjacentTiles.Count; i++)
                    {
                        if (adjacentTiles[i].IsOccupied() && adjacentTiles[i].GetOccupyingUnit().GetTeam() == Team.Enemy && !adjacentTiles[i].GetOccupyingUnit().m_isDead)
                        {
                            adjacentTiles[i].GetOccupyingUnit().SpendStamina(adjacentTiles[i].GetOccupyingUnit().GetCurStamina());
                        }
                    }
                }

                if (GameHelper.HasRelic<ContentTokenOfTheUprisingRelic>() && GetTypeline() == Typeline.Humanoid)
                {
                    List<GameTile> adjacentTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_gameTile, 2);
                    for (int i = 0; i < adjacentTiles.Count; i++)
                    {
                        if (adjacentTiles[i].IsOccupied() &&
                            adjacentTiles[i].GetOccupyingUnit().GetTeam() == Team.Player &&
                            !adjacentTiles[i].GetOccupyingUnit().m_isDead &&
                            adjacentTiles[i].GetOccupyingUnit().GetTypeline() == Typeline.Creation)
                        {
                            adjacentTiles[i].GetOccupyingUnit().AddStats(GetPower(), GetMaxHealth(), false, true);
                        }
                    }
                }

                if (GameHelper.HasRelic<ContentTotemOfRevengeRelic>())
                {
                    List<GameTile> adjacentTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_gameTile, 3);
                    for (int i = 0; i < adjacentTiles.Count; i++)
                    {
                        if (adjacentTiles[i].IsOccupied() &&
                            adjacentTiles[i].GetOccupyingUnit().GetTeam() == Team.Player &&
                            !adjacentTiles[i].GetOccupyingUnit().m_isDead)
                        {
                            adjacentTiles[i].GetOccupyingUnit().FillStamina();
                        }
                    }
                }
            }

            if (GameHelper.HasRelic<ContentVoiceOfTheDefenderRelic>() && GetTypeline() == Typeline.Creation)
            {
                player.AddMagicPower(1);
            }

            if (GameHelper.HasRelic<ContentDesignSchematicsRelic>() && GetTypeline() == Typeline.Creation)
            {
                AddStats(1, 3, true, false);
                AddMaxStamina(1, true);
            }

            if (GameHelper.HasRelic<ContentInstructionsRelic>() && GetTypeline() == Typeline.Creation)
            {
                AddStats(GetMaxStamina(), GetMaxStamina(), false, false);
            }
        }
    }

    public virtual AudioClip GetAttackSFX()
    {
        return m_attackSFX;
    }

    //============================================================================================================//

    public virtual void StartTurn() 
    {
        if (GameHelper.HasRelic<ContentFadingLightRelic>() && GetTeam() == Team.Player)
        {
            Heal(GetMaxHealth());
        }

        GameRegenerateKeyword regenKeyword = GetRegenerateKeyword();
        if (regenKeyword != null)
        {
            Heal(regenKeyword.m_regenVal);
        }

        if (GetTypeline() == Typeline.Humanoid)
        {
            if (GetTeam() == Team.Player && GameHelper.HasRelic<ContentMedKitRelic>())
            {
                int healAmount = m_gameTile.GetCostToPass(this) * 5;
                Heal(healAmount);
            }
        }
    }

    public virtual void EndTurn()
    {
        if (GameHelper.HasRelic<ContentPriceOfFreedomRelic>() && GetTeam() == Team.Player && GetCurStamina() == GetMaxStamina())
        {
            AddStats(2, 2, true, true);
            EmptyStamina();
        }

        RegenStamina();

        if (GetGameTile().GetTerrain().IsLava() && GetLavawalkKeyword() == null && GetFlyingKeyword() == null)
        {
            GetHitByAbility(Constants.LavaFieldDamageDealt);
            if (m_isDead)
            {
                return;
            }
        }

        if (GameHelper.IsUnitInWorld(this))
        {
            if (GameHelper.HasRelic<ContentSecretOfTheDeepRelic>() && GetTeam() == Team.Player && GetTypeline() == Typeline.Humanoid)
            {
                if (m_gameTile.GetTerrain().IsWater())
                {
                    Die();
                }
            }
        }
    }

    //============================================================================================================//

    public string GetGuid()
    {
        return m_guid;
    }

    public virtual JsonGameUnitData SaveToJson()
    {
        JsonGameKeywordHolderData keywordHolderJson = m_keywordHolder.SaveToJson();

        JsonGameUnitData jsonData = new JsonGameUnitData
        {
            baseName = m_name,
            customName = m_customName,
            team = (int)m_team,
            curHealth = m_curHealth,
            curStamina = m_curStamina,
            maxHealth = m_maxHealth,
            permMaxHealth = m_permMaxHealth,
            staminaRegen = m_staminaRegen,
            permStaminaRegen = m_permStaminaRegen,
            maxStamina = m_maxStamina,
            permMaxStamina = m_permMaxStamina,
            power = m_power,
            permPower = m_permPower,
            typeline = (int)m_typeline,
            jsonGameKeywordHolderData = keywordHolderJson,
            staminaToAttack = m_staminaToAttack,
            sightRange = m_sightRange,
            guid = GetGuid()
        };

        return jsonData;
    }

    public virtual void LoadFromJson(JsonGameUnitData jsonData)
    {
        m_keywordHolder.RemoveAllKeywords(false);

        m_customName = jsonData.customName;
        
        m_curHealth = jsonData.curHealth;
        m_team = (Team)jsonData.team;
        m_curStamina = jsonData.curStamina;
        m_maxHealth = jsonData.maxHealth;
        m_permMaxHealth = jsonData.permMaxHealth;
        m_staminaRegen = jsonData.staminaRegen;
        m_permStaminaRegen = jsonData.permStaminaRegen;
        m_maxStamina = jsonData.maxStamina;
        m_permMaxStamina = jsonData.permMaxStamina;
        m_power = jsonData.power;
        m_permPower = jsonData.permPower;
        m_typeline = (Typeline)jsonData.typeline;
        m_staminaToAttack = jsonData.staminaToAttack;
        m_sightRange = jsonData.sightRange;
        m_guid = jsonData.guid;

        m_keywordHolder.LoadFromJson((jsonData.jsonGameKeywordHolderData, this));
    }
}
