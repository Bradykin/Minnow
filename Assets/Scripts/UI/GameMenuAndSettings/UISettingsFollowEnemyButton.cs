using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UISettingsFollowEnemyButton : UIElementBase
        , IPointerClickHandler
{
    public TMP_Text m_followText;

    void Start()
    {
        SetFollowText();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayerDataManager.PlayerAccountData.m_followEnemy = !PlayerDataManager.PlayerAccountData.m_followEnemy;

        SetFollowText();

        AudioSFXController.Instance.PlaySFX(AudioHelper.UIClick);
    }

    public override void HandleTooltip()
    {
        //Left as stub
    }

    private void SetFollowText()
    {
        if (PlayerDataManager.PlayerAccountData.m_followEnemy)
        {
            m_followText.text = "Watch Enemy Turn";
        }
        else
        {
            m_followText.text = "Instant Enemy Turn";
        }
    }
}
