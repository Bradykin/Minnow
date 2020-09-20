using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRunicBladeCard : GameCardSpellBase
{
    public ContentRunicBladeCard()
    {
        m_name = "Runic Blade";
        m_playDesc = "The blade shimmers with fire!";
        m_targetType = Target.Ally;
        m_cost = 2;
        m_rarity = GameRarity.Uncommon;
        m_shouldExile = true;

        SetupBasicData();
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