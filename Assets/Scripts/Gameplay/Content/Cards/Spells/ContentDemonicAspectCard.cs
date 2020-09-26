using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDemonicAspectCard : GameCardSpellBase
{
    public ContentDemonicAspectCard()
    {
        m_name = "Demonic Aspect";
        m_desc = "Give an entity Victorious: Gain 2 AP.";
        m_playDesc = "The target gains a demonic visage!";
        m_targetType = Target.Ally;
        m_cost = 2;
        m_shouldExile = true;

        m_rarity = GameRarity.Rare;

        SetupBasicData();
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.AddKeyword(new GameVictoriousKeyword(new GameGainAPAction(targetEntity, 2)));
    }
}