using Game.Util;
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

public abstract class GameEntity : GameElementBase, ITurns, ISave, ILoad<JsonGameEntityData>
{
    //General data.  This should be set for every entity
    protected Team m_team;
    protected int m_curHealth;
    protected int m_maxHealth;
    protected int m_curAP;
    protected int m_apRegen;
    protected int m_maxAP;
    protected int m_power;
    protected Typeline m_typeline;

    //Specific data.  Only set if it varies from the default.  Be sure to add to the description so it shows up in the UI.
    protected GameKeywordHolder m_keywordHolder = new GameKeywordHolder();
    protected int m_apToAttack = 2;
    protected int m_sightRange = 3;
    public bool m_shouldAlwaysPassEnemies;

    //Functionality
    protected GameTile m_gameTile;
    public bool m_isDead;
    public UIEntity m_uiEntity;
    public Sprite m_iconWhite;
    protected string m_customName;

    public void CopyOff(GameEntity other)
    {
        m_maxHealth = other.m_maxHealth;
        m_apRegen = other.m_apRegen;
        m_maxAP = other.m_maxAP;
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

    public void SetHealthAPValues()
    {
        m_curHealth = GetMaxHealth();
        m_curAP = GetAPRegen();
        if (m_curAP > m_maxAP)
        {
            m_curAP = m_maxAP;
        }
    }

    public virtual void OnSummon()
    {
        SetHealthAPValues();
        
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

    public virtual void OnOtherSummon(GameEntity other)
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
                    UIHelper.CreateWorldElementNotification("Damage Shield Broken!", true, m_uiEntity.gameObject);
                    m_keywordHolder.RemoveKeyword(damageShieldKeyword);
                }
                else
                {
                    UIHelper.CreateWorldElementNotification("Damage Shield Weakened!", true, m_uiEntity.gameObject);
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
                player.DrawCards(3 * numSoulTrap);
            }

            int numCursedAmulet = GameHelper.RelicCount<ContentCursedAmuletRelic>();
            if (numCursedAmulet > 0 && m_gameTile != null)
            {
                List<GameTile> adjacentTiles = WorldGridManager.Instance.GetSurroundingTiles(m_gameTile, 1);
                for (int i = 0; i < adjacentTiles.Count; i++)
                {
                    if (adjacentTiles[i].IsOccupied() && adjacentTiles[i].m_occupyingEntity.GetTeam() == Team.Enemy && !adjacentTiles[i].m_occupyingEntity.m_isDead)
                    {
                        adjacentTiles[i].m_occupyingEntity.SpendAP(adjacentTiles[i].m_occupyingEntity.GetCurAP());
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

        if (m_uiEntity == Globals.m_selectedEntity)
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
            UIHelper.CreateWorldElementNotification(GetName() + " heals " + realHealVal, true, m_uiEntity.gameObject);
        }

        return realHealVal;
    }

    public virtual bool CanHitEntity(GameEntity other, bool checkRange = true)
    {
        if (GetTeam() == other.GetTeam()) //Can't attack your own team
        {
            return false;
        }

        if (!HasAPToAttack())
        {
            return false;
        }

        if (checkRange && !IsInRangeOfEntity(other))
        {
            return false;
        }

        return true;
    }

    public virtual bool IsInRangeOfGameElement(GameElementBase other)
    {
        switch(other)
        {
            case GameEntity gameEntity:
                return IsInRangeOfEntity(gameEntity);
            case GameBuildingBase gameBuildingBase:
                return IsInRangeOfBuilding(gameBuildingBase);
        }
        return false;
    }

    public virtual bool IsInRangeOfEntity(GameEntity other)
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

    public virtual bool HasAPToAttack()
    {
        if (m_curAP < GetAPToAttack())
        {
            return false;
        }

        return true;
    }

    public void AddAPRegen(int toAdd)
    {
        m_apRegen += toAdd;

        if (!HasCustomName())
        {
            SetCustomName();
        }
    }

    public void AddMaxAP(int toAdd)
    {
        if (!HasCustomName())
        {
            SetCustomName();
        }

        if (m_maxAP >= Constants.MaxTotalAP)
        {
            return;
        }

        m_maxAP += toAdd;
    }

    public int GetSightRange()
    {
        return m_sightRange;
    }

    public virtual int HitEntity(GameEntity other, bool spendAP = true)
    {
        if (spendAP)
        {
            SpendAP(GetAPToAttack());
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

    public virtual int HitBuilding(GameBuildingBase other, bool spendAP = true)
    {
        if (spendAP)
        {
            SpendAP(GetAPToAttack());
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

    protected virtual int GetDamageToDealTo(GameEntity target)
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

    public int GetCurAP()
    {
        return m_curAP;
    }

    public virtual int GetAPToAttack()
    {
        int apToAttack = m_apToAttack - GameHelper.RelicCount<ContentUrbanTacticsRelic>();
        if (GameHelper.GetGameController().m_currentWaveTurn == Globals.m_totemOfTheWolfTurn && GetTeam() == Team.Player)
        {
            apToAttack = Mathf.Max(1, apToAttack - GameHelper.RelicCount<ContentTotemOfTheWolfRelic>());
        }

        return Mathf.Max(1, apToAttack);
    }

    public void FillAP()
    {
        GainAP(GetMaxAP());
    }

    public void EmptyAP()
    {
        SpendAP(GetMaxAP());
    }

    public void GainAP(int toGain)
    {
        m_curAP += toGain;

        if (m_curAP > m_maxAP)
        {
            m_curAP = m_maxAP;
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

        if (!HasCustomName())
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

    public int GetMaxAP()
    {
        int toReturn = m_maxAP;

        if (GetTeam() == Team.Player)
        {
            toReturn += 1 * GameHelper.RelicCount<ContentHourglassOfSpeedRelic>();
        }

        return toReturn;
    }

    public virtual int GetAPRegen()
    {
        int toReturn = m_apRegen;

        if (GetTeam() == Team.Player)
        {
            toReturn += 1 * GameHelper.RelicCount<ContentLegendaryFragmentRelic>();

            toReturn -= 2 * GameHelper.RelicCount<ContentUrbanTacticsRelic>();

            int numGrandPact = GameHelper.RelicCount<ContentGrandPactRelic>();
            if (numGrandPact > 0)
            {
                Dictionary<int, int> numCreatureTypes = new Dictionary<int, int>();
                List<GameEntity> gameEntities = GameHelper.GetPlayer().m_controlledEntities;
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

    public override Color GetColor()
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

        if (WorldGridManager.Instance.GetPathLength(m_gameTile, tile, false, false, false) > m_curAP)
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

        SpendAP(pathCost);
    }

    public void MoveTowards(GameTile tile, int apToUse)
    {
        if (this == Globals.m_focusedDebugEnemyEntity)
        {
            Debug.Log("IS FOCUSED ENEMY");
        }

        if (tile == m_gameTile)
            return;

        if (apToUse <= 0)
            return;

        //TODO: ashulman rethink this. TEMP CODE TO REMOVE END OF TURN LAG WHEN BLOCKING CHOKEPOINT
        //int absoluteDistance = WorldGridManager.Instance.GetPathLength(GetGameTile(), tile, true, true, true);
        //bool letPassEnemies = absoluteDistance >= 8 && (!Constants.FogOfWar || GetGameTile().m_isFog || GetGameTile().m_isSoftFog);

        List<GameTile> pathToTile = WorldGridManager.Instance.CalculateAStarPath(m_gameTile, tile, false, true, true);

        if (pathToTile == null || pathToTile.Count == 0)
            return;

        int apSpent = 0;
        GameTile destinationTile = m_gameTile;
        for (int i = 0; i < pathToTile.Count; i++)
        {
            if (pathToTile[i] == m_gameTile)
                continue;

            if (!pathToTile[i].IsPassable(this, false))
                break;

            int projectedAPSpent = apSpent + pathToTile[i].GetCostToPass(this);

            if (projectedAPSpent > GetCurAP())
                break;

            apSpent += pathToTile[i].GetCostToPass(this);
            destinationTile = pathToTile[i];

            if (apSpent >= apToUse)
                break;
        }

        if (destinationTile == m_gameTile)
            return;

        if (destinationTile.IsOccupied())
            return;

        if (m_uiEntity != null)
        {
            m_uiEntity.MoveTo(destinationTile);
        }
    }

    public void SpendAP(int toSpend)
    {
        m_curAP -= toSpend;

        if (m_curAP < 0)
        {
            m_curAP = 0;
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

    private void RegenAP()
    {
        int apToRegen = GetAPRegen();
        //Adding one because regen happens at end of turn and this should happen just before the totem of the wolf. 
        if (GameHelper.GetGameController().m_currentWaveTurn + 1 == Globals.m_totemOfTheWolfTurn)
        {
            apToRegen *= (1 + GameHelper.RelicCount<ContentTotemOfTheWolfRelic>());
        }
        
        GainAP(apToRegen);
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
        RegenAP();

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
                int healAmount = m_gameTile.GetTerrain().GetCostToPass(this) * medkitCount * 3;
                Heal(healAmount);
            }
        }
    }

    //============================================================================================================//

    public JsonGameEntityData SaveToJsonAsJson()
    {
        string keywordHolderJson = m_keywordHolder.SaveToJsonAsString();

        JsonGameEntityData jsonData = new JsonGameEntityData
        {
            name = m_name,
            team = (int)m_team,
            curHealth = m_curHealth,
            curAP = m_curAP,
            maxHealth = m_maxHealth,
            apRegen = m_apRegen,
            maxAP = m_maxAP,
            power = m_power,
            typeline = (int)m_typeline,
            keywordHolderJson = keywordHolderJson,
            apToAttack = m_apToAttack,
            sightRange = m_sightRange
        };

        return jsonData;
    }

    public string SaveToJsonAsString()
    {
        JsonGameEntityData jsonData = SaveToJsonAsJson();

        var export = JsonUtility.ToJson(jsonData);

        return export;
    }

    public void LoadFromJson(JsonGameEntityData jsonData)
    {
        m_curHealth = jsonData.curHealth;
        m_team = (Team)jsonData.team;
        m_curAP = jsonData.curAP;
        m_maxHealth = jsonData.maxHealth;
        m_apRegen = jsonData.apRegen;
        m_maxAP = jsonData.maxAP;
        m_power = jsonData.power;
        m_typeline = (Typeline)jsonData.typeline;
        m_apToAttack = jsonData.apToAttack;
        m_sightRange = jsonData.sightRange;

        JsonKeywordHolderData jsonKeywordHolderData = JsonUtility.FromJson<JsonKeywordHolderData>(jsonData.keywordHolderJson);
        m_keywordHolder.LoadFromJson((jsonKeywordHolderData, this));
    }
}
