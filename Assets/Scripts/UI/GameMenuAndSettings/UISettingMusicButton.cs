using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISettingMusicButton : UIElementBase
        , IPointerClickHandler
{
    public Text m_musicText;

    void Start()
    {
        SetMusicString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GlobalSettings.m_musicVolume == 0.0f)
        {
            GlobalSettings.m_musicVolume = AudioHelper.DefaultMusicVolume;
        }
        else
        {
            GlobalSettings.m_musicVolume = 0.0f;
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
        if (GlobalSettings.m_musicVolume > 0)
        {
            m_musicText.text = "Music: ON";
        } 
        else
        {
            m_musicText.text = "Music: OFF";
        }
    }
}
