using Game.Util;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
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

public abstract class GameUnit : GameElementBase, ITurns, ISave<JsonGameUnitData>, ILoad<JsonGameUnitData>
{
    //General data.  This should be set for every unit
    protected Team m_team;
    protected int m_curHealth;
    protected int m_maxHealth;
    protected int m_curStamina;
    protected int m_staminaRegen;
    protected int m_maxStamina;
    protected int m_power;
    protected Typeline m_typeline;

    //Specific data.  Only set if it varies from the default.  Be sure to add to the descrip so it shows up in the UI.
    private GameKeywordHolder m_keywordHolder = new GameKeywordHolder();
    protected int m_staminaToAttack = 2;
    protected int m_sightRange = 3;
    public bool m_shouldAlwaysPassEnemies;

    //Functionality
    protected GameTile m_gameTile;
    public bool m_isDead;
    public bool m_returnedToDeckDeath;
    public WorldUnit m_worldUnit;
    public Sprite m_iconWhite;
    protected string m_customName;
    protected Vector3 m_worldTilePositionAdjustment = new Vector3(0,0,0);

    //Special functionality
    public bool m_instantWaterMovement;
    public bool m_startWithMaxStamina;
    public bool m_takesLavaFieldDamage = true;

    //Unique guid per unit, to use to link together like gameunits in save data
    private string m_guid = System.Guid.NewGuid().ToString();

    public void CopyOff(GameUnit other)
    {
        m_maxHealth = other.m_maxHealth;
        m_staminaRegen = other.m_staminaRegen;
        m_maxStamina = other.m_maxStamina;
        m_power = other.m_power;
        m_typeline = other.m_typeline;

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

    public virtual int GetHit(int damage, GameUnit gameUnit = null, bool shouldThorns = true)
    {
        if (gameUnit != null)
        {
            GameThornsKeyword thornsKeyword = GetThornsKeyword();
            if (thornsKeyword != null)
            {
                HitUnit(gameUnit, thornsKeyword.m_thornsDamage, false, false);
            }
        }

        if (m_isDead)
        {
            return 0;
        }

        if (IsInvulnerable())
        {
            UIHelper.CreateWorldElementNotification(GetName() + " is invulnerable and takes no damage!", false, m_gameTile.GetWorldTile().gameObject);
            return 0;
        }

        damage = CalculateDamageAmount(damage);

        if (damage <= 0)
        {
            UIHelper.CreateWorldElementNotification(GetName() + " takes no damage from the hit!", false, m_gameTile.GetWorldTile().gameObject);
            return 0;
        }

        GameDamageShieldKeyword damageShieldKeyword = GetDamageShieldKeyword();
        if (damageShieldKeyword != null)
        {
            if (!damageShieldKeyword.ShouldBeRemoved())
            {
                damageShieldKeyword.DecreaseShield(1);
                if (damageShieldKeyword.ShouldBeRemoved())
                {
                    UIHelper.CreateWorldElementNotification("Damage Shield Broken!", true, m_worldUnit.gameObject);
                    RemoveKeyword(damageShieldKeyword);
                }
                else
                {
                    UIHelper.CreateWorldElementNotification("Damage Shield Weakened!", true, m_worldUnit.gameObject);
                }
                return 0;
            }
        }

        if (GameHelper.HasRelic<ContentTalonOfTheCruelRelic>() && GetTeam() == Team.Enemy && GetFlyingKeyword() != null)
        {
            damage = damage * 2;
        }

        if (GameHelper.HasRelic<ContentHistoryInBloodRelic>())
        {
            damage = damage * 2;
        }

        m_curHealth -= damage;

        if (GameHelper.HasRelic<ContentAngelicFeatherRelic>() && m_curHealth > 0 && m_curHealth <= 5)
        {
            AddKeyword(new GameDamageShieldKeyword(3));
        }

        if (GameHelper.HasRelic<ContentBloodFeatherRelic>() && m_curHealth > 0 && m_curHealth <= 3)
        {
            AddStats(10, 0);
        }

        if (GameHelper.HasRelic<ContentGoldenFeatherRelic>() && m_curHealth > 0 && m_curHealth <= 1)
        {
            GameHelper.GetPlayer().m_wallet.AddResources(new GameWallet(10));
        }

        AudioHelper.PlaySFX(AudioHelper.UnitGetHit);

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

        string damageReducDesc = "";
        if (m_gameTile.GetDamageReduction(this) > 0)
        {
            damageReducDesc = " (Reduced by " + m_gameTile.GetDamageReduction(this) + " from " + m_gameTile.GetName() + ")";
        }

        if (m_curHealth <= 0)
        {
            Die();
        }
        else
        {
            UIHelper.CreateWorldElementNotification(GetName() + " takes " + damage + " damage!" + damageReducDesc, false, m_gameTile.GetWorldTile().gameObject);
        }

        return damage;
    }

    public virtual int CalculateDamageAmount(int damage)
    {
        damage -= m_gameTile.GetDamageReduction(this);

        GameBrittleKeyword brittleKeyword = GetBrittleKeyword();
        if (brittleKeyword != null)
        {
            damage += brittleKeyword.m_damageIncrease;
        }

        GameDamageReductionKeyword damageReductionKeyword = GetDamageReductionKeyword();
        if (damageReductionKeyword != null)
        {
            damage -= damageReductionKeyword.m_damageReduction;
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

    public virtual void Die(bool canRevive = true)
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

        if (GameHelper.HasRelic<ContentSachelOfDeceptionRelic>())
        {
            if (m_keywordHolder.GetNumVisibleKeywords() == 0)
            {
                AddKeyword(new GameDeathKeyword(new GameGainStatsAction(this, 3, 3)));
                m_isDead = false;
                m_curHealth = GetMaxHealth();
                UIHelper.CreateWorldElementNotification(GetName() + " deceives the foe and survives.", true, m_gameTile.GetWorldTile().gameObject);
                return;
            }
        }

        m_curHealth = 0;

        GameDeathKeyword deathKeyword = GetDeathKeyword();
        if (deathKeyword != null)
        {
            deathKeyword.DoAction();
        }

        TriggerDeathRelics();

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
                    surroundingTiles[i].m_occupyingUnit.GetFlyingKeyword() == null && 
                    surroundingTiles[i].m_occupyingUnit.GetWaterwalkKeyword() == null)
                {
                    surroundingTiles[i].m_occupyingUnit.Die();
                }
                else if (surroundingTiles[i].GetTerrain().IsIce())
                {                    
                    surroundingTiles[i].SetTerrain(GameTerrainFactory.GetIceCrackedTerrainClone(surroundingTiles[i].GetTerrain()));
                }
            }
        }

        if (m_worldUnit == Globals.m_selectedUnit)
        {
            WorldGridManager.Instance.ClearAllTilesMovementRange();
        }
        m_gameTile.GetWorldTile().RecycleUnit();
        UITooltipController.Instance.ClearTooltipStack();

        m_isDead = willSetDead;
    }

    //Returns the amount actually healed
    public virtual int Heal(int toHeal)
    {
        int maxHealth = GetMaxHealth();

        int realHealVal = toHeal;

        if (!GameHelper.HasRelic<ContentPrimeRibRelic>())
        {
            if (m_curHealth + toHeal > maxHealth)
            {
                realHealVal = maxHealth - m_curHealth;
            }
        }

        m_curHealth += toHeal;

        if (!GameHelper.HasRelic<ContentPrimeRibRelic>())
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

        return realHealVal;
    }

    public virtual bool CanHitUnit(GameUnit other, bool checkRange = true)
    {
        if (GetTeam() == other.GetTeam()) //Can't attack your own team
        {
            return false;
        }

        if (!HasStaminaToAttack())
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

        if (!HasStaminaToAttack())
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
        List<GameTile> tiles = WorldGridManager.Instance.CalculateAStarPath(m_gameTile, other.m_gameTile, true, false, false);

        if (tiles == null)
        {
            return false;
        }

        int distance = tiles.Count;

        if ((distance - 1) > GetRange())
        {
            return false;
        }

        return true;
    }

    public virtual bool IsInRangeOfBuilding(GameBuildingBase other)
    {
        List<GameTile> tiles = WorldGridManager.Instance.CalculateAStarPath(m_gameTile, other.GetGameTile(), true, false, false);

        if (tiles == null)
        {
            return false;
        }

        int distance = tiles.Count;

        if ((distance - 1) > GetRange())
        {
            return false;
        }

        return true;
    }

    public void SpellCast(GameCard.Target targetType, GameTile targetTile)
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
        }
    }

    public virtual bool HasStaminaToAttack()
    {
        if (m_curStamina < GetStaminaToAttack())
        {
            return false;
        }

        return true;
    }

    public void AddStaminaRegen(int toAdd)
    {
        m_staminaRegen += toAdd;

        if (!HasCustomName())
        {
            SetCustomName();
        }
    }

    public void AddMaxStamina(int toAdd)
    {
        if (toAdd == 0)
        {
            return;
        }

        if (!HasCustomName())
        {
            SetCustomName();
        }

        if (m_maxStamina >= Constants.MaxTotalStamina)
        {
            return;
        }

        if (GameHelper.HasRelic<ContentSecretTiesRelic>() && m_gameTile != null && GetTypeline() == Typeline.Creation)
        {
            List<GameTile> adjacentTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_gameTile, 3);
            for (int i = 0; i < adjacentTiles.Count; i++)
            {
                if (adjacentTiles[i].IsOccupied() &&
                    adjacentTiles[i].m_occupyingUnit.GetTeam() == Team.Player &&
                    !adjacentTiles[i].m_occupyingUnit.m_isDead &&
                    adjacentTiles[i].m_occupyingUnit.GetTypeline() == Typeline.Monster)
                {
                    adjacentTiles[i].m_occupyingUnit.AddKeyword(new GameVictoriousKeyword(new GameGainStatsAction(adjacentTiles[i].m_occupyingUnit, 3, 3)));
                }
            }
        }

        m_maxStamina += toAdd;
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

        if (toReturn < 0)
        {
            toReturn = 0;
        }

        return toReturn;
    }

    public virtual int HitUnit(GameUnit other, int damageAmount, bool spendStamina = true, bool shouldThorns = true)
    {
        if (spendStamina)
        {
            SpendStamina(GetStaminaToAttack());
        }

        int damageTaken = other.GetHit(damageAmount, this, shouldThorns);

        GameMomentumKeyword momentumKeyword = GetMomentumKeyword();

        if (momentumKeyword != null)
        { 
            momentumKeyword.DoAction();

            //If the player has Bestial Wrath relic, repeat the action
            if (GetTypeline() == Typeline.Monster && GetTeam() == Team.Player)
            {
                if (GameHelper.HasRelic<ContentBestialWrathRelic>())
                {
                    momentumKeyword.DoAction();
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

        return damageTaken;
    }

    public virtual int HitBuilding(GameBuildingBase other, bool spendStamina = true)
    {
        if (spendStamina)
        {
            SpendStamina(GetStaminaToAttack());
        }

        int damageTaken = other.GetHit(GetDamageToDealTo(other));

        GameMomentumKeyword momentumKeyword = GetMomentumKeyword();

        if (momentumKeyword != null)
        {
            momentumKeyword.DoAction();

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

        return 0;
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

    public virtual int GetStaminaToAttack()
    {
        int staminaToAttack = m_staminaToAttack;
        if (GetTeam() == Team.Player)
        {
            if (GameHelper.HasRelic<ContentUrbanTacticsRelic>())
            {
                staminaToAttack--;
            }

            if (GameHelper.GetGameController().m_currentTurnNumber == GameHelper.GetPlayer().m_totemOfTheWolfTurn)
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

    public void AddKeyword(GameKeywordBase newKeyword, bool canChangeName = true)
    {
        if (canChangeName)
        {
            UIHelper.CreateWorldElementNotification(GetName() + " gains " + newKeyword.GetName() + ".", true, m_gameTile.GetWorldTile().gameObject);
        }

        m_keywordHolder.AddKeyword(newKeyword);

        if (!HasCustomName() && canChangeName)
        {
            SetCustomName();
        }
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
            toReturn.AddKeyword(holderKeyword);
        }

        //Check relics and other effects to see if anything needs to be added to the return keyword
        if (GameHelper.HasRelic<ContentThornsOfRayRelic>() && GetTeam() == Team.Player)
        {
            toReturn.AddKeyword(new GameThornsKeyword(2));
        }

        //If the return keyword is still blank, set it to null
        if (toReturn.m_thornsDamage == 0)
        {
            toReturn = null;
        }

        //Return it
        return toReturn;
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

    public virtual GameForestwalkKeyword GetForestwalkKeyword()
    {
        return m_keywordHolder.GetKeyword<GameForestwalkKeyword>();
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
            toReturn.AddKeyword(holderKeyword);
        }

        //Check relics and other effects to see if anything needs to be added to the return keyword
        if (GameHelper.HasRelic<ContentPlagueMaskRelic>() && GetTeam() == Team.Player && GetTypeline() == Typeline.Monster)
        {
            toReturn.AddKeyword(new GameRegenerateKeyword(5));
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
            toReturn.AddKeyword(holderKeyword);
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
            toReturn.AddKeyword(holderKeyword);
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
            toReturn.AddKeyword(holderKeyword);
        }

        //Check relics and other effects to see if anything needs to be added to the return keyword
        if (GameHelper.HasRelic<ContentAdvancedWeaponryRelic>() && GetTeam() == Team.Player && toReturn.m_range >= 2)
        {
            toReturn.AddKeyword(new GameRangeKeyword(1));
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
        return m_keywordHolder.GetKeyword<GameDamageReductionKeyword>();
    }

    public GameKeywordHolder GetKeywordHolderForRead()
    {
        return m_keywordHolder;
    }

    public virtual int GetRange()
    {
        GameRangeKeyword rangeKeyword = GetRangeKeyword();
        if (rangeKeyword != null)
        {
            int range = rangeKeyword.m_range;

            if (m_gameTile != null)
            {
                int terrainRange = m_gameTile.GetTerrain().m_rangeModifier;

                if (terrainRange > 0 && GetTeam() == Team.Player && GameHelper.HasRelic<ContentNaturalProtectionRelic>())
                {
                    terrainRange += terrainRange * 2;
                }
                
                range += terrainRange;
            }

            return range;
        }

        return 1;
    }

    public virtual int GetPower()
    {
        int toReturn = m_power;

        if (GetTeam() == Team.Player)
        {
            if (GameHelper.HasRelic<ContentWolvenFangRelic>())
            {
                toReturn += (2 * (1 + new ContentWolvenFangRelic().GetRelicLevel()));
            }

            if (GameHelper.HasRelic<ContentSigilOfTheSwordsmanRelic>() && GetTypeline() == Typeline.Humanoid)
            {
                toReturn += 6;
            }

            if (GameHelper.HasRelic<ContentLegendaryFragmentRelic>())
            {
                toReturn -= 2;
            }

            if (GameHelper.HasRelic<ContentTalonOfTheMeradominRelic>())
            {
                toReturn += 5;
            }

            if (GameHelper.HasRelic<ContentSecretsOfNatureRelic>() && m_gameTile != null && m_gameTile.GetTerrain().IsForest())
            {
                toReturn += 10;
            }

            if (GetRange() > 1)
            {
                toReturn += GameHelper.GetPlayer().m_fletchingPowerIncrease;
            }

            if (GameHelper.HasRelic<ContentBondOfFamilyRelic>())
            {
                if (GetGameTile() != null)
                {
                    List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), 3);

                    for (int i = 0; i < surroundingTiles.Count; i++)
                    {
                        if (surroundingTiles[i].IsOccupied() && !surroundingTiles[i].m_occupyingUnit.m_isDead &&
                            surroundingTiles[i].m_occupyingUnit.GetTeam() == Team.Player)
                        {
                            if (surroundingTiles[i].m_occupyingUnit.GetTypeline() == Typeline.Humanoid)
                            {
                                toReturn += 4;
                            }
                            else if (surroundingTiles[i].m_occupyingUnit.GetTypeline() == Typeline.Creation)
                            {
                                toReturn -= 4;
                            }
                        }
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
        return m_typeline;
    }

    public int GetCurHealth()
    {
        return m_curHealth;
    }

    public int GetMaxHealth()
    {
        int toReturn = m_maxHealth;

        if (GetTeam() == Team.Player)
        {
            if (GameHelper.HasRelic<ContentOrbOfHealthRelic>())
            {
                toReturn += 6;
            }

            if (GameHelper.HasRelic<ContentSecretsOfNatureRelic>() && m_gameTile != null && m_gameTile.GetTerrain().IsForest())
            {
                toReturn += 10;
            }
        }

        return toReturn;
    }

    public int GetMaxStamina()
    {
        int toReturn = m_maxStamina;

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
                int toAdd = Mathf.FloorToInt((float)(GameHelper.GetPlayer().m_wallet.m_gold) / 250.0f);

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

        if (m_gameTile.m_isFog && !tile.m_isFog && GetTeam() == Team.Enemy)
        {
            if (GameHelper.HasRelic<ContentFearOfTheShakinaRelic>())
            {
                GetHit(20);
            }
        }

        int pathCost = WorldGridManager.Instance.GetPathLength(m_gameTile, tile, false, false, false);

        m_gameTile.ClearUnit();
        tile.PlaceUnit(this);

        if (spendStamina)
        {
            SpendStamina(pathCost);
        }
    }

    public GameTile GetMoveTowardsDestination(GameTile tile, int staminaToUse)
    {
        if (this == Globals.m_focusedDebugEnemyUnit)
        {
            Debug.Log("IS FOCUSED ENEMY");
        }

        if (tile == m_gameTile || staminaToUse <= 0)
        {
            return m_gameTile;
        }

        List<GameTile> pathToTile = WorldGridManager.Instance.CalculateAStarPath(m_gameTile, tile, false, true, true);

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
            return m_gameTile;
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

        if (m_curStamina == 0 && GameHelper.HasRelic<ContentTheReminderRelic>())
        {
            if (GetTypeline() == Typeline.Monster && GetTeam() == Team.Player)
            {
                GetHit(5);
            }
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

        GameForestwalkKeyword forestwalkKeyword = GetForestwalkKeyword();
        if (forestwalkKeyword != null)
        {
            returnDesc += forestwalkKeyword.GetDisplayString() + "\n";
        }

        GameTauntKeyword tauntKeyword = GetTauntKeyword();
        if (tauntKeyword != null)
        {
            returnDesc += tauntKeyword.GetDisplayString() + "\n";
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

        GainStamina(staminaToRegen, true);
    }

    public void AddStats(int powerToAdd, int healthToAdd)
    {
        UIHelper.CreateWorldElementNotification(GetName() + " gets +" + powerToAdd + "/+" + healthToAdd + ".", true, m_gameTile.GetWorldTile().gameObject);

        m_power += powerToAdd;
        m_maxHealth += healthToAdd;

        if (healthToAdd > 0)
        {
            m_curHealth += healthToAdd;
        }

        if (!HasCustomName())
        {
            SetCustomName();
        }
    }

    public void RemoveStats(int powerToRemove, int healthToRemove)
    {
        UIHelper.CreateWorldElementNotification(GetName() + " gets -" + powerToRemove + "/-" + healthToRemove + ".", false, m_gameTile.GetWorldTile().gameObject);

        m_power -= powerToRemove;
        m_maxHealth -= healthToRemove;

        if (m_maxHealth < 1)
        {
            UIHelper.CreateWorldElementNotification(GetName() + " can't be reduced below 1 Max Health.", GetTeam() == Team.Player, m_gameTile.GetWorldTile().gameObject);
            m_maxHealth = 1;
        }

        if (m_curHealth > m_maxHealth)
        {
            m_curHealth = m_maxHealth;
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
            return m_customName + ", the " + m_name;
        }
        else
        {
            return m_name;
        }
    }

    public virtual int GetUnitLevel()
    {
        //TODO: alex - Hook this up to player save data.

        return 0;
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
                if (m_keywordHolder.GetNumVisibleKeywords() == 0)
                {
                    List<GameKeywordBase> tordrimKeywords = new List<GameKeywordBase>();
                    tordrimKeywords.Add(new GameVictoriousKeyword(new GameExplodeAction(this, 25, 3)));
                    tordrimKeywords.Add(new GameEnrageKeyword(new GameGainResourceAction(new GameWallet(10))));
                    tordrimKeywords.Add(new GameFlyingKeyword());
                    tordrimKeywords.Add(new GameMomentumKeyword(new GameGainEnergyAction(1)));
                    tordrimKeywords.Add(new GameDeathKeyword(new GameDrawCardAction(3)));
                    tordrimKeywords.Add(new GameRangeKeyword(2));
                    tordrimKeywords.Add(new GameRegenerateKeyword(10));
                    tordrimKeywords.Add(new GameSpellcraftKeyword(new GameGainStaminaAction(this, 1)));
                    tordrimKeywords.Add(new GameKnowledgeableKeyword(new GameFullHealAction(this)));

                    int r = Random.Range(0, tordrimKeywords.Count);
                    AddKeyword(tordrimKeywords[r]);
                }
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

            if (GameHelper.HasRelic<ContentSymbolOfTheAllianceRelic>())
            {
                if (GameHelper.HasAllTypelines())
                {
                    AddKeyword(new GameDamageReductionKeyword(2));
                }
            }

            if (GameHelper.HasRelic<ContentMemoryOfTheDefenderRelic>() && GetTypeline() == Typeline.Creation)
            {
                GameHelper.GetPlayer().AddSpellPower(1);
            }

            if (GameHelper.HasRelic<ContentTauntingPipeRelic>() && GetTypeline() == Typeline.Humanoid)
            {
                AddKeyword(new GameTauntKeyword());
            }

            if (GameHelper.HasRelic<ContentCarapaceOfTutuiun>())
            {
                AddKeyword(new GameDamageReductionKeyword(1));
            }

            if (GameHelper.HasRelic<ContentStarOfDenumainRelic>())
            {
                AddKeyword(new GameDamageShieldKeyword(1));
            }

            if (GameHelper.HasRelic<ContentEvolvedMembraneRelic>())
            {
                AddKeyword(new GameVictoriousKeyword(new GameGainStatsAction(this, 1, 1)));
            }

            if (GameHelper.HasRelic<ContentAlterOfTordrimRelic>())
            {
                int powerChange = Random.Range(-3, 8);
                int healthChange = Random.Range(-3, 8);

                if (powerChange >= 0 && healthChange >= 0)
                {
                    AddStats(powerChange, healthChange);
                }
                else if (powerChange < 0 && healthChange < 0)
                {
                    RemoveStats(-powerChange, -healthChange);
                }
                else if (powerChange >= 0 && healthChange < 0)
                {
                    AddStats(powerChange, 0);
                    RemoveStats(0, -healthChange);
                }
                else if (powerChange < 0 && healthChange >= 0)
                {
                    AddStats(0, healthChange);
                    RemoveStats(-powerChange, 0);
                }
            }

            if (GameHelper.HasRelic<ContentJugOfTordrimRelic>())
            {
                int tempPower = GetPower();
                m_power = GetMaxHealth();
                m_maxHealth = tempPower;
            }
        }
    }

    public virtual void TriggerDeathRelics()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (GameHelper.HasRelic<ContentSackOfSoulsRelic>())
        {
            player.m_wallet.AddResources(new GameWallet(3));
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

            if (GameHelper.HasRelic<ContentCanvasOfHistoryRelic>() && m_gameTile != null)
            {
                List<GameTile> adjacentTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_gameTile, 2);
                for (int i = 0; i < adjacentTiles.Count; i++)
                {
                    if (adjacentTiles[i].IsOccupied() &&
                        adjacentTiles[i].m_occupyingUnit.GetTeam() == Team.Player &&
                        !adjacentTiles[i].m_occupyingUnit.m_isDead)
                    {
                        adjacentTiles[i].m_occupyingUnit.Heal(15);
                    }
                }
            }

            if (GameHelper.HasRelic<ContentBeadsOfProphecyRelic>() && m_gameTile != null)
            {
                List<GameTile> adjacentTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_gameTile, 1);
                for (int i = 0; i < adjacentTiles.Count; i++)
                {
                    if (adjacentTiles[i].IsOccupied() &&
                        adjacentTiles[i].m_occupyingUnit.GetTeam() == Team.Player &&
                        !adjacentTiles[i].m_occupyingUnit.m_isDead)
                    {
                        adjacentTiles[i].m_occupyingUnit.GainStamina(1);
                    }
                }
            }

            if (GameHelper.HasRelic<ContentToolOfTheDeadmanRelic>())
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
                }
                else
                {
                    player.AddScheduledAction(ScheduledActionTime.StartOfTurn, new GameGainShivAction(1));
                }
            }

            if (GameHelper.HasRelic<ContentCursedAmuletRelic>() && m_gameTile != null)
            {
                List<GameTile> adjacentTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_gameTile, 1);
                for (int i = 0; i < adjacentTiles.Count; i++)
                {
                    if (adjacentTiles[i].IsOccupied() && adjacentTiles[i].m_occupyingUnit.GetTeam() == Team.Enemy && !adjacentTiles[i].m_occupyingUnit.m_isDead)
                    {
                        adjacentTiles[i].m_occupyingUnit.SpendStamina(adjacentTiles[i].m_occupyingUnit.GetCurStamina());
                    }
                }
            }

            if (GameHelper.HasRelic<ContentTokenOfTheUprisingRelic>() && m_gameTile != null && GetTypeline() == Typeline.Humanoid)
            {
                List<GameTile> adjacentTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_gameTile, 2);
                for (int i = 0; i < adjacentTiles.Count; i++)
                {
                    if (adjacentTiles[i].IsOccupied() &&
                        adjacentTiles[i].m_occupyingUnit.GetTeam() == Team.Player &&
                        !adjacentTiles[i].m_occupyingUnit.m_isDead &&
                        adjacentTiles[i].m_occupyingUnit.GetTypeline() == Typeline.Creation)
                    {
                        adjacentTiles[i].m_occupyingUnit.AddStats(GetPower(), GetMaxHealth());
                    }
                }
            }

            if (GameHelper.HasRelic<ContentTotemOfRevengeRelic>() && m_gameTile != null)
            {
                List<GameTile> adjacentTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_gameTile, 3);
                for (int i = 0; i < adjacentTiles.Count; i++)
                {
                    if (adjacentTiles[i].IsOccupied() &&
                        adjacentTiles[i].m_occupyingUnit.GetTeam() == Team.Player &&
                        !adjacentTiles[i].m_occupyingUnit.m_isDead)
                    {
                        adjacentTiles[i].m_occupyingUnit.FillStamina();
                    }
                }
            }

            if (GameHelper.HasRelic<ContentVoiceOfTheDefenderRelic>() && GetTypeline() == Typeline.Creation)
            {
                player.AddSpellPower(1);
            }

            if (GameHelper.HasRelic<ContentDesignSchematicsRelic>() && GetTypeline() == Typeline.Creation)
            {
                AddStats(1, 3);
                AddMaxStamina(1);
            }

            if (GameHelper.HasRelic<ContentInstructionsRelic>() && GetTypeline() == Typeline.Creation)
            {
                AddStats(GetMaxStamina(), GetMaxStamina());
            }
        }
    }

    public virtual void InitializeWithLevel(int level) { }

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

        if (GetTeam() == Team.Player && GameHelper.HasRelic<ContentCallOfTheSeaRelic>())
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
                int healAmount = m_gameTile.GetCostToPass(this) * 10;
                Heal(healAmount);
            }
        }
    }

    public virtual void EndTurn()
    {
        if (GameHelper.HasRelic<ContentPriceOfFreedomRelic>() && GetTeam() == Team.Player && GetCurStamina() == GetMaxStamina())
        {
            AddStats(GetCurStamina(), GetCurStamina());
            EmptyStamina();
        }

        RegenStamina();

        if (GetGameTile().GetTerrain().IsLava() && GetLavawalkKeyword() == null && GetFlyingKeyword() == null)
        {
            GetHit(Constants.LavaFieldDamageDealt);
        }

        if (GameHelper.HasRelic<ContentSecretOfTheDeepRelic>() && GetTeam() == Team.Player && GetTypeline() == Typeline.Humanoid)
        {
            if (m_gameTile != null && m_gameTile.GetTerrain().IsWater())
            {
                Die();
            }
        }
    }

    //============================================================================================================//

    public string GetGuid()
    {
        return m_guid;
    }

    public JsonGameUnitData SaveToJson()
    {
        JsonKeywordHolderData keywordHolderJson = m_keywordHolder.SaveToJson();

        JsonGameUnitData jsonData = new JsonGameUnitData
        {
            baseName = m_name,
            customName = m_customName,
            team = (int)m_team,
            curHealth = m_curHealth,
            curStamina = m_curStamina,
            maxHealth = m_maxHealth,
            staminaRegen = m_staminaRegen,
            maxStamina = m_maxStamina,
            power = m_power,
            typeline = (int)m_typeline,
            keywordHolderJson = keywordHolderJson,
            staminaToAttack = m_staminaToAttack,
            sightRange = m_sightRange,
            guid = GetGuid()
        };

        return jsonData;
    }

    public void LoadFromJson(JsonGameUnitData jsonData)
    {
        m_keywordHolder.RemoveAllKeywords();

        m_customName = jsonData.customName;
        
        m_curHealth = jsonData.curHealth;
        m_team = (Team)jsonData.team;
        m_curStamina = jsonData.curStamina;
        m_maxHealth = jsonData.maxHealth;
        m_staminaRegen = jsonData.staminaRegen;
        m_maxStamina = jsonData.maxStamina;
        m_power = jsonData.power;
        m_typeline = (Typeline)jsonData.typeline;
        m_staminaToAttack = jsonData.staminaToAttack;
        m_sightRange = jsonData.sightRange;
        m_guid = jsonData.guid;

        m_keywordHolder.LoadFromJson((jsonData.keywordHolderJson, this));
    }
}
