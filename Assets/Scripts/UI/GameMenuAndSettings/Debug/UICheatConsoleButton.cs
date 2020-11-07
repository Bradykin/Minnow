using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UICheatConsoleButton : UIElementBase
        , IPointerClickHandler
{
    public InputField m_textInput;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            TriggerCheat();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TriggerCheat();
    }

    public override void HandleTooltip()
    {
        //Left as stub
    }

    private void TriggerCheat()
    {
        UICheatConsoleController.Instance.TriggerCheat(m_textInput.text);

        AudioSFXController.Instance.PlaySFX(AudioHelper.UIClick);
    }
}
