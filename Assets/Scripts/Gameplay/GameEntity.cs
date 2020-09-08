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
    None,
    Humanoid,
    Mystic,
    Monster,
    Construct,
    Legend
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
    public GameTile m_curTile;
    public bool m_isDead;
    public UIEntity m_uiEntity;
    public Sprite m_iconWhite;

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
            damage -= m_curTile.GetDamageReduction();
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
            UIHelper.CreateWorldElementNotification(m_name + " stands back up from a mortal wound.", true, m_curTile.m_curTile);
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
            if (player.m_controlledBuildings[i] is ContentGraveyardBuilding)
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
            WorldController.Instance.m_gameController.m_player.m_controlledEntities.Remove(this);
        }

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

        return realHealVal;
    }

    public virtual bool CanHitEntity(GameEntity other)
    {
        if (!IsInRangeOfEntity(other))
        {
            return false;
        }

        if (!HasAPToAttack())
        {
            return false;
        }

        if (GetTeam() == other.GetTeam()) //Can't attack your own team
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
        List <GameTile> tiles = WorldGridManager.Instance.CalculateAStarPath(m_curTile, other.m_curTile, true, false);
        
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
        List<GameTile> tiles = WorldGridManager.Instance.CalculateAStarPath(m_curTile, other.m_curTile.GetGameTile(), true, false);

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
        if (m_curAP < m_apToAttack)
        {
            return false;
        }

        return true;
    }

    public void AddAPRegen(int toAdd)
    {
        m_apRegen += toAdd;
    }

    public void AddMaxAP(int toAdd)
    {
        m_maxAP += toAdd;
    }

    public int GetSightRange()
    {
        return m_sightRange;
    }

    public virtual int HitEntity(GameEntity other)
    {
        SpendAP(m_apToAttack);
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
        }

        return damageTaken;
    }

    public virtual int HitBuilding(GameBuildingBase other)
    {
        SpendAP(m_apToAttack);
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
        return m_apToAttack;
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

    public GameKeywordHolder GetKeywordHolder()
    {
        return m_keywordHolder;
    }

    public virtual int GetRange()
    {
        GameRangeKeyword rangeKeyword = m_keywordHolder.GetKeyword<GameRangeKeyword>();
        if (rangeKeyword != null)
        {
            return rangeKeyword.m_range;
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

        toReturn += 1 * GameHelper.RelicCount<ContentSecretSoupRelic>();
        toReturn += 1 * GameHelper.RelicCount<ContentLegendaryFragmentRelic>();

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

        if (!tile.IsPassable(this))
        {
            return false;
        }

        if (WorldGridManager.Instance.GetPathLength(m_curTile, tile, false, false) > m_curAP)
        {
            return false;
        }
        
        return true;
    }

    public void MoveTo(GameTile tile)
    {
        if (tile == m_curTile)
            return;
        
        SpendAP(WorldGridManager.Instance.GetPathLength(m_curTile, tile, false, false));

        m_curTile.ClearEntity();
        tile.PlaceEntity(this);
    }

    public void MoveTowards(GameTile tile, int apToUse)
    {
        if (tile == m_curTile)
            return;

        if (apToUse <= 0)
            return;

        List<GameTile> pathToTile = WorldGridManager.Instance.CalculateAStarPath(m_curTile, tile, false, true);

        if (pathToTile == null || pathToTile.Count == 0)
            return;

        int apSpent = 0;
        GameTile destinationTile = m_curTile;
        for (int i = 0; i < pathToTile.Count; i++)
        {
            if (pathToTile[i] == m_curTile)
                continue;

            int projectedAPSpent = apSpent + pathToTile[i].GetCostToPass(this);

            if (projectedAPSpent > GetCurAP() || projectedAPSpent > apToUse)
                break;

            apSpent += pathToTile[i].GetCostToPass(this);
            destinationTile = pathToTile[i];

            if (apSpent < apToUse)
                break;
        }

        if (destinationTile == m_curTile)
            return;

        if (destinationTile.m_occupyingEntity != null)
            return;

        MoveTo(destinationTile);
    }

    public void SpendAP(int toSpend)
    {
        m_curAP -= toSpend;

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
        GainAP(GetAPRegen());
    }

    public void AddPower(int m_toAdd)
    {
        m_power += m_toAdd;
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
            if (regenValue > 0)
            {
                UIHelper.CreateWorldElementNotification(m_name + " regenerates " + regenValue, true, m_uiEntity);
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
