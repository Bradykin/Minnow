using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentElvenWizardCard : GameUnitCard
{
    public ContentElvenWizardCard()
    {
        m_unit = new ContentElvenWizard();

        m_cost = 4;

        FillBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Shiv);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.BuffSpell);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Spellcraft);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.LowCost);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.Scaler);
    }
}
