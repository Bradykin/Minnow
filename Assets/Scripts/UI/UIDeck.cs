using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIDeck : UIElementBase
    , IPointerClickHandler
{
    public Text m_countText;

    void Start()
    {
        m_stopScrolling = true;
    }

    void Update()
    {
        if (!GameHelper.IsInGame())
        {
            return;
        }

        m_countText.text = GameHelper.GetPlayer().m_curDeck.Count() + "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameHelper.HasRelic<ContentEyeOfTelloRelic>())
        {
            GameCard topCard = GameHelper.GetPlayer().m_curDeck.GetCardByIndex(0);

            if (topCard != null)
            {
                if (topCard.IsValidToPlay())
                {
                    AudioHelper.PlaySFX(AudioHelper.UICardClick);

                    if (topCard.m_targetType == GameCard.Target.None)
                    {
                        GameHelper.GetPlayer().PlayCard(topCard);
                        topCard.PlayCard();
                    }
                    else
                    {
                        UIHelper.SelectCard(UITooltipController.Instance.GetTooltipFromStackByIndex(0).GetComponent<UICard>());
                    }
                }
                else
                {
                    if (Globals.m_canSelect)
                    {
                        UIHelper.CreateWorldElementNotification("Not enough energy.", false, gameObject);

                        AudioHelper.PlaySFX(AudioHelper.UIError);
                    }
                }

                ClearTooltip();
                HandleTooltip();
            }
        }
        else
        {
            AudioSFXController.Instance.PlaySFX(AudioHelper.UIClick);

            GamePlayer player = GameHelper.GetPlayer();

            UIDeckViewController.Instance.Init(player.m_curDeck.GetDeck(), UIDeckViewController.DeckViewType.View, "Current Deck");
        }
    }

    public override void HandleTooltip()
    {
        if (GameHelper.HasRelic<ContentEyeOfTelloRelic>())
        {
            GameCard topCard = GameHelper.GetPlayer().m_curDeck.GetCardByIndex(0);

            if (topCard != null)
            {
                UIHelper.CreateCardTooltip(topCard);
            }
        }
        else
        {
            UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Deck", "This is your deck!  When you run out of cards here, your discard will shuffle back in."));
        }
    }
}
