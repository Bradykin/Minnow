﻿using Game.Util;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum BuildingType
{
    Critical,
    Economic,
    Defensive,
    Wall
}

public abstract class GameBuildingBase : GameElementBase, ITurns, ISave<JsonGameBuildingData>, ILoad<JsonGameBuildingData>
{
    protected GameTile m_gameTile;

    public Sprite m_destroyedIcon;
    public int m_curHealth;
    public int m_maxHealth;
    public BuildingType m_buildingType;

    public int m_sightRange = 3;
    public int m_range = 0;

    public bool m_isDestroyed;
    public bool m_expandsPlaceRange = false;
    public bool m_spellcraftBuilding = false;

    private Sprite m_iconWhite;

    protected Team m_team;
    protected GameWallet m_cost;

    public void LateInit()
    {
        m_icon = UIHelper.GetIconBuilding(m_name);
        m_iconWhite = UIHelper.GetIconBuilding(m_name + "W");
        m_destroyedIcon = UIHelper.GetIconBuilding(m_name + "D");
        m_curHealth = m_maxHealth;

        m_team = Team.Player;
    }

    public Team GetTeam()
    {
        return m_team;
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

    public virtual Sprite GetIcon()
    {
        if (m_isDestroyed)
        {
            return m_destroyedIcon;
        }
        else
        {
            return m_icon;
        }
    }

    public virtual string GetDesc()
    {
        return m_desc;
    }

    public GameWallet GetCost()
    {
        int returnCost = m_cost.m_gold;

        if (GameHelper.HasRelic<ContentRestorationBrickRelic>())
        {
            returnCost -= 15;
        }

        if (GameHelper.HasRelic<ContentDiscountTokenRelic>() && returnCost > 75)
        {
            returnCost = 75;
        }

        return new GameWallet(returnCost);
    }

    public virtual Sprite GetIconWhite()
    {
        return m_iconWhite;
    }

    public int GetCurHealth()
    {
        return m_curHealth;
    }

    public int GetSightRange()
    {
        int toReturn = m_sightRange;

        if (GetTeam() == Team.Player && GameHelper.HasRelic<ContentTheGreatestGiftRelic>())
        {
            toReturn += 1;
        }

        ContentLordOfShadowsEnemy lordOfShadowsEnemy = GameHelper.GetBoss<ContentLordOfShadowsEnemy>();
        if (lordOfShadowsEnemy != null)
        {
            toReturn -= lordOfShadowsEnemy.m_visionReductionAmount;
        }

        return toReturn;
    }

    public virtual int GetHit(int damage)
    {
        ContentLordOfChaosEnemy lordOfChaosEnemy = GameHelper.GetBoss<ContentLordOfChaosEnemy>();
        if (lordOfChaosEnemy != null && lordOfChaosEnemy.m_currentChaosWarpAbility == ContentLordOfChaosEnemy.ChaosWarpAbility.NobodyCanDealDamage)
        {
            damage = 0;
        }

        m_curHealth -= damage;

        if (m_curHealth <= 0)
        {
            Die();
        }
        else
        {
            UIHelper.CreateWorldElementNotification(m_name + " takes " + damage + " damage!", false, m_gameTile.GetWorldTile().gameObject);
        }

        return damage;
    }

    public virtual int GetHealed(int healing)
    {
        int realHealVal = healing;
        if (m_curHealth + healing > m_maxHealth)
        {
            realHealVal = m_maxHealth - m_curHealth;
        }

        m_curHealth += healing;
        if (m_curHealth > m_maxHealth)
        {
            m_curHealth = m_maxHealth;
        }

        if (realHealVal > 0)
        {
            UIHelper.CreateWorldElementNotification(m_name + " heals " + realHealVal + "!", true, m_gameTile.GetWorldTile().gameObject);
        }

        if (m_curHealth > 0)
        {
            m_isDestroyed = false;
        }

        return healing;
    }

    public virtual void Die()
    {
        m_isDestroyed = true;

        m_gameTile.SetDestroyedBuilding(this);

        UIHelper.CreateWorldElementNotification(m_name + " is destroyed!", false, m_gameTile.GetWorldTile().gameObject);

        UIHelper.ClearBuildingTiles();
        UIHelper.SetBuildingTiles();

        GameHelper.DestroyPlayerBuilding(this.m_gameTile);
    }

    public virtual void Restore()
    {
        m_isDestroyed = false;
        m_curHealth = m_maxHealth;
    }

    public abstract bool IsValidTerrainToPlace(GameTerrainBase terrain, GameTile tile);

    public bool SpellCast(GameCard.Target targetType, GameTile targetTile) 
    {
        if (!m_spellcraftBuilding)
        {
            return false;
        }

        if (Constants.UseLocationalSpellcraft)
        {
            if (targetType != GameCard.Target.None)
            {
                if (targetTile == null)
                {
                    Debug.LogError("Spellcast that isn't target none received null target tile");
                    return false;
                }

                int distanceBetween = WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(GetGameTile(), targetTile);
                if (distanceBetween > GameSpellcraftKeyword.m_spellcraftRange)
                {
                    return false;
                }
            }
        }

        OnSpellCraft();

        return true;
    }

    public virtual void OnOtherAttack(GameUnit attackingUnit, GameUnit attackedUnit, int damageAmount) { }

    public virtual void OnSpellCraft() { }

    public virtual void TriggerEndOfWave() { }

    //============================================================================================================//

    public virtual void StartTurn() { }

    public virtual void EndTurn()
    {
        if (!m_isDestroyed)
        {
            if (GameHelper.HasRelic<ContentLivingStoneRelic>())
            {
                int toIncrease = 1;
                m_maxHealth += toIncrease;
                GetHealed(toIncrease);
                UIHelper.TriggerRelicAnimation<ContentLivingStoneRelic>();
            }
        }
    }

    //============================================================================================================//

    public virtual JsonGameBuildingData SaveToJson()
    {
        JsonGameBuildingData jsonData = new JsonGameBuildingData
        {
            name = m_name,
            curHealth = m_curHealth,
            isDestroyed = m_isDestroyed,
        };

        return jsonData;
    }

    public virtual string SaveToJsonAsString()
    {
        JsonGameBuildingData jsonData = new JsonGameBuildingData
        {
            name = m_name,
            curHealth = m_curHealth,
            isDestroyed = m_isDestroyed,
        };

        var export = JsonConvert.SerializeObject(jsonData);

        return export;
    }

    public virtual void LoadFromJson(JsonGameBuildingData jsonData)
    {
        m_curHealth = jsonData.curHealth;
        m_isDestroyed = jsonData.isDestroyed;
    }
}
