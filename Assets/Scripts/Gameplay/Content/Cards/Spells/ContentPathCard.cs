using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPathCard : GameCardSpellBase
{
    public ContentPathCard()
    {
        m_name = "Path";
        m_targetType = Target.None;
        m_cost = 0;
        m_rarity = GameRarity.Uncommon;
        m_spellEffect = 1;

        SetupBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.HighCost);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Spellcraft);

        m_onPlaySFX = AudioHelper.MiscEffect;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return $"Gain {UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)} energy.";
    }

    public override void PlayCard()
    {
        if (!IsValidToPlay())
        {
            return;
        }

        base.PlayCard();

        GamePlayer player = GameHelper.GetPlayer();

        player.AddEnergy(GetSpellValue());
    }
}