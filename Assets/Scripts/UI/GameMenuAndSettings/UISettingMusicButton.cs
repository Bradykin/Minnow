using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UISettingMusicButton : UIElementBase
        , IPointerClickHandler
{
    public TMP_Text m_musicText;

    void Start()
    {
        SetMusicString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (PlayerDataManager.PlayerAccountData.m_musicVolume == 0.0f)
        {
            PlayerDataManager.PlayerAccountData.m_musicVolume = AudioHelper.DefaultMusicVolume;
        }
        else
        {
            PlayerDataManager.PlayerAccountData.m_musicVolume = 0.0f;
        }

        SetMusicString();

        AudioSFXController.Instance.PlaySFX(AudioHelper.UIClick);
    }

    public override void HandleTooltip()
    {
        //Left as stub
    }

    private void SetMusicString()
    {
        if (PlayerDataManager.PlayerAccountData.m_musicVolume > 0)
        {
            m_musicText.text = "Music: ON";
        } 
        else
        {
            m_musicText.text = "Music: OFF";
        }
    }
}
