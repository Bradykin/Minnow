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
        Entity,
        Building
    }

    public int m_cost;
    public string m_typeline;
    public string m_playDesc;
    public Target m_targetType;

    public virtual void PlayCard() { }
    public virtual void PlayCard(GameTile targetTile) { }
    public virtual void PlayCard(GameEntity targetEntity) { }
    public virtual void PlayCard(GameBuildingBase targetBuilding) { }

    public virtual void OnDraw()
    {
        if (GameHelper.RelicCount<GameMysticRuneRelic>() > 0)
        {
            m_cost = Random.Range(0, 4);
        }
    }

    public virtual bool IsValidToPlay() 
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return false;
        }

        if (player.m_curEnergy >= m_cost)
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

    public virtual bool IsValidToPlay(GameEntity targetEntity) 
    {
        if (!IsValidToPlay())
        {
            return false;
        }

        if (m_targetType == Target.Ally && targetEntity.GetTeam() == Team.Player)
        {
            return true;
        }

        if (m_targetType == Target.Enemy && targetEntity.GetTeam() == Team.Enemy)
        {
            return true;
        }

        if (m_targetType == Target.Entity)
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
}
