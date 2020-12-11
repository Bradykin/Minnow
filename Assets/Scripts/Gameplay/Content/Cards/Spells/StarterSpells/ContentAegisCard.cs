using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAegisCard : GameCardSpellBase
{
    public ContentAegisCard()
    {
        m_name = "Aegis";
        m_targetType = Target.Ally;
        m_rarity = GameRarity.Starter;

        m_cost = 1;
        m_desc = "Give target allied unit <b>Damage Shield</b>.\n";

        m_keywordHolder.AddKeyword(new GameDamageShieldKeyword());

        SetupBasicData();

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override string GetDesc()
    {
        string description = m_desc;

        if (GameHelper.HasRelic<ContentTraditionalMethodsRelic>())
        {
            description += "\nDraw a card.";
        }

        return description;
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddKeyword(new GameDamageShieldKeyword(), false, false);

        if (GameHelper.HasRelic<ContentTraditionalMethodsRelic>())
        {
            GameHelper.GetPlayer().DrawCard();
        }
    }
}
