using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingType
{
    Critical,
    Economic,
    Defensive
}

public abstract class GameBuildingBase : GameElementBase, ITurns, ITakeTurnAI, ISave, ILoad<JsonGameBuildingData>
{
    protected GameTile m_gameTile;

    public Sprite m_destroyedIcon;
    public int m_curHealth;
    public int m_maxHealth;
    public BuildingType m_buildingType;

    public int m_sightRange = 2;

    public bool m_isDestroyed;
    public bool m_expandsPlaceRange = false;

    public void LateInit()
    {
        m_icon = UIHelper.GetIconBuilding(m_name);
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

    public int GetCurHealth()
    {
        return m_curHealth;
    }

    public virtual int GetHit(int damage)
    {
        m_curHealth -= damage;

        UIHelper.CreateWorldElementNotification(m_name + " takes " + damage + " damage!", false, m_gameTile.GetWorldTile());

        if (m_curHealth <= 0)
        {
            Die();
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
            UIHelper.CreateWorldElementNotification(m_name + " heals " + realHealVal + "!", false, m_gameTile.GetWorldTile());
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

        UIHelper.CreateWorldElementNotification(m_name + " is destroyed!", false, m_gameTile.GetWorldTile());
    }

    public abstract bool IsValidTerrainToPlace(GameTerrainBase terrain);

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

    public virtual void TakeTurn() { }

    //============================================================================================================//

    public virtual void StartTurn() { }

    public virtual void EndTurn()
    {
        if (!m_isDestroyed)
        {
            int livingStoneCount = GameHelper.RelicCount<ContentLivingStoneRelic>();
            if (livingStoneCount > 0)
            {
                GetHealed(livingStoneCount);
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

        var export = JsonUtility.ToJson(jsonData);

        return export;
    }

    public void LoadFromJson(JsonGameBuildingData jsonData)
    {
        m_curHealth = jsonData.curHealth;
        m_isDestroyed = jsonData.isDestroyed;
    }
}
