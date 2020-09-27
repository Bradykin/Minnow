using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentReforgingCard : GameCardSpellBase
{
    public ContentReforgingCard()
    {
        m_name = "Reforging";
        m_desc = "Return a random ally creation unit which has died this wave to your hand.";
        m_targetType = Target.None;
        m_cost = 1;
        m_rarity = GameRarity.Rare;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.Creation);
        m_tags.AddTag(GameTag.TagType.Reanimate);
    }

    public override void PlayCard()
    {
        if (!IsValidToPlay())
        {
            return;
        }

        base.PlayCard();

        GamePlayer player = GameHelper.GetPlayer();

        List<GameCard> deadCreations = new List<GameCard>();

        for (int i = 0; i < player.m_cardsInExile.Count; i++)
        {
            if (player.m_cardsInExile[i] is GameCardEntityBase entityCard && entityCard.m_entity.m_isDead && entityCard.m_entity.GetTypeline() == Typeline.Creation)
            {
                deadCreations.Add(player.m_cardsInExile[i]);
            }
        }

        if (deadCreations.Count > 0)
        {
            int index = Random.Range(0, deadCreations.Count);
            ((GameCardEntityBase)deadCreations[index]).m_entity.m_isDead = false;
            player.AddCardToHand(deadCreations[index], false);
            player.m_cardsInExile.Remove(deadCreations[index]);
        }
    }
}