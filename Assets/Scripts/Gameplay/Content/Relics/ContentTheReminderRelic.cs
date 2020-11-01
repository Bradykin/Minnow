using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTheReminderRelic : GameRelic
{
    public ContentTheReminderRelic()
    {
        m_name = "The Reminder";
        m_desc = "Whenever an allied monster runs out of Stamina, it takes 5 damage.";
        m_rarity = GameRarity.Common;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Monster);
        m_tags.AddTag(GameTag.TagType.Enrage);
    }
}
