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

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.Knowledgeable);
        m_tags.AddTag(GameTag.TagType.LowCost);

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override string GetDesc()
    {
        string predictionString = "";
        if (GameHelper.GetGameController() != null)
        {
            if (GameHelper.GetGameController().m_runStateType != RunStateType.Intermission)
            {
                predictionString = "(" + GameHelper.GetPlayer().m_spellsPlayedPreviousTurn + ")";
            }
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

        for (int i = 0; i < GameHelper.GetPlayer().m_spellsPlayedPreviousTurn; i++)
        {
            GameHelper.GetPlayer().DrawCard();
        }
    }
}
