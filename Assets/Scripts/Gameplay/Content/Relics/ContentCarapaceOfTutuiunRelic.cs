using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCarapaceOfTutuiun : GameRelic
{
    public ContentCarapaceOfTutuiun()
    {
        m_name = "Carapace of Tutuiun";
        m_desc = "Allied units have -1 stamina regen, but when they are summoned, they <b>permanently</b> get <b>Damage Reduction 2</b>.";
        m_rarity = GameRarity.Common;

        LateInit();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.StaminaRegen);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.Tank);
    }
}
