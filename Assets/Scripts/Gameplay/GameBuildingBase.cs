using Game.Util;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingType
{
    Critical,
    Economic,
    Defensive
}

public abstract class GameBuildingBase : GameElementBase, ITurns, ISave, ILoad<JsonGameBuildingData>
{
    protected GameTile m_gameTile;

    public Sprite m_destroyedIcon;
    public int m_curHealth;
    public int m_maxHealth;
    public BuildingType m_buildingType;

    public int m_sightRange = 3;
    public int m_range = 3;

    public bool m_isDestroyed;
    public bool m_expandsPlaceRange = false;

    private Sprite m_iconWhite;

    public void LateInit()
    {
        m_icon = UIHelper.GetIconBuilding(m_name);
        m_iconWhite = UIHelper.GetIconBuilding(m_name + "W");
        m_destroyedIcon = UIHelper.GetIconBuilding(m_name + "D");
        m_curHealth = m_maxHealth;
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

    public string GetDesc()
    {
        return m_desc;
    }

    public virtual Sprite GetIconWhite()
    {
        return m_iconWhite;
    }

    public int GetCurHealth()
    {
        return m_curHealth;
    }

    public virtual int GetHit(int damage)
    {
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

    protected virtual void Die()
    {
        m_isDestroyed = true;

        UIHelper.CreateWorldElementNotification(m_name + " is destroyed!", false, m_gameTile.GetWorldTile().gameObject);

        UIHelper.ClearDefensiveBuildingTiles();
        UIHelper.SetDefensiveBuildingTiles();
    }

    public abstract bool IsValidTerrainToPlace(GameTerrainBase terrain, GameTile tile);

    public virtual void TriggerEndOfWave()
    {
        //The castle doesn't heal or come back at the end of the round
        if (this is ContentCastleBuilding)
        {
            return;
        }

        m_isDestroyed = false;
        m_curHealth = m_maxHealth;
    }

    //============================================================================================================//

    public virtual void StartTurn() { }

    public virtual void EndTurn()
    {
        if (!m_isDestroyed)
        {
            int livingStoneCount = GameHelper.RelicCount<ContentLivingStoneRelic>();
            if (livingStoneCount > 0)
            {
                int toIncrease = livingStoneCount * (new ContentLivingStoneRelic().GetRelicLevel() + 1);
                m_maxHealth += toIncrease;
                GetHealed(toIncrease);
            }
        }
    }

    //============================================================================================================//

    public string SaveToJsonAsString()
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

    public void LoadFromJson(JsonGameBuildingData jsonData)
    {
        m_curHealth = jsonData.curHealth;
        m_isDestroyed = jsonData.isDestroyed;
    }
}
