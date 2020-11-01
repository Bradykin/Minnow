using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentVowOfTheShakinaRelic : GameRelic
{
    public ContentVowOfTheShakinaRelic()
    {
        m_name = "Vow of the Shakina";
        m_desc = "Whenever an allied unit with enrage reveals fog, it takes 2 damage (only once per move).";
        m_rarity = GameRarity.Common;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Enrage);
        m_tags.AddTag(GameTag.TagType.Monster);
    }
}
