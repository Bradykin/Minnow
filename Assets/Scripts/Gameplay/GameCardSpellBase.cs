using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCardSpellBase : GameCard
{
    protected int m_spellEffect;

    protected virtual int GetSpellValue()
    {
        int toReturn = m_spellEffect;

        toReturn += 5 * GameHelper.RelicCount<ContentDominerickRefrainRelic>();

        return toReturn;
    }
}
