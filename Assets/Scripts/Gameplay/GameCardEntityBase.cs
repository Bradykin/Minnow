using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCardEntityBase : GameCard
{
    public GameUnit m_entity { get; protected set; }

    public override string GetName()
    {
        return m_entity.GetName();
    }

    public GameUnit GetEntity()
    {
        return m_entity;
    }

    public void SetEntity(GameUnit newEntity)
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

        if (m_rarity != GameRarity.Starter)
        {
            AddBasicTags();
        }
    }

    private void AddBasicTags()
    {
        if (GetEntity().GetTypeline() == Typeline.Humanoid)
        {
            m_tags.AddTag(GameTag.TagType.Humanoid);
        }
        if (GetEntity().GetTypeline() == Typeline.Monster)
        {
            m_tags.AddTag(GameTag.TagType.Monster);
        }
        if (GetEntity().GetTypeline() == Typeline.Creation)
        {
            m_tags.AddTag(GameTag.TagType.Creation);
        }

        if (GetEntity().GetKeyword<GameKnowledgeableKeyword>() != null)
        {
            m_tags.AddTag(GameTag.TagType.Knowledgeable);
        }
        if (GetEntity().GetKeyword<GameVictoriousKeyword>() != null)
        {
            m_tags.AddTag(GameTag.TagType.Victorious);
        }
        if (GetEntity().GetKeyword<GameEnrageKeyword>() != null)
        {
            m_tags.AddTag(GameTag.TagType.Enrage);
        }
        if (GetEntity().GetKeyword<GameMomentumKeyword>() != null)
        {
            m_tags.AddTag(GameTag.TagType.Momentum);
        }
        if (GetEntity().GetKeyword<GameRangeKeyword>() != null)
        {
            m_tags.AddTag(GameTag.TagType.Range);
        }
        if (GetEntity().GetKeyword<GameSpellcraftKeyword>() != null)
        {
            m_tags.AddTag(GameTag.TagType.Spellcraft);
        }

        if (m_cost >= 2) //Not calling GetCost() here to avoid all temp modifiers
        {
            m_tags.AddTag(GameTag.TagType.HighCost);
        }

        if (m_cost == 0) //Not calling GetCost() here to avoid all temp modifiers
        {
            m_tags.AddTag(GameTag.TagType.LowCost);
        }
    }

    public void InitEnemyCard()
    {
        FillBasicData();

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
        return "Unit - " + m_entity.GetTypeline();
    }

    public override void ResetCard()
    {
        base.ResetCard();

        m_entity.Reset();
    }

    public override string GetDesc()
    {
        string desc = m_entity.GetDesc();

        if (GetEntity().GetKeywordHolderForRead().m_keywords.Count > 0 && (desc != ""))
        {
            desc += "\n";
        }

        desc += GetEntity().GetKeywordHolderForRead().GetDesc();

        return desc;
    }
}
