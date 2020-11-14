using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIAcceptMetaprogressionRewardButton : UIElementBase
        , IPointerClickHandler
{
    public Text m_acceptText;

    void Update()
    {
        if (UIMetaprogressionNotificationController.HasMultipleNotifications())
        {
            m_acceptText.text = "Next";
        }
        else
        {
            m_acceptText.text = "Accept";
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioSFXController.Instance.PlaySFX(AudioHelper.UIClick);

        UIMetaprogressionNotificationController.AcceptReward();
    }

    public override void HandleTooltip()
    {
        //Left as stub
    }
}
