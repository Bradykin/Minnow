﻿using Game.Util;
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

public abstract class GameUnit : GameElementBase, ITurns, ISave, ILoad<JsonGameUnitData>
{
    //General data.  This should be set for every entity
    protected Team m_team;
    protected int m_curHealth;
    protected int m_maxHealth;
    protected int m_curStamina;
    protected int m_staminaRegen;
    protected int m_maxStamina;
    protected int m_power;
    protected Typeline m_typeline;

    //Specific data.  Only set if it varies from the default.  Be sure to add to the description so it shows up in the UI.
    protected GameKeywordHolder m_keywordHolder = new GameKeywordHolder();
    protected int m_staminaToAttack = 2;
    protected int m_sightRange = 3;
    public bool m_shouldAlwaysPassEnemies;

    //Functionality
    protected GameTile m_gameTile;
    public bool m_isDead;
    public UIUnit m_worldUnit;
    public Sprite m_iconWhite;
    protected string m_customName;

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
        m_icon = UIHelper.GetIconEntity(m_name);
        m_iconWhite = UIHelper.GetIconEntity(m_name + "W");
    }

    public void SetHealthStaminaValues()
    {
        m_curHealth = GetMaxHealth();
        m_curStamina = GetStaminaRegen();
        if (m_curStamina > m_maxStamina)
        {
            m_curStamina = m_maxStamina;
        }
    }

    public virtual void OnSummon()
    {
        SetHealthStaminaValues();
        
        List<GameSummonKeyword> summonKeywords = m_keywordHolder.GetKeywords<GameSummonKeyword>();
        for (int i = 0; i < summonKeywords.Count; i++)
        {
            summonKeywords[i].DoAction();
        }

        if (GetTeam() == Team.Player && GetTypeline() == Typeline.Monster)
        {
            int numTideOfMonsters = GameHelper.RelicCount<ContentLegacyOfMonstersRelic>();
            if (numTideOfMonsters > 0)
            {
                AddPower(numTideOfMonsters);
            }
        }

        if (GetTeam() == Team.Player)
        {
            GameHelper.GetPlayer().InformWasSummoned(this);
        }
    }

    public virtual void OnOtherSummon(GameUnit other)
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

    public virtual int GetHit(int damage)
    {
        if (m_isDead)
        {
            return 0;
        }

        damage = CalculateDamageAmount(damage);

        if (damage <= 0)
        {
            UIHelper.CreateWorldElementNotification(GetName() + " takes no damage from the hit!", false, m_gameTile.GetWorldTile().gameObject);
            return 0;
        }

        GameDamageShieldKeyword damageShieldKeyword = m_keywordHolder.GetKeyword<GameDamageShieldKeyword>();
        if (damageShieldKeyword != null)
        {
            if (damageShieldKeyword.m_numShields > 0)
            {
                damageShieldKeyword.DecreaseShield(1);
                if (damageShieldKeyword.m_numShields == 0)
                {
                    UIHelper.CreateWorldElementNotification("Damage Shield Broken!", true, m_worldUnit.gameObject);
                    m_keywordHolder.RemoveKeyword(damageShieldKeyword);
                }
                else
                {
                    UIHelper.CreateWorldElementNotification("Damage Shield Weakened!", true, m_worldUnit.gameObject);
                }
                return 0;
            }
        }

        m_curHealth -= damage;

        List<GameEnrageKeyword> enrageKeywords = m_keywordHolder.GetKeywords<GameEnrageKeyword>();
        int numBestialWrath = GameHelper.RelicCount<ContentBestialWrathRelic>();

        for (int i = 0; i < enrageKeywords.Count; i++)
        {
            enrageKeywords[i].DoAction(damage);
            if (GetTypeline() == Typeline.Monster)
            {
                for (int k = 0; k < numBestialWrath; k++)
                {
                    enrageKeywords[i].DoAction(damage);
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

        GameBrittleKeyword brittleKeyword = m_keywordHolder.GetKeyword<GameBrittleKeyword>();
        if (brittleKeyword != null)
        {
            damage += brittleKeyword.m_amount;
        }

        if (damage < 0)
        {
            damage = 0;
        }

        return damage;
    }

    protected virtual bool ShouldRevive()
    {
        bool shouldRevive = false;

        if (GetTeam() == Team.Player)
        {
            for (int i = 0; i < GameHelper.RelicCount<ContentDestinyRelic>(); i++)
            {
                shouldRevive = GameHelper.PercentChanceRoll(33);
            }
        }

        return shouldRevive;
    }

    public virtual void Die()
    {
        if (m_isDead)
        {
            return;
        }

        bool willSetDead = true;

        bool shouldRevive = ShouldRevive();

        if (shouldRevive)
        {
            m_curHealth = 1;
            UIHelper.CreateWorldElementNotification(GetName() + " resists death.", true, m_gameTile.GetWorldTile().gameObject);
            return;
        }

        m_curHealth = 0;

        GamePlayer player = GameHelper.GetPlayer();
        if (player == null)
        {
            Debug.LogError("Cannot kill entity as player doesn't exist.");
            return;
        }

        List<GameDeathKeyword> deathKeywords = m_keywordHolder.GetKeywords<GameDeathKeyword>();
        for (int i = 0; i < deathKeywords.Count; i++)
        {
            deathKeywords[i].DoAction();
        }

        if (GetTeam() == Team.Enemy)
        {
            int numSkulls = GameHelper.RelicCount<ContentMorlemainsSkullRelic>();
            if (numSkulls > 0)
            {
                player.AddEnergy(numSkulls);
            }

            int numCatchers = GameHelper.RelicCount<ContentSpiritCatcherRelic>();
            if (numCatchers > 0)
            {
                player.DrawCards(numCatchers);
            }
        }
        else if (GetTeam() == Team.Player)
        {
            int numSoulTrap = GameHelper.RelicCount<ContentSoulTrapRelic>();
            if (numSoulTrap > 0)
            {
                if (GameHelper.GetGameController().m_currentTurn == player)
                {
                    player.DrawCards(3 * numSoulTrap);
                }
                else
                {
                    player.AddScheduledAction(ScheduledActionTime.StartOfTurn, new GameDrawCardAction(3 * numSoulTrap));
                }
            }

            int numCursedAmulet = GameHelper.RelicCount<ContentCursedAmuletRelic>();
            if (numCursedAmulet > 0 && m_gameTile != null)
            {
                List<GameTile> adjacentTiles = WorldGridManager.Instance.GetSurroundingTiles(m_gameTile, 1);
                for (int i = 0; i < adjacentTiles.Count; i++)
                {
                    if (adjacentTiles[i].IsOccupied() && adjacentTiles[i].m_occupyingUnit.GetTeam() == Team.Enemy && !adjacentTiles[i].m_occupyingUnit.m_isDead)
                    {
                        adjacentTiles[i].m_occupyingUnit.SpendStamina(adjacentTiles[i].m_occupyingUnit.GetCurStamina());
                    }
                }
            }

            if (GameHelper.RelicCount<ContentDesignSchematicsRelic>() > 0 && GetTypeline() == Typeline.Creation)
            {
                GameCardEntityBase cardFromEntity = GameCardFactory.GetCardFromEntity(this);
                GameHelper.GetPlayer().m_curDeck.AddToDiscard(cardFromEntity);
                willSetDead = false;
            }
            WorldController.Instance.m_gameController.m_player.m_controlledEntities.Remove(this);
        }

        UIHelper.CreateWorldElementNotification(GetName() + " dies.", false, m_gameTile.GetWorldTile().gameObject);

        if (m_worldUnit == Globals.m_selectedEntity)
        {
            WorldGridManager.Instance.ClearAllTilesMovementRange();
        }
        m_gameTile.GetWorldTile().RecycleEntity();
        UITooltipController.Instance.ClearTooltipStack();

        m_isDead = willSetDead;
    }

    //Returns the amount actually healed
    public virtual int Heal(int toHeal)
    {
        int maxHealth = GetMaxHealth();

        int realHealVal = toHeal;
        if (m_curHealth + toHeal > maxHealth)
        {
            realHealVal = maxHealth - m_curHealth;
        }

        m_curHealth += toHeal;

        if (m_curHealth >= maxHealth)
        {
            m_curHealth = maxHealth;
        }

        if (realHealVal > 0)
        {
            UIHelper.CreateWorldElementNotification(GetName() + " heals " + realHealVal, true, m_worldUnit.gameObject);
        }

        return realHealVal;
    }

    public virtual bool CanHitEntity(GameUnit other, bool checkRange = true)
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

    public virtual bool IsInRangeOfGameElement(GameElementBase other)
    {
        switch(other)
        {
            case GameUnit gameEntity:
                return IsInRangeOfUnit(gameEntity);
            case GameBuildingBase gameBuildingBase:
                return IsInRangeOfBuilding(gameBuildingBase);
        }
        return false;
    }

    public virtual bool IsInRangeOfUnit(GameUnit other)
    {
        List <GameTile> tiles = WorldGridManager.Instance.CalculateAStarPath(m_gameTile, other.m_gameTile, true, false, false);
        
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
        List<GameSpellcraftKeyword> spellcraftKeywords = m_keywordHolder.GetKeywords<GameSpellcraftKeyword>();

        if (spellcraftKeywords.Count == 0)
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

        for (int i = 0; i < spellcraftKeywords.Count; i++)
        {
            spellcraftKeywords[i].DoAction();
        }
    }

    public void TriggerKnowledgeable()
    {
        List<GameKnowledgeableKeyword> knowledgeableKeywords = m_keywordHolder.GetKeywords<GameKnowledgeableKeyword>();
        for (int i = 0; i < knowledgeableKeywords.Count; i++)
        {
            knowledgeableKeywords[i].DoAction();
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
        if (!HasCustomName())
        {
            SetCustomName();
        }

        if (m_maxStamina >= Constants.MaxTotalStamina)
        {
            return;
        }

        m_maxStamina += toAdd;
    }

    public int GetSightRange()
    {
        return m_sightRange;
    }

    public virtual int HitUnit(GameUnit other, bool spendStamina = true)
    {
        if (spendStamina)
        {
            SpendStamina(GetStaminaToAttack());
        }

        int damageTaken = other.GetHit(GetDamageToDealTo(other));

        List<GameMomentumKeyword> momentumKeywords = m_keywordHolder.GetKeywords<GameMomentumKeyword>();
        int numBestialWrath = GameHelper.RelicCount<ContentBestialWrathRelic>();

        for (int i = 0; i < momentumKeywords.Count; i++)
        {
            momentumKeywords[i].DoAction();
            if (GetTypeline() == Typeline.Monster)
            {
                for (int k = 0; k < numBestialWrath; k++)
                {
                    momentumKeywords[i].DoAction();
                }
            }
        }

        if (other.m_isDead)
        {
            List<GameVictoriousKeyword> victoriousKeywords = m_keywordHolder.GetKeywords<GameVictoriousKeyword>();

            for (int i = 0; i < victoriousKeywords.Count; i++)
            {
                victoriousKeywords[i].DoAction();
                if (GetTypeline() == Typeline.Monster)
                {
                    for (int k = 0; k < numBestialWrath; k++)
                    {
                        victoriousKeywords[i].DoAction();
                    }
                }
            }
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

        List<GameMomentumKeyword> momentumKeywords = m_keywordHolder.GetKeywords<GameMomentumKeyword>();
        int numBestialWrath = GameHelper.RelicCount<ContentBestialWrathRelic>();

        for (int i = 0; i < momentumKeywords.Count; i++)
        {
            momentumKeywords[i].DoAction();
            if (GetTypeline() == Typeline.Monster)
            {
                for (int k = 0; k < numBestialWrath; k++)
                {
                    momentumKeywords[i].DoAction();
                }
            }
        }

        if (other.m_isDestroyed)
        {
            List<GameVictoriousKeyword> victoriousKeywords = m_keywordHolder.GetKeywords<GameVictoriousKeyword>();
            
            for (int i = 0; i < victoriousKeywords.Count; i++)
            {
                victoriousKeywords[i].DoAction();
                if (GetTypeline() == Typeline.Monster)
                {
                    for (int k = 0; k < numBestialWrath; k++)
                    {
                        victoriousKeywords[i].DoAction();
                    }
                }
            }
        }

        return 0;
    }

    protected virtual int GetDamageToDealTo(GameUnit target)
    {
        return GetPower();
    }

    protected virtual int GetDamageToDealTo(GameBuildingBase target)
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
        int staminaToAttack = m_staminaToAttack - GameHelper.RelicCount<ContentUrbanTacticsRelic>();
        if (GameHelper.GetGameController().m_currentWaveTurn == Globals.m_totemOfTheWolfTurn && GetTeam() == Team.Player)
        {
            staminaToAttack = Mathf.Max(1, staminaToAttack - GameHelper.RelicCount<ContentTotemOfTheWolfRelic>());
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

    public void GainStamina(int toGain)
    {
        m_curStamina += toGain;

        if (m_curStamina > m_maxStamina)
        {
            m_curStamina = m_maxStamina;
        }

        UIHelper.ReselectEntity();
    }

    public void Reset()
    {
        m_isDead = false;
    }

    public void AddKeyword(GameKeywordBase newKeyword)
    {
        m_keywordHolder.m_keywords.Add(newKeyword);

        if (!HasCustomName() && !(newKeyword is GameDamageShieldKeyword))
        {
            SetCustomName();
        }
    }

    public T GetKeyword<T>()
    {
        return m_keywordHolder.GetKeyword<T>();
    }

    public List<T> GetKeywords<T>()
    {
        return m_keywordHolder.GetKeywords<T>();
    }

    public List<GameKeywordBase> GetKeywords()
    {
        return m_keywordHolder.GetKeywords();
    }

    public GameKeywordHolder GetKeywordHolderForRead()
    {
        return m_keywordHolder;
    }

    public virtual int GetRange()
    {
        GameRangeKeyword rangeKeyword = m_keywordHolder.GetKeyword<GameRangeKeyword>();
        if (rangeKeyword != null)
        {
            int range = rangeKeyword.m_range;

            if (m_gameTile != null)
            {
                int terrainRange = m_gameTile.GetTerrain().m_rangeModifier;

                if (terrainRange > 0 && GetTeam() == Team.Player && GameHelper.RelicCount<ContentNaturalProtectionRelic>() > 0)
                {
                    terrainRange += terrainRange * GameHelper.RelicCount<ContentNaturalProtectionRelic>();
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
            toReturn += 2 * GameHelper.RelicCount<ContentWolvenFangRelic>();
            toReturn -= 2 * GameHelper.RelicCount<ContentLegendaryFragmentRelic>();

            if (GetRange() > 1)
            {
                toReturn += Globals.m_fletchingCount;
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
            toReturn += 6 * GameHelper.RelicCount<ContentOrbOfHealthRelic>();
        }

        return toReturn;
    }

    public void AddMaxHealth(int toAdd, bool shouldIncreaseCurrent = true)
    {
        m_maxHealth += toAdd;

        if (toAdd > 0 && shouldIncreaseCurrent)
        {
            m_curHealth += toAdd;
        }

        if (!HasCustomName())
        {
            SetCustomName();
        }
    }

    public int GetMaxStamina()
    {
        int toReturn = m_maxStamina;

        if (GetTeam() == Team.Player)
        {
            toReturn += 1 * GameHelper.RelicCount<ContentHourglassOfSpeedRelic>();
        }

        return toReturn;
    }

    public virtual int GetStaminaRegen()
    {
        int toReturn = m_staminaRegen;

        if (GetTeam() == Team.Player)
        {
            toReturn += 1 * GameHelper.RelicCount<ContentLegendaryFragmentRelic>();

            toReturn -= 2 * GameHelper.RelicCount<ContentUrbanTacticsRelic>();

            int numGrandPact = GameHelper.RelicCount<ContentGrandPactRelic>();
            if (numGrandPact > 0)
            {
                Dictionary<int, int> numCreatureTypes = new Dictionary<int, int>();
                List<GameUnit> gameEntities = GameHelper.GetPlayer().m_controlledEntities;
                for (int i = 0; i < gameEntities.Count; i++)
                {
                    int typelineInt = (int)gameEntities[i].GetTypeline();
                    if (!numCreatureTypes.ContainsKey(typelineInt))
                    {
                        numCreatureTypes.Add(typelineInt, 1);
                    }
                    else
                    {
                        numCreatureTypes[typelineInt]++;
                    }
                }

                bool hasAll = true;
                for (int i = 0; i < (int)Typeline.Count; i++)
                {
                    if (!numCreatureTypes.ContainsKey(0))
                    {
                        hasAll = false;
                        break;
                    }
                }

                if (hasAll)
                {
                    toReturn += numGrandPact;
                }
            }

            if (this is ContentDwarvenSoldier)
            {
                toReturn += GameHelper.RelicCount<ContentTraditionalMethodsRelic>();
            }
            
            if (m_typeline == Typeline.Monster)
            {
                toReturn += GameHelper.RelicCount<ContentLegacyOfMonstersRelic>();
            }
        }
        
        if (GetTeam() == Team.Enemy)
        {
            if (GameHelper.IsValidChaosLevel(2))
            {
                toReturn += 1;
            }
        }
        toReturn += 1 * GameHelper.RelicCount<ContentSecretSoupRelic>();

        return toReturn;
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

    public void MoveTo(GameTile tile)
    {
        if (tile == m_gameTile)
            return;

        int pathCost = WorldGridManager.Instance.GetPathLength(m_gameTile, tile, false, false, false);

        m_gameTile.ClearEntity();
        tile.PlaceEntity(this);

        SpendStamina(pathCost);
    }

    public GameTile GetMoveTowardsDestination(GameTile tile, int staminaToUse)
    {
        if (this == Globals.m_focusedDebugEnemyEntity)
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
        }
        else
        {
            return destinationTile;
        }

        return m_gameTile;
    }

    public void SpendStamina(int toSpend)
    {
        m_curStamina -= toSpend;

        if (m_curStamina < 0)
        {
            m_curStamina = 0;
        }

        UIHelper.ReselectEntity();
    }

    public virtual string GetDesc()
    {
        string descString = "";
        if (m_desc != null && m_desc != "")
        {
            descString += m_desc;
        }

        return descString;
    }

    private void RegenStamina()
    {
        int staminaToRegen = GetStaminaRegen();
        //Adding one because regen happens at end of turn and this should happen just before the totem of the wolf. 
        if (GameHelper.GetGameController().m_currentWaveTurn + 1 == Globals.m_totemOfTheWolfTurn && GetTeam() == Team.Player)
        {
            staminaToRegen *= (1 + GameHelper.RelicCount<ContentTotemOfTheWolfRelic>());
        }
        
        GainStamina(staminaToRegen);
    }

    public void AddPower(int m_toAdd)
    {
        m_power += m_toAdd;

        if (!HasCustomName())
        {
            SetCustomName();
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

        m_customName = GameNamesFactory.GetCustomEntityName(m_typeline);
    }

    public string GetName()
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

    //============================================================================================================//

    public virtual void StartTurn() { }

    public virtual void EndTurn()
    {
        RegenStamina();

        List<GameRegenerateKeyword> regenKeywords = m_keywordHolder.GetKeywords<GameRegenerateKeyword>();
        for (int i = 0; i < regenKeywords.Count; i++)
        {
            Heal(regenKeywords[i].m_regenVal);
        }

        if (GetTypeline() == Typeline.Humanoid)
        {
            int medkitCount = GameHelper.RelicCount<ContentMedKitRelic>();
            if (medkitCount > 0 && GetTeam() == Team.Player)
            {
                int healAmount = m_gameTile.GetCostToPass(this) * medkitCount * 3;
                Heal(healAmount);
            }
        }
    }

    //============================================================================================================//

    public JsonGameUnitData SaveToJsonAsJson()
    {
        string keywordHolderJson = m_keywordHolder.SaveToJsonAsString();

        JsonGameUnitData jsonData = new JsonGameUnitData
        {
            name = m_name,
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
            sightRange = m_sightRange
        };

        return jsonData;
    }

    public string SaveToJsonAsString()
    {
        JsonGameUnitData jsonData = SaveToJsonAsJson();

        var export = JsonUtility.ToJson(jsonData);

        return export;
    }

    public void LoadFromJson(JsonGameUnitData jsonData)
    {
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

        JsonKeywordHolderData jsonKeywordHolderData = JsonUtility.FromJson<JsonKeywordHolderData>(jsonData.keywordHolderJson);
        m_keywordHolder.LoadFromJson((jsonKeywordHolderData, this));
    }
}
