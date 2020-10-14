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
        m_basicUnit.Init(GamePlayer.StarterSimpleUnit, UICard.CardDisplayType.StarterTypeSelect);
        m_advancedUnit.Init(GamePlayer.StarterAdvancedUnit, UICard.CardDisplayType.StarterTypeSelect);
        m_damageSpell.Init(GamePlayer.StarterDamageSpell, UICard.CardDisplayType.StarterTypeSelect);
        m_defensiveSpell.Init(GamePlayer.StarterDefensiveSpell, UICard.CardDisplayType.StarterTypeSelect);
        m_exileSpell.Init(GamePlayer.StarterExileSpell, UICard.CardDisplayType.StarterTypeSelect);

        m_basicUnit.gameObject.AddComponent<UICardStarterTypeSelect>().Init(StarterCardType.BasicUnit);
        m_advancedUnit.gameObject.AddComponent<UICardStarterTypeSelect>().Init(StarterCardType.AdvancedUnit);
        m_damageSpell.gameObject.AddComponent<UICardStarterTypeSelect>().Init(StarterCardType.DamageSpell);
        m_defensiveSpell.gameObject.AddComponent<UICardStarterTypeSelect>().Init(StarterCardType.DefensiveSpell);
        m_exileSpell.gameObject.AddComponent<UICardStarterTypeSelect>().Init(StarterCardType.ExileSpell);

        m_optionOne.gameObject.SetActive(false);
        m_optionTwo.gameObject.SetActive(false);
        m_optionThree.gameObject.SetActive(false);
    }

    public void SetCurSelectedType(StarterCardType type)
    {
        m_curSelectedType = type;

        if (m_curSelectedType == StarterCardType.None)
        {
            m_optionOne.gameObject.SetActive(false);
            m_optionTwo.gameObject.SetActive(false);
            m_optionThree.gameObject.SetActive(false);
        }
        else
        {
            GameCard cardOne;
            GameCard cardTwo;
            GameCard cardThree;

            if (m_curSelectedType == StarterCardType.BasicUnit)
            {
                cardOne = new ContentDwarvenSoldierCard();
                cardTwo = new ContentDwarvenSoldierCard();
                cardThree = new ContentDwarvenSoldierCard();
            }
            else if (m_curSelectedType == StarterCardType.AdvancedUnit)
            {
                cardOne = new ContentStoneGolemCard();
                cardTwo = new ContentStoneGolemCard();
                cardThree = new ContentStoneGolemCard();
            }
            else if (m_curSelectedType == StarterCardType.DamageSpell)
            {
                cardOne = new ContentFireboltCard();
                cardTwo = new ContentDrainingBoltCard();
                cardThree = new ContentWeakeningBoltCard();
            }
            else if (m_curSelectedType == StarterCardType.DefensiveSpell)
            {
                cardOne = new ContentAegisCard();
                cardTwo = new ContentCureWoundsCard();
                cardThree = new ContentJoltCard();
            }
            else if (m_curSelectedType == StarterCardType.ExileSpell)
            {
                cardOne = new ContentGrowTalonsCard();
                cardTwo = new ContentStaminaTrainingCard();
                cardThree = new ContentOptimizeCard();
            }
            else
            {
                Debug.LogError("Received unknown StarterCardType in UIStarterCardSelectionController");
                return;
            }

            bool optionOneActive = GameMetaProgression.IsCardUnlocked(cardOne);
            m_optionOne.gameObject.SetActive(optionOneActive);
            if (optionOneActive)
            {
                m_optionOne.Init(cardOne, UICard.CardDisplayType.StarterSelect);
                m_optionOne.GetCardStarterSelect().Init(m_curSelectedType);
            }

            bool optionTwoActive = GameMetaProgression.IsCardUnlocked(cardTwo);
            m_optionTwo.gameObject.SetActive(optionTwoActive);
            if (optionTwoActive)
            {
                m_optionTwo.Init(cardTwo, UICard.CardDisplayType.StarterSelect);
                m_optionTwo.GetCardStarterSelect().Init(m_curSelectedType);
            }

            bool optionThreeActive = GameMetaProgression.IsCardUnlocked(cardThree);
            m_optionThree.gameObject.SetActive(optionThreeActive);
            if (optionThreeActive)
            {
                m_optionThree.Init(cardThree, UICard.CardDisplayType.StarterSelect);
                m_optionThree.GetCardStarterSelect().Init(m_curSelectedType);
            }
        }
    }

    public void SetStarterCard(StarterCardType type, GameCard card)
    {
        if (type == StarterCardType.BasicUnit)
        {
            GamePlayer.StarterSimpleUnit = card;
            m_basicUnit.Init(GamePlayer.StarterSimpleUnit, UICard.CardDisplayType.StarterTypeSelect);
        }
        else if (type == StarterCardType.AdvancedUnit)
        {
            GamePlayer.StarterAdvancedUnit = card;
            m_advancedUnit.Init(GamePlayer.StarterAdvancedUnit, UICard.CardDisplayType.StarterTypeSelect);
        }
        else if (type == StarterCardType.DamageSpell)
        {
            GamePlayer.StarterDamageSpell = card;
            m_damageSpell.Init(GamePlayer.StarterDamageSpell, UICard.CardDisplayType.StarterTypeSelect);
        }
        else if (type == StarterCardType.DefensiveSpell)
        {
            GamePlayer.StarterDefensiveSpell = card;
            m_defensiveSpell.Init(GamePlayer.StarterDefensiveSpell, UICard.CardDisplayType.StarterTypeSelect);
        }
        else if (type == StarterCardType.ExileSpell)
        {
            GamePlayer.StarterExileSpell = card;
            m_exileSpell.Init(GamePlayer.StarterExileSpell, UICard.CardDisplayType.StarterTypeSelect);
        }
    }
}
