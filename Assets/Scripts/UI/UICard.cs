using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UICard : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
{
    public enum CardDisplayType
    {
        Hand,
        Deck,
        Select,
        Tooltip,
        StarterSelect,
        StarterTypeSelect
    }

    public Image m_tintImage;

    public Text m_nameText;
    public Text m_costText;
    public Image m_image;
    public Image m_rarityImage;
    public Text m_typelineText;
    public Text m_descText;
    public Text m_powerText;
    public Text m_healthText;
    public UIStaminaContainer m_staminaContainer;
    public GameObject m_skullIndicator;

    public CardDisplayType m_displayType;

    public GameCard m_card { get; private set; }

    public bool m_isHovered;

    private UICardHand m_handCard;
    private UICardSelectButton m_cardSelect;
    private UICardDeckView m_cardDeck;
    private UITooltipCard m_cardTooltip;
    private UICardStarterSelect m_cardStarterSelect;

    private bool m_hasSetDisplayType;

    public GameUnitCard m_unitCard;

    void Start()
    {
        if (m_tintImage != null)
        {
            m_tintImage.color = UIHelper.GetDefaultTintColor();
        }
    }

    public void Init(GameCard card, CardDisplayType displayType)
    {
        m_card = card;
        m_displayType = displayType;

        if (m_card is GameUnitCard)
        {
            m_unitCard = (GameUnitCard)m_card;
        }
        else
        {
            m_unitCard = null;
        }

        SetCardData();

        if (!m_hasSetDisplayType)
        {
            if (m_displayType == CardDisplayType.Hand)
            {
                m_handCard = gameObject.AddComponent<UICardHand>();
            }
            else if (m_displayType == CardDisplayType.Select)
            {
                m_cardSelect = gameObject.AddComponent<UICardSelectButton>();
            }
            else if (m_displayType == CardDisplayType.Deck)
            {
                m_cardDeck = gameObject.AddComponent<UICardDeckView>();
            }
            else if (m_displayType == CardDisplayType.Tooltip)
            {
                m_cardTooltip = gameObject.AddComponent<UITooltipCard>();
            }
            else if (m_displayType == CardDisplayType.StarterSelect)
            {
                m_cardStarterSelect = gameObject.AddComponent<UICardStarterSelect>();
            }

            m_hasSetDisplayType = true;
        }
    }

    void Update()
    {
        if (ShouldShowSelectedTint())
        {
            if (m_tintImage != null)
            {
                m_tintImage.color = UIHelper.GetSelectTintColor(true);
            }
        }
        else
        {
            if (!m_isHovered)
            {
                if (m_tintImage != null)
                {
                    m_tintImage.color = UIHelper.GetSelectTintColor(false);
                }
            }
        }

        SetCardData();
    }

    public void SetCardData()
    {
        if (m_card == null)
        {
            return;
        }

        m_image.sprite = m_card.m_icon;
        m_nameText.text = m_card.GetName();
        if (m_costText != null && m_unitCard != null && m_unitCard.GetUnit().GetTeam() == Team.Enemy)
        {
            if (m_skullIndicator != null)
            {
                m_skullIndicator.SetActive(true);
            }
            m_costText.text = "";
        }
        else if (m_costText != null)
        {
            m_costText.text = m_card.GetCost() + "";
            if (m_skullIndicator != null)
            {
                m_skullIndicator.SetActive(false);
            }
        }
        m_typelineText.text = m_card.GetTypeline();
        m_descText.text = m_card.GetDesc();

        if (m_rarityImage != null)
        {
            m_rarityImage.color = UIHelper.GetRarityColor(m_card.m_rarity);
        }

        if (m_unitCard != null)
        {
            m_powerText.text = m_unitCard.m_unit.GetPower() + "";
            m_healthText.text = m_unitCard.m_unit.GetMaxHealth() + "";

            m_staminaContainer.gameObject.SetActive(true);
            m_staminaContainer.Init(m_unitCard.GetUnit().GetStaminaRegen(), m_unitCard.GetUnit().GetMaxStamina(), m_unitCard.GetUnit().GetTeam());
        }
        else
        {
            m_powerText.text = "";
            m_healthText.text = "";
            m_staminaContainer.gameObject.SetActive(false);
        }
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        Globals.m_canScroll = false;
        m_isHovered = true;

        AudioHelper.PlaySFX(AudioHelper.UICardHover);

        if (!ShouldShowSelectedTint())
        {
            if (m_displayType == CardDisplayType.Hand)
            {
                if (m_tintImage != null)
                {
                    m_tintImage.color = UIHelper.GetValidTintColor(m_card.IsValidToPlay());
                }
            }
            else
            {
                if (m_tintImage != null)
                {
                    m_tintImage.color = UIHelper.GetValidTintColor(true);
                }
            }
        }

        Globals.m_hoveredCard = this;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        Globals.m_canScroll = true;
        m_isHovered = false;

        if (!ShouldShowSelectedTint())
        {
            if (m_tintImage != null)
            {
                m_tintImage.color = UIHelper.GetDefaultTintColor();
            }
        }

        Globals.m_hoveredCard = null;
    }

    public bool GetIsBig()
    {
        if (m_handCard == null)
        {
            return false;
        }

        return m_handCard.m_isBig;
    }

    public void InitCardDeck(UIDeckViewController.DeckViewType viewType)
    {
        m_cardDeck.Init(viewType);
    }

    public UITooltipCard GetCardTooltip()
    {
        return m_cardTooltip;
    }  
    
    public UICardStarterSelect GetCardStarterSelect()
    {
        return m_cardStarterSelect;
    }

    private bool ShouldShowSelectedTint()
    {
        bool isSelected = Globals.m_selectedCard == this;

        if (m_displayType == CardDisplayType.StarterSelect)
        {
            isSelected = m_cardStarterSelect.IsSelected();
        }

        return isSelected;
    }
}
