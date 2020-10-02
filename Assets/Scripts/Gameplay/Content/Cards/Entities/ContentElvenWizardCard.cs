using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentElvenWizardCard : GameCardEntityBase
{
    public ContentElvenWizardCard()
    {
        m_entity = new ContentElvenWizard();

        m_cost = 3;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.BuffSpell);
        m_tags.AddTag(GameTag.TagType.DamageSpell);
        m_tags.AddTag(GameTag.TagType.Shiv);
        m_tags.AddTag(GameTag.TagType.Scaler);
    }
}
