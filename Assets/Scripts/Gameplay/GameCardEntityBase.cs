using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCardEntityBase : GameCard
{
    public GameEntity m_entity { get; protected set; }

    public override string GetName()
    {
        return m_entity.GetName();
    }

    public GameEntity GetEntity()
    {
        return m_entity;
    }

    public void SetEntity(GameEntity newEntity)
    {
        m_entity = newEntity;
    }

    public void FillBasicData()
    {
        m_name = m_entity.GetName();
        m_desc = m_entity.GetDesc();
        m_icon = UIHelper.GetIconCard(m_name);
        m_rarity = m_entity.m_rarity;
        m_typeline = GetTypeline();

        m_shouldExile = true;
    }

    public void InitEnemyCard()
    {
        FillBasicData();

        m_playDesc = "Error: ENEMY ENTITY PLAYED?";
        m_cost = -1;
    }

    public override void PlayCard(GameTile targetTile)
    {
        if (!IsValidToPlay(targetTile))
        {
            return;
        }

        base.PlayCard(targetTile);

        GameHelper.MakePlayerEntity(targetTile, m_entity);

        m_entity.OnSummon();
    }

    public override bool IsValidToPlay(GameTile targetTile)
    {
        if (!base.IsValidToPlay(targetTile))
        {
            return false;
        }

        if (!targetTile.m_canPlace)
        {
            return false;
        }

        if (!targetTile.IsPassable(GetEntity(), false))
        {
            return false;
        }

        if (targetTile.IsOccupied())
        {
            return false;
        }

        return true;
    }

    protected string GetTypeline()
    {
        return "Summon - " + m_entity.GetTypeline();
    }

    public override void ResetCard()
    {
        base.ResetCard();

        m_entity.Reset();
    }

    public override string GetDesc()
    {
        string desc = base.GetDesc();

        if (GetEntity().GetKeywordHolderForRead().m_keywords.Count > 0 && (desc != ""))
        {
            desc += "\n";
        }

        desc += GetEntity().GetKeywordHolderForRead().GetDesc();

        return desc;
    }
}
