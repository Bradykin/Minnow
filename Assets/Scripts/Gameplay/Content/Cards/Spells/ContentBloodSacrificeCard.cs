using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBloodSacrificeCard : GameCardSpellBase
{
    public ContentBloodSacrificeCard()
    {
        m_spellEffect = 1;

        m_name = "Blood Sacrifice";
        m_targetType = Target.Ally;
        m_cost = 0;
        m_rarity = GameRarity.Rare;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.Reanimate);
        m_tags.AddTag(GameTag.TagType.HighCost);
        m_tags.AddTag(GameTag.TagType.Knowledgeable);
        m_tags.AddTag(GameTag.TagType.UtilitySpell);
        m_tags.AddTag(GameTag.TagType.Spellpower);
        m_tags.AddTag(GameTag.TagType.Creation);
    }

    public override string GetDesc()
    {
        string spString = "";
        if (HasSpellPower())
        {
            spString = GetSpellPowerString();
        }

        return "Sacrifice an allied unit to draw " + m_spellEffect + spString + " card and gain " + m_spellEffect + spString + " energy.\n" + GetModifiedBySpellPowerString();
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.Die();

        GamePlayer player = GameHelper.GetPlayer();
        player.AddEnergy(GetSpellValue());
        player.DrawCards(GetSpellValue());
    }
}
