using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPurgeCard : GameCardSpellBase
{
    public ContentPurgeCard()
    {
        m_spellEffect = 5;
        
        m_name = "Purge";
        m_targetType = Target.Unit;
        m_cost = 2;
        m_rarity = GameRarity.Uncommon;

        SetupBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.MagicPower, isReceiver: false);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.BuffSpell);
    }

    public override string GetDesc()
    {
        if (m_spellEffect != GetSpellValue())
        {
            return "Target non-elite unit loses all keywords. If it was an allied unit that previously had keywords, it <b>permanently</b> gains +" + m_spellEffect + "/+" + m_spellEffect + " (+" + GetSpellValue() + "/+" + GetSpellValue() + ").\n" + GetModifiedByMagicPowerString();
        }
        else
        {
            return "Target non-elite unit loses all keywords. If it was an allied unit that previously had keywords, it <b>permanently</b> gains +" + m_spellEffect + "/+" + m_spellEffect + ".\n" + GetModifiedByMagicPowerString();
        }
    }

    public override bool IsValidToPlay(GameUnit targetUnit)
    {
        return base.IsValidToPlay(targetUnit) && !GameHelper.IsBossOrElite(targetUnit);
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        if (targetUnit.GetTeam() == Team.Player)
        {
            AudioHelper.PlaySFX(AudioHelper.SmallBuff);
        }
        else
        {
            AudioHelper.PlaySFX(AudioHelper.SmallDebuff);
        }

        if (targetUnit.GetTeam() == Team.Player)
        {
            if (targetUnit.GetKeywordHolderForRead().GetNumVisibleKeywords() > 0)
            {
                targetUnit.AddStats(GetSpellValue(), GetSpellValue(), true, true);
            }
        }

        targetUnit.GetKeywordHolderForRead().RemoveAllVisibleKeywords();
    }

    protected override void HandleAudio()
    {
        //Left blank intentionally, audio is handled in PlayCard
    }
}