using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUnitCardBase : GameCard
{
    public GameUnit m_unit { get; protected set; }

    public override string GetName()
    {
        return m_unit.GetName();
    }

    public GameUnit GetUnit()
    {
        return m_unit;
    }

    public void SetUnit(GameUnit newUnit)
    {
        m_unit = newUnit;
    }

    public void FillBasicData()
    {
        m_name = m_unit.GetName();
        m_desc = m_unit.GetDesc();
        m_icon = UIHelper.GetIconCard(m_name);
        m_rarity = m_unit.m_rarity;
        m_typeline = GetTypeline();

        m_shouldExile = true;

        if (m_rarity != GameRarity.Starter)
        {
            AddBasicTags();
        }
    }

    private void AddBasicTags()
    {
        if (GetUnit().GetTypeline() == Typeline.Humanoid)
        {
            m_tags.AddTag(GameTag.TagType.Humanoid);
        }
        if (GetUnit().GetTypeline() == Typeline.Monster)
        {
            m_tags.AddTag(GameTag.TagType.Monster);
        }
        if (GetUnit().GetTypeline() == Typeline.Creation)
        {
            m_tags.AddTag(GameTag.TagType.Creation);
        }

        if (GetUnit().GetKeyword<GameKnowledgeableKeyword>() != null)
        {
            m_tags.AddTag(GameTag.TagType.Knowledgeable);
        }
        if (GetUnit().GetKeyword<GameVictoriousKeyword>() != null)
        {
            m_tags.AddTag(GameTag.TagType.Victorious);
        }
        if (GetUnit().GetKeyword<GameEnrageKeyword>() != null)
        {
            m_tags.AddTag(GameTag.TagType.Enrage);
        }
        if (GetUnit().GetKeyword<GameMomentumKeyword>() != null)
        {
            m_tags.AddTag(GameTag.TagType.Momentum);
        }
        if (GetUnit().GetKeyword<GameRangeKeyword>() != null)
        {
            m_tags.AddTag(GameTag.TagType.Range);
        }
        if (GetUnit().GetKeyword<GameSpellcraftKeyword>() != null)
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

        GameHelper.MakePlayerUnit(targetTile, m_unit);

        m_unit.OnSummon();
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

        if (!targetTile.IsPassable(GetUnit(), false))
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
        return "Unit - " + m_unit.GetTypeline();
    }

    public override void ResetCard()
    {
        base.ResetCard();

        m_unit.Reset();
    }

    public override string GetDesc()
    {
        string desc = m_unit.GetDesc();

        if (GetUnit().GetKeywordHolderForRead().m_keywords.Count > 0 && (desc != ""))
        {
            desc += "\n";
        }

        desc += GetUnit().GetKeywordHolderForRead().GetDesc();

        return desc;
    }
}
