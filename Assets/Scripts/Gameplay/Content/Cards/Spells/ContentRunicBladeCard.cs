using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRunicBladeCard : GameCardSpellBase
{
    public ContentRunicBladeCard()
    {
        m_name = "Runic Blade";
        m_targetType = Target.Ally;
        m_cost = 2;
        m_rarity = GameRarity.Uncommon;
        m_shouldExile = true;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.BuffSpell);
        m_tags.AddTag(GameTag.TagType.Victorious);
        m_tags.AddTag(GameTag.TagType.Spellcraft);
    }

    public override string GetDesc()
    {
        return "Target ally gains Victorious: Trigger spellcraft.";
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.AddKeyword(new GameVictoriousKeyword(new GameSpellcraftAttackAction(targetEntity)));
    }
}