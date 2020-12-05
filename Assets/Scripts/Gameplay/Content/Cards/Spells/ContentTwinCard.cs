using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTwinCard : GameCardSpellBase
{
    public ContentTwinCard()
    {
        m_name = "Twin";
        m_targetType = Target.Ally;
        m_cost = 2;
        m_rarity = GameRarity.Rare;
        m_shouldExile = true;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.HighCost);
        m_tags.AddTag(GameTag.TagType.BuffSpell);

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override string GetDesc()
    {
        return "Add a copy of target allied unit to your hand.";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        JsonGameUnitData copyData = targetUnit.SaveToJson();
        copyData.customName = "";
        copyData.guid = System.Guid.NewGuid().ToString();

        GameUnit copyUnit = GameUnitFactory.GetUnitFromJson(copyData);
        GameUnitCard copyCard = GameCardFactory.GetCardFromUnit(copyUnit);
        copyCard.SetDesc(copyCard.m_unit.GetDesc());
        GameHelper.GetPlayer().AddCardToHand(copyCard, false);
    }
}