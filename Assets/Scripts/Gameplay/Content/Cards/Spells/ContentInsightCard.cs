using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentInsightCard : GameCardSpellBase
{
    public ContentInsightCard()
    {
        m_spellEffect = 4;

        m_name = "Insight";
        m_targetType = Target.None;
        m_cost = 1;
        m_rarity = GameRarity.Rare;

        m_keywordHolder.AddKeyword(new GameSpellcraftKeyword(null));

        SetupBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Spellcraft, 2);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.MagicPower);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.UtilitySpell);

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return "Trigger <b>Spellcraft</b> " + m_spellEffect + mpString + " times (including the trigger from this card).\n" + GetModifiedByMagicPowerString();
    }

    public override void PlayCard()
    {
        if (!IsValidToPlay())
        {
            return;
        }

        base.PlayCard();

        GamePlayer player = GameHelper.GetPlayer();
        if (player == null)
        {
            return;
        }

        int spellValue = GetSpellValue();
        for (int i = 0; i < spellValue - 1; i++)
        {
            TriggerSpellcraft(null);
        }
    }
}