using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIConfirmationAccept : UIElementBase
    , IPointerClickHandler
{
    void Update()
    {
        if (UIHelper.GetKeyDown(KeyCode.Space))
        {
            Accept();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Accept();
    }

    private void Accept()
    {
        AudioSFXController.Instance.PlaySFX(AudioHelper.UIClick);

        UIConfirmationController.Instance.Accept();
    }

    public override void HandleTooltip()
    {
        //Left as stub
    }
}
