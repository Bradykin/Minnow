using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentReforgingCard : GameCardSpellBase
{
    public ContentReforgingCard()
    {
        m_name = "Reforging";
        m_desc = "Return a random allied <b>Creation</b> unit which has died this wave to your hand.";
        m_targetType = Target.None;
        m_cost = 1;
        m_rarity = GameRarity.Rare;

        SetupBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Creation, 2);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Reanimate, 2);

        m_onPlaySFX = AudioHelper.MetalBuff;
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
            if (player.m_cardsInExile[i] is GameUnitCard unitCard && unitCard.m_unit.m_isDead && unitCard.m_unit.GetTypeline() == Typeline.Creation)
            {
                deadCreations.Add(player.m_cardsInExile[i]);
            }
        }

        if (deadCreations.Count > 0)
        {
            int index = Random.Range(0, deadCreations.Count);
            ((GameUnitCard)deadCreations[index]).m_unit.m_isDead = false;
            player.AddCardToHand(deadCreations[index], false);
            player.m_cardsInExile.Remove(deadCreations[index]);
        }
    }
}