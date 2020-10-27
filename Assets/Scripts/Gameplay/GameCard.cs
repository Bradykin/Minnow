using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameCard : GameElementBase
{
    public enum Target
    { 
        Tile,
        Ally,
        Enemy,
        Unit,
        Building,
        None //This is used for spells like 'Draw 3'
    }

    protected int m_cost;
    protected int m_costTempModifier = 0;
    public string m_typeline;
    public Target m_targetType;
    public bool m_shouldExile;
    public int m_storedTagWeight;

    protected int m_playerUnlockLevel;

    public virtual string GetName()
    {
        return m_name;
    }

    public int GetCost()
    {
        int toReturn = m_cost + m_costTempModifier;

        if (this is GameCardSpellBase)
        {
            toReturn -= GameHelper.RelicCount<ContentTomeOfDuluhainRelic>();
        }
        else if (this is GameUnitCard)
        {
            toReturn -= GameHelper.RelicCount<ContentPinnacleOfFearRelic>();
        }

        if (toReturn < 0)
        {
            toReturn = 0;
        }

        return toReturn;
    }

    public void SetTempCost(int i)
    {
        m_costTempModifier = i;
    }

    public virtual string GetDesc()
    {
        return m_desc;
    }

    public void SetDesc(string desc)
    {
        m_desc = desc;
    }

    public virtual bool PlayerHasUnlockedCard()
    {
        return Constants.CheatsOn || (PlayerDataManager.GetCurLevel() >= GetPlayerUnlockLevel());
    }

    public int GetPlayerUnlockLevel()
    {
        return m_playerUnlockLevel;
    }

    public virtual void PlayCard() 
    {
        PayCost();
    }

    public virtual void PlayCard(GameTile targetTile) 
    {
        PayCost();
    }

    public virtual void PlayCard(GameUnit targetUnit) 
    {
        PayCost();
    }

    public virtual void PlayCard(GameBuildingBase targetBuilding) 
    {
        PayCost();
    }

    public virtual void OnDraw()
    {
        if (GameHelper.RelicCount<ContentMysticRuneRelic>() > 0)
        {
            m_cost = Random.Range(0, 3);
        }
    }

    protected virtual void PayCost()
    {
        GamePlayer player = GameHelper.GetPlayer();
        if (player == null)
        {
            return;
        }

        player.SpendEnergy(GetCost());
    }

    public virtual void ResetCard()
    {

    }

    public virtual bool IsValidToPlay() 
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return false;
        }

        if (player.m_curEnergy >= GetCost())
        {
            return true;
        }

        return false; 
    }

    public virtual bool IsValidToPlay(GameTile targetTile) 
    {
        if (!IsValidToPlay())
        {
            return false;
        }

        if (m_targetType == Target.Tile)
        {
            return true;
        }

        return false; 
    }

    public virtual bool IsValidToPlay(GameUnit targetUnit) 
    {
        if (!IsValidToPlay())
        {
            return false;
        }

        if (m_targetType == Target.Ally && targetUnit.GetTeam() == Team.Player)
        {
            return true;
        }

        if (m_targetType == Target.Enemy && targetUnit.GetTeam() == Team.Enemy)
        {
            return true;
        }

        if (m_targetType == Target.Unit)
        {
            return true;
        }

        return false; 
    }

    public virtual bool IsValidToPlay(GameBuildingBase targetBuilding)
    {
        if (!IsValidToPlay())
        {
            return false;
        }

        if (m_targetType == Target.Building)
        {
            return true;
        }

        return false;
    }

    public virtual void InitializeWithLevel(int level) { }

    public virtual int GetCardLevel()
    {
        //TODO: alex - Hook this up to player save data.

        return 0;
    }
}
