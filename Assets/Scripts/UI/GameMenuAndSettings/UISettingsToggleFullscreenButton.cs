using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UISettingsToggleFullscreenButton : UIElementBase
        , IPointerClickHandler
{
    public TMP_Text m_toggleFullscreenText;
    public GameObject m_toggleResolutionButton;

    private bool isFullScreen;

    void Start()
    {
        isFullScreen = Screen.fullScreen;
        m_toggleResolutionButton.SetActive(!isFullScreen);

        SetToggleFullscreenText();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Screen.fullScreen = !Screen.fullScreen;
        isFullScreen = !isFullScreen;

        m_toggleResolutionButton.SetActive(!isFullScreen);
        SetToggleFullscreenText();

        AudioSFXController.Instance.PlaySFX(AudioHelper.UIClick);
    }

    public override void HandleTooltip()
    {
        //Left as stub
    }

    private void SetToggleFullscreenText()
    {
        if (isFullScreen)
        {
            m_toggleFullscreenText.text = "Set Windowed";
        }
        else
        {
            m_toggleFullscreenText.text = "Set Fullscreen";
        }
    }
}
