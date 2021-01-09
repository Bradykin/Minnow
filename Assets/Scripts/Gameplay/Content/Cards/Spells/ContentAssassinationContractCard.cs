using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAssassinationContractCard : GameCardSpellBase
{
    public ContentAssassinationContractCard()
    {
        m_name = "Assassination Contract";
        m_targetType = Target.None;
        m_cost = 3;
        m_rarity = GameRarity.Common;
        m_spellEffect = 2;

        m_keywordHolder.AddKeyword(new GameShivKeyword());

        SetupBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Shiv, 3);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Spellcraft);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.MagicPower);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.DamageSpell);

        m_onPlaySFX = AudioHelper.DaggerSwingSpell;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return $"Add {UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)} <b>shivs</b> to your hand.";
    }

    public override void PlayCard()
    {
        if (!IsValidToPlay())
        {
            return;
        }

        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        base.PlayCard();

        for (int i = 0; i < GetSpellValue(); i++)
        {
            player.AddCardToHand(new ContentShivCard(), false);
        }
    }
}
