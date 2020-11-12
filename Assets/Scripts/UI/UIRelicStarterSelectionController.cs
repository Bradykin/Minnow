using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public class UIRelicStarterSelectionController : Singleton<UIRelicStarterSelectionController>
{
    public List<UIRelic> m_starterRelics;

    void Start()
    {
        m_starterRelics[0].Init(new ContentMaskOfAgesRelic(), UIRelic.RelicSelectionType.SelectStarter);
        m_starterRelics[4].Init(new ContentWolvenFangRelic(), UIRelic.RelicSelectionType.SelectStarter);
        m_starterRelics[2].Init(new ContentOrbOfEnergyRelic(), UIRelic.RelicSelectionType.SelectStarter);
        m_starterRelics[3].Init(new ContentLoadedChestRelic(), UIRelic.RelicSelectionType.SelectStarter);
        m_starterRelics[1].Init(new ContentHoovesOfProductionRelic(), UIRelic.RelicSelectionType.SelectStarter);
    }
}
