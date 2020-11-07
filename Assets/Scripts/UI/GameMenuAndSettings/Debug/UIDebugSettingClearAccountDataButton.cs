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
        //Alex, clear account data here

        AudioSFXController.Instance.PlaySFX(AudioHelper.UIClick);
    }

    public override void HandleTooltip()
    {
        //Left as stub
    }
}
