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
        GlobalSettings.m_music = !GlobalSettings.m_music;

        SetMusicString();
    }

    public override void HandleTooltip()
    {
        //Left as stub
    }

    private void SetMusicString()
    {
        if (GlobalSettings.m_music)
        {
            m_musicText.text = "Music: ON";
        } 
        else if (!GlobalSettings.m_music)
        {
            m_musicText.text = "Music: OFF";
        }
    }
}
