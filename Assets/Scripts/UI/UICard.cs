using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

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
        LargeTooltip,
        StarterSelect,
        StarterTypeSelect,
        RewardDisplay
    }

    public Image m_image;
    public Image m_tintImage;

    public GameObject m_lockedObj;
    public GameObject m_enemyObj;

    public GameObject m_unitObj;
    public GameObject m_costCircle;

    public TMP_Text m_nameText;
    public TMP_Text m_costText;
    public TMP_Text m_typelineText;
    public TMP_Text m_descText;
    public TMP_Text m_statsText;

    public UIStaminaContainer m_staminaContainer;

    public CardDisplayType m_displayType;

    public GameCard m_card { get; private set; }

    public bool m_isHovered;

    private UICardHand m_handCard;
    private UICardSelectButton m_cardSelect;
    private UICardDeckView m_cardDeck;
    private UITooltipCard m_cardTooltip;
    private UICardStarterSelect m_cardStarterSelect;

    private bool m_hasSetDisplayType;
    private bool m_isLocked;
    private bool m_isShowingTooltip;

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

        if (m_displayType == CardDisplayType.StarterSelect)
        {
            m_isLocked = !GameMetaprogressionUnlocksDataManager.HasUnlockedStarterCard(m_card) && !Constants.UnlockAllContent;
        }
        else
        {
            m_isLocked = false;
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
            else if (m_displayType == CardDisplayType.Tooltip ||
                m_displayType == CardDisplayType.LargeTooltip)
            {
                m_cardTooltip = gameObject.AddComponent<UITooltipCard>();

                m_cardTooltip.Init(m_displayType == CardDisplayType.LargeTooltip);
            }
            else if (m_displayType == CardDisplayType.StarterSelect)
            {
                m_cardStarterSelect = gameObject.AddComponent<UICardStarterSelect>();
            }

            m_hasSetDisplayType = true;
        }

        if (m_lockedObj != null)
        {
            m_lockedObj.SetActive(m_isLocked);
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
        if (m_unitCard != null && m_unitCard.GetUnit().GetTeam() == Team.Enemy)
        {
            if (m_enemyObj != null)
            {
                m_enemyObj.SetActive(true);
            }

            if (m_costText != null)
            {
                m_costText.text = "";
            }
        }
        else
        {
            if (m_costText != null)
            {
                if (m_card.IsXSpell())
                {
                    m_costText.text = "X"; 
                }
                else
                {
                    m_costText.text = m_card.GetCost() + "";
                }
            }

            if (m_enemyObj != null)
            {
                m_enemyObj.SetActive(false);
            }
        }
        m_typelineText.text = m_card.GetTypeline();
        m_descText.text = m_card.GetDesc();

        if (m_costCircle != null)
        {
            m_costCircle.GetComponent<Image>().color = UIHelper.GetRarityColor(m_card.m_rarity);
        }

        if (m_unitCard != null)
        {
            m_statsText.text = m_unitCard.m_unit.GetPower() + "/" + m_unitCard.m_unit.GetMaxHealth();

            if (m_unitObj != null)
            {
                m_unitObj.gameObject.SetActive(true);
            }

            m_staminaContainer.Init(m_unitCard.GetUnit().GetStaminaRegen(), m_unitCard.GetUnit().GetMaxStamina(), m_unitCard.GetUnit().GetTeam());
        }
        else
        {
            m_statsText.text = "";

            if (m_unitObj != null)
            {
                m_unitObj.SetActive(false);
            }
        }
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        Globals.m_canScroll = false;
        m_isHovered = true;

        if (!m_isShowingTooltip)
        {
            HandleTooltip();
            m_isShowingTooltip = true;
        }

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

        if (m_isShowingTooltip)
        {
            UITooltipController.Instance.ClearTooltipStack();

            m_isShowingTooltip = false;
        }

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

    public void HandleTooltip()
    {
        if (m_displayType == CardDisplayType.StarterSelect && IsLocked())
        {
            GameMap neededMap = GameMetaprogressionUnlocksDataManager.GetMapForStarterCard(m_card);
            if (neededMap == null)
            {
                UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("WIP", "Card not yet available in this version of the game"));
            }
            else
            {
                UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Locked Card", "Unlock this starter card by beating Chaos 2 on " + neededMap.GetBaseName()));
            }
        }
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

    public bool IsLocked()
    {
        return m_isLocked;
    }
}
