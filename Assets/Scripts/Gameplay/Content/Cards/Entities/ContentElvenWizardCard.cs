using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentElvenWizardCard : GameCardEntityBase
{
    public ContentElvenWizardCard()
    {
        m_entity = new ContentElvenWizard();

        FillBasicData();

        m_playDesc = "A mighty wizard joins your cause.";
        m_typeline = "Summon - Elf";
        m_cost = 1;
    }
}
