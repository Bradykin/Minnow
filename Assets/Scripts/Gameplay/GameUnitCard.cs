using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUnitCard : GameCard
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
        m_icon = UIHelper.GetIconCard(m_name);
        m_rarity = m_unit.m_rarity;
        m_typeline = GetTypeline();

        m_shouldExile = true;

        if (m_rarity != GameRarity.Starter)
        {
            AddBasicTags();
        }

        InitializeWithLevel(GetCardLevel());
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

        if (GetUnit().GetKnowledgeableKeyword() != null)
        {
            m_tags.AddTag(GameTag.TagType.Knowledgeable);
        }
        if (GetUnit().GetVictoriousKeyword() != null)
        {
            m_tags.AddTag(GameTag.TagType.Victorious);
        }
        if (GetUnit().GetEnrageKeyword() != null)
        {
            m_tags.AddTag(GameTag.TagType.Enrage);
        }
        if (GetUnit().GetMomentumKeyword() != null)
        {
            m_tags.AddTag(GameTag.TagType.Momentum);
        }
        if (GetUnit().GetRangeKeyword() != null)
        {
            m_tags.AddTag(GameTag.TagType.Range);
        }
        if (GetUnit().GetSpellcraftKeyword() != null)
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

        if (GetUnit().GetKeywordHolderForRead().GetNumVisibleKeywords() == 0)
        {
            m_tags.AddTag(GameTag.TagType.Vanilla);
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

    public override string GetTypeline()
    {
        string typeline = "Unit - " + m_unit.GetTypeline();

        if (GetUnit() is GameEnemyUnit)
        {
            GameEnemyUnit enemyUnit = (GameEnemyUnit)GetUnit();
            if (enemyUnit.m_isElite)
            {
                typeline = "Elite " + typeline;
            }
            else if (enemyUnit.m_isBoss)
            {
                typeline = "Boss " + typeline;
            }
        }
        return typeline;
    }

    public override void ResetCard()
    {
        base.ResetCard();

        m_unit.Reset();
    }

    public override string GetDesc()
    {
        string desc = m_unit.GetDesc();

        List<GameKeywordBase> keywords = GetUnit().GetKeywordHolderForRead().GetKeywordsForRead();

        bool hasAnyVisibleKeywords = keywords.Count > 0;
        if (hasAnyVisibleKeywords)
        {
            for (int i = 0; i < keywords.Count; i++)
            {
                if (keywords[i].m_isVisible)
                {
                    hasAnyVisibleKeywords = true;
                    break;
                }
            }

            hasAnyVisibleKeywords = false;
        }

        if (hasAnyVisibleKeywords && (desc != ""))
        {
            desc += "\n";
        }

        desc += GetUnit().GetKeywordDesc();

        return desc;
    }

    //============================================================================================================//

    public override JsonGameCardData SaveToJson()
    {
        JsonGameCardData jsonData = new JsonGameCardData
        {
            baseName = GetBaseName(),
            jsonGameUnitData = m_unit.SaveToJson()
        };

        if (m_unit.GetGameTile() != null)
        {
            jsonData.jsonGameUnitXPosition = m_unit.GetGameTile().m_gridPosition.x;
            jsonData.jsonGameUnitYPosition = m_unit.GetGameTile().m_gridPosition.y;
        }

        return jsonData;
    }

    public override void LoadFromJson(JsonGameCardData jsonData)
    {
        m_unit = GameUnitFactory.GetUnitFromJson(jsonData.jsonGameUnitData);
    }
}
