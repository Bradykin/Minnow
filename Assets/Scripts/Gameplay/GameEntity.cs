using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team : int
{
    Player,
    Enemy
}

public enum Typeline : int
{
    Humanoid,
    Mystic,
    Monster,
    Construct
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
    protected int m_sightRange = 2;

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

        m_keywordHolder = other.m_keywordHolder.Clone(other);

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

    public virtual void OnSummon()
    {
        m_curHealth = GetMaxHealth();
        m_curAP = GetAPRegen();
        if (m_curAP > m_maxAP)
        {
            m_curAP = m_maxAP;
        }

        GameSummonKeyword summonKeyword = m_keywordHolder.GetKeyword<GameSummonKeyword>();
        if (summonKeyword != null)
        {
            summonKeyword.DoAction();
        }

        if (GetTeam() == Team.Player && GetTypeline() == Typeline.Monster)
        {
            int numTideOfMonsters = GameHelper.RelicCount<ContentLegacayOfMonstersRelic>();
            if (numTideOfMonsters > 0)
            {
                AddPower(numTideOfMonsters);
            }
        }
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

        bool ignoreTileDamageReduction = false;

        if (GetTeam() == Team.Enemy && GameHelper.RelicCount<ContentNaturalDaggerRelic>() > 0)
        {
            ignoreTileDamageReduction = true;
        }

        if (!ignoreTileDamageReduction)
        {
            damage -= m_gameTile.GetDamageReduction(this);
        }

        if (damage < 0)
        {
            damage = 0;
            return damage;
        }

        m_curHealth -= damage;

        GameEnrageKeyword enrageKeyword = m_keywordHolder.GetKeyword<GameEnrageKeyword>();
        if (enrageKeyword != null)
        {
            enrageKeyword.DoAction();
        }

        if (m_curHealth <= 0)
        {
            Die();
        }
        else
        {
            UIHelper.CreateWorldElementNotification(GetName() + " takes " + damage + " damage!", false, m_gameTile.GetWorldTile());
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
                shouldRevive = GameHelper.PercentChanceRoll(25);
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

        bool shouldRevive = ShouldRevive();

        if (shouldRevive)
        {
            m_curHealth = 1;
            UIHelper.CreateWorldElementNotification(GetName() + " resists death.", true, m_gameTile.GetWorldTile());
            return;
        }

        m_curHealth = 0;

        GamePlayer player = GameHelper.GetPlayer();
        if (player == null)
        {
            Debug.LogError("Cannot kill entity as player doesn't exist.");
            return;
        }

        GameDeathKeyword deathKeyword = m_keywordHolder.GetKeyword<GameDeathKeyword>();
        if (deathKeyword != null)
        {
            deathKeyword.DoAction();
        }

        for (int i = 0; i < player.m_controlledBuildings.Count; i++)
        {
            if (player.m_controlledBuildings[i] is ContentGraveyardBuilding && !player.m_controlledBuildings[i].m_isDestroyed)
            {
                int goldToGain = ((ContentGraveyardBuilding)player.m_controlledBuildings[i]).m_goldToGain;
                player.m_wallet.AddResources(new GameWallet(goldToGain));
            }
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
            int numRelics = GameHelper.RelicCount<ContentSoulTrapRelic>();
            if (numRelics > 0)
            {
                player.DrawCards(numRelics);
            }

            if (GameHelper.RelicCount<ContentDesignSchematicsRelic>() > 0 && GetTypeline() == Typeline.Construct)
            {
                GameCard cardFromEntity = GameCardFactory.GetCardFromEntity(this);
                GameHelper.GetPlayer().m_curDeck.AddToDiscard(cardFromEntity);
            }
            WorldController.Instance.m_gameController.m_player.m_controlledEntities.Remove(this);
        }

        UIHelper.CreateWorldElementNotification(GetName() + " dies.", false, m_gameTile.GetWorldTile());

        WorldGridManager.Instance.ClearAllTilesMovementRange();
        m_gameTile.GetWorldTile().RecycleEntity();

        m_isDead = true;
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
            UIHelper.CreateWorldElementNotification(GetName() + " heals " + realHealVal, true, m_uiEntity);
        }

        return realHealVal;
    }

    public virtual bool CanHitEntity(GameEntity other)
    {
        if (GetTeam() == other.GetTeam()) //Can't attack your own team
        {
            return false;
        }

        if (!HasAPToAttack())
        {
            return false;
        }

        if (!IsInRangeOfEntity(other))
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

    public void SpellCast()
    {
        GameSpellcraftKeyword spellcraftKeyword = m_keywordHolder.GetKeyword<GameSpellcraftKeyword>();
        if (spellcraftKeyword != null)
        {
            spellcraftKeyword.DoAction();
        }
    }

    public void DrawCard()
    {
        GameKnowledgeableKeyword knowledgeableKeyword = m_keywordHolder.GetKeyword<GameKnowledgeableKeyword>();
        if (knowledgeableKeyword != null)
        {
            knowledgeableKeyword.DoAction();
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
        m_maxAP += toAdd;

        if (!HasCustomName())
        {
            SetCustomName();
        }
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

        GameMomentumKeyword momentumKeyword = m_keywordHolder.GetKeyword<GameMomentumKeyword>();
        if (momentumKeyword != null)
        {
            momentumKeyword.DoAction();
        }

        if (other.m_isDead)
        {
            GameVictoriousKeyword victoriousKeyword = m_keywordHolder.GetKeyword<GameVictoriousKeyword>();
            if (victoriousKeyword != null)
            {
                victoriousKeyword.DoAction();
            }

            if (GetTeam() == Team.Enemy && GameHelper.RelicCount<ContentCursedAmuletRelic>() > 0)
            {
                SpendAP(GetCurAP());
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

        GameMomentumKeyword momentumKeyword = m_keywordHolder.GetKeyword<GameMomentumKeyword>();
        if (momentumKeyword != null)
        {
            momentumKeyword.DoAction();
        }

        if (other.m_isDestroyed)
        {
            GameVictoriousKeyword victoriousKeyword = m_keywordHolder.GetKeyword<GameVictoriousKeyword>();
            if (victoriousKeyword != null)
            {
                victoriousKeyword.DoAction();
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

    public GameKeywordHolder GetKeywordHolderForRead()
    {
        return m_keywordHolder;
    }

    public virtual int GetRange()
    {
        GameRangeKeyword rangeKeyword = m_keywordHolder.GetKeyword<GameRangeKeyword>();
        if (rangeKeyword != null)
        {
            return rangeKeyword.m_range + m_gameTile.GetTerrain().m_rangeModifier;
        }

        return 1;
    }

    public virtual int GetPower()
    {
        int toReturn = m_power;

        if (GetTeam() == Team.Player)
        {
            toReturn += 1 * GameHelper.RelicCount<ContentWolvenFangRelic>();
            toReturn -= 1 * GameHelper.RelicCount<ContentLegendaryFragmentRelic>();
        }

        if (m_keywordHolder.GetKeyword<GameRangeKeyword>() != null)
        {
            toReturn += Globals.m_fletchingCount;
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
            toReturn += 3 * GameHelper.RelicCount<ContentOrbOfHealthRelic>();
        }

        return toReturn;
    }

    public void AddMaxHealth(int toAdd)
    {
        m_maxHealth += toAdd;

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

            int numAllianceOfTheTribes = GameHelper.RelicCount<ContentGrandPactRelic>();
            if (numAllianceOfTheTribes > 0)
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

                if (numCreatureTypes.ContainsKey(0) && numCreatureTypes.ContainsKey(1) && numCreatureTypes.ContainsKey(2) && numCreatureTypes.ContainsKey(3))
                {
                    toReturn += 2 * numAllianceOfTheTribes;
                }
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
        
        SpendAP(WorldGridManager.Instance.GetPathLength(m_gameTile, tile, false, false, false));

        m_gameTile.ClearEntity();
        tile.PlaceEntity(this);
    }

    public void MoveTowards(GameTile tile, int apToUse)
    {
        if (tile == m_gameTile)
            return;

        if (apToUse <= 0)
            return;

        List<GameTile> pathToTile = WorldGridManager.Instance.CalculateAStarPath(m_gameTile, tile, false, true, true);

        if (pathToTile == null || pathToTile.Count == 0)
            return;

        int apSpent = 0;
        GameTile destinationTile = m_gameTile;
        for (int i = 0; i < pathToTile.Count; i++)
        {
            if (pathToTile[i] == m_gameTile)
                continue;

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

        m_uiEntity.MoveTo(destinationTile);
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

    public string GetDesc()
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
        if (GameHelper.GetPlayer().m_currentWaveTurn == Globals.m_totemOfTheWolfTurn)
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

        GameRegenerateKeyword regenKeyword = m_keywordHolder.GetKeyword<GameRegenerateKeyword>();
        if (regenKeyword != null)
        {
            int regenValue = Heal(regenKeyword.m_regenVal);
        }

        if (GetTypeline() == Typeline.Humanoid)
        {
            int medkitCount = GameHelper.RelicCount<ContentMedKitRelic>();
            if (medkitCount > 0 && GetTeam() == Team.Player)
            {
                int healAmount = m_gameTile.GetTerrain().GetCostToPass(this) * medkitCount;
                Heal(healAmount);
            }
        }
    }

    //============================================================================================================//

    public string SaveToJson()
    {
        string keywordHolderJson = m_keywordHolder.SaveToJson();
        
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
