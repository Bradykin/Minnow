using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISettingsSFXButton : UIElementBase
        , IPointerClickHandler
{
    public Text m_sfxText;

    void Start()
    {
        SetSFXText();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GlobalSettings.m_sfxVolume == 0.0f)
        {
            GlobalSettings.m_sfxVolume = AudioHelper.DefaultSFXVolume;
        }
        else
        {
            GlobalSettings.m_sfxVolume = 0.0f;
        }

        SetSFXText();

        AudioSFXController.Instance.PlaySFX(AudioHelper.UIClick);
    }

    public override void HandleTooltip()
    {
        //Left as stub
    }

    private void SetSFXText()
    {
        if (GlobalSettings.m_sfxVolume > 0.0f)
        {
            m_sfxText.text = "SFX: ON";
        }
        else
        {
            m_sfxText.text = "SFX: OFF";
        }
    }
}
