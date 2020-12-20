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
    }

    private void AddBasicTags()
    {
        if (GetUnit().GetTypeline() == Typeline.Humanoid)
        {
            m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.Humanoid);
        }
        if (GetUnit().GetTypeline() == Typeline.Monster)
        {
            m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.Monster);
        }
        if (GetUnit().GetTypeline() == Typeline.Creation)
        {
            m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.Creation);
        }

        if (GetUnit().GetKnowledgeableKeyword() != null)
        {
            m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.Knowledgeable);
        }
        if (GetUnit().GetVictoriousKeyword() != null)
        {
            m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.Victorious);
        }
        if (GetUnit().GetEnrageKeyword() != null)
        {
            m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.Enrage);
        }
        if (GetUnit().GetMomentumKeyword() != null)
        {
            m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.Momentum);
        }
        if (GetUnit().GetRangeKeyword() != null)
        {
            m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.Range);
        }
        if (GetUnit().GetSpellcraftKeyword() != null)
        {
            m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.Spellcraft);
        }

        if (m_cost >= 3 || m_xSpell) //Not calling GetCost() here to avoid all temp modifiers
        {
            m_tagHolder.AddPullTag(GameTagHolder.TagType.HighCost);
        }
        else if (m_cost == 2) //Not calling GetCost() here to avoid all temp modifiers
        {
            m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.HighCost);
        }
        else if (m_cost <= 1) //Not calling GetCost() here to avoid all temp modifiers
        {
            m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.LowCost);
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

        if (!targetTile.CanPlace())
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
        string typeline = "";
        if (m_rarity == GameRarity.Starter)
        {
            typeline += "Starter ";
        }
        typeline += "Unit - " + m_unit.GetTypeline();

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

        IReadOnlyList<GameKeywordBase> keywords = GetUnit().GetKeywordHolderForRead().GetKeywordsForRead();

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

        if (m_unit.GetStaminaToAttack(null) == 1)
        {
            desc += "Only takes 1 stamina to attack.";
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
