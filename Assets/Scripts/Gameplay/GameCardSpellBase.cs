using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCardSpellBase : GameCard
{
    protected int m_spellEffect;

    protected virtual int GetSpellValue()
    {
        return m_spellEffect;
    }
}
