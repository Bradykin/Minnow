using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UISettingsToggleResolutionButton : UIElementBase
        , IPointerClickHandler
{
    public TMP_Text m_resolutionText;

    private int resolutionIndex = 0;

    private List<Vector2Int> resolutions = new List<Vector2Int>();

    void Start()
    {
        resolutions.Add(new Vector2Int(Screen.currentResolution.width, Screen.currentResolution.height));
        resolutions.Add(new Vector2Int(1920, 1080));
        resolutions.Add(new Vector2Int(1680, 1050));
        resolutions.Add(new Vector2Int(1600, 900));
        resolutions.Add(new Vector2Int(1280, 800));
        resolutions.Add(new Vector2Int(800, 600));
        
        SetToggleResolutionText();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        resolutionIndex++;
        if (resolutionIndex >= resolutions.Count)
        {
            resolutionIndex = 0;
        }

        Screen.SetResolution(resolutions[resolutionIndex].x, resolutions[resolutionIndex].y, false);

        SetToggleResolutionText();

        AudioSFXController.Instance.PlaySFX(AudioHelper.UIClick);
    }

    public override void HandleTooltip()
    {
        //Left as stub
    }

    private void SetToggleResolutionText()
    {
        if (resolutionIndex == 0)
        {
            m_resolutionText.text = "Default";
        }
        else
        {
            m_resolutionText.text = $"{resolutions[resolutionIndex].x}x{resolutions[resolutionIndex].y}";
        }
    }
}
