using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBloodMoneyCard : GameCardSpellBase
{
    public ContentBloodMoneyCard()
    {
        m_name = "Blood Money";
        m_targetType = Target.Ally;
        m_cost = 2;
        m_rarity = GameRarity.Rare;
        m_shouldExile = true;
        m_spellEffect = 10;

        m_keywordHolder.AddKeyword(new GameEnrageKeyword(null));

        SetupBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Enrage);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Healing, isReceiver: false);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.Gold, 3);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.UtilitySpell); 

        m_onPlaySFX = AudioHelper.GoldSpell;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return $"Target allied unit gains '<b>Enrage</b>: Gain {UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)} gold.'";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddKeyword(new GameEnrageKeyword(new GameGainGoldAction(GetSpellValue())), false, false);
    }
}