using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIIntermissionAction : UIElementBase
    , IPointerClickHandler
{
    public TMP_Text m_titleText;
    public TMP_Text m_descText;
    public Image m_image;

    public GameActionIntermission m_action;

    public void Init(GameActionIntermission action)
    {
        m_action = action;

        m_titleText.text = m_action.GetBaseName();
        m_descText.text = m_action.GetDesc();
        m_image.sprite = m_action.GetIcon();
    }

    public override void HandleTooltip()
    {
        //Left empty
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UIIntermissionActionSelectController.Instance.AcceptAction(m_action);
    }
}
