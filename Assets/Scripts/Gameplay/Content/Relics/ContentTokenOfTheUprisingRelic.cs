using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTokenOfTheUprisingRelic : GameRelic
{
    public ContentTokenOfTheUprisingRelic()
    {
        m_name = "Token of the Uprising";
        m_desc = "When an allied <b>Humanoid</b> dies, all allied <b>Creation</b> units within range 2 gain +X/+Y, where X is the dead unit's power and y is the dead unit's toughness.";
        m_rarity = GameRarity.Rare;

        LateInit();

        m_tagHolder.AddTag(GameTagHolder.TagType.Humanoid);
        m_tagHolder.AddTag(GameTagHolder.TagType.Creation);
        m_tagHolder.AddTag(GameTagHolder.TagType.BuffSpell);
        m_tagHolder.AddTag(GameTagHolder.TagType.Reanimate);
    }
}
