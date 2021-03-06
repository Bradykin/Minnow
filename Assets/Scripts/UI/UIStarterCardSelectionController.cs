﻿using System.Collections;
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
        m_basicUnit.Init(GameCardFactory.GetCardByName(PlayerDataManager.PlayerAccountData.StarterSimpleUnitName), UICard.CardDisplayType.StarterTypeSelect);
        m_advancedUnit.Init(GameCardFactory.GetCardByName(PlayerDataManager.PlayerAccountData.StarterAdvancedUnitName), UICard.CardDisplayType.StarterTypeSelect);
        m_damageSpell.Init(GameCardFactory.GetCardByName(PlayerDataManager.PlayerAccountData.StarterDamageSpellName), UICard.CardDisplayType.StarterTypeSelect);
        m_defensiveSpell.Init(GameCardFactory.GetCardByName(PlayerDataManager.PlayerAccountData.StarterDefensiveSpellName), UICard.CardDisplayType.StarterTypeSelect);
        m_exileSpell.Init(GameCardFactory.GetCardByName(PlayerDataManager.PlayerAccountData.StarterExileSpellName), UICard.CardDisplayType.StarterTypeSelect);

        m_basicUnit.gameObject.AddComponent<UICardStarterTypeSelect>().Init(StarterCardType.BasicUnit);
        m_advancedUnit.gameObject.AddComponent<UICardStarterTypeSelect>().Init(StarterCardType.AdvancedUnit);
        m_damageSpell.gameObject.AddComponent<UICardStarterTypeSelect>().Init(StarterCardType.DamageSpell);
        m_defensiveSpell.gameObject.AddComponent<UICardStarterTypeSelect>().Init(StarterCardType.DefensiveSpell);
        m_exileSpell.gameObject.AddComponent<UICardStarterTypeSelect>().Init(StarterCardType.ExileSpell);

        m_optionOne.gameObject.SetActive(false);
        m_optionTwo.gameObject.SetActive(false);
        m_optionThree.gameObject.SetActive(false);
    }

    public void ResetStarterCardInit()
    {
        m_basicUnit.Init(GameCardFactory.GetCardByName(PlayerDataManager.PlayerAccountData.StarterSimpleUnitName), UICard.CardDisplayType.StarterTypeSelect);
        m_advancedUnit.Init(GameCardFactory.GetCardByName(PlayerDataManager.PlayerAccountData.StarterAdvancedUnitName), UICard.CardDisplayType.StarterTypeSelect);
        m_damageSpell.Init(GameCardFactory.GetCardByName(PlayerDataManager.PlayerAccountData.StarterDamageSpellName), UICard.CardDisplayType.StarterTypeSelect);
        m_defensiveSpell.Init(GameCardFactory.GetCardByName(PlayerDataManager.PlayerAccountData.StarterDefensiveSpellName), UICard.CardDisplayType.StarterTypeSelect);
        m_exileSpell.Init(GameCardFactory.GetCardByName(PlayerDataManager.PlayerAccountData.StarterExileSpellName), UICard.CardDisplayType.StarterTypeSelect);

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
                cardTwo = new ContentSandwalkerCard();
                cardThree = new ContentMechanizedBeastCard();
            }
            else if (m_curSelectedType == StarterCardType.AdvancedUnit)
            {
                cardOne = new ContentAlphaBoarCard();
                cardTwo = new ContentLizardSoldierCard();
                cardThree = new ContentUndeadMammothCard();
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

            m_optionOne.Init(cardOne, UICard.CardDisplayType.StarterSelect);
            m_optionOne.GetCardStarterSelect().Init(m_curSelectedType);
            m_optionOne.gameObject.SetActive(true);

            m_optionTwo.Init(cardTwo, UICard.CardDisplayType.StarterSelect);
            m_optionTwo.GetCardStarterSelect().Init(m_curSelectedType);
            m_optionTwo.gameObject.SetActive(true);

            m_optionThree.Init(cardThree, UICard.CardDisplayType.StarterSelect);
            m_optionThree.GetCardStarterSelect().Init(m_curSelectedType);
            m_optionThree.gameObject.SetActive(true);
        }
    }

    public void SetStarterCard(StarterCardType type, GameCard card)
    {
        if (type == StarterCardType.BasicUnit)
        {
            PlayerDataManager.PlayerAccountData.StarterSimpleUnitName = card.GetBaseName();
            m_basicUnit.Init(GameCardFactory.GetCardByName(PlayerDataManager.PlayerAccountData.StarterSimpleUnitName), UICard.CardDisplayType.StarterTypeSelect);
        }
        else if (type == StarterCardType.AdvancedUnit)
        {
            PlayerDataManager.PlayerAccountData.StarterAdvancedUnitName = card.GetBaseName();
            m_advancedUnit.Init(GameCardFactory.GetCardByName(PlayerDataManager.PlayerAccountData.StarterAdvancedUnitName), UICard.CardDisplayType.StarterTypeSelect);
        }
        else if (type == StarterCardType.DamageSpell)
        {
            PlayerDataManager.PlayerAccountData.StarterDamageSpellName = card.GetBaseName();
            m_damageSpell.Init(GameCardFactory.GetCardByName(PlayerDataManager.PlayerAccountData.StarterDamageSpellName), UICard.CardDisplayType.StarterTypeSelect);
        }
        else if (type == StarterCardType.DefensiveSpell)
        {
            PlayerDataManager.PlayerAccountData.StarterDefensiveSpellName = card.GetBaseName();
            m_defensiveSpell.Init(GameCardFactory.GetCardByName(PlayerDataManager.PlayerAccountData.StarterDefensiveSpellName), UICard.CardDisplayType.StarterTypeSelect);
        }
        else if (type == StarterCardType.ExileSpell)
        {
            PlayerDataManager.PlayerAccountData.StarterExileSpellName = card.GetBaseName();
            m_exileSpell.Init(GameCardFactory.GetCardByName(PlayerDataManager.PlayerAccountData.StarterExileSpellName), UICard.CardDisplayType.StarterTypeSelect);
        }
    }
}
