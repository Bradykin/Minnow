using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWisdomOfThePastCard : GameCardSpellBase
{
    public ContentWisdomOfThePastCard()
    {
        m_name = "Wisdom of the Past";
        m_targetType = Target.None;
        m_cost = 1;
        m_rarity = GameRarity.Uncommon;

        m_playerUnlockLevel = 1;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.Knowledgeable);
        m_tags.AddTag(GameTag.TagType.LowCost);

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override string GetDesc()
    {
        string predictionString = "";
        if (!Globals.m_inIntermission)
        {
            predictionString = "(" + Globals.m_spellsPlayedPreviousTurn + ")";
        }

        return "Draw cards equal to the number of spells played last turn " + predictionString + ".";
    }

    public override void PlayCard()
    {
        if (!IsValidToPlay())
        {
            return;
        }

        base.PlayCard();

        for (int i = 0; i < Globals.m_spellsPlayedPreviousTurn; i++)
        {
            GameHelper.GetPlayer().DrawCard();
        }
    }
}
