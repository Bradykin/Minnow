using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWillOfNatureCard : GameCardSpellBase
{
    public ContentWillOfNatureCard()
    {
        m_spellEffect = 15;

        m_name = "Will of Nature";
        m_targetType = Target.None;
        m_cost = 2;
        m_rarity = GameRarity.Uncommon;

        SetupBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Tank, isReceiver: false);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.Healing);

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return "Heal <b>all</b> allied units for " + m_spellEffect + mpString + ".";
    }

    public override void PlayCard()
    {
        if (!IsValidToPlay())
        {
            return;
        }

        base.PlayCard();

        List<GameUnit> playerUnits = GameHelper.GetPlayer().m_controlledUnits;

        for (int i = 0; i < playerUnits.Count; i++)
        {
            playerUnits[i].Heal(GetSpellValue());
        }

    }
}