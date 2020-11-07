using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIDebugSettingClearAccountDataButton : UIElementBase
        , IPointerClickHandler
{
    void Start()
    {
        if (!Constants.CheatsOn)
        {
            gameObject.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayerDataManager.ClearPlayerAccountData();

        Files.ClearPlayerAccountData();

        if (Constants.DebugRandomStarterLevels)
        {
            PlayerDataManager.RandomizeStarterCardLevels();
        }

        UIStarterCardSelectionController.Instance.ResetStarterCardInit();

        AudioSFXController.Instance.PlaySFX(AudioHelper.UIClick);
    }

    public override void HandleTooltip()
    {
        //Left as stub
    }
}
