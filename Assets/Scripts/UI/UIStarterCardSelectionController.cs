using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public class UIStarterCardSelectionController : Singleton<UIStarterCardSelectionController>
{
    public enum StarterCardType
    {
        None,
        BasicUnit,
        AdvancedUnit,
        DamageSpell,
        DefensiveSpell,
        ExileSpell
    }

    public UICard m_basicUnit;
    public UICard m_advancedUnit;
    public UICard m_damageSpell;
    public UICard m_defensiveSpell;
    public UICard m_exileSpell;

    public UICard m_optionOne;
    public UICard m_optionTwo;
    public UICard m_optionThree;

    public StarterCardType m_curSelectedType = StarterCardType.None;

    void Start()
    {
        m_basicUnit.gameObject.AddComponent<UICardStarterTypeSelect>().Init(StarterCardType.BasicUnit);
        m_advancedUnit.gameObject.AddComponent<UICardStarterTypeSelect>().Init(StarterCardType.AdvancedUnit);
        m_damageSpell.gameObject.AddComponent<UICardStarterTypeSelect>().Init(StarterCardType.DamageSpell);
        m_defensiveSpell.gameObject.AddComponent<UICardStarterTypeSelect>().Init(StarterCardType.DefensiveSpell);
        m_exileSpell.gameObject.AddComponent<UICardStarterTypeSelect>().Init(StarterCardType.ExileSpell);
    }

    void Update()
    {
        m_basicUnit.Init(GamePlayer.StarterSimpleUnit, UICard.CardDisplayType.StarterSelect);
        m_advancedUnit.Init(GamePlayer.StarterAdvancedUnit, UICard.CardDisplayType.StarterSelect);
        m_damageSpell.Init(GamePlayer.StarterDamageSpell, UICard.CardDisplayType.StarterSelect);
        m_defensiveSpell.Init(GamePlayer.StarterDefensiveSpell, UICard.CardDisplayType.StarterSelect);
        m_exileSpell.Init(GamePlayer.StarterExileSpell, UICard.CardDisplayType.StarterSelect);

        if (m_curSelectedType == StarterCardType.None)
        {
            m_optionOne.gameObject.SetActive(false);
            m_optionTwo.gameObject.SetActive(false);
            m_optionThree.gameObject.SetActive(false);
        }
        else
        {
            m_optionOne.gameObject.SetActive(true);
            m_optionTwo.gameObject.SetActive(true);
            m_optionThree.gameObject.SetActive(true);

            if (m_curSelectedType == StarterCardType.BasicUnit)
            {
                m_optionOne.Init(new ContentDwarvenSoldierCard(), UICard.CardDisplayType.StarterSelect);
                m_optionTwo.Init(new ContentDwarvenSoldierCard(), UICard.CardDisplayType.StarterSelect);
                m_optionThree.Init(new ContentDwarvenSoldierCard(), UICard.CardDisplayType.StarterSelect);
            }
            else if (m_curSelectedType == StarterCardType.AdvancedUnit)
            {
                m_optionOne.Init(new ContentStoneGolemCard(), UICard.CardDisplayType.StarterSelect);
                m_optionTwo.Init(new ContentStoneGolemCard(), UICard.CardDisplayType.StarterSelect);
                m_optionThree.Init(new ContentStoneGolemCard(), UICard.CardDisplayType.StarterSelect);
            }
            else if (m_curSelectedType == StarterCardType.DamageSpell)
            {
                m_optionOne.Init(new ContentFireboltCard(), UICard.CardDisplayType.StarterSelect);
                m_optionTwo.Init(new ContentDrainingBoltCard(), UICard.CardDisplayType.StarterSelect);
                m_optionThree.Init(new ContentWeakeningBoltCard(), UICard.CardDisplayType.StarterSelect);
            }
            else if (m_curSelectedType == StarterCardType.DefensiveSpell)
            {
                m_optionOne.Init(new ContentAegisCard(), UICard.CardDisplayType.StarterSelect);
                m_optionTwo.Init(new ContentCureWoundsCard(), UICard.CardDisplayType.StarterSelect);
                m_optionThree.Init(new ContentJoltCard(), UICard.CardDisplayType.StarterSelect);
            }
            else if (m_curSelectedType == StarterCardType.ExileSpell)
            {
                m_optionOne.Init(new ContentGrowTalonsCard(), UICard.CardDisplayType.StarterSelect);
                m_optionTwo.Init(new ContentStaminaTrainingCard(), UICard.CardDisplayType.StarterSelect);
                m_optionThree.Init(new ContentOptimizeCard(), UICard.CardDisplayType.StarterSelect);
            }

            m_optionOne.GetCardStarterSelect().Init(m_curSelectedType);
            m_optionTwo.GetCardStarterSelect().Init(m_curSelectedType);
            m_optionThree.GetCardStarterSelect().Init(m_curSelectedType);
        }
    }
}
