using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCardSpellBase : GameCard
{
    protected AudioClip m_onPlaySFX;

    //This is for keywords on the card that we want to display info for.
    protected GameKeywordHolder m_keywordHolder = new GameKeywordHolder(null);

    protected int m_spellEffect;

    protected void SetupBasicData()
    {
        if (m_shouldExile)
        {
            m_typeline = $"Exile - {m_targetType.ToString()}";
        }
        else
        {
            m_typeline = m_targetType.ToString();
        }
        m_icon = UIHelper.GetIconCard(m_name);

        AddBasicTags();
    }

    private void AddBasicTags()
    {
        if (m_shouldExile)
        {
            m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.Exile);
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

   protected virtual int GetSpellValue()
   {
        int toReturn = m_spellEffect;

        GamePlayer player = GameHelper.GetPlayer();

        if (player != null)
        {
            toReturn += player.GetMagicPower();
        }

        if (toReturn < 0)
        {
            toReturn = 0;
        }

        return toReturn;
    }

    protected string GetDamageDescString(bool useMagicPower = true)
    {
        string mpString = "";
        if (useMagicPower && HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return $"Deal <color=#027FFF>{m_spellEffect}{mpString}</color> damage.\n";
    }

    protected string GetHealDescString()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return $"Heal for <color=#027FFF>{m_spellEffect}{mpString}</color> damage.\n";
    }

    protected string GetMagicPowerString()
    {
        if (GetSpellValue() == m_spellEffect)
        {
            return "";
        }
        else
        {
            return $"({GetSpellValue()})";
        }
    }

    protected virtual bool CanTriggerSpellcraft()
    {
        return true;
    }

    protected string GetModifiedByMagicPowerString()
    {
        return "<i>(Modified by <b>Magic Power</b>)</i>";
    }

    protected bool HasMagicPower()
    {
        return GetSpellValue() != m_spellEffect;
    }

    private void PlayCardImpl()
    {
        GameHelper.GetPlayer().m_spellsPlayedThisTurn++;
    }

    public override void PlayCard()
    {
        base.PlayCard();

        PlayCardImpl();

        TriggerSpellcraft(null);

        HandleAudio();
    }

    public override void PlayCard(GameBuildingBase targetBuilding)
    {
        base.PlayCard(targetBuilding);

        PlayCardImpl();

        TriggerSpellcraft(targetBuilding.GetGameTile());

        HandleAudio();
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        base.PlayCard(targetUnit);

        PlayCardImpl();

        if (CanTriggerSpellcraft())
        {
            TriggerSpellcraft(targetUnit.GetGameTile());
        }

        HandleAudio();
    }

    public override void PlayCard(GameTile targetTile)
    {
        base.PlayCard(targetTile);

        PlayCardImpl();

        TriggerSpellcraft(targetTile);

        HandleAudio();
    }

    protected virtual void HandleAudio()
    {
        AudioHelper.PlaySFX(m_onPlaySFX);
    }

    protected void TriggerSpellcraft(GameTile tileCastAt)
    {
        GamePlayer player = GameHelper.GetPlayer();
        GameOpponent opponent = GameHelper.GetOpponent();

        if (player == null)
        {
            return;
        }

        player.TriggerSpellcraft(m_targetType, tileCastAt);
        opponent.TriggerSpellcraft(m_targetType, tileCastAt);

        //If we have Impalium, do it again
        if (GameHelper.HasRelic<ContentImpaliumRelic>())
        {
            player.TriggerSpellcraft(m_targetType, tileCastAt);
            UIHelper.TriggerRelicAnimation<ContentImpaliumRelic>();
        }
    }

    public GameKeywordHolder GetKeywordHolderForRead()
    {
        return m_keywordHolder;
    }

    public override string GetDesc()
    {
        return m_desc;
    }
}
